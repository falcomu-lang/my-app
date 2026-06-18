using System.Drawing;
using System.Windows.Forms;

namespace AoiMeasureTool
{
    public sealed partial class MainForm
    {
        private PictureBox _mainPictureBox;
        private PictureBox[] _preprocessPictureBoxes;
        private CheckBox[] _enabledChecks;
        private TrackBar[] _thresholdBars;
        private Label[] _thresholdLabels;
        private NumericUpDown[] _erodeInputs;
        private NumericUpDown[] _dilateInputs;
        private NumericUpDown[] _openInputs;
        private NumericUpDown[] _closeInputs;
        private NumericUpDown _roiX;
        private NumericUpDown _roiY;
        private NumericUpDown _roiW;
        private NumericUpDown _roiH;
        private ComboBox _baseCombo;
        private ComboBox _modeCombo;
        private NumericUpDown _scaleInput;
        private TextBox _resultText;
        private CheckBox _edgeWhiteObject;
        private CheckBox _edgeInvert;
        private NumericUpDown _edgeThreshold;
        private Label _edgeLabel;

        private void InitializeComponent()
        {
            Text = "AOI Image Size Measurement Tool";
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(1200, 760);

            _preprocessPictureBoxes = new PictureBox[4];
            _enabledChecks = new CheckBox[4];
            _thresholdBars = new TrackBar[4];
            _thresholdLabels = new Label[4];
            _erodeInputs = new NumericUpDown[4];
            _dilateInputs = new NumericUpDown[4];
            _openInputs = new NumericUpDown[4];
            _closeInputs = new NumericUpDown[4];

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
            testImageButton.Click += TestImageButton_Click;
            topBar.Controls.Add(testImageButton);

            var measureButton = CreateButton("Measure");
            measureButton.Click += MeasureButton_Click;
            topBar.Controls.Add(measureButton);

            var edgeButton = CreateButton("Find Upper Edge");
            edgeButton.Click += EdgeButton_Click;
            topBar.Controls.Add(edgeButton);

            var closeButton = CreateButton("Close");
            closeButton.BackColor = Color.FromArgb(150, 48, 48);
            closeButton.Click += CloseButton_Click;
            topBar.Controls.Add(closeButton);

            _mainPictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Black,
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle
            };
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
            _enabledChecks[index].CheckedChanged += PreprocessValueChanged;
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
            _thresholdBars[index].Scroll += ThresholdBar_Scroll;
            controls.Controls.Add(_thresholdBars[index], 1, 0);

            var morph = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false };
            _erodeInputs[index] = AddMorphInput(morph, "E");
            _dilateInputs[index] = AddMorphInput(morph, "D");
            _openInputs[index] = AddMorphInput(morph, "O");
            _closeInputs[index] = AddMorphInput(morph, "C");
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

            _roiX = CreateNumeric(0, 100000, 0);
            _roiY = CreateNumeric(0, 100000, 0);
            _roiW = CreateNumeric(1, 100000, 100);
            _roiH = CreateNumeric(1, 100000, 100);
            _baseCombo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            _modeCombo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            _scaleInput = CreateDecimalNumeric(0.0001m, 100000m, 5.2m);
            _edgeThreshold = CreateNumeric(0, 255, 128);
            _edgeWhiteObject = new CheckBox { Text = "White object", Checked = true, ForeColor = Color.White };
            _edgeInvert = new CheckBox { Text = "Invert binary", ForeColor = Color.White };

            _baseCombo.Items.AddRange(new object[] { "Preprocess 1", "Preprocess 2", "Preprocess 3", "Preprocess 4" });
            _baseCombo.SelectedIndex = 0;
            _modeCombo.Items.AddRange(new object[] { "Top-Bottom Distance", "Left-Right Distance" });
            _modeCombo.SelectedIndex = 0;

            AddLabeled(panel, "ROI X", _roiX, 0, 0);
            AddLabeled(panel, "ROI Y", _roiY, 2, 0);
            AddLabeled(panel, "ROI W", _roiW, 0, 1);
            AddLabeled(panel, "ROI H", _roiH, 2, 1);
            _roiX.ValueChanged += RoiValueChanged;
            _roiY.ValueChanged += RoiValueChanged;
            _roiW.ValueChanged += RoiValueChanged;
            _roiH.ValueChanged += RoiValueChanged;

            AddLabeled(panel, "Base", _baseCombo, 0, 2);
            AddLabeled(panel, "Mode", _modeCombo, 2, 2);
            AddLabeled(panel, "um/px", _scaleInput, 0, 3);
            AddLabeled(panel, "Edge T", _edgeThreshold, 2, 3);
            panel.Controls.Add(_edgeWhiteObject, 0, 4);
            panel.SetColumnSpan(_edgeWhiteObject, 2);
            panel.Controls.Add(_edgeInvert, 2, 4);
            panel.SetColumnSpan(_edgeInvert, 2);

            _edgeLabel = new Label
            {
                Dock = DockStyle.Fill,
                ForeColor = Color.White,
                Text = "Left Upper Edge Point = -    Right Upper Edge Point = -"
            };
            panel.Controls.Add(_edgeLabel, 0, 5);
            panel.SetColumnSpan(_edgeLabel, 4);

            return panel;
        }

        private Control BuildResultPanel()
        {
            _resultText = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                BackColor = Color.FromArgb(18, 20, 24),
                ForeColor = Color.White,
                Font = new Font("Consolas", 11f)
            };
            return _resultText;
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

        private NumericUpDown AddMorphInput(FlowLayoutPanel panel, string label)
        {
            panel.Controls.Add(new Label { Text = label, Width = 16, ForeColor = Color.White, TextAlign = ContentAlignment.MiddleCenter });
            var input = CreateNumeric(0, 20, 0);
            input.Width = 48;
            input.ValueChanged += PreprocessValueChanged;
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
