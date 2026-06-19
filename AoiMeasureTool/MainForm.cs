using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OpenCvSharp.Extensions;
using Cv2 = OpenCvSharp.Cv2;
using ColorConversionCodes = OpenCvSharp.ColorConversionCodes;
using CvMat = OpenCvSharp.Mat;
using ImreadModes = OpenCvSharp.ImreadModes;

namespace AoiMeasureTool
{
    public partial class MainForm : Form
    {
        private CvMat _sourceImage;
        private CvMat _grayImage;
        private readonly CvMat[] _preprocessImages = new CvMat[4];
        private PictureBox[] _preprocessPictureBoxes;
        private CheckBox[] _preprocessEnabledChecks;
        private TrackBar[] _thresholdTrackBars;
        private NumericUpDown[] _thresholdInputs;
        private NumericUpDown[] _erodeInputs;
        private NumericUpDown[] _dilateInputs;
        private NumericUpDown[] _openInputs;
        private NumericUpDown[] _closeInputs;
        private bool _synchronizingThreshold;
        private int _selectedPreprocessIndex;
        private float _activeImageScale = 1f;
        private float _activeFitScale = 1f;
        private bool _isDraggingActiveImage;
        private bool _showingOriginalInActivePreview;
        private Point _lastActiveMousePosition;
        private float _imageScale = 1f;
        private float _fitScale = 1f;
        private bool _isDraggingImage;
        private Point _lastMousePosition;

        public MainForm()
        {
            InitializeComponent();
            InitializePreprocessControls();
        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            if (openFileDialogImage.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                CvMat loadedImage = null;
                Bitmap displayImage = null;

                try
                {
                    loadedImage = Cv2.ImRead(openFileDialogImage.FileName, ImreadModes.Color);
                    if (loadedImage.Empty())
                    {
                        throw new InvalidOperationException("OpenCV could not decode this image.");
                    }

                    displayImage = BitmapConverter.ToBitmap(loadedImage);
                    var binaryOriginalImage = new Bitmap(displayImage);

                    var oldImage = pictureBoxImage.Image;
                    pictureBoxImage.Image = displayImage;
                    displayImage = null;
                    oldImage?.Dispose();
                    SetPictureBoxImage(pictureBoxBinaryOriginal, binaryOriginalImage);
                    FitImageToViewport();

                    _sourceImage?.Dispose();
                    _sourceImage = loadedImage;
                    loadedImage = null;

                    _grayImage?.Dispose();
                    _grayImage = new CvMat();
                    Cv2.CvtColor(_sourceImage, _grayImage, ColorConversionCodes.BGR2GRAY);
                    UpdateAllPreprocessImages();

                    labelImageInfo.Text = string.Format(
                        "{0}    {1} x {2} px",
                        Path.GetFileName(openFileDialogImage.FileName),
                        _sourceImage.Width,
                        _sourceImage.Height);
                }
                finally
                {
                    displayImage?.Dispose();
                    loadedImage?.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    "Unable to load image.\r\n\r\n" + ex.Message,
                    "Load Image",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InitializePreprocessControls()
        {
            _preprocessPictureBoxes = new[]
            {
                pictureBoxPreprocess1,
                pictureBoxPreprocess2,
                pictureBoxPreprocess3,
                pictureBoxPreprocess4
            };

            _preprocessEnabledChecks = new[]
            {
                checkBoxPreprocess1Enabled,
                checkBoxPreprocess2Enabled,
                checkBoxPreprocess3Enabled,
                checkBoxPreprocess4Enabled
            };

            _thresholdTrackBars = new[] { trackBarThreshold1, trackBarThreshold2, trackBarThreshold3, trackBarThreshold4 };
            _thresholdInputs = new[] { numericThreshold1, numericThreshold2, numericThreshold3, numericThreshold4 };
            _erodeInputs = new[] { numericErode1, numericErode2, numericErode3, numericErode4 };
            _dilateInputs = new[] { numericDilate1, numericDilate2, numericDilate3, numericDilate4 };
            _openInputs = new[] { numericOpen1, numericOpen2, numericOpen3, numericOpen4 };
            _closeInputs = new[] { numericClose1, numericClose2, numericClose3, numericClose4 };

            for (var i = 0; i < 4; i++)
            {
                SetPreprocessControlsEnabled(i, _preprocessEnabledChecks[i].Checked);
            }
        }

        private PreprocessParam CreatePreprocessParam(int index)
        {
            return new PreprocessParam
            {
                Enabled = _preprocessEnabledChecks[index].Checked,
                WhiteObject = index < 2,
                Threshold = (int)_thresholdInputs[index].Value,
                ErodeIterations = (int)_erodeInputs[index].Value,
                DilateIterations = (int)_dilateInputs[index].Value,
                OpenIterations = (int)_openInputs[index].Value,
                CloseIterations = (int)_closeInputs[index].Value
            };
        }

        private void UpdateAllPreprocessImages()
        {
            if (_preprocessPictureBoxes == null)
            {
                return;
            }

            for (var i = 0; i < 4; i++)
            {
                UpdatePreprocessImage(i);
            }
        }

        private void UpdatePreprocessImage(int index)
        {
            DisposePreprocessImage(index);

            if (_grayImage == null || _grayImage.Empty() || !_preprocessEnabledChecks[index].Checked)
            {
                SetPictureBoxImage(_preprocessPictureBoxes[index], null);
                if (index == _selectedPreprocessIndex)
                {
                    UpdateActivePreprocessPreview();
                }
                return;
            }

            _preprocessImages[index] = ImageProcessor.Preprocess(_grayImage, CreatePreprocessParam(index));
            SetPictureBoxImage(_preprocessPictureBoxes[index], BitmapConverter.ToBitmap(_preprocessImages[index]));
            if (index == _selectedPreprocessIndex)
            {
                UpdateActivePreprocessPreview();
            }
        }

        private void PreprocessThumbnail_Click(object sender, EventArgs e)
        {
            var control = sender as Control;
            if (control == null || control.Tag == null)
            {
                return;
            }

            _selectedPreprocessIndex = Convert.ToInt32(control.Tag);
            tabControlPreprocess.SelectedIndex = _selectedPreprocessIndex;
            UpdateActivePreprocessPreview();
        }

        private void UpdateActivePreprocessPreview()
        {
            if (_preprocessPictureBoxes == null || _showingOriginalInActivePreview)
            {
                return;
            }

            var sourceImage = _preprocessPictureBoxes[_selectedPreprocessIndex].Image;
            SetActivePreviewImage(sourceImage == null ? null : new Bitmap(sourceImage));
            labelActivePreprocess.Text = string.Format(
                "前處理 {0}｜滾輪縮放、左鍵拖曳、右鍵看原圖",
                _selectedPreprocessIndex + 1);
        }

        private void ActivePreprocess_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && pictureBoxBinaryOriginal.Image != null)
            {
                _showingOriginalInActivePreview = true;
                SetActivePreviewImage(new Bitmap(pictureBoxBinaryOriginal.Image));
                labelActivePreprocess.Text = "原圖（放開右鍵返回處理結果）";
                return;
            }

            if (e.Button != MouseButtons.Left || pictureBoxActivePreprocess.Image == null)
            {
                return;
            }

            _isDraggingActiveImage = true;
            _lastActiveMousePosition = panelActiveViewport.PointToClient(pictureBoxActivePreprocess.PointToScreen(e.Location));
            pictureBoxActivePreprocess.Cursor = Cursors.SizeAll;
            pictureBoxActivePreprocess.Capture = true;
        }

        private void ActivePreprocess_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDraggingActiveImage)
            {
                return;
            }

            var currentPosition = panelActiveViewport.PointToClient(pictureBoxActivePreprocess.PointToScreen(e.Location));
            pictureBoxActivePreprocess.Left += currentPosition.X - _lastActiveMousePosition.X;
            pictureBoxActivePreprocess.Top += currentPosition.Y - _lastActiveMousePosition.Y;
            _lastActiveMousePosition = currentPosition;
            ConstrainActiveImagePosition();
        }

        private void ActivePreprocess_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _showingOriginalInActivePreview = false;
                UpdateActivePreprocessPreview();
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                _isDraggingActiveImage = false;
                pictureBoxActivePreprocess.Cursor = Cursors.Default;
                pictureBoxActivePreprocess.Capture = false;
            }
        }

        private void ActivePreprocess_MouseWheel(object sender, MouseEventArgs e)
        {
            if (pictureBoxActivePreprocess.Image == null)
            {
                return;
            }

            var sourceControl = (Control)sender;
            var mousePosition = panelActiveViewport.PointToClient(sourceControl.PointToScreen(e.Location));
            var imageX = (mousePosition.X - pictureBoxActivePreprocess.Left) / _activeImageScale;
            var imageY = (mousePosition.Y - pictureBoxActivePreprocess.Top) / _activeImageScale;
            var zoomFactor = e.Delta > 0 ? 1.15f : 1f / 1.15f;
            var minimumScale = _activeFitScale * 0.25f;
            var maximumScale = _activeFitScale * 20f;
            _activeImageScale = Math.Max(minimumScale, Math.Min(maximumScale, _activeImageScale * zoomFactor));

            var width = Math.Max(1, (int)Math.Round(pictureBoxActivePreprocess.Image.Width * _activeImageScale));
            var height = Math.Max(1, (int)Math.Round(pictureBoxActivePreprocess.Image.Height * _activeImageScale));
            pictureBoxActivePreprocess.Size = new Size(width, height);
            pictureBoxActivePreprocess.Left = (int)Math.Round(mousePosition.X - imageX * _activeImageScale);
            pictureBoxActivePreprocess.Top = (int)Math.Round(mousePosition.Y - imageY * _activeImageScale);
            ConstrainActiveImagePosition();
        }

        private void ActivePreview_MouseEnter(object sender, EventArgs e)
        {
            pictureBoxActivePreprocess.Focus();
        }

        private void SetActivePreviewImage(Bitmap image)
        {
            SetPictureBoxImage(pictureBoxActivePreprocess, image);
            FitActiveImageToViewport();
        }

        private void FitActiveImageToViewport()
        {
            if (pictureBoxActivePreprocess.Image == null)
            {
                pictureBoxActivePreprocess.Location = Point.Empty;
                pictureBoxActivePreprocess.Size = panelActiveViewport.ClientSize;
                _activeImageScale = 1f;
                _activeFitScale = 1f;
                return;
            }

            var scaleX = panelActiveViewport.ClientSize.Width / (float)pictureBoxActivePreprocess.Image.Width;
            var scaleY = panelActiveViewport.ClientSize.Height / (float)pictureBoxActivePreprocess.Image.Height;
            _activeFitScale = Math.Min(scaleX, scaleY);
            _activeImageScale = _activeFitScale;
            var width = Math.Max(1, (int)Math.Round(pictureBoxActivePreprocess.Image.Width * _activeImageScale));
            var height = Math.Max(1, (int)Math.Round(pictureBoxActivePreprocess.Image.Height * _activeImageScale));
            pictureBoxActivePreprocess.Size = new Size(width, height);
            pictureBoxActivePreprocess.Left = (panelActiveViewport.ClientSize.Width - width) / 2;
            pictureBoxActivePreprocess.Top = (panelActiveViewport.ClientSize.Height - height) / 2;
        }

        private void ConstrainActiveImagePosition()
        {
            pictureBoxActivePreprocess.Left = pictureBoxActivePreprocess.Width <= panelActiveViewport.ClientSize.Width
                ? (panelActiveViewport.ClientSize.Width - pictureBoxActivePreprocess.Width) / 2
                : Math.Max(panelActiveViewport.ClientSize.Width - pictureBoxActivePreprocess.Width, Math.Min(0, pictureBoxActivePreprocess.Left));

            pictureBoxActivePreprocess.Top = pictureBoxActivePreprocess.Height <= panelActiveViewport.ClientSize.Height
                ? (panelActiveViewport.ClientSize.Height - pictureBoxActivePreprocess.Height) / 2
                : Math.Max(panelActiveViewport.ClientSize.Height - pictureBoxActivePreprocess.Height, Math.Min(0, pictureBoxActivePreprocess.Top));
        }

        private static void SetPictureBoxImage(PictureBox pictureBox, Bitmap image)
        {
            var oldImage = pictureBox.Image;
            pictureBox.Image = image;
            oldImage?.Dispose();
        }

        private void DisposePreprocessImage(int index)
        {
            _preprocessImages[index]?.Dispose();
            _preprocessImages[index] = null;
        }

        private void PreprocessEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (_preprocessEnabledChecks == null)
            {
                return;
            }

            var index = Array.IndexOf(_preprocessEnabledChecks, sender as CheckBox);
            if (index < 0)
            {
                return;
            }

            SetPreprocessControlsEnabled(index, _preprocessEnabledChecks[index].Checked);
            UpdatePreprocessImage(index);
        }

        private void SetPreprocessControlsEnabled(int index, bool enabled)
        {
            _thresholdTrackBars[index].Enabled = enabled;
            _thresholdInputs[index].Enabled = enabled;
            _erodeInputs[index].Enabled = enabled;
            _dilateInputs[index].Enabled = enabled;
            _openInputs[index].Enabled = enabled;
            _closeInputs[index].Enabled = enabled;
        }

        private void ThresholdTrackBar_Scroll(object sender, EventArgs e)
        {
            if (_synchronizingThreshold || _thresholdTrackBars == null)
            {
                return;
            }

            var index = Array.IndexOf(_thresholdTrackBars, sender as TrackBar);
            if (index < 0)
            {
                return;
            }

            _synchronizingThreshold = true;
            _thresholdInputs[index].Value = _thresholdTrackBars[index].Value;
            _synchronizingThreshold = false;
            UpdatePreprocessImage(index);
        }

        private void ThresholdValue_ValueChanged(object sender, EventArgs e)
        {
            if (_synchronizingThreshold || _thresholdInputs == null)
            {
                return;
            }

            var index = Array.IndexOf(_thresholdInputs, sender as NumericUpDown);
            if (index < 0)
            {
                return;
            }

            _synchronizingThreshold = true;
            _thresholdTrackBars[index].Value = (int)_thresholdInputs[index].Value;
            _synchronizingThreshold = false;
            UpdatePreprocessImage(index);
        }

        private void MorphologyValue_ValueChanged(object sender, EventArgs e)
        {
            if (_erodeInputs == null)
            {
                return;
            }

            var input = sender as NumericUpDown;
            var index = Array.IndexOf(_erodeInputs, input);
            if (index < 0) index = Array.IndexOf(_dilateInputs, input);
            if (index < 0) index = Array.IndexOf(_openInputs, input);
            if (index < 0) index = Array.IndexOf(_closeInputs, input);
            if (index >= 0) UpdatePreprocessImage(index);
        }

        private void FitImageToViewport()
        {
            if (pictureBoxImage.Image == null)
            {
                return;
            }

            var scaleX = panelImageViewport.ClientSize.Width / (float)pictureBoxImage.Image.Width;
            var scaleY = panelImageViewport.ClientSize.Height / (float)pictureBoxImage.Image.Height;
            _fitScale = Math.Min(scaleX, scaleY);
            _imageScale = _fitScale;
            ApplyImageTransform(true);
        }

        private void PictureBoxImage_MouseWheel(object sender, MouseEventArgs e)
        {
            if (pictureBoxImage.Image == null)
            {
                return;
            }

            var sourceControl = (Control)sender;
            var mousePosition = panelImageViewport.PointToClient(sourceControl.PointToScreen(e.Location));
            var imageX = (mousePosition.X - pictureBoxImage.Left) / _imageScale;
            var imageY = (mousePosition.Y - pictureBoxImage.Top) / _imageScale;
            var zoomFactor = e.Delta > 0 ? 1.15f : 1f / 1.15f;
            var minimumScale = _fitScale * 0.25f;
            var maximumScale = _fitScale * 20f;

            _imageScale = Math.Max(minimumScale, Math.Min(maximumScale, _imageScale * zoomFactor));
            var newWidth = Math.Max(1, (int)Math.Round(pictureBoxImage.Image.Width * _imageScale));
            var newHeight = Math.Max(1, (int)Math.Round(pictureBoxImage.Image.Height * _imageScale));
            pictureBoxImage.Size = new Size(newWidth, newHeight);
            pictureBoxImage.Left = (int)Math.Round(mousePosition.X - imageX * _imageScale);
            pictureBoxImage.Top = (int)Math.Round(mousePosition.Y - imageY * _imageScale);
            ConstrainImagePosition();
        }

        private void PictureBoxImage_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || pictureBoxImage.Image == null)
            {
                return;
            }

            _isDraggingImage = true;
            _lastMousePosition = panelImageViewport.PointToClient(pictureBoxImage.PointToScreen(e.Location));
            pictureBoxImage.Cursor = Cursors.SizeAll;
            pictureBoxImage.Capture = true;
        }

        private void PictureBoxImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDraggingImage)
            {
                return;
            }

            var currentPosition = panelImageViewport.PointToClient(pictureBoxImage.PointToScreen(e.Location));
            pictureBoxImage.Left += currentPosition.X - _lastMousePosition.X;
            pictureBoxImage.Top += currentPosition.Y - _lastMousePosition.Y;
            _lastMousePosition = currentPosition;
            ConstrainImagePosition();
        }

        private void PictureBoxImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            _isDraggingImage = false;
            pictureBoxImage.Cursor = Cursors.Default;
            pictureBoxImage.Capture = false;
        }

        private void ImageViewport_MouseEnter(object sender, EventArgs e)
        {
            pictureBoxImage.Focus();
        }

        private void ApplyImageTransform(bool centerImage)
        {
            var width = Math.Max(1, (int)Math.Round(pictureBoxImage.Image.Width * _imageScale));
            var height = Math.Max(1, (int)Math.Round(pictureBoxImage.Image.Height * _imageScale));
            pictureBoxImage.Size = new Size(width, height);

            if (centerImage)
            {
                pictureBoxImage.Left = (panelImageViewport.ClientSize.Width - width) / 2;
                pictureBoxImage.Top = (panelImageViewport.ClientSize.Height - height) / 2;
            }

            ConstrainImagePosition();
        }

        private void ConstrainImagePosition()
        {
            pictureBoxImage.Left = pictureBoxImage.Width <= panelImageViewport.ClientSize.Width
                ? (panelImageViewport.ClientSize.Width - pictureBoxImage.Width) / 2
                : Math.Max(panelImageViewport.ClientSize.Width - pictureBoxImage.Width, Math.Min(0, pictureBoxImage.Left));

            pictureBoxImage.Top = pictureBoxImage.Height <= panelImageViewport.ClientSize.Height
                ? (panelImageViewport.ClientSize.Height - pictureBoxImage.Height) / 2
                : Math.Max(panelImageViewport.ClientSize.Height - pictureBoxImage.Height, Math.Min(0, pictureBoxImage.Top));
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            pictureBoxImage.Image?.Dispose();
            pictureBoxImage.Image = null;
            SetPictureBoxImage(pictureBoxBinaryOriginal, null);
            SetPictureBoxImage(pictureBoxActivePreprocess, null);
            _sourceImage?.Dispose();
            _sourceImage = null;
            _grayImage?.Dispose();
            _grayImage = null;

            for (var i = 0; i < _preprocessImages.Length; i++)
            {
                DisposePreprocessImage(i);
                if (_preprocessPictureBoxes != null)
                {
                    SetPictureBoxImage(_preprocessPictureBoxes[i], null);
                }
            }
        }
    }
}
