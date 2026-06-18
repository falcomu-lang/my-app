using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace AoiMeasureTool
{
    public sealed class MainForm : Form
    {
        private readonly PictureBox _mainPictureBox = new PictureBox();
        private readonly PictureBox[] _preprocessPictureBoxes = new PictureBox[4];
        private readonly CheckBox[] _enabledChecks = new CheckBox[4];
        private readonly TrackBar[] _thresholdBars = new TrackBar[4];
        private readonly Label[] _thresholdLabels = new Label[4];
        private readonly NumericUpDown[] _erodeInputs = new NumericUpDown[4];
        private readonly NumericUpDown[] _dilateInputs = new NumericUpDown[4];
        private readonly NumericUpDown[] _openInputs = new NumericUpDown[4];
        private readonly NumericUpDown[] _closeInputs = new NumericUpDown[4];

        private readonly NumericUpDown _roiX = CreateNumeric(0, 100000, 0);
        private readonly NumericUpDown _roiY = CreateNumeric(0, 100000, 0);
        private readonly NumericUpDown _roiW = CreateNumeric(1, 100000, 100);
        private readonly NumericUpDown _roiH = CreateNumeric(1, 100000, 100);
        private readonly ComboBox _baseCombo = new ComboBox();
        private readonly ComboBox _modeCombo = new ComboBox();
        private readonly NumericUpDown _scaleInput = CreateDecimalNumeric(0.0001m, 100000m, 5.2m);
        private readonly TextBox _resultText = new TextBox();

        private readonly CheckBox _edgeWhiteObject = new CheckBox();
        private readonly CheckBox _edgeInvert = new CheckBox();
        private readonly NumericUpDown _edgeThreshold = CreateNumeric(0, 255, 128);
        private readonly Label _edgeLabel = new Label();

        private Mat _sourceColor;
        private Mat _gray;
        private readonly Mat[] _preprocessed = new Mat[4];
        private MeasureResult _lastMeasure;
        private EdgePointResult _lastEdgePoints;

        private bool _isDraggingRoi;
        private System.Drawing.Point _dragStart;

        public MainForm()
        {
            Text = "AOI Image Size Measurement Tool";
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new System.Drawing.Size(1200, 760);
            BuildUi();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            DisposeImages();
            base.OnFormClosed(e);
        }

        private void BuildUi()
        {
            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                BackColor = Color.FromArgb(30, 32, 36),
                Padding = new Padding(8)
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58));
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            Controls.Add(root);

            var topBar = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.FromArgb(42, 45, 50)
            };
            root.SetColumnSpan(topBar, 2);
            root.Controls.Add(topBar, 0, 0);

            var loadButton = CreateButton("Load Image");
            loadButton.Click += LoadButton_Click;
            topBar.Controls.Add(loadButton);

            var testImageButton = CreateButton("Open Test Folder");
            testImageButton.Click += delegate
            {
                var testPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestImages");
                if (Directory.Exists(testPath))
                {
                    System.Diagnostics.Process.Start("explorer.exe", testPath);
                }
            };
            topBar.Controls.Add(testImageButton);

            var measureButton = CreateButton("Measure");
            measureButton.Click += MeasureButton_Click;
            topBar.Controls.Add(measureButton);

            var edgeButton = CreateButton("Find Upper Edge");
            edgeButton.Click += EdgeButton_Click;
            topBar.Controls.Add(edgeButton);

            var closeButton = CreateButton("Close");
            closeButton.BackColor = Color.FromArgb(150, 48, 48);
            closeButton.Click += delegate { Close(); };
            topBar.Controls.Add(closeButton);

            _mainPictureBox.Dock = DockStyle.Fill;
            _mainPictureBox.BackColor = Color.Black;
            _mainPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            _mainPictureBox.BorderStyle = BorderStyle.FixedSingle;
            _mainPictureBox.MouseDown += MainPictureBox_MouseDown;
            _mainPictureBox.MouseMove += MainPictureBox_MouseMove;
            _mainPictureBox.MouseUp += MainPictureBox_MouseUp;
            root.Controls.Add(_mainPictureBox, 0, 1);

            var rightPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Color.FromArgb(30, 32, 36)
            };
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 52));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 28));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            root.Controls.Add(rightPanel, 1, 1);

            rightPanel.Controls.Add(BuildPreprocessGrid(), 0, 0);
            rightPanel.Controls.Add(BuildControlPanel(), 0, 1);
            rightPanel.Controls.Add(BuildResultPanel(), 0, 2);

            _baseCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _baseCombo.Items.AddRange(new object[] { "Preprocess 1", "Preprocess 2", "Preprocess 3", "Preprocess 4" });
            _baseCombo.SelectedIndex = 0;
            _modeCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _modeCombo.Items.AddRange(new object[] { "Top-Bottom Distance", "Left-Right Distance" });
            _modeCombo.SelectedIndex = 0;

            _edgeWhiteObject.Text = "White object";
            _edgeWhiteObject.Checked = true;
            _edgeWhiteObject.ForeColor = Color.White;
            _edgeInvert.Text = "Invert binary";
            _edgeInvert.ForeColor = Color.White;
        }

        private Control BuildPreprocessGrid()
        {
            var grid = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 2 };
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            for (var i = 0; i < 4; i++)
            {
                grid.Controls.Add(BuildPreprocessPanel(i), i % 2, i / 2);
            }

            return grid;
        }

        private Control BuildPreprocessPanel(int index)
        {
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(5),
                BackColor = Color.FromArgb(38, 41, 46)
            };
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 72));

            _enabledChecks[index] = new CheckBox
            {
                Text = index < 2 ? "Preprocess " + (index + 1) + "  Gray > T" : "Preprocess " + (index + 1) + "  Gray < T",
                Checked = true,
                Dock = DockStyle.Fill,
                ForeColor = Color.White
            };
            _enabledChecks[index].CheckedChanged += delegate { UpdateProcessing(); };
            panel.Controls.Add(_enabledChecks[index], 0, 0);

            _preprocessPictureBoxes[index] = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Black,
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle
            };
            panel.Controls.Add(_preprocessPictureBoxes[index], 0, 1);

            var controls = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 2 };
            controls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
            controls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55));
            controls.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            controls.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            _thresholdLabels[index] = CreateSmallLabel("T=128");
            controls.Controls.Add(_thresholdLabels[index], 0, 0);
            _thresholdBars[index] = new TrackBar { Minimum = 0, Maximum = 255, Value = 128, TickFrequency = 32, Dock = DockStyle.Fill };
            _thresholdBars[index].Scroll += delegate
            {
                _thresholdLabels[index].Text = "T=" + _thresholdBars[index].Value;
                UpdateProcessing();
            };
            controls.Controls.Add(_thresholdBars[index], 1, 0);

            var morph = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false };
            _erodeInputs[index] = AddMorphInput(morph, "E", index);
            _dilateInputs[index] = AddMorphInput(morph, "D", index);
            _openInputs[index] = AddMorphInput(morph, "O", index);
            _closeInputs[index] = AddMorphInput(morph, "C", index);
            controls.SetColumnSpan(morph, 2);
            controls.Controls.Add(morph, 0, 1);
            panel.Controls.Add(controls, 0, 2);

            return panel;
        }

        private Control BuildControlPanel()
        {
            var panel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 7, Padding = new Padding(6) };
            for (var i = 0; i < 4; i++)
            {
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            }

            for (var i = 0; i < 7; i++)
            {
                panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
            }

            AddLabeled(panel, "ROI X", _roiX, 0, 0);
            AddLabeled(panel, "ROI Y", _roiY, 2, 0);
            AddLabeled(panel, "ROI W", _roiW, 0, 1);
            AddLabeled(panel, "ROI H", _roiH, 2, 1);
            _roiX.ValueChanged += delegate { RefreshOverlay(); };
            _roiY.ValueChanged += delegate { RefreshOverlay(); };
            _roiW.ValueChanged += delegate { RefreshOverlay(); };
            _roiH.ValueChanged += delegate { RefreshOverlay(); };

            AddLabeled(panel, "Base", _baseCombo, 0, 2);
            AddLabeled(panel, "Mode", _modeCombo, 2, 2);
            AddLabeled(panel, "um/px", _scaleInput, 0, 3);
            AddLabeled(panel, "Edge T", _edgeThreshold, 2, 3);
            panel.Controls.Add(_edgeWhiteObject, 0, 4);
            panel.SetColumnSpan(_edgeWhiteObject, 2);
            panel.Controls.Add(_edgeInvert, 2, 4);
            panel.SetColumnSpan(_edgeInvert, 2);

            _edgeLabel.Dock = DockStyle.Fill;
            _edgeLabel.ForeColor = Color.White;
            _edgeLabel.Text = "Left Upper Edge Point = -    Right Upper Edge Point = -";
            panel.Controls.Add(_edgeLabel, 0, 5);
            panel.SetColumnSpan(_edgeLabel, 4);

            return panel;
        }

        private Control BuildResultPanel()
        {
            _resultText.Dock = DockStyle.Fill;
            _resultText.Multiline = true;
            _resultText.ReadOnly = true;
            _resultText.BackColor = Color.FromArgb(18, 20, 24);
            _resultText.ForeColor = Color.White;
            _resultText.Font = new Font("Consolas", 11f);
            return _resultText;
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

        private static Button CreateButton(string text)
        {
            return new Button
            {
                Text = text,
                Width = 150,
                Height = 40,
                Margin = new Padding(6),
                BackColor = Color.FromArgb(64, 82, 110),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
        }

        private static Label CreateSmallLabel(string text)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.White
            };
        }

        private static NumericUpDown AddMorphInput(FlowLayoutPanel panel, string label, int index)
        {
            panel.Controls.Add(new Label { Text = label, Width = 16, ForeColor = Color.White, TextAlign = ContentAlignment.MiddleCenter });
            var input = CreateNumeric(0, 20, 0);
            input.Width = 48;
            input.ValueChanged += delegate
            {
                var form = input.FindForm() as MainForm;
                if (form != null)
                {
                    form.UpdateProcessing();
                }
            };
            panel.Controls.Add(input);
            return input;
        }

        private static void AddLabeled(TableLayoutPanel panel, string label, Control control, int column, int row)
        {
            var caption = CreateSmallLabel(label);
            panel.Controls.Add(caption, column, row);
            panel.Controls.Add(control, column + 1, row);
            control.Dock = DockStyle.Fill;
        }

        private static NumericUpDown CreateNumeric(int min, int max, int value)
        {
            return new NumericUpDown
            {
                Minimum = min,
                Maximum = max,
                Value = value,
                DecimalPlaces = 0
            };
        }

        private static NumericUpDown CreateDecimalNumeric(decimal min, decimal max, decimal value)
        {
            return new NumericUpDown
            {
                Minimum = min,
                Maximum = max,
                Value = value,
                DecimalPlaces = 4,
                Increment = 0.1m
            };
        }
    }
}
