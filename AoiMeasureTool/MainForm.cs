using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace AoiMeasureTool
{
    public sealed partial class MainForm : Form
    {
        private Mat _sourceColor;
        private Mat _gray;
        private readonly Mat[] _preprocessed = new Mat[4];
        private MeasureResult _lastMeasure;
        private EdgePointResult _lastEdgePoints;

        private bool _isDraggingRoi;
        private System.Drawing.Point _dragStart;

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            DisposeImages();
            base.OnFormClosed(e);
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff|All Files|*.*";
                dialog.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestImages");
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                LoadImage(dialog.FileName);
            }
        }

        private void TestImageButton_Click(object sender, EventArgs e)
        {
            var testPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestImages");
            if (Directory.Exists(testPath))
            {
                System.Diagnostics.Process.Start("explorer.exe", testPath);
            }
        }

        private void MeasureButton_Click(object sender, EventArgs e)
        {
            var index = _baseCombo.SelectedIndex;
            if (index < 0 || index >= _preprocessed.Length || _preprocessed[index] == null)
            {
                _resultText.Text = "Selected preprocess image is disabled or empty.";
                return;
            }

            _lastMeasure = ImageProcessor.MeasureDistance(
                _preprocessed[index],
                CurrentRoi(),
                _modeCombo.SelectedIndex == 0,
                (double)_scaleInput.Value);
            _resultText.Text = _lastMeasure.ToString();
            RefreshOverlay();
        }

        private void EdgeButton_Click(object sender, EventArgs e)
        {
            _lastEdgePoints = ImageProcessor.FindUpperEdgePoints(
                _gray,
                CurrentRoi(),
                (int)_edgeThreshold.Value,
                _edgeWhiteObject.Checked,
                _edgeInvert.Checked);

            if (_lastEdgePoints.Success)
            {
                _edgeLabel.Text = string.Format(
                    "Left Upper Edge Point = ({0}, {1})    Right Upper Edge Point = ({2}, {3})",
                    _lastEdgePoints.LeftUpperEdgePoint.X,
                    _lastEdgePoints.LeftUpperEdgePoint.Y,
                    _lastEdgePoints.RightUpperEdgePoint.X,
                    _lastEdgePoints.RightUpperEdgePoint.Y);
            }
            else
            {
                _edgeLabel.Text = _lastEdgePoints.Message;
            }

            RefreshOverlay();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadImage(string path)
        {
            DisposeImages();
            _sourceColor = ImageProcessor.LoadColorImage(path);
            _gray = ImageProcessor.ToGray(_sourceColor);
            _roiX.Maximum = _sourceColor.Width - 1;
            _roiY.Maximum = _sourceColor.Height - 1;
            _roiW.Maximum = _sourceColor.Width;
            _roiH.Maximum = _sourceColor.Height;
            _roiX.Value = 0;
            _roiY.Value = 0;
            _roiW.Value = _sourceColor.Width;
            _roiH.Value = _sourceColor.Height;
            Text = "AOI Image Size Measurement Tool - " + Path.GetFileName(path);
            UpdateProcessing();
        }

        private void UpdateProcessing()
        {
            if (_gray == null || _gray.Empty())
            {
                return;
            }

            for (var i = 0; i < 4; i++)
            {
                if (_preprocessed[i] != null)
                {
                    _preprocessed[i].Dispose();
                    _preprocessed[i] = null;
                }

                if (!_enabledChecks[i].Checked)
                {
                    SetPicture(_preprocessPictureBoxes[i], null);
                    continue;
                }

                _preprocessed[i] = ImageProcessor.Preprocess(_gray, CreateParam(i));
                SetPicture(_preprocessPictureBoxes[i], _preprocessed[i]);
            }

            RefreshOverlay();
        }

        private PreprocessParam CreateParam(int index)
        {
            return new PreprocessParam
            {
                Enabled = _enabledChecks[index].Checked,
                Threshold = _thresholdBars[index].Value,
                WhiteWhenGreaterThanThreshold = index < 2,
                ErodeIterations = (int)_erodeInputs[index].Value,
                DilateIterations = (int)_dilateInputs[index].Value,
                OpenIterations = (int)_openInputs[index].Value,
                CloseIterations = (int)_closeInputs[index].Value
            };
        }

        private void RefreshOverlay()
        {
            if (_sourceColor == null || _sourceColor.Empty())
            {
                return;
            }

            using (var display = ImageProcessor.DrawOverlay(_sourceColor, CurrentRoi(), _lastMeasure, _lastEdgePoints))
            {
                SetPicture(_mainPictureBox, display);
            }
        }

        private RoiInfo CurrentRoi()
        {
            return new RoiInfo
            {
                X = (int)_roiX.Value,
                Y = (int)_roiY.Value,
                Width = (int)_roiW.Value,
                Height = (int)_roiH.Value
            };
        }

        private void MainPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            _isDraggingRoi = true;
            _dragStart = e.Location;
        }

        private void MainPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDraggingRoi)
            {
                return;
            }

            // Reserved for future picture-coordinate ROI drag selection.
        }

        private void MainPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            _isDraggingRoi = false;
        }

        private void RoiValueChanged(object sender, EventArgs e)
        {
            RefreshOverlay();
        }

        private void PreprocessValueChanged(object sender, EventArgs e)
        {
            UpdateProcessing();
        }

        private void ThresholdBar_Scroll(object sender, EventArgs e)
        {
            for (var i = 0; i < _thresholdBars.Length; i++)
            {
                if (ReferenceEquals(sender, _thresholdBars[i]))
                {
                    _thresholdLabels[i].Text = "T=" + _thresholdBars[i].Value;
                    break;
                }
            }

            UpdateProcessing();
        }

        private void DisposeImages()
        {
            if (_sourceColor != null)
            {
                _sourceColor.Dispose();
                _sourceColor = null;
            }

            if (_gray != null)
            {
                _gray.Dispose();
                _gray = null;
            }

            for (var i = 0; i < _preprocessed.Length; i++)
            {
                if (_preprocessed[i] != null)
                {
                    _preprocessed[i].Dispose();
                    _preprocessed[i] = null;
                }
            }

            foreach (var picture in _preprocessPictureBoxes.Concat(new[] { _mainPictureBox }))
            {
                if (picture != null)
                {
                    SetPicture(picture, null);
                }
            }
        }

        private static void SetPicture(PictureBox pictureBox, Mat mat)
        {
            var old = pictureBox.Image;
            pictureBox.Image = mat == null || mat.Empty() ? null : BitmapConverter.ToBitmap(mat);
            if (old != null)
            {
                old.Dispose();
            }
        }
    }
}
