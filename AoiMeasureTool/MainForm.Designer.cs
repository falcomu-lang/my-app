using System.Drawing;
using System.Windows.Forms;

namespace AoiMeasureTool
{
    public sealed partial class MainForm
    {
        private TableLayoutPanel _rootTable;
        private FlowLayoutPanel _topBar;
        private Button _loadButton;
        private Button _testImageButton;
        private Button _measureButton;
        private Button _edgeButton;
        private Button _closeButton;
        private PictureBox _mainPictureBox;
        private TableLayoutPanel _rightPanel;
        private TableLayoutPanel _preprocessGrid;
        private TableLayoutPanel _controlPanel;
        private TextBox _resultText;

        private TableLayoutPanel _preprocess1Panel;
        private TableLayoutPanel _preprocess2Panel;
        private TableLayoutPanel _preprocess3Panel;
        private TableLayoutPanel _preprocess4Panel;
        private CheckBox _preprocess1Enabled;
        private CheckBox _preprocess2Enabled;
        private CheckBox _preprocess3Enabled;
        private CheckBox _preprocess4Enabled;
        private PictureBox _preprocess1PictureBox;
        private PictureBox _preprocess2PictureBox;
        private PictureBox _preprocess3PictureBox;
        private PictureBox _preprocess4PictureBox;
        private Label _preprocess1ThresholdLabel;
        private Label _preprocess2ThresholdLabel;
        private Label _preprocess3ThresholdLabel;
        private Label _preprocess4ThresholdLabel;
        private TrackBar _preprocess1ThresholdBar;
        private TrackBar _preprocess2ThresholdBar;
        private TrackBar _preprocess3ThresholdBar;
        private TrackBar _preprocess4ThresholdBar;
        private NumericUpDown _preprocess1Erode;
        private NumericUpDown _preprocess1Dilate;
        private NumericUpDown _preprocess1Open;
        private NumericUpDown _preprocess1Close;
        private NumericUpDown _preprocess2Erode;
        private NumericUpDown _preprocess2Dilate;
        private NumericUpDown _preprocess2Open;
        private NumericUpDown _preprocess2Close;
        private NumericUpDown _preprocess3Erode;
        private NumericUpDown _preprocess3Dilate;
        private NumericUpDown _preprocess3Open;
        private NumericUpDown _preprocess3Close;
        private NumericUpDown _preprocess4Erode;
        private NumericUpDown _preprocess4Dilate;
        private NumericUpDown _preprocess4Open;
        private NumericUpDown _preprocess4Close;

        private Label _roiXLabel;
        private Label _roiYLabel;
        private Label _roiWLabel;
        private Label _roiHLabel;
        private NumericUpDown _roiX;
        private NumericUpDown _roiY;
        private NumericUpDown _roiW;
        private NumericUpDown _roiH;
        private Label _baseLabel;
        private Label _modeLabel;
        private Label _scaleLabel;
        private Label _edgeThresholdLabel;
        private ComboBox _baseCombo;
        private ComboBox _modeCombo;
        private NumericUpDown _scaleInput;
        private NumericUpDown _edgeThreshold;
        private CheckBox _edgeWhiteObject;
        private CheckBox _edgeInvert;
        private Label _edgeLabel;

        private PictureBox[] _preprocessPictureBoxes;
        private CheckBox[] _enabledChecks;
        private TrackBar[] _thresholdBars;
        private Label[] _thresholdLabels;
        private NumericUpDown[] _erodeInputs;
        private NumericUpDown[] _dilateInputs;
        private NumericUpDown[] _openInputs;
        private NumericUpDown[] _closeInputs;

        private void InitializeComponent()
        {
            Text = "AOI Image Size Measurement Tool";
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(1200, 760);
            BackColor = Color.FromArgb(30, 32, 36);

            _rootTable = new TableLayoutPanel();
            _topBar = new FlowLayoutPanel();
            _loadButton = new Button();
            _testImageButton = new Button();
            _measureButton = new Button();
            _edgeButton = new Button();
            _closeButton = new Button();
            _mainPictureBox = new PictureBox();
            _rightPanel = new TableLayoutPanel();
            _preprocessGrid = new TableLayoutPanel();
            _controlPanel = new TableLayoutPanel();
            _resultText = new TextBox();

            _preprocess1Panel = new TableLayoutPanel();
            _preprocess2Panel = new TableLayoutPanel();
            _preprocess3Panel = new TableLayoutPanel();
            _preprocess4Panel = new TableLayoutPanel();
            _preprocess1Enabled = new CheckBox();
            _preprocess2Enabled = new CheckBox();
            _preprocess3Enabled = new CheckBox();
            _preprocess4Enabled = new CheckBox();
            _preprocess1PictureBox = new PictureBox();
            _preprocess2PictureBox = new PictureBox();
            _preprocess3PictureBox = new PictureBox();
            _preprocess4PictureBox = new PictureBox();
            _preprocess1ThresholdLabel = new Label();
            _preprocess2ThresholdLabel = new Label();
            _preprocess3ThresholdLabel = new Label();
            _preprocess4ThresholdLabel = new Label();
            _preprocess1ThresholdBar = new TrackBar();
            _preprocess2ThresholdBar = new TrackBar();
            _preprocess3ThresholdBar = new TrackBar();
            _preprocess4ThresholdBar = new TrackBar();
            _preprocess1Erode = CreateMorphInput();
            _preprocess1Dilate = CreateMorphInput();
            _preprocess1Open = CreateMorphInput();
            _preprocess1Close = CreateMorphInput();
            _preprocess2Erode = CreateMorphInput();
            _preprocess2Dilate = CreateMorphInput();
            _preprocess2Open = CreateMorphInput();
            _preprocess2Close = CreateMorphInput();
            _preprocess3Erode = CreateMorphInput();
            _preprocess3Dilate = CreateMorphInput();
            _preprocess3Open = CreateMorphInput();
            _preprocess3Close = CreateMorphInput();
            _preprocess4Erode = CreateMorphInput();
            _preprocess4Dilate = CreateMorphInput();
            _preprocess4Open = CreateMorphInput();
            _preprocess4Close = CreateMorphInput();

            _roiXLabel = CreateWhiteLabel("ROI X");
            _roiYLabel = CreateWhiteLabel("ROI Y");
            _roiWLabel = CreateWhiteLabel("ROI W");
            _roiHLabel = CreateWhiteLabel("ROI H");
            _roiX = CreateNumeric(0, 100000, 0);
            _roiY = CreateNumeric(0, 100000, 0);
            _roiW = CreateNumeric(1, 100000, 100);
            _roiH = CreateNumeric(1, 100000, 100);
            _baseLabel = CreateWhiteLabel("Base");
            _modeLabel = CreateWhiteLabel("Mode");
            _scaleLabel = CreateWhiteLabel("um/px");
            _edgeThresholdLabel = CreateWhiteLabel("Edge T");
            _baseCombo = new ComboBox();
            _modeCombo = new ComboBox();
            _scaleInput = CreateDecimalNumeric(0.0001m, 100000m, 5.2m);
            _edgeThreshold = CreateNumeric(0, 255, 128);
            _edgeWhiteObject = new CheckBox();
            _edgeInvert = new CheckBox();
            _edgeLabel = new Label();

            ((System.ComponentModel.ISupportInitialize)_mainPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_preprocess1PictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_preprocess2PictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_preprocess3PictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_preprocess4PictureBox).BeginInit();
            SuspendLayout();

            ConfigureRootLayout();
            ConfigureTopBar();
            ConfigureMainPictureBox();
            ConfigureRightPanel();
            ConfigurePreprocessArea();
            ConfigureControlPanel();
            ConfigureResultText();
            ConfigureLogicArrays();

            ((System.ComponentModel.ISupportInitialize)_mainPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)_preprocess1PictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)_preprocess2PictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)_preprocess3PictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)_preprocess4PictureBox).EndInit();
            ResumeLayout(false);
        }

        private void ConfigureRootLayout()
        {
            _rootTable.Dock = DockStyle.Fill;
            _rootTable.ColumnCount = 2;
            _rootTable.RowCount = 2;
            _rootTable.BackColor = Color.FromArgb(30, 32, 36);
            _rootTable.Padding = new Padding(8);
            _rootTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58));
            _rootTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42));
            _rootTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
            _rootTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            Controls.Add(_rootTable);
        }

        private void ConfigureTopBar()
        {
            _topBar.Dock = DockStyle.Fill;
            _topBar.FlowDirection = FlowDirection.LeftToRight;
            _topBar.WrapContents = false;
            _topBar.BackColor = Color.FromArgb(42, 45, 50);
            _rootTable.SetColumnSpan(_topBar, 2);
            _rootTable.Controls.Add(_topBar, 0, 0);

            ConfigureButton(_loadButton, "Load Image", Color.FromArgb(64, 82, 110));
            ConfigureButton(_testImageButton, "Open Test Folder", Color.FromArgb(64, 82, 110));
            ConfigureButton(_measureButton, "Measure", Color.FromArgb(64, 82, 110));
            ConfigureButton(_edgeButton, "Find Upper Edge", Color.FromArgb(64, 82, 110));
            ConfigureButton(_closeButton, "Close", Color.FromArgb(150, 48, 48));
            _loadButton.Click += LoadButton_Click;
            _testImageButton.Click += TestImageButton_Click;
            _measureButton.Click += MeasureButton_Click;
            _edgeButton.Click += EdgeButton_Click;
            _closeButton.Click += CloseButton_Click;

            _topBar.Controls.Add(_loadButton);
            _topBar.Controls.Add(_testImageButton);
            _topBar.Controls.Add(_measureButton);
            _topBar.Controls.Add(_edgeButton);
            _topBar.Controls.Add(_closeButton);
        }

        private void ConfigureMainPictureBox()
        {
            _mainPictureBox.Dock = DockStyle.Fill;
            _mainPictureBox.BackColor = Color.Black;
            _mainPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            _mainPictureBox.BorderStyle = BorderStyle.FixedSingle;
            _mainPictureBox.MouseDown += MainPictureBox_MouseDown;
            _mainPictureBox.MouseMove += MainPictureBox_MouseMove;
            _mainPictureBox.MouseUp += MainPictureBox_MouseUp;
            _rootTable.Controls.Add(_mainPictureBox, 0, 1);
        }

        private void ConfigureRightPanel()
        {
            _rightPanel.Dock = DockStyle.Fill;
            _rightPanel.ColumnCount = 1;
            _rightPanel.RowCount = 3;
            _rightPanel.BackColor = Color.FromArgb(30, 32, 36);
            _rightPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 52));
            _rightPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 28));
            _rightPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            _rootTable.Controls.Add(_rightPanel, 1, 1);
        }

        private void ConfigurePreprocessArea()
        {
            _preprocessGrid.Dock = DockStyle.Fill;
            _preprocessGrid.ColumnCount = 2;
            _preprocessGrid.RowCount = 2;
            _preprocessGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            _preprocessGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            _preprocessGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            _preprocessGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            _rightPanel.Controls.Add(_preprocessGrid, 0, 0);

            ConfigurePreprocessPanel(_preprocess1Panel, _preprocess1Enabled, _preprocess1PictureBox, _preprocess1ThresholdLabel, _preprocess1ThresholdBar, "Preprocess 1  Gray > T", _preprocess1Erode, _preprocess1Dilate, _preprocess1Open, _preprocess1Close);
            ConfigurePreprocessPanel(_preprocess2Panel, _preprocess2Enabled, _preprocess2PictureBox, _preprocess2ThresholdLabel, _preprocess2ThresholdBar, "Preprocess 2  Gray > T", _preprocess2Erode, _preprocess2Dilate, _preprocess2Open, _preprocess2Close);
            ConfigurePreprocessPanel(_preprocess3Panel, _preprocess3Enabled, _preprocess3PictureBox, _preprocess3ThresholdLabel, _preprocess3ThresholdBar, "Preprocess 3  Gray < T", _preprocess3Erode, _preprocess3Dilate, _preprocess3Open, _preprocess3Close);
            ConfigurePreprocessPanel(_preprocess4Panel, _preprocess4Enabled, _preprocess4PictureBox, _preprocess4ThresholdLabel, _preprocess4ThresholdBar, "Preprocess 4  Gray < T", _preprocess4Erode, _preprocess4Dilate, _preprocess4Open, _preprocess4Close);

            _preprocessGrid.Controls.Add(_preprocess1Panel, 0, 0);
            _preprocessGrid.Controls.Add(_preprocess2Panel, 1, 0);
            _preprocessGrid.Controls.Add(_preprocess3Panel, 0, 1);
            _preprocessGrid.Controls.Add(_preprocess4Panel, 1, 1);
        }

        private void ConfigurePreprocessPanel(TableLayoutPanel panel, CheckBox enabledCheck, PictureBox pictureBox, Label thresholdLabel, TrackBar thresholdBar, string title, NumericUpDown erode, NumericUpDown dilate, NumericUpDown open, NumericUpDown close)
        {
            panel.Dock = DockStyle.Fill;
            panel.ColumnCount = 1;
            panel.RowCount = 3;
            panel.Padding = new Padding(5);
            panel.BackColor = Color.FromArgb(38, 41, 46);
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 72));

            enabledCheck.Text = title;
            enabledCheck.Checked = true;
            enabledCheck.Dock = DockStyle.Fill;
            enabledCheck.ForeColor = Color.White;
            enabledCheck.CheckedChanged += PreprocessValueChanged;
            panel.Controls.Add(enabledCheck, 0, 0);

            pictureBox.Dock = DockStyle.Fill;
            pictureBox.BackColor = Color.Black;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            panel.Controls.Add(pictureBox, 0, 1);

            var controls = new TableLayoutPanel();
            controls.Dock = DockStyle.Fill;
            controls.ColumnCount = 2;
            controls.RowCount = 2;
            controls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
            controls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55));
            controls.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            controls.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            panel.Controls.Add(controls, 0, 2);

            thresholdLabel.Text = "T=128";
            thresholdLabel.Dock = DockStyle.Fill;
            thresholdLabel.TextAlign = ContentAlignment.MiddleLeft;
            thresholdLabel.ForeColor = Color.White;
            controls.Controls.Add(thresholdLabel, 0, 0);

            thresholdBar.Minimum = 0;
            thresholdBar.Maximum = 255;
            thresholdBar.Value = 128;
            thresholdBar.TickFrequency = 32;
            thresholdBar.Dock = DockStyle.Fill;
            thresholdBar.Scroll += ThresholdBar_Scroll;
            controls.Controls.Add(thresholdBar, 1, 0);

            var morphPanel = new FlowLayoutPanel();
            morphPanel.Dock = DockStyle.Fill;
            morphPanel.WrapContents = false;
            controls.SetColumnSpan(morphPanel, 2);
            controls.Controls.Add(morphPanel, 0, 1);
            AddMorphControl(morphPanel, "E", erode);
            AddMorphControl(morphPanel, "D", dilate);
            AddMorphControl(morphPanel, "O", open);
            AddMorphControl(morphPanel, "C", close);
        }

        private void ConfigureControlPanel()
        {
            _controlPanel.Dock = DockStyle.Fill;
            _controlPanel.ColumnCount = 4;
            _controlPanel.RowCount = 7;
            _controlPanel.Padding = new Padding(6);
            for (var i = 0; i < 4; i++)
            {
                _controlPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            }

            for (var i = 0; i < 7; i++)
            {
                _controlPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
            }

            _rightPanel.Controls.Add(_controlPanel, 0, 1);

            AddLabeledControl(_controlPanel, _roiXLabel, _roiX, 0, 0);
            AddLabeledControl(_controlPanel, _roiYLabel, _roiY, 2, 0);
            AddLabeledControl(_controlPanel, _roiWLabel, _roiW, 0, 1);
            AddLabeledControl(_controlPanel, _roiHLabel, _roiH, 2, 1);
            _roiX.ValueChanged += RoiValueChanged;
            _roiY.ValueChanged += RoiValueChanged;
            _roiW.ValueChanged += RoiValueChanged;
            _roiH.ValueChanged += RoiValueChanged;

            _baseCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _baseCombo.Items.AddRange(new object[] { "Preprocess 1", "Preprocess 2", "Preprocess 3", "Preprocess 4" });
            _baseCombo.SelectedIndex = 0;
            _modeCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _modeCombo.Items.AddRange(new object[] { "Top-Bottom Distance", "Left-Right Distance" });
            _modeCombo.SelectedIndex = 0;
            AddLabeledControl(_controlPanel, _baseLabel, _baseCombo, 0, 2);
            AddLabeledControl(_controlPanel, _modeLabel, _modeCombo, 2, 2);
            AddLabeledControl(_controlPanel, _scaleLabel, _scaleInput, 0, 3);
            AddLabeledControl(_controlPanel, _edgeThresholdLabel, _edgeThreshold, 2, 3);

            _edgeWhiteObject.Text = "White object";
            _edgeWhiteObject.Checked = true;
            _edgeWhiteObject.ForeColor = Color.White;
            _edgeWhiteObject.Dock = DockStyle.Fill;
            _edgeInvert.Text = "Invert binary";
            _edgeInvert.ForeColor = Color.White;
            _edgeInvert.Dock = DockStyle.Fill;
            _controlPanel.Controls.Add(_edgeWhiteObject, 0, 4);
            _controlPanel.SetColumnSpan(_edgeWhiteObject, 2);
            _controlPanel.Controls.Add(_edgeInvert, 2, 4);
            _controlPanel.SetColumnSpan(_edgeInvert, 2);

            _edgeLabel.Dock = DockStyle.Fill;
            _edgeLabel.ForeColor = Color.White;
            _edgeLabel.Text = "Left Upper Edge Point = -    Right Upper Edge Point = -";
            _controlPanel.Controls.Add(_edgeLabel, 0, 5);
            _controlPanel.SetColumnSpan(_edgeLabel, 4);
        }

        private void ConfigureResultText()
        {
            _resultText.Dock = DockStyle.Fill;
            _resultText.Multiline = true;
            _resultText.ReadOnly = true;
            _resultText.BackColor = Color.FromArgb(18, 20, 24);
            _resultText.ForeColor = Color.White;
            _resultText.Font = new Font("Consolas", 11f);
            _rightPanel.Controls.Add(_resultText, 0, 2);
        }

        private void ConfigureLogicArrays()
        {
            _preprocessPictureBoxes = new[] { _preprocess1PictureBox, _preprocess2PictureBox, _preprocess3PictureBox, _preprocess4PictureBox };
            _enabledChecks = new[] { _preprocess1Enabled, _preprocess2Enabled, _preprocess3Enabled, _preprocess4Enabled };
            _thresholdBars = new[] { _preprocess1ThresholdBar, _preprocess2ThresholdBar, _preprocess3ThresholdBar, _preprocess4ThresholdBar };
            _thresholdLabels = new[] { _preprocess1ThresholdLabel, _preprocess2ThresholdLabel, _preprocess3ThresholdLabel, _preprocess4ThresholdLabel };
            _erodeInputs = new[] { _preprocess1Erode, _preprocess2Erode, _preprocess3Erode, _preprocess4Erode };
            _dilateInputs = new[] { _preprocess1Dilate, _preprocess2Dilate, _preprocess3Dilate, _preprocess4Dilate };
            _openInputs = new[] { _preprocess1Open, _preprocess2Open, _preprocess3Open, _preprocess4Open };
            _closeInputs = new[] { _preprocess1Close, _preprocess2Close, _preprocess3Close, _preprocess4Close };
        }

        private static void ConfigureButton(Button button, string text, Color backColor)
        {
            button.Text = text;
            button.Width = 150;
            button.Height = 40;
            button.Margin = new Padding(6);
            button.BackColor = backColor;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
        }

        private static Label CreateWhiteLabel(string text)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.White
            };
        }

        private static NumericUpDown CreateNumeric(int min, int max, int value)
        {
            return new NumericUpDown
            {
                Minimum = min,
                Maximum = max,
                Value = value,
                DecimalPlaces = 0,
                Dock = DockStyle.Fill
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
                Increment = 0.1m,
                Dock = DockStyle.Fill
            };
        }

        private NumericUpDown CreateMorphInput()
        {
            var input = CreateNumeric(0, 20, 0);
            input.Width = 48;
            input.Dock = DockStyle.None;
            input.ValueChanged += PreprocessValueChanged;
            return input;
        }

        private static void AddMorphControl(FlowLayoutPanel panel, string label, NumericUpDown input)
        {
            panel.Controls.Add(new Label
            {
                Text = label,
                Width = 16,
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            });
            panel.Controls.Add(input);
        }

        private static void AddLabeledControl(TableLayoutPanel panel, Label label, Control control, int column, int row)
        {
            panel.Controls.Add(label, column, row);
            panel.Controls.Add(control, column + 1, row);
            control.Dock = DockStyle.Fill;
        }
    }
}
