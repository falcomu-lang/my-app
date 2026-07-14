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

        private enum UserRoleMode
        {
            Operator = 0,
            Engineer = 1,
            Manager = 2
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
        private UserRoleMode _currentUserRole = UserRoleMode.Manager;
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
        private int _referenceScanLineThreshold = 200;
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
        private bool _isSyncingImageViewerCameraProfile;
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
        private ComboBox _comboBoxImageViewerCameraProfile;
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
        private Button _buttonExportMultiImageFolderCsv;
        private DataGridView _dataGridViewMeasureRecords;
        private Label _labelInnerCcdXPrecision;
        private Label _labelInnerCcdYPrecision;
        private Label _labelInnerMeasurementScaleFactor;
        private readonly GroupBox[] _innerCameraGroupBoxes = new GroupBox[3];
        private readonly TextBox[] _innerCameraNameTextBoxes = new TextBox[3];
        private readonly TextBox[] _innerCameraUsageTextBoxes = new TextBox[3];
        private readonly NumericUpDown[] _innerCameraCcdXPrecisions = new NumericUpDown[3];
        private readonly NumericUpDown[] _innerCameraCcdYPrecisions = new NumericUpDown[3];
        private readonly NumericUpDown[] _innerCameraMeasurementScaleFactors = new NumericUpDown[3];
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
        private readonly Panel[] _continuousInspectionPreviewPanels = new Panel[3];
        private readonly PictureBox[] _continuousInspectionPictureBoxes = new PictureBox[3];
        private readonly Label[] _continuousInspectionResultLabels = new Label[3];
        private readonly Label[] _continuousInspectionYieldLabels = new Label[3];
        private readonly CheckBox[] _continuousInspectionSaveOriginalImageChecks = new CheckBox[3];
        private readonly Button[] _continuousInspectionJudgeButtons = new Button[3];
        private Button _buttonContinuousInspectionResetYield;
        private readonly string[] _continuousInspectionImagePaths = new string[3];
        private readonly float[] _continuousInspectionImageScales = new float[3];
        private readonly float[] _continuousInspectionFitScales = new float[3];
        private readonly bool[] _continuousInspectionDragging = new bool[3];
        private readonly bool[] _continuousInspectionOverlayVisible = new bool[3];
        private readonly Point[] _continuousInspectionLastMousePositions = new Point[3];
        private readonly float[] _continuousInspectionOffsetXs = new float[3];
        private readonly float[] _continuousInspectionOffsetYs = new float[3];
        private readonly int[] _continuousInspectionPassCounts = new int[3];
        private readonly int[] _continuousInspectionJudgeCounts = new int[3];
        private string _savedContinuousInspectionMainParameter;
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
        private readonly Dictionary<string, int> _subParameterInnerSettingsProfileIndexes =
            new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
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
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                return;
            }
            _tabPageContinuousInspection = tabPageContinuousInspection;
            ShowMainWorkspaceTabs();
            InitializePreprocessControls();
            InitializeDualThresholdControls();
            InitializeReferenceCornerControls();
            InitializeMeasureDistanceControls();
            InitializeInnerSettingsControls();
            InitializeImageViewerControls();
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

        private void InitializeImageViewerControls()
        {
            _comboBoxImageViewerCameraProfile = comboBoxImageViewerCameraProfile;
            if (_comboBoxImageViewerCameraProfile != null)
            {
                _comboBoxImageViewerCameraProfile.SelectedIndexChanged += ImageViewerCameraProfileComboBox_SelectedIndexChanged;
            }
            RefreshImageViewerCameraProfiles();
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
                    SyncMultiImageConfirmWithActiveProduct(productKey);
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

        private void ApplyUserRole(UserRoleMode role)
        {
            _currentUserRole = role;
            UpdateSidebarVisibilityForRole(role);

            switch (role)
            {
                case UserRoleMode.Operator:
                    ShowContinuousInspectionWorkspace();
                    break;
                case UserRoleMode.Engineer:
                    ShowEngineerWorkspace();
                    break;
                default:
                    ShowMainWorkspaceTabs();
                    tabControlMain.SelectedTab = tabPageImageViewer;
                    break;
            }
        }

        private void UpdateSidebarVisibilityForRole(UserRoleMode role)
        {
            if (buttonReferenceCorner == null)
            {
                return;
            }

            var isManager = role == UserRoleMode.Manager;
            var isEngineer = role == UserRoleMode.Engineer;
            var isOperator = role == UserRoleMode.Operator;

            buttonLoadImage.Visible = isManager;
            buttonReferenceCorner.Visible = isManager;
            buttonMeasureDistance.Visible = isManager || isEngineer;
            buttonMultiImageConfirm.Visible = isManager || isEngineer;
            buttonInnerSettings.Visible = isManager;
            buttonJudgementCriteria.Visible = isManager || isEngineer;
            buttonDetectionParameterSummary.Visible = isManager;
            buttonContinuousInspection.Visible = isManager || isEngineer || isOperator;
            labelOpenCvStatus.Visible = isManager || isEngineer || isOperator;
            buttonLoadImageInViewer.Visible = role != UserRoleMode.Operator;
            labelImageViewerCameraProfile.Visible = role != UserRoleMode.Operator;
            comboBoxImageViewerCameraProfile.Visible = role != UserRoleMode.Operator;
            if (tabControlMain != null)
            {
                if (isManager)
                {
                    tabPageBinarization.Parent = tabControlMain;
                    tabPageBinarization2.Parent = tabControlMain;
                }
                else
                {
                    tabPageBinarization.Parent = null;
                    tabPageBinarization2.Parent = null;
                }
            }
        }

        private void ShowEngineerWorkspace()
        {
            if (tabControlMain == null)
            {
                return;
            }

            tabControlMain.TabPages.Clear();
            tabControlMain.TabPages.Add(tabPageImageViewer);
            tabControlMain.TabPages.Add(_tabPageJudgementCriteria);
            tabControlMain.TabPages.Add(_tabPageMultiImageConfirm);
            tabControlMain.TabPages.Add(_tabPageMeasureDistance);
            tabControlMain.TabPages.Add(_tabPageContinuousInspection);
            tabControlMain.SelectedTab = _tabPageContinuousInspection;
        }

        private void RoleOperatorButton_Click(object sender, EventArgs e)
        {
            ApplyUserRole(UserRoleMode.Operator);
        }

        private void RoleEngineerButton_Click(object sender, EventArgs e)
        {
            ApplyUserRole(UserRoleMode.Engineer);
        }

        private void RoleManagerButton_Click(object sender, EventArgs e)
        {
            ApplyUserRole(UserRoleMode.Manager);
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

            buttonContinuousInspectionLoadImage1.Text = "讀取圖";
            buttonContinuousInspectionLoadImage2.Text = "讀取圖";
            buttonContinuousInspectionLoadImage3.Text = "讀取圖";
            checkBoxContinuousInspectionSaveOriginalImage1.Text = "保存原始影像";
            checkBoxContinuousInspectionSaveOriginalImage2.Text = "保存原始影像";
            checkBoxContinuousInspectionSaveOriginalImage3.Text = "保存原始影像";
            _comboBoxContinuousInspectionMainParameter = comboBoxContinuousInspectionMainParameter;
            _comboBoxContinuousInspectionMainParameter.SelectedIndexChanged += ContinuousInspectionMainParameterComboBox_SelectedIndexChanged;
            _continuousInspectionSubParameterLabels[0] = labelContinuousInspectionSubParameter1;
            _continuousInspectionSubParameterLabels[1] = labelContinuousInspectionSubParameter2;
            _continuousInspectionSubParameterLabels[2] = labelContinuousInspectionSubParameter3;
            _continuousInspectionPreviewPanels[0] = panelContinuousInspectionPreview1;
            _continuousInspectionPreviewPanels[1] = panelContinuousInspectionPreview2;
            _continuousInspectionPreviewPanels[2] = panelContinuousInspectionPreview3;
            _continuousInspectionPictureBoxes[0] = pictureBoxContinuousInspection1;
            _continuousInspectionPictureBoxes[1] = pictureBoxContinuousInspection2;
            _continuousInspectionPictureBoxes[2] = pictureBoxContinuousInspection3;
            _continuousInspectionResultLabels[0] = labelContinuousInspectionResult1;
            _continuousInspectionResultLabels[1] = labelContinuousInspectionResult2;
            _continuousInspectionResultLabels[2] = labelContinuousInspectionResult3;
            _continuousInspectionYieldLabels[0] = labelContinuousInspectionYield1;
            _continuousInspectionYieldLabels[1] = labelContinuousInspectionYield2;
            _continuousInspectionYieldLabels[2] = labelContinuousInspectionYield3;
            _continuousInspectionSaveOriginalImageChecks[0] = checkBoxContinuousInspectionSaveOriginalImage1;
            _continuousInspectionSaveOriginalImageChecks[1] = checkBoxContinuousInspectionSaveOriginalImage2;
            _continuousInspectionSaveOriginalImageChecks[2] = checkBoxContinuousInspectionSaveOriginalImage3;
            _continuousInspectionJudgeButtons[0] = buttonContinuousInspectionJudge1;
            _continuousInspectionJudgeButtons[1] = buttonContinuousInspectionJudge2;
            _continuousInspectionJudgeButtons[2] = buttonContinuousInspectionJudge3;
            _buttonContinuousInspectionResetYield = buttonContinuousInspectionResetYield;

            buttonContinuousInspectionLoadImage1.Tag = 0;
            buttonContinuousInspectionLoadImage2.Tag = 1;
            buttonContinuousInspectionLoadImage3.Tag = 2;
            buttonContinuousInspectionLoadImage1.Click += ContinuousInspectionLoadImageButton_Click;
            buttonContinuousInspectionLoadImage2.Click += ContinuousInspectionLoadImageButton_Click;
            buttonContinuousInspectionLoadImage3.Click += ContinuousInspectionLoadImageButton_Click;
            buttonContinuousInspectionJudge1.Tag = 0;
            buttonContinuousInspectionJudge2.Tag = 1;
            buttonContinuousInspectionJudge3.Tag = 2;
            buttonContinuousInspectionJudge1.Click += ContinuousInspectionJudgeButton_Click;
            buttonContinuousInspectionJudge2.Click += ContinuousInspectionJudgeButton_Click;
            buttonContinuousInspectionJudge3.Click += ContinuousInspectionJudgeButton_Click;
            if (_buttonContinuousInspectionResetYield != null)
            {
                _buttonContinuousInspectionResetYield.Click += ContinuousInspectionResetYield_Click;
            }

            for (var i = 0; i < _continuousInspectionPictureBoxes.Length; i++)
            {
                var pictureBox = _continuousInspectionPictureBoxes[i];
                var panel = _continuousInspectionPreviewPanels[i];
                if (pictureBox == null || panel == null)
                {
                    continue;
                }

                pictureBox.Tag = i;
                panel.Tag = i;
                pictureBox.Dock = DockStyle.None;
                pictureBox.SizeMode = PictureBoxSizeMode.Normal;
                pictureBox.Visible = false;
                panel.Paint += ContinuousInspectionPreviewPanel_Paint;
                panel.MouseWheel += ContinuousInspectionPictureBox_MouseWheel;
                panel.MouseDown += ContinuousInspectionPictureBox_MouseDown;
                panel.MouseMove += ContinuousInspectionPictureBox_MouseMove;
                panel.MouseUp += ContinuousInspectionPictureBox_MouseUp;
                panel.MouseEnter += ContinuousInspectionPictureBox_MouseEnter;
                SetControlDoubleBuffered(panel, true);
            }
            for (var i = 0; i < _continuousInspectionYieldLabels.Length; i++)
            {
                if (_continuousInspectionResultLabels[i] != null)
                {
                    _continuousInspectionResultLabels[i].BackColor = Color.White;
                }

                UpdateContinuousInspectionYieldLabel(i);
            }
        }

        private void ContinuousInspectionResetYield_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < _continuousInspectionPassCounts.Length; i++)
            {
                _continuousInspectionPassCounts[i] = 0;
                _continuousInspectionJudgeCounts[i] = 0;
                UpdateContinuousInspectionYieldLabel(i);
            }
        }

        private void RefreshContinuousInspectionMainParameterItems()
        {
            if (_comboBoxContinuousInspectionMainParameter == null)
            {
                return;
            }

            var selectedName = _comboBoxContinuousInspectionMainParameter.SelectedItem as string;
            if (string.IsNullOrWhiteSpace(selectedName))
            {
                selectedName = _savedContinuousInspectionMainParameter;
            }
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

            if (_comboBoxContinuousInspectionMainParameter.Items.Count > 0 &&
                _comboBoxContinuousInspectionMainParameter.SelectedIndex < 0 &&
                string.IsNullOrWhiteSpace(_savedContinuousInspectionMainParameter))
            {
                _comboBoxContinuousInspectionMainParameter.SelectedIndex = 0;
                return;
            }

            ApplyContinuousInspectionSubParameters();
        }

        private void ContinuousInspectionMainParameterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _savedContinuousInspectionMainParameter = _comboBoxContinuousInspectionMainParameter?.SelectedItem as string;
            EnsureDetectionParameterReference(_savedContinuousInspectionMainParameter);
            ApplyContinuousInspectionSubParameters();
            RefreshImageViewerCameraProfiles();
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

                _continuousInspectionOverlayVisible[i] = false;
                _continuousInspectionPreviewPanels[i]?.Invalidate();
            }
        }

        private void UpdateContinuousInspectionYieldLabel(int index)
        {
            if (index < 0 || index >= _continuousInspectionYieldLabels.Length || _continuousInspectionYieldLabels[index] == null)
            {
                return;
            }

            var total = _continuousInspectionJudgeCounts[index];
            var pass = _continuousInspectionPassCounts[index];
            var percentage = total <= 0 ? 0d : pass * 100d / total;
            _continuousInspectionYieldLabels[index].Text = string.Format(
                CultureInfo.InvariantCulture,
                "良率 {0}/{1}({2:0.#}%)",
                pass,
                total,
                percentage);
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
                    var preserveTransform = _continuousInspectionPictureBoxes[index] != null &&
                        _continuousInspectionPictureBoxes[index].Image != null;
                    SetPictureBoxImage(_continuousInspectionPictureBoxes[index], new Bitmap(image));
                    ApplyContinuousInspectionImageLayout(index, !preserveTransform);
                }

                _continuousInspectionImagePaths[index] = openFileDialogImage.FileName;
                SaveContinuousInspectionOriginalImageIfNeeded(
                    index,
                    _continuousInspectionSubParameterLabels[index]?.Text,
                    openFileDialogImage.FileName);
                _continuousInspectionOverlayVisible[index] = false;
                if (_continuousInspectionResultLabels[index] != null)
                {
                    _continuousInspectionResultLabels[index].Text = string.Empty;
                    _continuousInspectionResultLabels[index].BackColor = Color.White;
                }
                _continuousInspectionPreviewPanels[index]?.Invalidate();
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
                _continuousInspectionResultLabels[index].BackColor = Color.White;
                return;
            }

            var imagePath = _continuousInspectionImagePaths[index];
            if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
            {
                _continuousInspectionResultLabels[index].Text = "未載入圖片";
                _continuousInspectionResultLabels[index].BackColor = Color.White;
                return;
            }

            try
            {
                Bitmap annotatedBitmap;
                var rows = BuildContinuousInspectionJudgementResults(imagePath, subParameterName, out annotatedBitmap);
                var resultText = SummarizeContinuousInspectionJudgement(rows);
                _continuousInspectionResultLabels[index].Text = resultText;
                _continuousInspectionResultLabels[index].BackColor = GetContinuousInspectionResultBackColor(resultText);
                _continuousInspectionJudgeCounts[index]++;
                if (string.Equals(resultText, "A", StringComparison.Ordinal))
                {
                    _continuousInspectionPassCounts[index]++;
                }
                UpdateContinuousInspectionYieldLabel(index);
                _continuousInspectionOverlayVisible[index] = true;
                if (annotatedBitmap != null)
                {
                    SetPictureBoxImage(_continuousInspectionPictureBoxes[index], annotatedBitmap);
                    ApplyContinuousInspectionImageLayout(index, false);
                }
                else
                {
                    _continuousInspectionPreviewPanels[index]?.Invalidate();
                }
            }
            catch (Exception ex)
            {
                _continuousInspectionResultLabels[index].Text = "不可判斷";
                _continuousInspectionResultLabels[index].BackColor = Color.White;
                MessageBox.Show(this, "無法執行判斷。\r\n\r\n" + ex.Message, "連續檢測", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<MultiImageJudgementResultRow> BuildContinuousInspectionJudgementResults(string imagePath, string productKey, out Bitmap annotatedBitmap)
        {
            annotatedBitmap = null;
            var originalBitmap = _multiImageConfirmBitmap;
            var originalSourceImageSize = _multiImageConfirmSourceImageSize;
            var originalProductKey = _multiImageConfirmProductKey;
            var originalImageIndex = _multiImageConfirmImageIndex;
            var originalImagePaths = new List<string>(_multiImageConfirmImagePaths);
            var originalJudgementCriteriaRules = CloneJudgementCriteriaRules(_judgementCriteriaRules);
            var originalCcdXPrecision = _innerSettings.CcdXPrecision;
            var originalCcdYPrecision = _innerSettings.CcdYPrecision;
            var originalMeasurementScaleFactor = _innerSettings.MeasurementScaleFactor;

            try
            {
                productKey = string.IsNullOrWhiteSpace(productKey) ? "DEFAULT" : productKey;
                ApplyInnerSettingsProfileForSubParameter(productKey);
                var profileState = _productProfileService.GetOrCreateState(productKey);
                _judgementCriteriaRules = CloneJudgementCriteriaRules(profileState.JudgementCriteriaRules);
                _multiImageConfirmBitmap = new Bitmap(imagePath);
                _multiImageConfirmSourceImageSize = _multiImageConfirmBitmap.Size;
                _multiImageConfirmProductKey = profileState.ProductKey;
                _multiImageConfirmImagePaths.Clear();
                _multiImageConfirmImagePaths.Add(imagePath);
                _multiImageConfirmImageIndex = 0;
                var rows = BuildMultiImageJudgementResults();
                annotatedBitmap = BuildContinuousInspectionAnnotatedBitmap();
                return rows;
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
                _innerSettings.CcdXPrecision = originalCcdXPrecision;
                _innerSettings.CcdYPrecision = originalCcdYPrecision;
                _innerSettings.MeasurementScaleFactor = originalMeasurementScaleFactor;
            }
        }

        private void SaveContinuousInspectionOriginalImageIfNeeded(int index, string subParameterName, string imagePath)
        {
            if (index < 0 ||
                index >= _continuousInspectionSaveOriginalImageChecks.Length ||
                _continuousInspectionSaveOriginalImageChecks[index] == null ||
                !_continuousInspectionSaveOriginalImageChecks[index].Checked)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(subParameterName) || string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
            {
                return;
            }

            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetDirectory = Path.Combine(baseDirectory, (index + 1).ToString(CultureInfo.InvariantCulture));
            Directory.CreateDirectory(targetDirectory);

            var safeSubParameterName = SanitizeFileName(subParameterName);
            var timestamp = DateTime.Now.ToString("MM_dd_HH_mm_ss", CultureInfo.InvariantCulture);
            var targetPath = Path.Combine(targetDirectory, safeSubParameterName + "_" + timestamp + ".png");

            using (var image = Image.FromFile(imagePath))
            using (var bitmap = new Bitmap(image))
            {
                bitmap.Save(targetPath, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private static string SanitizeFileName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "Unnamed";
            }

            var invalidChars = Path.GetInvalidFileNameChars();
            var sanitized = value;
            foreach (var invalidChar in invalidChars)
            {
                sanitized = sanitized.Replace(invalidChar, '_');
            }

            return string.IsNullOrWhiteSpace(sanitized) ? "Unnamed" : sanitized;
        }

        private Bitmap BuildContinuousInspectionAnnotatedBitmap()
        {
            if (_multiImageConfirmBitmap == null)
            {
                return null;
            }

            return new Bitmap(_multiImageConfirmBitmap);
        }

        private void ContinuousInspectionPreviewPanel_Paint(object sender, PaintEventArgs e)
        {
            var panel = sender as Panel;
            var index = GetContinuousInspectionImageIndex(panel);
            if (index < 0)
            {
                return;
            }

            var pictureBox = _continuousInspectionPictureBoxes[index];
            if (pictureBox?.Image == null)
            {
                return;
            }

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            var image = pictureBox.Image;
            var imageRect = new Rectangle(
                (int)Math.Round(_continuousInspectionOffsetXs[index]),
                (int)Math.Round(_continuousInspectionOffsetYs[index]),
                Math.Max(1, (int)Math.Round(image.Width * _continuousInspectionImageScales[index])),
                Math.Max(1, (int)Math.Round(image.Height * _continuousInspectionImageScales[index])));

            e.Graphics.DrawImage(image, imageRect);
            DrawContinuousInspectionOverlay(e.Graphics, index, imageRect);
        }

        private void DrawContinuousInspectionOverlay(Graphics graphics, int index, Rectangle imageRect)
        {
            if (graphics == null || imageRect.Width <= 0 || imageRect.Height <= 0)
            {
                return;
            }

            if (!_continuousInspectionOverlayVisible[index])
            {
                return;
            }

            var imagePath = _continuousInspectionImagePaths[index];
            var productKey = _continuousInspectionSubParameterLabels[index]?.Text?.Trim();
            if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath) || string.IsNullOrWhiteSpace(productKey) || string.Equals(productKey, "未設定子參數", StringComparison.Ordinal))
            {
                return;
            }

            var originalBitmap = _multiImageConfirmBitmap;
            var originalSourceImageSize = _multiImageConfirmSourceImageSize;
            var originalProductKey = _multiImageConfirmProductKey;
            var originalImageIndex = _multiImageConfirmImageIndex;
            var originalImagePaths = new List<string>(_multiImageConfirmImagePaths);
            var originalJudgementCriteriaRules = CloneJudgementCriteriaRules(_judgementCriteriaRules);
            var originalCcdXPrecision = _innerSettings.CcdXPrecision;
            var originalCcdYPrecision = _innerSettings.CcdYPrecision;
            var originalMeasurementScaleFactor = _innerSettings.MeasurementScaleFactor;

            try
            {
                ApplyInnerSettingsProfileForSubParameter(productKey);
                var profileState = _productProfileService.GetOrCreateState(productKey);
                _judgementCriteriaRules = CloneJudgementCriteriaRules(profileState.JudgementCriteriaRules);
                _multiImageConfirmBitmap = new Bitmap(imagePath);
                _multiImageConfirmSourceImageSize = _multiImageConfirmBitmap.Size;
                _multiImageConfirmProductKey = profileState.ProductKey;
                _multiImageConfirmImagePaths.Clear();
                _multiImageConfirmImagePaths.Add(imagePath);
                _multiImageConfirmImageIndex = 0;

                var referenceCandidate = GetMultiImageConfirmReferenceCandidate();
                if (referenceCandidate != null)
                {
                    DrawMultiImageConfirmReferenceBaseline(graphics, referenceCandidate, imageRect);
                }

                var measureRecords = GetMultiImageConfirmMeasureRecords(referenceCandidate);
                for (var i = 0; i < measureRecords.Count; i++)
                {
                    var record = measureRecords[i];
                    var color = MeasurementOverlayService.GetSourceColor(record.SourceName);
                    using (var pen = new Pen(color, 2f))
                    using (var brush = new SolidBrush(color))
                    {
                        var lineResult = GetCachedMultiImageLineMeasurement(record);
                        if (lineResult == null || !lineResult.IsValid)
                        {
                            continue;
                        }

                        var startPoint = GetMultiImageConfirmDisplayPoint(lineResult.StartPoint, imageRect);
                        var endPoint = GetMultiImageConfirmDisplayPoint(lineResult.EndPoint, imageRect);
                        MeasurementOverlayService.DrawMeasureRecord(graphics, pen, brush, startPoint, endPoint);
                    }
                }
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
                _innerSettings.CcdXPrecision = originalCcdXPrecision;
                _innerSettings.CcdYPrecision = originalCcdYPrecision;
                _innerSettings.MeasurementScaleFactor = originalMeasurementScaleFactor;
            }
        }

        private static Color GetContinuousInspectionResultBackColor(string resultText)
        {
            if (string.Equals(resultText, "A", StringComparison.OrdinalIgnoreCase))
            {
                return Color.FromArgb(198, 239, 206);
            }

            if (string.Equals(resultText, "B", StringComparison.OrdinalIgnoreCase))
            {
                return Color.FromArgb(255, 235, 156);
            }

            if (string.Equals(resultText, "NG", StringComparison.OrdinalIgnoreCase))
            {
                return Color.FromArgb(255, 199, 206);
            }

            return Color.White;
        }

        private void ContinuousInspectionPictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            var index = GetContinuousInspectionImageIndex(sender as Control);
            if (index < 0)
            {
                return;
            }

            var pictureBox = _continuousInspectionPictureBoxes[index];
            var panel = _continuousInspectionPreviewPanels[index];
            if (pictureBox == null || panel == null || pictureBox.Image == null)
            {
                return;
            }

            var sourceControl = sender as Control;
            var mousePosition = panel.PointToClient(sourceControl.PointToScreen(e.Location));
            var imageX = (mousePosition.X - _continuousInspectionOffsetXs[index]) / _continuousInspectionImageScales[index];
            var imageY = (mousePosition.Y - _continuousInspectionOffsetYs[index]) / _continuousInspectionImageScales[index];
            var zoomFactor = e.Delta > 0 ? 1.15f : 1f / 1.15f;
            var minimumScale = _continuousInspectionFitScales[index] * 0.25f;
            var maximumScale = _continuousInspectionFitScales[index] * 20f;

            _continuousInspectionImageScales[index] = Math.Max(
                minimumScale,
                Math.Min(maximumScale, _continuousInspectionImageScales[index] * zoomFactor));

            _continuousInspectionOffsetXs[index] = (float)(mousePosition.X - imageX * _continuousInspectionImageScales[index]);
            _continuousInspectionOffsetYs[index] = (float)(mousePosition.Y - imageY * _continuousInspectionImageScales[index]);
            ConstrainContinuousInspectionImagePosition(index);
            panel.Invalidate();
        }

        private void ContinuousInspectionPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            var index = GetContinuousInspectionImageIndex(sender as Control);
            if (index < 0 || e.Button != MouseButtons.Left)
            {
                return;
            }

            var pictureBox = _continuousInspectionPictureBoxes[index];
            var panel = _continuousInspectionPreviewPanels[index];
            if (pictureBox == null || panel == null || pictureBox.Image == null)
            {
                return;
            }

            _continuousInspectionDragging[index] = true;
            _continuousInspectionLastMousePositions[index] = e.Location;
            panel.Cursor = Cursors.SizeAll;
            panel.Capture = true;
        }

        private void ContinuousInspectionPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            var index = GetContinuousInspectionImageIndex(sender as Control);
            if (index < 0 || !_continuousInspectionDragging[index])
            {
                return;
            }

            var pictureBox = _continuousInspectionPictureBoxes[index];
            var panel = _continuousInspectionPreviewPanels[index];
            if (pictureBox == null || panel == null)
            {
                return;
            }

            var currentPosition = e.Location;
            _continuousInspectionOffsetXs[index] += currentPosition.X - _continuousInspectionLastMousePositions[index].X;
            _continuousInspectionOffsetYs[index] += currentPosition.Y - _continuousInspectionLastMousePositions[index].Y;
            _continuousInspectionLastMousePositions[index] = currentPosition;
            ConstrainContinuousInspectionImagePosition(index);
            panel.Invalidate();
        }

        private void ContinuousInspectionPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            var index = GetContinuousInspectionImageIndex(sender as Control);
            if (index < 0 || e.Button != MouseButtons.Left)
            {
                return;
            }

            _continuousInspectionDragging[index] = false;
            var panel = _continuousInspectionPreviewPanels[index];
            if (panel != null)
            {
                panel.Cursor = Cursors.Default;
                panel.Capture = false;
            }
        }

        private void ContinuousInspectionPictureBox_MouseEnter(object sender, EventArgs e)
        {
            var index = GetContinuousInspectionImageIndex(sender as Control);
            if (index < 0)
            {
                return;
            }

            _continuousInspectionPreviewPanels[index]?.Focus();
        }

        private void ApplyContinuousInspectionImageLayout(int index, bool centerImage)
        {
            if (index < 0 || index >= _continuousInspectionPictureBoxes.Length)
            {
                return;
            }

            var pictureBox = _continuousInspectionPictureBoxes[index];
            var panel = _continuousInspectionPreviewPanels[index];
            if (pictureBox == null || panel == null || pictureBox.Image == null)
            {
                return;
            }

            if (_continuousInspectionFitScales[index] <= 0f || centerImage)
            {
                var scaleX = panel.ClientSize.Width / (float)pictureBox.Image.Width;
                var scaleY = panel.ClientSize.Height / (float)pictureBox.Image.Height;
                _continuousInspectionFitScales[index] = Math.Min(scaleX, scaleY);
            }

            if (_continuousInspectionImageScales[index] <= 0f || centerImage)
            {
                _continuousInspectionImageScales[index] = _continuousInspectionFitScales[index];
            }

            if (centerImage)
            {
                var width = pictureBox.Image.Width * _continuousInspectionImageScales[index];
                var height = pictureBox.Image.Height * _continuousInspectionImageScales[index];
                _continuousInspectionOffsetXs[index] = (panel.ClientSize.Width - width) / 2f;
                _continuousInspectionOffsetYs[index] = (panel.ClientSize.Height - height) / 2f;
            }

            ConstrainContinuousInspectionImagePosition(index);
            panel.Invalidate();
        }

        private static int GetContinuousInspectionImageIndex(Control control)
        {
            return control?.Tag as int? ?? -1;
        }

        private void ConstrainContinuousInspectionImagePosition(int index)
        {
            var pictureBox = _continuousInspectionPictureBoxes[index];
            var panel = _continuousInspectionPreviewPanels[index];
            if (pictureBox == null || panel == null || pictureBox.Image == null)
            {
                return;
            }

            var width = pictureBox.Image.Width * _continuousInspectionImageScales[index];
            var height = pictureBox.Image.Height * _continuousInspectionImageScales[index];

            _continuousInspectionOffsetXs[index] = width <= panel.ClientSize.Width
                ? (panel.ClientSize.Width - width) / 2f
                : Math.Max(panel.ClientSize.Width - width, Math.Min(0f, _continuousInspectionOffsetXs[index]));

            _continuousInspectionOffsetYs[index] = height <= panel.ClientSize.Height
                ? (panel.ClientSize.Height - height) / 2f
                : Math.Max(panel.ClientSize.Height - height, Math.Min(0f, _continuousInspectionOffsetYs[index]));
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
                return "B";
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
                RefreshImageViewerCameraProfiles();
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
            RefreshImageViewerCameraProfiles();
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
                if (!string.IsNullOrWhiteSpace(loadedData.UserRole))
                {
                    UserRoleMode parsedRole;
                    if (Enum.TryParse(loadedData.UserRole, true, out parsedRole))
                    {
                        _currentUserRole = parsedRole;
                    }
                }
                _savedContinuousInspectionMainParameter = string.IsNullOrWhiteSpace(loadedData.ContinuousInspectionMainParameter)
                    ? null
                    : loadedData.ContinuousInspectionMainParameter;
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
                RestoreContinuousInspectionMainParameterSelection();
                ApplyUserRole(_currentUserRole);
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
                SaveInnerSettings();
                PersistActiveProductProfile();
                SaveDetectionParameterReferenceList();
                var currentProductKey = GetCurrentProductKeyOrDefault();
                var settingsData = new AppSettingsData
                {
                    LastImagePath = _lastImagePath,
                    ActiveProductKey = _activeProductKey,
                    ContinuousInspectionMainParameter = _comboBoxContinuousInspectionMainParameter?.SelectedItem as string,
                    UserRole = _currentUserRole.ToString()
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

        private void RestoreContinuousInspectionMainParameterSelection()
        {
            if (_comboBoxContinuousInspectionMainParameter == null || string.IsNullOrWhiteSpace(_savedContinuousInspectionMainParameter))
            {
                return;
            }

            var index = _comboBoxContinuousInspectionMainParameter.FindStringExact(_savedContinuousInspectionMainParameter);
            if (index >= 0)
            {
                _comboBoxContinuousInspectionMainParameter.SelectedIndex = index;
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

            var profiles = data.CameraProfiles;
            for (var i = 0; i < 3; i++)
            {
                InnerSettingsCameraProfile profile = null;
                if (profiles != null && i < profiles.Count)
                {
                    profile = profiles[i];
                }

                if (profile == null)
                {
                    profile = new InnerSettingsCameraProfile
                    {
                        CameraName = "Camera " + (i + 1),
                        UsageName = string.Empty,
                        CcdXPrecision = data.CcdXPrecision,
                        CcdYPrecision = data.CcdYPrecision,
                        MeasurementScaleFactor = data.MeasurementScaleFactor
                    };
                }

                if (_innerCameraNameTextBoxes[i] != null)
                {
                    _innerCameraNameTextBoxes[i].Text = profile.CameraName ?? string.Empty;
                }

                if (_innerCameraUsageTextBoxes[i] != null)
                {
                    _innerCameraUsageTextBoxes[i].Text = profile.UsageName ?? string.Empty;
                }

                if (_innerCameraCcdXPrecisions[i] != null)
                {
                    _innerCameraCcdXPrecisions[i].Value = ClampNumericUpDown(_innerCameraCcdXPrecisions[i], (decimal)profile.CcdXPrecision);
                }

                if (_innerCameraCcdYPrecisions[i] != null)
                {
                    _innerCameraCcdYPrecisions[i].Value = ClampNumericUpDown(_innerCameraCcdYPrecisions[i], (decimal)profile.CcdYPrecision);
                }

                if (_innerCameraMeasurementScaleFactors[i] != null)
                {
                    var scale = profile.MeasurementScaleFactor <= 0 ? 1.0 : profile.MeasurementScaleFactor;
                    _innerCameraMeasurementScaleFactors[i].Value = ClampNumericUpDown(_innerCameraMeasurementScaleFactors[i], (decimal)scale);
                }
            }

            RefreshImageViewerCameraProfiles();
        }

        private void RefreshImageViewerCameraProfiles()
        {
            if (_comboBoxImageViewerCameraProfile == null)
            {
                return;
            }

            var selectedSubParameter = GetSelectedSubParameterBindingName();

            _comboBoxImageViewerCameraProfile.BeginUpdate();
            try
            {
                _comboBoxImageViewerCameraProfile.Items.Clear();
                var profiles = _innerSettings != null ? _innerSettings.CameraProfiles : null;
                if (profiles != null && profiles.Count > 0)
                {
                    for (var i = 0; i < profiles.Count; i++)
                    {
                        var profile = profiles[i];
                        var name = profile == null ? string.Empty : profile.CameraName;
                        var usage = profile == null ? string.Empty : profile.UsageName;
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            name = "Camera " + (i + 1);
                        }

                        var itemText = string.IsNullOrWhiteSpace(usage) ? name : name + " - " + usage;
                        _comboBoxImageViewerCameraProfile.Items.Add(itemText);
                    }
                }

                if (_comboBoxImageViewerCameraProfile.Items.Count == 0)
                {
                    _comboBoxImageViewerCameraProfile.Items.Add("Camera 1");
                    _comboBoxImageViewerCameraProfile.Items.Add("Camera 2");
                    _comboBoxImageViewerCameraProfile.Items.Add("Camera 3");
                }

                var targetIndex = GetInnerSettingsProfileIndexForSubParameter(selectedSubParameter);
                if (targetIndex >= 0 && targetIndex < _comboBoxImageViewerCameraProfile.Items.Count)
                {
                    _isSyncingImageViewerCameraProfile = true;
                    _comboBoxImageViewerCameraProfile.SelectedIndex = targetIndex;
                }
                else if (_comboBoxImageViewerCameraProfile.Items.Count > 0)
                {
                    _isSyncingImageViewerCameraProfile = true;
                    _comboBoxImageViewerCameraProfile.SelectedIndex = 0;
                }
            }
            finally
            {
                _isSyncingImageViewerCameraProfile = false;
                _comboBoxImageViewerCameraProfile.EndUpdate();
            }

            ApplySelectedInnerSettingsProfile();
        }

        private void ImageViewerCameraProfileComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isSyncingImageViewerCameraProfile)
            {
                return;
            }

            var selectedSubParameter = GetSelectedSubParameterBindingName();
            if (string.IsNullOrWhiteSpace(selectedSubParameter))
            {
                return;
            }

            _subParameterInnerSettingsProfileIndexes[selectedSubParameter] = Math.Max(0, _comboBoxImageViewerCameraProfile.SelectedIndex);
            ApplySelectedInnerSettingsProfile();
            SaveDetectionParameterReferenceList();
        }

        private string GetSelectedSubParameterBindingName()
        {
            var productKey = GetCurrentProductKeyOrDefault();
            return string.Equals(productKey, "DEFAULT", StringComparison.OrdinalIgnoreCase) ? string.Empty : productKey;
        }

        private int GetInnerSettingsProfileIndexForSubParameter(string subParameterName)
        {
            if (string.IsNullOrWhiteSpace(subParameterName))
            {
                return 0;
            }

            int index;
            return _subParameterInnerSettingsProfileIndexes.TryGetValue(subParameterName, out index)
                ? Math.Max(0, index)
                : 0;
        }

        private void ApplyInnerSettingsProfileForSubParameter(string subParameterName)
        {
            ApplyInnerSettingsProfileIndex(GetInnerSettingsProfileIndexForSubParameter(subParameterName));
        }

        private void ApplySelectedInnerSettingsProfile()
        {
            if (_innerSettings == null ||
                _innerSettings.CameraProfiles == null ||
                _comboBoxImageViewerCameraProfile == null)
            {
                return;
            }

            var selectedIndex = _comboBoxImageViewerCameraProfile.SelectedIndex;
            if (selectedIndex < 0 || selectedIndex >= _innerSettings.CameraProfiles.Count)
            {
                selectedIndex = 0;
            }

            if (selectedIndex < 0 || selectedIndex >= _innerSettings.CameraProfiles.Count)
            {
                return;
            }

            ApplyInnerSettingsProfileIndex(selectedIndex);
        }

        private void ApplyInnerSettingsProfileIndex(int selectedIndex)
        {
            if (_innerSettings == null || _innerSettings.CameraProfiles == null)
            {
                return;
            }

            if (selectedIndex < 0 || selectedIndex >= _innerSettings.CameraProfiles.Count)
            {
                selectedIndex = 0;
            }

            if (selectedIndex < 0 || selectedIndex >= _innerSettings.CameraProfiles.Count)
            {
                return;
            }

            var profile = _innerSettings.CameraProfiles[selectedIndex];
            if (profile == null)
            {
                return;
            }

            _innerSettings.CcdXPrecision = profile.CcdXPrecision;
            _innerSettings.CcdYPrecision = profile.CcdYPrecision;
            _innerSettings.MeasurementScaleFactor = profile.MeasurementScaleFactor <= 0 ? 1.0 : profile.MeasurementScaleFactor;
        }

        private string GetSelectedMainParameterBindingName()
        {
            var selectedMainParameter = _comboBoxContinuousInspectionMainParameter?.SelectedItem as string;
            if (!string.IsNullOrWhiteSpace(selectedMainParameter))
            {
                return selectedMainParameter;
            }

            return _savedContinuousInspectionMainParameter;
        }

        private static List<JudgementCriterionRule> CloneJudgementCriteriaRules(List<JudgementCriterionRule> rules)
        {
            return ProfileDataCloner.CloneJudgementCriteria(rules);
        }

        private void SaveInnerSettings()
        {
            if (_innerCameraCcdXPrecisions[0] == null || _innerCameraCcdYPrecisions[0] == null || _innerCameraMeasurementScaleFactors[0] == null)
            {
                return;
            }

            _innerSettings.CameraProfiles.Clear();
            for (var i = 0; i < 3; i++)
            {
                _innerSettings.CameraProfiles.Add(new InnerSettingsCameraProfile
                {
                    CameraName = _innerCameraNameTextBoxes[i] == null ? "Camera " + (i + 1) : _innerCameraNameTextBoxes[i].Text,
                    UsageName = _innerCameraUsageTextBoxes[i] == null ? string.Empty : _innerCameraUsageTextBoxes[i].Text,
                    CcdXPrecision = _innerCameraCcdXPrecisions[i] == null ? 0d : (double)_innerCameraCcdXPrecisions[i].Value,
                    CcdYPrecision = _innerCameraCcdYPrecisions[i] == null ? 0d : (double)_innerCameraCcdYPrecisions[i].Value,
                    MeasurementScaleFactor = _innerCameraMeasurementScaleFactors[i] == null ? 1.0 : (double)_innerCameraMeasurementScaleFactors[i].Value
                });
            }

            if (_innerSettings.CameraProfiles.Count > 0)
            {
                _innerSettings.CcdXPrecision = _innerSettings.CameraProfiles[0].CcdXPrecision;
                _innerSettings.CcdYPrecision = _innerSettings.CameraProfiles[0].CcdYPrecision;
                _innerSettings.MeasurementScaleFactor = _innerSettings.CameraProfiles[0].MeasurementScaleFactor;
            }
            _innerSettingsRepository.Save(GetInnerSettingsPath(), _innerSettings);
            RefreshImageViewerCameraProfiles();
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
                SyncMultiImageConfirmWithActiveProduct(GetCurrentProductKeyOrDefault());
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
