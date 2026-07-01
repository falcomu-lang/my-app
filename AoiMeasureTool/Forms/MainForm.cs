using System;
using System.Drawing;
using System.Collections.Generic;
using System.Globalization;
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
        private enum MultiImageLineDisplayMode
        {
            SourceLines = 0,
            FoundLines = 1,
            Hidden = 2
        }

        private CvMat _sourceImage;
        private CvMat _grayImage;
        private readonly CvMat[] _preprocessImages = new CvMat[4];
        private readonly string _settingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "setting.ini");
        private readonly IniSettingsRepository _settingsRepository = new IniSettingsRepository();
        private readonly InnerSettingsRepository _innerSettingsRepository = new InnerSettingsRepository();
        private readonly ParameterReferenceListRepository _parameterReferenceListRepository = new ParameterReferenceListRepository();
        private string _lastImagePath;
        private string _activeProductKey;
        private readonly System.Collections.Generic.Dictionary<string, PreprocessSnapshot[]> _productProfiles =
            new System.Collections.Generic.Dictionary<string, PreprocessSnapshot[]>(System.StringComparer.OrdinalIgnoreCase);
        private readonly System.Collections.Generic.Dictionary<string, ReferenceCornerSnapshot> _referenceCornerProfiles =
            new System.Collections.Generic.Dictionary<string, ReferenceCornerSnapshot>(System.StringComparer.OrdinalIgnoreCase);
        private readonly System.Collections.Generic.Dictionary<string, List<MeasureRecord>> _measureProfiles =
            new System.Collections.Generic.Dictionary<string, List<MeasureRecord>>(System.StringComparer.OrdinalIgnoreCase);
        private readonly System.Collections.Generic.Dictionary<string, List<JudgementCriterionRule>> _judgementCriteriaProfiles =
            new System.Collections.Generic.Dictionary<string, List<JudgementCriterionRule>>(System.StringComparer.OrdinalIgnoreCase);
        private readonly System.Collections.Generic.Dictionary<string, DualThresholdSnapshot> _dualThresholdProfiles =
            new System.Collections.Generic.Dictionary<string, DualThresholdSnapshot>(System.StringComparer.OrdinalIgnoreCase);
        private readonly ProductProfileService _productProfileService;
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
        private bool _referenceCornerEnabled;
        private int _referenceSourceIndex;
        private ReferenceCornerPointMode _referencePointMode = ReferenceCornerPointMode.ContourNearest;
        private bool _referenceRoiSaved;
        private bool _referenceCornerFound;
        private bool _isSelectingReferenceRoi;
        private Point _referenceRoiStart;
        private Rectangle _referenceRoiRectangle;
        private ReferenceCornerCandidate _referenceCornerCandidate;
        private bool _referencePreviewPanning;
        private float _referenceImageScale = 1f;
        private float _referenceFitScale = 1f;
        private Point _lastReferenceMousePosition;
        private ComboBox _comboBoxReferencePointMode;
        private Label _labelReferencePointMode;
        private float _activeImageScale = 1f;
        private float _activeFitScale = 1f;
        private bool _isDraggingActiveImage;
        private Point _lastActiveMousePosition;
        private bool _measurePreviewPanning;
        private float _measureImageScale = 1f;
        private float _measureFitScale = 1f;
        private Point _lastMeasureMousePosition;
        private bool _multiImageConfirmPanning;
        private float _multiImageConfirmImageScale = 1f;
        private float _multiImageConfirmFitScale = 1f;
        private float _multiImageConfirmOffsetX = 0f;
        private float _multiImageConfirmOffsetY = 0f;
        private Point _lastMultiImageConfirmMousePosition;
        private bool _showingOriginalInActivePreview;
        private bool _suppressPersistOnProductSwitch;
        private bool _isApplyingProductState;
        private bool _isApplyingReferenceCornerState;
        private InnerSettingsData _innerSettings = new InnerSettingsData();
        private float _savedActiveImageScale = 1f;
        private int _savedActiveImageLeft;
        private int _savedActiveImageTop;
        private float _imageScale = 1f;
        private float _fitScale = 1f;
        private bool _isDraggingImage;
        private Point _lastMousePosition;
        private TabPage _tabPageMeasureDistance;
        private TabPage _tabPageMultiImageConfirm;
        private TabPage _tabPageInnerSettings;
        private TabPage _tabPageJudgementCriteria;
        private TabPage _tabPageDetectionParameterSummary;
        private TabPage _tabPageContinuousInspection;
        private Panel _panelMeasurePreview;
        private Panel _panelMultiImageConfirmViewport;
        private PictureBox _pictureBoxMultiImageConfirm;
        private PictureBox _pictureBoxMeasurePreview;
        private PictureBox _pictureBoxDualThresholdOriginal;
        private PictureBox _pictureBoxDualThresholdPreview;
        private Panel _panelDualThresholdOriginalViewport;
        private Panel _panelDualThresholdPreviewViewport;
        private Button _buttonDualThresholdLoadOriginal;
        private Button _buttonDualThresholdLoadBinary;
        private NumericUpDown _numericDualThresholdLower;
        private NumericUpDown _numericDualThresholdUpper;
        private TrackBar _trackBarDualThresholdLower;
        private TrackBar _trackBarDualThresholdUpper;
        private CheckBox _checkBoxDualThresholdEnabled;
        private NumericUpDown _numericDualThresholdErode;
        private NumericUpDown _numericDualThresholdDilate;
        private NumericUpDown _numericDualThresholdOpen;
        private NumericUpDown _numericDualThresholdClose;
        private Button _buttonLoadMultiImageFolder;
        private Button _buttonMultiImagePrev;
        private Button _buttonMultiImageNext;
        private Button _buttonMultiImageLineSequence;
        private ComboBox _comboBoxMultiImageLineDisplayMode;
        private DataGridView _dataGridViewMultiImageInfo;
        private DataGridView _dataGridViewMultiImageJudgementResult;
        private DataGridView _dataGridViewMeasureRecords;
        private NumericUpDown _numericInnerCcdXPrecision;
        private NumericUpDown _numericInnerCcdYPrecision;
        private Label _labelInnerCcdXPrecision;
        private Label _labelInnerCcdYPrecision;
        private Button _buttonSaveMeasurePoint;
        private Button _buttonClearMeasurePoint;
        private Button _buttonSaveMeasureRecords;
        private Button _buttonSaveInnerSettings;
        private TextBox _textJudgementName;
        private TextBox _textJudgementCalculation;
        private TextBox _textJudgementSpec;
        private TextBox _textJudgementCalculationB;
        private TextBox _textJudgementSpecB;
        private Button _buttonJudgementAdd;
        private Button _buttonJudgementReset;
        private Button _buttonJudgementSave;
        private Button _buttonJudgementMoveUp;
        private Button _buttonJudgementMoveDown;
        private DataGridView _dataGridViewJudgementCriteria;
        private TextBox _textBoxDetectionMainParameterName;
        private Button _buttonDetectionMainParameterConfirm;
        private Button _buttonDetectionMainParameterMoveUp;
        private Button _buttonDetectionMainParameterMoveDown;
        private Button _buttonDetectionMainParameterSaveOrder;
        private ListBox _listBoxDetectionMainParameter;
        private Button _buttonDetectionSubParameter1MoveUp;
        private Button _buttonDetectionSubParameter1MoveDown;
        private Button _buttonDetectionSubParameter1SaveOrder;
        private ListBox _listBoxDetectionSubParameter1;
        private CheckBox _checkBoxDetectionSubParameter1Enabled;
        private Button _buttonDetectionSubParameter2MoveUp;
        private Button _buttonDetectionSubParameter2MoveDown;
        private Button _buttonDetectionSubParameter2SaveOrder;
        private ListBox _listBoxDetectionSubParameter2;
        private CheckBox _checkBoxDetectionSubParameter2Enabled;
        private Button _buttonDetectionSubParameter3MoveUp;
        private Button _buttonDetectionSubParameter3MoveDown;
        private Button _buttonDetectionSubParameter3SaveOrder;
        private ListBox _listBoxDetectionSubParameter3;
        private CheckBox _checkBoxDetectionSubParameter3Enabled;
        private Button _buttonDetectionSaveParameterReference;
        private ComboBox _comboBoxContinuousInspectionMainParameter;
        private readonly Label[] _continuousInspectionSubParameterLabels = new Label[3];
        private readonly PictureBox[] _continuousInspectionPictureBoxes = new PictureBox[3];
        private readonly Label[] _continuousInspectionResultLabels = new Label[3];
        private readonly string[] _continuousInspectionImagePaths = new string[3];
        private ContextMenuStrip _judgementCriteriaMenu;
        private ToolStripMenuItem _judgementCriteriaEditMenuItem;
        private ToolStripMenuItem _judgementCriteriaDeleteMenuItem;
        private int _judgementCriteriaEditingIndex = -1;
        private Button _buttonParallelMeasure;
        private Button _buttonPerpendicularMeasure;
        private ComboBox _comboBoxMeasureSource;
        private Label _labelMeasureStatus;
        private ContextMenuStrip _measureRecordMenu;
        private ToolStripMenuItem _measureEditMenuItem;
        private ToolStripMenuItem _measureDeleteMenuItem;
        private System.Windows.Forms.Timer _measureBlinkTimer;
        private MeasureRecord _measureBlinkRecord;
        private int _measureBlinkRemainingTicks;
        private System.Windows.Forms.Timer _multiImageLineSequenceTimer;
        private bool _multiImageLineSequenceVisible;
        private int _multiImageLineSequenceRemainingTicks;
        private MultiImageLineDisplayMode _multiImageLineDisplayMode = MultiImageLineDisplayMode.FoundLines;
        private readonly Dictionary<string, List<MultiImageLineMeasurementResult>> _multiImageLineMeasurementCache =
            new Dictionary<string, List<MultiImageLineMeasurementResult>>(StringComparer.OrdinalIgnoreCase);
        private bool _isMeasureSelecting;
        private readonly List<Point> _measurePoints = new List<Point>(2);
        private readonly List<MeasureRecord> _measureRecords = new List<MeasureRecord>();
        private List<JudgementCriterionRule> _judgementCriteriaRules = new List<JudgementCriterionRule>();
        private readonly List<string> _detectionMainParameters = new List<string>();
        private readonly List<string> _detectionSubParameter1Items = new List<string>();
        private readonly Dictionary<string, DetectionParameterReference> _detectionParameterReferences =
            new Dictionary<string, DetectionParameterReference>(StringComparer.OrdinalIgnoreCase);
        private MeasureRecord _editingMeasureRecord;
        private DataGridViewRow _editingMeasureRow;
        private bool _isEditingMeasureRecord;
        private bool _measureSourceAvailable;
        private MeasureDirectionMode _measureDirectionMode = MeasureDirectionMode.None;
        private readonly List<string> _multiImageConfirmImagePaths = new List<string>();
        private int _multiImageConfirmImageIndex = -1;
        private Bitmap _multiImageConfirmBitmap;
        private Size _multiImageConfirmSourceImageSize = Size.Empty;
        private bool _multiImageConfirmShowingPreprocess;
        private string _multiImageConfirmProductKey;
        private bool _synchronizingDualThreshold;
        private bool _dualThresholdOriginalDragging;
        private bool _dualThresholdPreviewDragging;
        private Point _dualThresholdOriginalLastMousePosition;
        private Point _dualThresholdPreviewLastMousePosition;
        private float _dualThresholdOriginalImageScale = 1f;
        private float _dualThresholdOriginalFitScale = 1f;
        private float _dualThresholdPreviewImageScale = 1f;
        private float _dualThresholdPreviewFitScale = 1f;
        private float _dualThresholdSavedPreviewImageScale = 1f;
        private int _dualThresholdSavedPreviewLeft;
        private int _dualThresholdSavedPreviewTop;

        public MainForm()
        {
            _productProfileService = new ProductProfileService(_productProfiles, _referenceCornerProfiles, _measureProfiles, _judgementCriteriaProfiles, _dualThresholdProfiles);
            InitializeComponent();
            _tabPageContinuousInspection = tabPageContinuousInspection;
            ShowMainWorkspaceTabs();
            InitializePreprocessControls();
            InitializeDualThresholdControls();
            InitializeReferenceCornerControls();
            InitializeMeasureDistanceControls();
            InitializeInnerSettingsControls();
            InitializeJudgementCriteriaControls();
            InitializeDetectionParameterSummaryControls();
            InitializeContinuousInspectionControls();
            EnableDoubleBuffering();
            LoadSavedAppSettings();
            LoadLastImageIfAvailable();
        }

        private void EnableDoubleBuffering()
        {
            SetControlDoubleBuffered(_panelMultiImageConfirmViewport, true);
            SetControlDoubleBuffered(panelMultiImageInfo, true);
            SetControlDoubleBuffered(tabControlMultiImageInfo, true);
            SetControlDoubleBuffered(_tabPageMultiImageConfirm, true);
        }

        private static void SetControlDoubleBuffered(Control control, bool enabled)
        {
            if (control == null)
            {
                return;
            }

            var property = typeof(Control).GetProperty(
                "DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            property?.SetValue(control, enabled, null);
        }

        private void SidebarImageViewerButton_Click(object sender, EventArgs e)
        {
            ShowMainWorkspaceTabs();
            tabControlMain.SelectedTab = tabPageImageViewer;
        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            ShowMainWorkspaceTabs();

            if (_sourceImage != null && !_sourceImage.Empty())
            {
                PersistActiveProductProfile();
            }

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
                    UpdateDualThresholdOriginalImage();
                    FitImageToViewport();

                    _sourceImage?.Dispose();
                    _sourceImage = loadedImage;
                    loadedImage = null;

                    _grayImage?.Dispose();
                    _grayImage = new CvMat();
                    Cv2.CvtColor(_sourceImage, _grayImage, ColorConversionCodes.BGR2GRAY);
                    UpdateAllPreprocessImages();

                    var productKey = GetProductKeyFromImagePath(openFileDialogImage.FileName);
                    SwitchActiveProduct(productKey);
                    RefreshMeasureDistancePreview();

                    labelImageInfo.Text = string.Format(
                        "{0}    {1} x {2} px",
                        Path.GetFileName(openFileDialogImage.FileName),
                        _sourceImage.Width,
                        _sourceImage.Height);

                    _lastImagePath = openFileDialogImage.FileName;
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

        private void ReferenceCornerButton_Click(object sender, EventArgs e)
        {
            ShowReferenceCornerWorkspace();
        }

        private void MeasureDistanceButton_Click(object sender, EventArgs e)
        {
            ShowMeasureDistanceWorkspace();
        }

        private void MultiImageConfirmButton_Click(object sender, EventArgs e)
        {
            ShowMultiImageConfirmWorkspace();
        }

        private void InnerSettingsButton_Click(object sender, EventArgs e)
        {
            ShowInnerSettingsWorkspace();
        }

        private void JudgementCriteriaButton_Click(object sender, EventArgs e)
        {
            ShowJudgementCriteriaWorkspace();
        }

        private void DetectionParameterSummaryButton_Click(object sender, EventArgs e)
        {
            ShowDetectionParameterSummaryWorkspace();
        }

        private void ContinuousInspectionButton_Click(object sender, EventArgs e)
        {
            ShowContinuousInspectionWorkspace();
        }

        private void ShowMainWorkspaceTabs()
        {
            if (tabControlMain == null)
            {
                return;
            }

            tabControlMain.TabPages.Clear();
            tabControlMain.TabPages.Add(tabPageImageViewer);
            tabControlMain.TabPages.Add(tabPageBinarization);
            tabControlMain.TabPages.Add(tabPageBinarization2);

            tabControlMain.SelectedTab = tabPageImageViewer;
        }

        private void ShowInnerSettingsWorkspace()
        {
            if (tabControlMain == null)
            {
                return;
            }

            tabControlMain.TabPages.Clear();
            tabControlMain.TabPages.Add(_tabPageInnerSettings);
            tabControlMain.SelectedTab = _tabPageInnerSettings;
        }

        private void ShowJudgementCriteriaWorkspace()
        {
            if (tabControlMain == null)
            {
                return;
            }

            tabControlMain.TabPages.Clear();
            tabControlMain.TabPages.Add(_tabPageJudgementCriteria);
            tabControlMain.SelectedTab = _tabPageJudgementCriteria;
            RefreshJudgementCriteriaView();
        }

        private void ShowDetectionParameterSummaryWorkspace()
        {
            if (tabControlMain == null)
            {
                return;
            }

            tabControlMain.TabPages.Clear();
            tabControlMain.TabPages.Add(_tabPageDetectionParameterSummary);
            tabControlMain.SelectedTab = _tabPageDetectionParameterSummary;
        }

        private void ShowContinuousInspectionWorkspace()
        {
            if (tabControlMain == null || _tabPageContinuousInspection == null)
            {
                return;
            }

            tabControlMain.TabPages.Clear();
            tabControlMain.TabPages.Add(_tabPageContinuousInspection);
            tabControlMain.SelectedTab = _tabPageContinuousInspection;
            RefreshContinuousInspectionMainParameterItems();
        }

        private void InitializeContinuousInspectionControls()
        {
            if (panelContinuousInspection == null)
            {
                return;
            }

            labelContinuousInspectionTitle.Visible = false;
            labelContinuousInspectionMainParameter.Text = "主參數";
            labelContinuousInspectionMainParameter.Location = new Point(30, 26);
            comboBoxContinuousInspectionMainParameter.Location = new Point(98, 22);
            groupBoxContinuousInspection1.Text = "子參數 1";
            groupBoxContinuousInspection2.Text = "子參數 2";
            groupBoxContinuousInspection3.Text = "子參數 3";
            groupBoxContinuousInspection1.Location = new Point(24, 60);
            groupBoxContinuousInspection2.Location = new Point(344, 60);
            groupBoxContinuousInspection3.Location = new Point(664, 60);
            buttonContinuousInspectionLoadImage1.Text = "讀取圖";
            buttonContinuousInspectionLoadImage2.Text = "讀取圖";
            buttonContinuousInspectionLoadImage3.Text = "讀取圖";
            _comboBoxContinuousInspectionMainParameter = comboBoxContinuousInspectionMainParameter;
            _comboBoxContinuousInspectionMainParameter.SelectedIndexChanged += ContinuousInspectionMainParameterComboBox_SelectedIndexChanged;
            _continuousInspectionSubParameterLabels[0] = labelContinuousInspectionSubParameter1;
            _continuousInspectionSubParameterLabels[1] = labelContinuousInspectionSubParameter2;
            _continuousInspectionSubParameterLabels[2] = labelContinuousInspectionSubParameter3;
            _continuousInspectionPictureBoxes[0] = pictureBoxContinuousInspection1;
            _continuousInspectionPictureBoxes[1] = pictureBoxContinuousInspection2;
            _continuousInspectionPictureBoxes[2] = pictureBoxContinuousInspection3;

            buttonContinuousInspectionLoadImage1.Tag = 0;
            buttonContinuousInspectionLoadImage2.Tag = 1;
            buttonContinuousInspectionLoadImage3.Tag = 2;
            buttonContinuousInspectionLoadImage1.Location = new Point(16, 394);
            buttonContinuousInspectionLoadImage2.Location = new Point(16, 394);
            buttonContinuousInspectionLoadImage3.Location = new Point(16, 394);
            buttonContinuousInspectionLoadImage1.Click += ContinuousInspectionLoadImageButton_Click;
            buttonContinuousInspectionLoadImage2.Click += ContinuousInspectionLoadImageButton_Click;
            buttonContinuousInspectionLoadImage3.Click += ContinuousInspectionLoadImageButton_Click;

            InitializeContinuousInspectionResultArea(groupBoxContinuousInspection1, 0);
            InitializeContinuousInspectionResultArea(groupBoxContinuousInspection2, 1);
            InitializeContinuousInspectionResultArea(groupBoxContinuousInspection3, 2);
        }

        private void InitializeContinuousInspectionResultArea(GroupBox groupBox, int index)
        {
            if (groupBox == null)
            {
                return;
            }

            var resultLabel = groupBox.Controls["labelContinuousInspectionResult" + index] as Label;
            if (resultLabel == null)
            {
                resultLabel = new Label
                {
                    Name = "labelContinuousInspectionResult" + index,
                    BorderStyle = BorderStyle.FixedSingle,
                    Font = new Font("Microsoft JhengHei UI", 12F),
                    Location = new Point(16, 340),
                    Size = new Size(258, 40),
                    Text = string.Empty,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                groupBox.Controls.Add(resultLabel);
                resultLabel.BringToFront();
            }

            var judgeButton = groupBox.Controls["buttonContinuousInspectionJudge" + index] as Button;
            if (judgeButton == null)
            {
                judgeButton = new Button
                {
                    Name = "buttonContinuousInspectionJudge" + index,
                    BackColor = Color.FromArgb(224, 228, 231),
                    FlatStyle = FlatStyle.Flat,
                    Location = new Point(154, 394),
                    Size = new Size(120, 40),
                    Text = "判斷",
                    Tag = index
                };
                judgeButton.FlatAppearance.BorderSize = 0;
                judgeButton.Click += ContinuousInspectionJudgeButton_Click;
                groupBox.Controls.Add(judgeButton);
                judgeButton.BringToFront();
            }

            _continuousInspectionResultLabels[index] = resultLabel;
        }

        private void RefreshContinuousInspectionMainParameterItems()
        {
            if (_comboBoxContinuousInspectionMainParameter == null)
            {
                return;
            }

            var selectedName = _comboBoxContinuousInspectionMainParameter.SelectedItem as string;
            _comboBoxContinuousInspectionMainParameter.BeginUpdate();
            try
            {
                _comboBoxContinuousInspectionMainParameter.Items.Clear();
                foreach (var parameterName in _detectionMainParameters)
                {
                    _comboBoxContinuousInspectionMainParameter.Items.Add(parameterName);
                }
            }
            finally
            {
                _comboBoxContinuousInspectionMainParameter.EndUpdate();
            }

            if (!string.IsNullOrWhiteSpace(selectedName))
            {
                var index = _comboBoxContinuousInspectionMainParameter.FindStringExact(selectedName);
                if (index >= 0)
                {
                    _comboBoxContinuousInspectionMainParameter.SelectedIndex = index;
                    return;
                }
            }

            if (_comboBoxContinuousInspectionMainParameter.Items.Count > 0 && _comboBoxContinuousInspectionMainParameter.SelectedIndex < 0)
            {
                _comboBoxContinuousInspectionMainParameter.SelectedIndex = 0;
                return;
            }

            ApplyContinuousInspectionSubParameters();
        }

        private void ContinuousInspectionMainParameterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyContinuousInspectionSubParameters();
        }

        private void ApplyContinuousInspectionSubParameters()
        {
            var selectedName = _comboBoxContinuousInspectionMainParameter?.SelectedItem as string;
            DetectionParameterReference parameterReference = null;
            if (!string.IsNullOrWhiteSpace(selectedName))
            {
                _detectionParameterReferences.TryGetValue(selectedName, out parameterReference);
            }

            var values = new[]
            {
                parameterReference?.SubParameter1,
                parameterReference?.SubParameter2,
                parameterReference?.SubParameter3
            };

            for (var i = 0; i < _continuousInspectionSubParameterLabels.Length; i++)
            {
                if (_continuousInspectionSubParameterLabels[i] != null)
                {
                    _continuousInspectionSubParameterLabels[i].Text = string.IsNullOrWhiteSpace(values[i]) ? "未設定子參數" : values[i];
                }
            }
        }

        private void ContinuousInspectionLoadImageButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var index = button?.Tag as int? ?? -1;
            if (index < 0 || index >= _continuousInspectionPictureBoxes.Length)
            {
                return;
            }

            if (openFileDialogImage.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                using (var image = Image.FromFile(openFileDialogImage.FileName))
                {
                    SetPictureBoxImage(_continuousInspectionPictureBoxes[index], new Bitmap(image));
                }

                _continuousInspectionImagePaths[index] = openFileDialogImage.FileName;
                if (_continuousInspectionResultLabels[index] != null)
                {
                    _continuousInspectionResultLabels[index].Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Unable to load image.\r\n\r\n" + ex.Message, "Continuous Inspection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ContinuousInspectionJudgeButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var index = button?.Tag as int? ?? -1;
            if (index < 0 || index >= _continuousInspectionSubParameterLabels.Length || _continuousInspectionResultLabels[index] == null)
            {
                return;
            }

            var subParameterName = _continuousInspectionSubParameterLabels[index].Text.Trim();
            if (string.IsNullOrWhiteSpace(subParameterName) || string.Equals(subParameterName, "未設定子參數", StringComparison.Ordinal))
            {
                _continuousInspectionResultLabels[index].Text = "未設定條件";
                return;
            }

            var imagePath = _continuousInspectionImagePaths[index];
            if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
            {
                _continuousInspectionResultLabels[index].Text = "未載入圖片";
                return;
            }

            try
            {
                var rows = BuildContinuousInspectionJudgementResults(imagePath, subParameterName);
                _continuousInspectionResultLabels[index].Text = SummarizeContinuousInspectionJudgement(rows);
            }
            catch (Exception ex)
            {
                _continuousInspectionResultLabels[index].Text = "不可判斷";
                MessageBox.Show(this, "無法執行判斷。\r\n\r\n" + ex.Message, "連續檢測", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<MultiImageJudgementResultRow> BuildContinuousInspectionJudgementResults(string imagePath, string productKey)
        {
            var originalBitmap = _multiImageConfirmBitmap;
            var originalSourceImageSize = _multiImageConfirmSourceImageSize;
            var originalProductKey = _multiImageConfirmProductKey;
            var originalImageIndex = _multiImageConfirmImageIndex;
            var originalImagePaths = new List<string>(_multiImageConfirmImagePaths);
            var originalJudgementCriteriaRules = CloneJudgementCriteriaRules(_judgementCriteriaRules);

            try
            {
                productKey = string.IsNullOrWhiteSpace(productKey) ? "DEFAULT" : productKey;
                var profileState = _productProfileService.GetOrCreateState(productKey);
                _judgementCriteriaRules = CloneJudgementCriteriaRules(profileState.JudgementCriteriaRules);
                _multiImageConfirmBitmap = new Bitmap(imagePath);
                _multiImageConfirmSourceImageSize = _multiImageConfirmBitmap.Size;
                _multiImageConfirmProductKey = profileState.ProductKey;
                _multiImageConfirmImagePaths.Clear();
                _multiImageConfirmImagePaths.Add(imagePath);
                _multiImageConfirmImageIndex = 0;
                return BuildMultiImageJudgementResults();
            }
            finally
            {
                _multiImageConfirmBitmap?.Dispose();
                _multiImageConfirmBitmap = originalBitmap;
                _multiImageConfirmSourceImageSize = originalSourceImageSize;
                _multiImageConfirmProductKey = originalProductKey;
                _judgementCriteriaRules = originalJudgementCriteriaRules;
                _multiImageConfirmImagePaths.Clear();
                _multiImageConfirmImagePaths.AddRange(originalImagePaths);
                _multiImageConfirmImageIndex = originalImageIndex;
            }
        }

        private static string SummarizeContinuousInspectionJudgement(List<MultiImageJudgementResultRow> rows)
        {
            if (rows == null || rows.Count == 0)
            {
                return "未設定條件";
            }

            var hasA = false;
            var hasB = false;
            foreach (var row in rows)
            {
                if (row == null || string.IsNullOrWhiteSpace(row.JudgementText))
                {
                    continue;
                }

                if (string.Equals(row.JudgementText, "C規", StringComparison.OrdinalIgnoreCase))
                {
                    return "NG";
                }

                if (string.Equals(row.JudgementText, "B規", StringComparison.OrdinalIgnoreCase))
                {
                    hasB = true;
                    continue;
                }

                if (string.Equals(row.JudgementText, "A規", StringComparison.OrdinalIgnoreCase))
                {
                    hasA = true;
                    continue;
                }
            }

            if (hasB)
            {
                return "BOK";
            }

            if (hasA)
            {
                return "A";
            }

            return "不可判斷";
        }

        private void ApplySnapshots(PreprocessSnapshot[] snapshots)
        {
            PreprocessProfileApplier.ApplySnapshots(
                snapshots,
                _preprocessEnabledChecks,
                _thresholdTrackBars,
                _thresholdInputs,
                _erodeInputs,
                _dilateInputs,
                _openInputs,
                _closeInputs);

            if (snapshots != null)
            {
                for (var i = 0; i < 4; i++)
                {
                    if (snapshots[i] != null)
                    {
                        SetPreprocessControlsEnabled(i, snapshots[i].Enabled);
                    }
                }
            }

            UpdateAllPreprocessImages();
        }

        private static string GetProductKeyFromImagePath(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
            {
                return "DEFAULT";
            }

            var fileName = Path.GetFileNameWithoutExtension(imagePath);
            return string.IsNullOrWhiteSpace(fileName) ? "DEFAULT" : fileName.Trim();
        }

        private PreprocessSnapshot[] CaptureCurrentSnapshots()
        {
            return PreprocessProfileApplier.CaptureSnapshots(
                _preprocessEnabledChecks,
                _thresholdInputs,
                _erodeInputs,
                _dilateInputs,
                _openInputs,
                _closeInputs);
        }

        private PreprocessSnapshot[] GetPreprocessSnapshotsForProduct(string productKey)
        {
            productKey = string.IsNullOrWhiteSpace(productKey) ? "DEFAULT" : productKey;

            if (string.Equals(GetCurrentProductKeyOrDefault(), productKey, System.StringComparison.OrdinalIgnoreCase))
            {
                return CaptureCurrentSnapshots();
            }

            return _productProfileService.GetOrCreateState(productKey).PreprocessSnapshots;
        }

        private ReferenceCornerSnapshot GetReferenceCornerSnapshotForProduct(string productKey)
        {
            productKey = string.IsNullOrWhiteSpace(productKey) ? "DEFAULT" : productKey;

            if (string.Equals(GetCurrentProductKeyOrDefault(), productKey, System.StringComparison.OrdinalIgnoreCase))
            {
                return CaptureCurrentReferenceCornerSnapshot();
            }

            return _productProfileService.GetOrCreateState(productKey).ReferenceCornerSnapshot;
        }

        private DualThresholdSnapshot GetDualThresholdSnapshotForProduct(string productKey)
        {
            productKey = string.IsNullOrWhiteSpace(productKey) ? "DEFAULT" : productKey;

            if (string.Equals(GetCurrentProductKeyOrDefault(), productKey, System.StringComparison.OrdinalIgnoreCase))
            {
                return CaptureDualThresholdSnapshot();
            }

            return _productProfileService.GetOrCreateState(productKey).DualThresholdSnapshot;
        }

        private void PersistActiveProductProfile()
        {
            _productProfileService.PersistProduct(
                GetCurrentProductKeyOrDefault(),
                CaptureCurrentSnapshots(),
                CaptureCurrentReferenceCornerSnapshot(),
                _measureRecords,
                CloneJudgementCriteriaRules(_judgementCriteriaRules),
                CaptureDualThresholdSnapshot());
        }

        private string GetCurrentProductKeyOrDefault()
        {
            if (!string.IsNullOrWhiteSpace(_activeProductKey))
            {
                return _activeProductKey;
            }

            if (!string.IsNullOrWhiteSpace(_lastImagePath))
            {
                return GetProductKeyFromImagePath(_lastImagePath);
            }

            return "DEFAULT";
        }

        private void SwitchActiveProduct(string productKey)
        {
            productKey = string.IsNullOrWhiteSpace(productKey) ? "DEFAULT" : productKey;

            if (!string.IsNullOrWhiteSpace(_activeProductKey) &&
                string.Equals(_activeProductKey, productKey, System.StringComparison.OrdinalIgnoreCase))
            {
                ApplyProductState(_productProfileService.GetOrCreateState(productKey));
                UpdateReferenceCornerPreview();
                return;
            }

            if (!_suppressPersistOnProductSwitch)
            {
                PersistActiveProductProfile();
            }
            _isApplyingProductState = true;
            try
            {
                _activeProductKey = productKey;
                ApplyProductState(_productProfileService.GetOrCreateState(productKey));
                UpdateReferenceCornerPreview();
            }
            finally
            {
                _isApplyingProductState = false;
            }
        }

        private void ApplyProductState(ProductProfileState state)
        {
            if (state == null)
            {
                return;
            }

            ApplyProductProfile(state.PreprocessSnapshots);
            ApplyDualThresholdSnapshot(state.DualThresholdSnapshot);
            ApplyReferenceCornerProfile(state.ReferenceCornerSnapshot);
            ApplyMeasureProfile(state.MeasureRecords);
            ApplyJudgementCriteriaProfile(state.JudgementCriteriaRules);
        }

        private void ApplyProductProfile(PreprocessSnapshot[] snapshots)
        {
            ApplySnapshots(ProfileDataCloner.CloneSnapshots(snapshots));
        }

        private void ApplyReferenceCornerProfile(ReferenceCornerSnapshot snapshot)
        {
            ApplyReferenceCornerSnapshot(snapshot);
        }

        private void ApplyMeasureProfile(List<MeasureRecord> records)
        {
            ApplyMeasureRecords(CloneMeasureRecords(records));
        }

        private void ApplyJudgementCriteriaProfile(List<JudgementCriterionRule> rules)
        {
            _judgementCriteriaRules = CloneJudgementCriteriaRules(rules);
            RefreshJudgementCriteriaView();
        }

        private void LoadSavedAppSettings()
        {
            try
            {
                var loadedData = _settingsRepository.Load(_settingsPath);
                _productProfileService.ReplaceAll(loadedData);
                _innerSettings = _innerSettingsRepository.Load(GetInnerSettingsPath());

                _lastImagePath = loadedData.LastImagePath;
                _activeProductKey = string.IsNullOrWhiteSpace(loadedData.ActiveProductKey) ? null : loadedData.ActiveProductKey;
                _detectionSubParameter1Items.Clear();
                _detectionSubParameter1Items.AddRange(loadedData.ListSortItems);

                if (!_referenceCornerProfiles.ContainsKey("DEFAULT"))
                {
                    _referenceCornerProfiles["DEFAULT"] = new ReferenceCornerSnapshot
                    {
                        Enabled = false,
                        SourceIndex = 0,
                        Roi = Rectangle.Empty,
                        RoiSaved = false
                    };
                }

                if (!_measureProfiles.ContainsKey("DEFAULT"))
                {
                    _measureProfiles["DEFAULT"] = new List<MeasureRecord>();
                }

                if (string.IsNullOrWhiteSpace(_activeProductKey))
                {
                    _activeProductKey = !string.IsNullOrWhiteSpace(_lastImagePath)
                        ? GetProductKeyFromImagePath(_lastImagePath)
                        : "DEFAULT";
                }

                ApplyProductState(_productProfileService.GetOrCreateState(GetCurrentProductKeyOrDefault()));
                ApplyInnerSettings(_innerSettings);
                LoadDetectionSubParameter1List();
            }
            catch
            {
                // Ignore malformed settings and keep defaults.
            }
        }

        private void SaveCurrentAppSettings()
        {
            try
            {
                PersistActiveProductProfile();
                var currentProductKey = GetCurrentProductKeyOrDefault();
                var settingsData = new AppSettingsData
                {
                    LastImagePath = _lastImagePath,
                    ActiveProductKey = _activeProductKey
                };
                settingsData.ListSortItems.AddRange(_detectionSubParameter1Items);
                if (!string.IsNullOrWhiteSpace(currentProductKey) &&
                    !settingsData.ListSortItems.Exists(item => string.Equals(item, currentProductKey, StringComparison.OrdinalIgnoreCase)))
                {
                    settingsData.ListSortItems.Add(currentProductKey);
                }

                settingsData.DualThresholdSettings = CaptureDualThresholdSnapshot();
                _productProfileService.ExportTo(settingsData);
                _settingsRepository.Save(_settingsPath, settingsData, currentProductKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Unable to save settings.\r\n\r\n" + ex.Message, "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetInnerSettingsPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "innerSetting.ini");
        }

        private void ApplyInnerSettings(InnerSettingsData data)
        {
            if (data == null)
            {
                data = new InnerSettingsData();
            }

            _innerSettings = data;

            if (_numericInnerCcdXPrecision != null)
            {
                _numericInnerCcdXPrecision.Value = ClampNumericUpDown(_numericInnerCcdXPrecision, (decimal)data.CcdXPrecision);
            }

            if (_numericInnerCcdYPrecision != null)
            {
                _numericInnerCcdYPrecision.Value = ClampNumericUpDown(_numericInnerCcdYPrecision, (decimal)data.CcdYPrecision);
            }
        }

        private static List<JudgementCriterionRule> CloneJudgementCriteriaRules(List<JudgementCriterionRule> rules)
        {
            return ProfileDataCloner.CloneJudgementCriteria(rules);
        }

        private void SaveInnerSettings()
        {
            if (_numericInnerCcdXPrecision == null || _numericInnerCcdYPrecision == null)
            {
                return;
            }

            _innerSettings.CcdXPrecision = (double)_numericInnerCcdXPrecision.Value;
            _innerSettings.CcdYPrecision = (double)_numericInnerCcdYPrecision.Value;
            _innerSettingsRepository.Save(GetInnerSettingsPath(), _innerSettings);
        }

        private static decimal ClampNumericUpDown(NumericUpDown control, decimal value)
        {
            if (value < control.Minimum)
            {
                return control.Minimum;
            }

            if (value > control.Maximum)
            {
                return control.Maximum;
            }

            return value;
        }

        private void LoadLastImageIfAvailable()
        {
            if (string.IsNullOrWhiteSpace(_lastImagePath) || !File.Exists(_lastImagePath))
            {
                return;
            }

            try
            {
                var image = Cv2.ImRead(_lastImagePath, ImreadModes.Color);
                if (image.Empty())
                {
                    return;
                }

                var bitmap = BitmapConverter.ToBitmap(image);
                var oldImage = pictureBoxImage.Image;
                pictureBoxImage.Image = bitmap;
                oldImage?.Dispose();
                FitImageToViewport();

                _sourceImage?.Dispose();
                _sourceImage = image;
                image = null;

                _grayImage?.Dispose();
                _grayImage = new CvMat();
                Cv2.CvtColor(_sourceImage, _grayImage, ColorConversionCodes.BGR2GRAY);
                UpdateAllPreprocessImages();

                _suppressPersistOnProductSwitch = true;
                try
                {
                    SwitchActiveProduct(GetProductKeyFromImagePath(_lastImagePath));
                }
                finally
                {
                    _suppressPersistOnProductSwitch = false;
                }
                ApplyProductState(_productProfileService.GetOrCreateState(GetCurrentProductKeyOrDefault()));
                RefreshMeasureDistancePreview();

                SetPictureBoxImage(pictureBoxBinaryOriginal, new Bitmap(bitmap));
                UpdateDualThresholdOriginalImage();
                labelImageInfo.Text = string.Format(
                    "{0}    {1} x {2} px",
                    Path.GetFileName(_lastImagePath),
                    _sourceImage.Width,
                    _sourceImage.Height);
            }
            catch
            {
                // Ignore auto-load errors.
            }
        }

        private void ButtonLoadSavedSettings_Click(object sender, EventArgs e)
        {
            LoadSavedAppSettings();
        }

        private void ButtonSaveCurrentSettings_Click(object sender, EventArgs e)
        {
            SaveCurrentAppSettings();
            MessageBox.Show(this, "Settings saved.", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void SetPictureBoxImage(PictureBox pictureBox, Bitmap image)
        {
            var oldImage = pictureBox.Image;
            pictureBox.Image = image;
            oldImage?.Dispose();
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
            PersistActiveProductProfile();
            SaveCurrentAppSettings();

            pictureBoxImage.Image?.Dispose();
            pictureBoxImage.Image = null;
            SetPictureBoxImage(pictureBoxBinaryOriginal, null);
            SetPictureBoxImage(pictureBoxActivePreprocess, null);
            SetPictureBoxImage(_pictureBoxDualThresholdOriginal, null);
            SetPictureBoxImage(_pictureBoxDualThresholdPreview, null);
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

        private void ApplyDualThresholdSnapshot(DualThresholdSnapshot snapshot)
        {
            if (snapshot == null)
            {
                snapshot = new DualThresholdSnapshot
                {
                    Enabled = true,
                    LowerThreshold = 80,
                    UpperThreshold = 180
                };
            }

            if (_checkBoxDualThresholdEnabled != null)
            {
                _checkBoxDualThresholdEnabled.Checked = snapshot.Enabled;
            }

            if (_numericDualThresholdLower != null)
            {
                _numericDualThresholdLower.Value = ClampNumericUpDown(_numericDualThresholdLower, snapshot.LowerThreshold);
                _trackBarDualThresholdLower.Value = (int)_numericDualThresholdLower.Value;
            }

            if (_numericDualThresholdUpper != null)
            {
                _numericDualThresholdUpper.Value = ClampNumericUpDown(_numericDualThresholdUpper, snapshot.UpperThreshold);
                _trackBarDualThresholdUpper.Value = (int)_numericDualThresholdUpper.Value;
            }

            if (_numericDualThresholdErode != null) _numericDualThresholdErode.Value = ClampNumericUpDown(_numericDualThresholdErode, snapshot.ErodeIterations);
            if (_numericDualThresholdDilate != null) _numericDualThresholdDilate.Value = ClampNumericUpDown(_numericDualThresholdDilate, snapshot.DilateIterations);
            if (_numericDualThresholdOpen != null) _numericDualThresholdOpen.Value = ClampNumericUpDown(_numericDualThresholdOpen, snapshot.OpenIterations);
            if (_numericDualThresholdClose != null) _numericDualThresholdClose.Value = ClampNumericUpDown(_numericDualThresholdClose, snapshot.CloseIterations);

            UpdateDualThresholdPreview();
        }

        private DualThresholdSnapshot CaptureDualThresholdSnapshot()
        {
            if (_checkBoxDualThresholdEnabled == null || _numericDualThresholdLower == null || _numericDualThresholdUpper == null)
            {
                return new DualThresholdSnapshot();
            }

            return new DualThresholdSnapshot
            {
                Enabled = _checkBoxDualThresholdEnabled.Checked,
                LowerThreshold = (int)_numericDualThresholdLower.Value,
                UpperThreshold = (int)_numericDualThresholdUpper.Value,
                ErodeIterations = _numericDualThresholdErode == null ? 0 : (int)_numericDualThresholdErode.Value,
                DilateIterations = _numericDualThresholdDilate == null ? 0 : (int)_numericDualThresholdDilate.Value,
                OpenIterations = _numericDualThresholdOpen == null ? 0 : (int)_numericDualThresholdOpen.Value,
                CloseIterations = _numericDualThresholdClose == null ? 0 : (int)_numericDualThresholdClose.Value
            };
        }
    }
}
