namespace AoiMeasureTool
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuOpenImage;
        private System.Windows.Forms.ToolStripSeparator menuFileSeparator;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem menuEdit;
        private System.Windows.Forms.ToolStripMenuItem menuView;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Label labelAppName;
        private System.Windows.Forms.Label labelNavigation;
        private System.Windows.Forms.Button buttonLoadImage;
        private System.Windows.Forms.Button buttonLoadImageInViewer;
        private System.Windows.Forms.Button buttonReferenceCorner;
        private System.Windows.Forms.Button buttonMeasureDistance;
        private System.Windows.Forms.Button buttonMultiImageConfirm;
        private System.Windows.Forms.Button buttonInnerSettings;
        private System.Windows.Forms.Button buttonJudgementCriteria;
        private System.Windows.Forms.Button buttonDetectionParameterSummary;
        private System.Windows.Forms.Button buttonContinuousInspection;
        private System.Windows.Forms.Label labelOpenCvStatus;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelImageInfo;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageImageViewer;
        private System.Windows.Forms.TabPage tabPageBinarization;
        private System.Windows.Forms.TabPage tabPageBinarization2;
        private System.Windows.Forms.TabPage tabPageReferenceCorner;
        private System.Windows.Forms.TabPage tabPageMeasureDistance;
        private System.Windows.Forms.TabPage tabPageMultiImageConfirm;
        private System.Windows.Forms.TabPage tabPageInnerSettings;
        private System.Windows.Forms.TabPage tabPageJudgementCriteria;
        private System.Windows.Forms.TabPage tabPageDetectionParameterSummary;
        private System.Windows.Forms.TabPage tabPageContinuousInspection;
        private System.Windows.Forms.Panel panelContinuousInspection;
        private System.Windows.Forms.Label labelContinuousInspectionTitle;
        private System.Windows.Forms.Label labelContinuousInspectionHint;
        private System.Windows.Forms.Label labelContinuousInspectionMainParameter;
        private System.Windows.Forms.ComboBox comboBoxContinuousInspectionMainParameter;
        private System.Windows.Forms.GroupBox groupBoxContinuousInspection1;
        private System.Windows.Forms.GroupBox groupBoxContinuousInspection2;
        private System.Windows.Forms.GroupBox groupBoxContinuousInspection3;
        private System.Windows.Forms.Label labelContinuousInspectionSubParameter1;
        private System.Windows.Forms.Label labelContinuousInspectionSubParameter2;
        private System.Windows.Forms.Label labelContinuousInspectionSubParameter3;
        private System.Windows.Forms.Panel panelContinuousInspectionPreview1;
        private System.Windows.Forms.Panel panelContinuousInspectionPreview2;
        private System.Windows.Forms.Panel panelContinuousInspectionPreview3;
        private System.Windows.Forms.PictureBox pictureBoxContinuousInspection1;
        private System.Windows.Forms.PictureBox pictureBoxContinuousInspection2;
        private System.Windows.Forms.PictureBox pictureBoxContinuousInspection3;
        private System.Windows.Forms.Button buttonContinuousInspectionLoadImage1;
        private System.Windows.Forms.Button buttonContinuousInspectionLoadImage2;
        private System.Windows.Forms.Button buttonContinuousInspectionLoadImage3;
        private System.Windows.Forms.Label labelContinuousInspectionResult1;
        private System.Windows.Forms.Label labelContinuousInspectionResult2;
        private System.Windows.Forms.Label labelContinuousInspectionResult3;
        private System.Windows.Forms.Button buttonContinuousInspectionJudge1;
        private System.Windows.Forms.Button buttonContinuousInspectionJudge2;
        private System.Windows.Forms.Button buttonContinuousInspectionJudge3;
        private System.Windows.Forms.Button buttonContinuousInspectionResetYield;
        private System.Windows.Forms.Label labelContinuousInspectionYield1;
        private System.Windows.Forms.Label labelContinuousInspectionYield2;
        private System.Windows.Forms.Label labelContinuousInspectionYield3;
        private System.Windows.Forms.CheckBox checkBoxContinuousInspectionSaveOriginalImage1;
        private System.Windows.Forms.CheckBox checkBoxContinuousInspectionSaveOriginalImage2;
        private System.Windows.Forms.CheckBox checkBoxContinuousInspectionSaveOriginalImage3;
        private System.Windows.Forms.Panel panelDetectionParameterSummary;
        private System.Windows.Forms.GroupBox groupBoxDetectionParameterCreate;
        private System.Windows.Forms.TextBox textBoxDetectionMainParameterName;
        private System.Windows.Forms.Button buttonDetectionMainParameterConfirm;
        private System.Windows.Forms.GroupBox groupBoxDetectionMainParameterList;
        private System.Windows.Forms.GroupBox groupBoxDetectionSubParameter1List;
        private System.Windows.Forms.GroupBox groupBoxDetectionSubParameter2List;
        private System.Windows.Forms.GroupBox groupBoxDetectionSubParameter3List;
        private System.Windows.Forms.ListBox listBoxDetectionMainParameter;
        private System.Windows.Forms.ListBox listBoxDetectionSubParameter1;
        private System.Windows.Forms.ListBox listBoxDetectionSubParameter2;
        private System.Windows.Forms.ListBox listBoxDetectionSubParameter3;
        private System.Windows.Forms.Button buttonDetectionMainParameterMoveUp;
        private System.Windows.Forms.Button buttonDetectionMainParameterMoveDown;
        private System.Windows.Forms.Button buttonDetectionMainParameterSaveOrder;
        private System.Windows.Forms.Button buttonDetectionSubParameter1MoveUp;
        private System.Windows.Forms.Button buttonDetectionSubParameter1MoveDown;
        private System.Windows.Forms.Button buttonDetectionSubParameter1SaveOrder;
        private System.Windows.Forms.CheckBox checkBoxDetectionSubParameter1Enabled;
        private System.Windows.Forms.Button buttonDetectionSubParameter2MoveUp;
        private System.Windows.Forms.Button buttonDetectionSubParameter2MoveDown;
        private System.Windows.Forms.Button buttonDetectionSubParameter2SaveOrder;
        private System.Windows.Forms.CheckBox checkBoxDetectionSubParameter2Enabled;
        private System.Windows.Forms.Button buttonDetectionSubParameter3MoveUp;
        private System.Windows.Forms.Button buttonDetectionSubParameter3MoveDown;
        private System.Windows.Forms.Button buttonDetectionSubParameter3SaveOrder;
        private System.Windows.Forms.CheckBox checkBoxDetectionSubParameter3Enabled;
        private System.Windows.Forms.Button buttonDetectionSaveParameterReference;
        private System.Windows.Forms.Label labelWorkspace;
        private System.Windows.Forms.Label labelInnerSettingsTitle;
        private System.Windows.Forms.Panel panelInnerSettings;
        private System.Windows.Forms.Label labelInnerCcdXPrecision;
        private System.Windows.Forms.NumericUpDown numericInnerCcdXPrecision;
        private System.Windows.Forms.Label labelInnerCcdYPrecision;
        private System.Windows.Forms.NumericUpDown numericInnerCcdYPrecision;
        private System.Windows.Forms.Label labelInnerMeasurementScaleFactor;
        private System.Windows.Forms.NumericUpDown numericInnerMeasurementScaleFactor;
        private System.Windows.Forms.Button buttonSaveInnerSettings;
        private System.Windows.Forms.Label labelJudgementCriteriaTitle;
        private System.Windows.Forms.Button buttonJudgementSyntaxHelp;
        private System.Windows.Forms.Panel panelJudgementCriteria;
        private System.Windows.Forms.Label labelJudgementName;
        private System.Windows.Forms.TextBox textBoxJudgementName;
        private System.Windows.Forms.Label labelJudgementCalculation;
        private System.Windows.Forms.TextBox textBoxJudgementCalculation;
        private System.Windows.Forms.Label labelJudgementSpec;
        private System.Windows.Forms.TextBox textBoxJudgementSpec;
        private System.Windows.Forms.Label labelJudgementCalculationB;
        private System.Windows.Forms.TextBox textBoxJudgementCalculationB;
        private System.Windows.Forms.Label labelJudgementSpecB;
        private System.Windows.Forms.TextBox textBoxJudgementSpecB;
        private System.Windows.Forms.Button buttonJudgementAdd;
        private System.Windows.Forms.Button buttonJudgementReset;
        private System.Windows.Forms.Button buttonJudgementSave;
        private System.Windows.Forms.Button buttonJudgementMoveUp;
        private System.Windows.Forms.Button buttonJudgementMoveDown;
        private System.Windows.Forms.DataGridView dataGridViewJudgementCriteria;
        private System.Windows.Forms.Panel panelMeasureSource;
        private System.Windows.Forms.Label labelMeasureSource;
        private System.Windows.Forms.ComboBox comboBoxMeasureSource;
        private System.Windows.Forms.Button buttonSaveMeasurePoint;
        private System.Windows.Forms.Button buttonClearMeasurePoint;
        private System.Windows.Forms.Button buttonSaveMeasureRecords;
        private System.Windows.Forms.Button buttonParallelMeasure;
        private System.Windows.Forms.Button buttonPerpendicularMeasure;
        private System.Windows.Forms.Label labelMeasureStatus;
        private System.Windows.Forms.Panel panelMeasurePreview;
        private System.Windows.Forms.PictureBox pictureBoxMeasurePreview;
        private System.Windows.Forms.DataGridView dataGridViewMeasureRecords;
        private System.Windows.Forms.Panel panelReferenceCornerControls;
        private System.Windows.Forms.CheckBox checkBoxReferenceCornerEnabled;
        private System.Windows.Forms.Label labelReferenceSource;
        private System.Windows.Forms.ComboBox comboBoxReferenceSource;
        private System.Windows.Forms.Button buttonSaveReferenceRoi;
        private System.Windows.Forms.Label labelReferenceCornerStatus;
        private System.Windows.Forms.Panel panelReferencePreview;
        private System.Windows.Forms.PictureBox pictureBoxReferencePreview;
        private System.Windows.Forms.Panel panelMultiImageConfirmViewport;
        private System.Windows.Forms.PictureBox pictureBoxMultiImageConfirm;
        private System.Windows.Forms.Button buttonLoadMultiImageFolder;
        private System.Windows.Forms.GroupBox groupBoxMultiImagePreviewSource;
        private System.Windows.Forms.ComboBox comboBoxMultiImagePreviewSource;
        private System.Windows.Forms.Button buttonLoadMultiImagePreprocess;
        private System.Windows.Forms.Button buttonLoadMultiImageOriginal;
        private System.Windows.Forms.Label labelMultiImageStatus;
        private System.Windows.Forms.Button buttonMultiImagePrev;
        private System.Windows.Forms.Button buttonMultiImageNext;
        private System.Windows.Forms.Button buttonMultiImageLineSequence;
        private System.Windows.Forms.ComboBox comboBoxMultiImageLineDisplayMode;
        private System.Windows.Forms.Panel panelMultiImageInfo;
        private System.Windows.Forms.TabControl tabControlMultiImageInfo;
        private System.Windows.Forms.TabPage tabPageMultiImageRawData;
        private System.Windows.Forms.TabPage tabPageMultiImageJudgementResult;
        private System.Windows.Forms.DataGridView dataGridViewMultiImageInfo;
        private System.Windows.Forms.Panel panelImageViewport;
        private System.Windows.Forms.PictureBox pictureBoxImage;
        private System.Windows.Forms.Panel panelBinaryOriginal;
        private System.Windows.Forms.Label labelBinaryOriginal;
        private System.Windows.Forms.PictureBox pictureBoxBinaryOriginal;
        private System.Windows.Forms.Panel panelActivePreprocess;
        private System.Windows.Forms.Label labelActivePreprocess;
        private System.Windows.Forms.Panel panelActiveViewport;
        private System.Windows.Forms.PictureBox pictureBoxActivePreprocess;
        private System.Windows.Forms.Panel panelPreprocess1;
        private System.Windows.Forms.Panel panelPreprocess2;
        private System.Windows.Forms.Panel panelPreprocess3;
        private System.Windows.Forms.Panel panelPreprocess4;
        private System.Windows.Forms.Label labelPreprocess1Title;
        private System.Windows.Forms.Label labelPreprocess2Title;
        private System.Windows.Forms.Label labelPreprocess3Title;
        private System.Windows.Forms.Label labelPreprocess4Title;
        private System.Windows.Forms.PictureBox pictureBoxPreprocess1;
        private System.Windows.Forms.PictureBox pictureBoxPreprocess2;
        private System.Windows.Forms.PictureBox pictureBoxPreprocess3;
        private System.Windows.Forms.PictureBox pictureBoxPreprocess4;
        private System.Windows.Forms.TabControl tabControlPreprocess;
        private System.Windows.Forms.TabPage tabPagePreprocess1;
        private System.Windows.Forms.TabPage tabPagePreprocess2;
        private System.Windows.Forms.TabPage tabPagePreprocess3;
        private System.Windows.Forms.TabPage tabPagePreprocess4;
        private System.Windows.Forms.CheckBox checkBoxPreprocess1Enabled;
        private System.Windows.Forms.CheckBox checkBoxPreprocess2Enabled;
        private System.Windows.Forms.CheckBox checkBoxPreprocess3Enabled;
        private System.Windows.Forms.CheckBox checkBoxPreprocess4Enabled;
        private System.Windows.Forms.TrackBar trackBarThreshold1;
        private System.Windows.Forms.TrackBar trackBarThreshold2;
        private System.Windows.Forms.TrackBar trackBarThreshold3;
        private System.Windows.Forms.TrackBar trackBarThreshold4;
        private System.Windows.Forms.NumericUpDown numericThreshold1;
        private System.Windows.Forms.NumericUpDown numericThreshold2;
        private System.Windows.Forms.NumericUpDown numericThreshold3;
        private System.Windows.Forms.NumericUpDown numericThreshold4;
        private System.Windows.Forms.NumericUpDown numericErode1;
        private System.Windows.Forms.NumericUpDown numericErode2;
        private System.Windows.Forms.NumericUpDown numericErode3;
        private System.Windows.Forms.NumericUpDown numericErode4;
        private System.Windows.Forms.NumericUpDown numericDilate1;
        private System.Windows.Forms.NumericUpDown numericDilate2;
        private System.Windows.Forms.NumericUpDown numericDilate3;
        private System.Windows.Forms.NumericUpDown numericDilate4;
        private System.Windows.Forms.NumericUpDown numericOpen1;
        private System.Windows.Forms.NumericUpDown numericOpen2;
        private System.Windows.Forms.NumericUpDown numericOpen3;
        private System.Windows.Forms.NumericUpDown numericOpen4;
        private System.Windows.Forms.NumericUpDown numericClose1;
        private System.Windows.Forms.NumericUpDown numericClose2;
        private System.Windows.Forms.NumericUpDown numericClose3;
        private System.Windows.Forms.NumericUpDown numericClose4;
        private System.Windows.Forms.Label labelThreshold1;
        private System.Windows.Forms.Label labelThreshold2;
        private System.Windows.Forms.Label labelThreshold3;
        private System.Windows.Forms.Label labelThreshold4;
        private System.Windows.Forms.Label labelErode1;
        private System.Windows.Forms.Label labelErode2;
        private System.Windows.Forms.Label labelErode3;
        private System.Windows.Forms.Label labelErode4;
        private System.Windows.Forms.Label labelDilate1;
        private System.Windows.Forms.Label labelDilate2;
        private System.Windows.Forms.Label labelDilate3;
        private System.Windows.Forms.Label labelDilate4;
        private System.Windows.Forms.Label labelOpen1;
        private System.Windows.Forms.Label labelOpen2;
        private System.Windows.Forms.Label labelOpen3;
        private System.Windows.Forms.Label labelOpen4;
        private System.Windows.Forms.Label labelClose1;
        private System.Windows.Forms.Label labelClose2;
        private System.Windows.Forms.Label labelClose3;
        private System.Windows.Forms.Label labelClose4;
        private System.Windows.Forms.Panel panelPreprocessActions;
        private System.Windows.Forms.Button buttonLoadSavedSettings;
        private System.Windows.Forms.Button buttonSaveCurrentSettings;
        private System.Windows.Forms.Panel panelDualThresholdOriginal;
        private System.Windows.Forms.Label labelDualThresholdOriginal;
        private System.Windows.Forms.Panel panelDualThresholdOriginalViewport;
        private System.Windows.Forms.PictureBox pictureBoxDualThresholdOriginal;
        private System.Windows.Forms.Panel panelDualThresholdPreview;
        private System.Windows.Forms.Label labelDualThresholdPreview;
        private System.Windows.Forms.Panel panelDualThresholdPreviewViewport;
        private System.Windows.Forms.PictureBox pictureBoxDualThresholdPreview;
        private System.Windows.Forms.Panel panelDualThresholdControls;
        private System.Windows.Forms.Label labelDualThresholdSettings;
        private System.Windows.Forms.CheckBox checkBoxDualThresholdEnabled;
        private System.Windows.Forms.Label labelDualThresholdLower;
        private System.Windows.Forms.TrackBar trackBarDualThresholdLower;
        private System.Windows.Forms.NumericUpDown numericDualThresholdLower;
        private System.Windows.Forms.Label labelDualThresholdUpper;
        private System.Windows.Forms.TrackBar trackBarDualThresholdUpper;
        private System.Windows.Forms.NumericUpDown numericDualThresholdUpper;
        private System.Windows.Forms.Label labelDualThresholdErode;
        private System.Windows.Forms.NumericUpDown numericDualThresholdErode;
        private System.Windows.Forms.Label labelDualThresholdDilate;
        private System.Windows.Forms.NumericUpDown numericDualThresholdDilate;
        private System.Windows.Forms.Label labelDualThresholdOpen;
        private System.Windows.Forms.NumericUpDown numericDualThresholdOpen;
        private System.Windows.Forms.Label labelDualThresholdClose;
        private System.Windows.Forms.NumericUpDown numericDualThresholdClose;
        private System.Windows.Forms.Panel panelDualThresholdActions;
        private System.Windows.Forms.Button buttonDualThresholdLoadSettings;
        private System.Windows.Forms.Button buttonDualThresholdSaveSettings;
        private System.Windows.Forms.OpenFileDialog openFileDialogImage;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenImage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.labelAppName = new System.Windows.Forms.Label();
            this.labelNavigation = new System.Windows.Forms.Label();
            this.buttonLoadImage = new System.Windows.Forms.Button();
            this.buttonReferenceCorner = new System.Windows.Forms.Button();
            this.buttonMeasureDistance = new System.Windows.Forms.Button();
            this.buttonMultiImageConfirm = new System.Windows.Forms.Button();
            this.buttonInnerSettings = new System.Windows.Forms.Button();
            this.buttonJudgementCriteria = new System.Windows.Forms.Button();
            this.buttonDetectionParameterSummary = new System.Windows.Forms.Button();
            this.buttonContinuousInspection = new System.Windows.Forms.Button();
            this.labelOpenCvStatus = new System.Windows.Forms.Label();
            this.buttonLoadImageInViewer = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelImageInfo = new System.Windows.Forms.Label();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageImageViewer = new System.Windows.Forms.TabPage();
            this.labelWorkspace = new System.Windows.Forms.Label();
            this.panelImageViewport = new System.Windows.Forms.Panel();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.tabPageBinarization = new System.Windows.Forms.TabPage();
            this.tabPageBinarization2 = new System.Windows.Forms.TabPage();
            this.panelDualThresholdOriginal = new System.Windows.Forms.Panel();
            this.labelDualThresholdOriginal = new System.Windows.Forms.Label();
            this.panelDualThresholdOriginalViewport = new System.Windows.Forms.Panel();
            this.pictureBoxDualThresholdOriginal = new System.Windows.Forms.PictureBox();
            this.panelDualThresholdPreview = new System.Windows.Forms.Panel();
            this.labelDualThresholdPreview = new System.Windows.Forms.Label();
            this.panelDualThresholdPreviewViewport = new System.Windows.Forms.Panel();
            this.pictureBoxDualThresholdPreview = new System.Windows.Forms.PictureBox();
            this.panelDualThresholdControls = new System.Windows.Forms.Panel();
            this.labelDualThresholdSettings = new System.Windows.Forms.Label();
            this.checkBoxDualThresholdEnabled = new System.Windows.Forms.CheckBox();
            this.labelDualThresholdLower = new System.Windows.Forms.Label();
            this.trackBarDualThresholdLower = new System.Windows.Forms.TrackBar();
            this.numericDualThresholdLower = new System.Windows.Forms.NumericUpDown();
            this.labelDualThresholdUpper = new System.Windows.Forms.Label();
            this.trackBarDualThresholdUpper = new System.Windows.Forms.TrackBar();
            this.numericDualThresholdUpper = new System.Windows.Forms.NumericUpDown();
            this.labelDualThresholdErode = new System.Windows.Forms.Label();
            this.numericDualThresholdErode = new System.Windows.Forms.NumericUpDown();
            this.labelDualThresholdDilate = new System.Windows.Forms.Label();
            this.numericDualThresholdDilate = new System.Windows.Forms.NumericUpDown();
            this.labelDualThresholdOpen = new System.Windows.Forms.Label();
            this.numericDualThresholdOpen = new System.Windows.Forms.NumericUpDown();
            this.labelDualThresholdClose = new System.Windows.Forms.Label();
            this.numericDualThresholdClose = new System.Windows.Forms.NumericUpDown();
            this.panelDualThresholdActions = new System.Windows.Forms.Panel();
            this.buttonDualThresholdLoadSettings = new System.Windows.Forms.Button();
            this.buttonDualThresholdSaveSettings = new System.Windows.Forms.Button();
            this.panelBinaryOriginal = new System.Windows.Forms.Panel();
            this.labelBinaryOriginal = new System.Windows.Forms.Label();
            this.pictureBoxBinaryOriginal = new System.Windows.Forms.PictureBox();
            this.panelActivePreprocess = new System.Windows.Forms.Panel();
            this.labelActivePreprocess = new System.Windows.Forms.Label();
            this.panelActiveViewport = new System.Windows.Forms.Panel();
            this.pictureBoxActivePreprocess = new System.Windows.Forms.PictureBox();
            this.panelPreprocess1 = new System.Windows.Forms.Panel();
            this.labelPreprocess1Title = new System.Windows.Forms.Label();
            this.pictureBoxPreprocess1 = new System.Windows.Forms.PictureBox();
            this.panelPreprocess2 = new System.Windows.Forms.Panel();
            this.labelPreprocess2Title = new System.Windows.Forms.Label();
            this.pictureBoxPreprocess2 = new System.Windows.Forms.PictureBox();
            this.panelPreprocess3 = new System.Windows.Forms.Panel();
            this.labelPreprocess3Title = new System.Windows.Forms.Label();
            this.pictureBoxPreprocess3 = new System.Windows.Forms.PictureBox();
            this.panelPreprocess4 = new System.Windows.Forms.Panel();
            this.labelPreprocess4Title = new System.Windows.Forms.Label();
            this.pictureBoxPreprocess4 = new System.Windows.Forms.PictureBox();
            this.tabControlPreprocess = new System.Windows.Forms.TabControl();
            this.tabPagePreprocess1 = new System.Windows.Forms.TabPage();
            this.checkBoxPreprocess1Enabled = new System.Windows.Forms.CheckBox();
            this.labelThreshold1 = new System.Windows.Forms.Label();
            this.trackBarThreshold1 = new System.Windows.Forms.TrackBar();
            this.numericThreshold1 = new System.Windows.Forms.NumericUpDown();
            this.labelErode1 = new System.Windows.Forms.Label();
            this.numericErode1 = new System.Windows.Forms.NumericUpDown();
            this.labelDilate1 = new System.Windows.Forms.Label();
            this.numericDilate1 = new System.Windows.Forms.NumericUpDown();
            this.labelOpen1 = new System.Windows.Forms.Label();
            this.numericOpen1 = new System.Windows.Forms.NumericUpDown();
            this.labelClose1 = new System.Windows.Forms.Label();
            this.numericClose1 = new System.Windows.Forms.NumericUpDown();
            this.tabPagePreprocess2 = new System.Windows.Forms.TabPage();
            this.checkBoxPreprocess2Enabled = new System.Windows.Forms.CheckBox();
            this.labelThreshold2 = new System.Windows.Forms.Label();
            this.trackBarThreshold2 = new System.Windows.Forms.TrackBar();
            this.numericThreshold2 = new System.Windows.Forms.NumericUpDown();
            this.labelErode2 = new System.Windows.Forms.Label();
            this.numericErode2 = new System.Windows.Forms.NumericUpDown();
            this.labelDilate2 = new System.Windows.Forms.Label();
            this.numericDilate2 = new System.Windows.Forms.NumericUpDown();
            this.labelOpen2 = new System.Windows.Forms.Label();
            this.numericOpen2 = new System.Windows.Forms.NumericUpDown();
            this.labelClose2 = new System.Windows.Forms.Label();
            this.numericClose2 = new System.Windows.Forms.NumericUpDown();
            this.tabPagePreprocess3 = new System.Windows.Forms.TabPage();
            this.checkBoxPreprocess3Enabled = new System.Windows.Forms.CheckBox();
            this.labelThreshold3 = new System.Windows.Forms.Label();
            this.trackBarThreshold3 = new System.Windows.Forms.TrackBar();
            this.numericThreshold3 = new System.Windows.Forms.NumericUpDown();
            this.labelErode3 = new System.Windows.Forms.Label();
            this.numericErode3 = new System.Windows.Forms.NumericUpDown();
            this.labelDilate3 = new System.Windows.Forms.Label();
            this.numericDilate3 = new System.Windows.Forms.NumericUpDown();
            this.labelOpen3 = new System.Windows.Forms.Label();
            this.numericOpen3 = new System.Windows.Forms.NumericUpDown();
            this.labelClose3 = new System.Windows.Forms.Label();
            this.numericClose3 = new System.Windows.Forms.NumericUpDown();
            this.tabPagePreprocess4 = new System.Windows.Forms.TabPage();
            this.checkBoxPreprocess4Enabled = new System.Windows.Forms.CheckBox();
            this.labelThreshold4 = new System.Windows.Forms.Label();
            this.trackBarThreshold4 = new System.Windows.Forms.TrackBar();
            this.numericThreshold4 = new System.Windows.Forms.NumericUpDown();
            this.labelErode4 = new System.Windows.Forms.Label();
            this.numericErode4 = new System.Windows.Forms.NumericUpDown();
            this.labelDilate4 = new System.Windows.Forms.Label();
            this.numericDilate4 = new System.Windows.Forms.NumericUpDown();
            this.labelOpen4 = new System.Windows.Forms.Label();
            this.numericOpen4 = new System.Windows.Forms.NumericUpDown();
            this.labelClose4 = new System.Windows.Forms.Label();
            this.numericClose4 = new System.Windows.Forms.NumericUpDown();
            this.panelPreprocessActions = new System.Windows.Forms.Panel();
            this.buttonLoadSavedSettings = new System.Windows.Forms.Button();
            this.buttonSaveCurrentSettings = new System.Windows.Forms.Button();
            this.tabPageReferenceCorner = new System.Windows.Forms.TabPage();
            this.panelReferenceCornerControls = new System.Windows.Forms.Panel();
            this.checkBoxReferenceCornerEnabled = new System.Windows.Forms.CheckBox();
            this.labelReferenceSource = new System.Windows.Forms.Label();
            this.comboBoxReferenceSource = new System.Windows.Forms.ComboBox();
            this.buttonSaveReferenceRoi = new System.Windows.Forms.Button();
            this.panelReferencePreview = new System.Windows.Forms.Panel();
            this.pictureBoxReferencePreview = new System.Windows.Forms.PictureBox();
            this.labelReferenceCornerStatus = new System.Windows.Forms.Label();
            this.tabPageMeasureDistance = new System.Windows.Forms.TabPage();
            this.tabPageInnerSettings = new System.Windows.Forms.TabPage();
            this.labelInnerSettingsTitle = new System.Windows.Forms.Label();
            this.panelInnerSettings = new System.Windows.Forms.Panel();
            this.labelInnerCcdXPrecision = new System.Windows.Forms.Label();
            this.numericInnerCcdXPrecision = new System.Windows.Forms.NumericUpDown();
            this.labelInnerCcdYPrecision = new System.Windows.Forms.Label();
            this.numericInnerCcdYPrecision = new System.Windows.Forms.NumericUpDown();
            this.labelInnerMeasurementScaleFactor = new System.Windows.Forms.Label();
            this.numericInnerMeasurementScaleFactor = new System.Windows.Forms.NumericUpDown();
            this.buttonSaveInnerSettings = new System.Windows.Forms.Button();
            this.tabPageJudgementCriteria = new System.Windows.Forms.TabPage();
            this.tabPageDetectionParameterSummary = new System.Windows.Forms.TabPage();
            this.tabPageContinuousInspection = new System.Windows.Forms.TabPage();
            this.panelContinuousInspection = new System.Windows.Forms.Panel();
            this.labelContinuousInspectionTitle = new System.Windows.Forms.Label();
            this.labelContinuousInspectionHint = new System.Windows.Forms.Label();
            this.labelContinuousInspectionMainParameter = new System.Windows.Forms.Label();
            this.comboBoxContinuousInspectionMainParameter = new System.Windows.Forms.ComboBox();
            this.groupBoxContinuousInspection1 = new System.Windows.Forms.GroupBox();
            this.checkBoxContinuousInspectionSaveOriginalImage1 = new System.Windows.Forms.CheckBox();
            this.labelContinuousInspectionYield1 = new System.Windows.Forms.Label();
            this.buttonContinuousInspectionJudge1 = new System.Windows.Forms.Button();
            this.labelContinuousInspectionResult1 = new System.Windows.Forms.Label();
            this.buttonContinuousInspectionResetYield = new System.Windows.Forms.Button();
            this.buttonContinuousInspectionLoadImage1 = new System.Windows.Forms.Button();
            this.panelContinuousInspectionPreview1 = new System.Windows.Forms.Panel();
            this.pictureBoxContinuousInspection1 = new System.Windows.Forms.PictureBox();
            this.labelContinuousInspectionSubParameter1 = new System.Windows.Forms.Label();
            this.groupBoxContinuousInspection2 = new System.Windows.Forms.GroupBox();
            this.checkBoxContinuousInspectionSaveOriginalImage2 = new System.Windows.Forms.CheckBox();
            this.labelContinuousInspectionYield2 = new System.Windows.Forms.Label();
            this.buttonContinuousInspectionJudge2 = new System.Windows.Forms.Button();
            this.labelContinuousInspectionResult2 = new System.Windows.Forms.Label();
            this.buttonContinuousInspectionLoadImage2 = new System.Windows.Forms.Button();
            this.panelContinuousInspectionPreview2 = new System.Windows.Forms.Panel();
            this.pictureBoxContinuousInspection2 = new System.Windows.Forms.PictureBox();
            this.labelContinuousInspectionSubParameter2 = new System.Windows.Forms.Label();
            this.groupBoxContinuousInspection3 = new System.Windows.Forms.GroupBox();
            this.checkBoxContinuousInspectionSaveOriginalImage3 = new System.Windows.Forms.CheckBox();
            this.labelContinuousInspectionYield3 = new System.Windows.Forms.Label();
            this.buttonContinuousInspectionJudge3 = new System.Windows.Forms.Button();
            this.labelContinuousInspectionResult3 = new System.Windows.Forms.Label();
            this.buttonContinuousInspectionLoadImage3 = new System.Windows.Forms.Button();
            this.panelContinuousInspectionPreview3 = new System.Windows.Forms.Panel();
            this.pictureBoxContinuousInspection3 = new System.Windows.Forms.PictureBox();
            this.labelContinuousInspectionSubParameter3 = new System.Windows.Forms.Label();
            this.panelDetectionParameterSummary = new System.Windows.Forms.Panel();
            this.groupBoxDetectionParameterCreate = new System.Windows.Forms.GroupBox();
            this.buttonDetectionMainParameterConfirm = new System.Windows.Forms.Button();
            this.textBoxDetectionMainParameterName = new System.Windows.Forms.TextBox();
            this.groupBoxDetectionMainParameterList = new System.Windows.Forms.GroupBox();
            this.listBoxDetectionMainParameter = new System.Windows.Forms.ListBox();
            this.buttonDetectionMainParameterMoveUp = new System.Windows.Forms.Button();
            this.buttonDetectionMainParameterMoveDown = new System.Windows.Forms.Button();
            this.buttonDetectionMainParameterSaveOrder = new System.Windows.Forms.Button();
            this.groupBoxDetectionSubParameter1List = new System.Windows.Forms.GroupBox();
            this.listBoxDetectionSubParameter1 = new System.Windows.Forms.ListBox();
            this.buttonDetectionSubParameter1MoveUp = new System.Windows.Forms.Button();
            this.buttonDetectionSubParameter1MoveDown = new System.Windows.Forms.Button();
            this.buttonDetectionSubParameter1SaveOrder = new System.Windows.Forms.Button();
            this.checkBoxDetectionSubParameter1Enabled = new System.Windows.Forms.CheckBox();
            this.groupBoxDetectionSubParameter2List = new System.Windows.Forms.GroupBox();
            this.listBoxDetectionSubParameter2 = new System.Windows.Forms.ListBox();
            this.buttonDetectionSubParameter2MoveUp = new System.Windows.Forms.Button();
            this.buttonDetectionSubParameter2MoveDown = new System.Windows.Forms.Button();
            this.buttonDetectionSubParameter2SaveOrder = new System.Windows.Forms.Button();
            this.checkBoxDetectionSubParameter2Enabled = new System.Windows.Forms.CheckBox();
            this.groupBoxDetectionSubParameter3List = new System.Windows.Forms.GroupBox();
            this.listBoxDetectionSubParameter3 = new System.Windows.Forms.ListBox();
            this.buttonDetectionSubParameter3MoveUp = new System.Windows.Forms.Button();
            this.buttonDetectionSubParameter3MoveDown = new System.Windows.Forms.Button();
            this.buttonDetectionSubParameter3SaveOrder = new System.Windows.Forms.Button();
            this.checkBoxDetectionSubParameter3Enabled = new System.Windows.Forms.CheckBox();
            this.buttonDetectionSaveParameterReference = new System.Windows.Forms.Button();
            this.labelJudgementCriteriaTitle = new System.Windows.Forms.Label();
            this.buttonJudgementSyntaxHelp = new System.Windows.Forms.Button();
            this.panelJudgementCriteria = new System.Windows.Forms.Panel();
            this.labelJudgementName = new System.Windows.Forms.Label();
            this.textBoxJudgementName = new System.Windows.Forms.TextBox();
            this.labelJudgementCalculation = new System.Windows.Forms.Label();
            this.textBoxJudgementCalculation = new System.Windows.Forms.TextBox();
            this.labelJudgementSpec = new System.Windows.Forms.Label();
            this.textBoxJudgementSpec = new System.Windows.Forms.TextBox();
            this.labelJudgementCalculationB = new System.Windows.Forms.Label();
            this.textBoxJudgementCalculationB = new System.Windows.Forms.TextBox();
            this.labelJudgementSpecB = new System.Windows.Forms.Label();
            this.textBoxJudgementSpecB = new System.Windows.Forms.TextBox();
            this.buttonJudgementAdd = new System.Windows.Forms.Button();
            this.buttonJudgementReset = new System.Windows.Forms.Button();
            this.buttonJudgementSave = new System.Windows.Forms.Button();
            this.buttonJudgementMoveUp = new System.Windows.Forms.Button();
            this.buttonJudgementMoveDown = new System.Windows.Forms.Button();
            this.dataGridViewJudgementCriteria = new System.Windows.Forms.DataGridView();
            this.panelMeasureSource = new System.Windows.Forms.Panel();
            this.labelMeasureSource = new System.Windows.Forms.Label();
            this.comboBoxMeasureSource = new System.Windows.Forms.ComboBox();
            this.buttonParallelMeasure = new System.Windows.Forms.Button();
            this.buttonPerpendicularMeasure = new System.Windows.Forms.Button();
            this.buttonSaveMeasurePoint = new System.Windows.Forms.Button();
            this.buttonClearMeasurePoint = new System.Windows.Forms.Button();
            this.labelMeasureStatus = new System.Windows.Forms.Label();
            this.panelMeasurePreview = new System.Windows.Forms.Panel();
            this.pictureBoxMeasurePreview = new System.Windows.Forms.PictureBox();
            this.dataGridViewMeasureRecords = new System.Windows.Forms.DataGridView();
            this.buttonSaveMeasureRecords = new System.Windows.Forms.Button();
            this.tabPageMultiImageConfirm = new System.Windows.Forms.TabPage();
            this.buttonLoadMultiImageFolder = new System.Windows.Forms.Button();
            this.groupBoxMultiImagePreviewSource = new System.Windows.Forms.GroupBox();
            this.comboBoxMultiImagePreviewSource = new System.Windows.Forms.ComboBox();
            this.buttonLoadMultiImagePreprocess = new System.Windows.Forms.Button();
            this.buttonLoadMultiImageOriginal = new System.Windows.Forms.Button();
            this.labelMultiImageStatus = new System.Windows.Forms.Label();
            this.buttonMultiImagePrev = new System.Windows.Forms.Button();
            this.buttonMultiImageNext = new System.Windows.Forms.Button();
            this.buttonMultiImageLineSequence = new System.Windows.Forms.Button();
            this.comboBoxMultiImageLineDisplayMode = new System.Windows.Forms.ComboBox();
            this.panelMultiImageInfo = new System.Windows.Forms.Panel();
            this.tabControlMultiImageInfo = new System.Windows.Forms.TabControl();
            this.tabPageMultiImageRawData = new System.Windows.Forms.TabPage();
            this.tabPageMultiImageJudgementResult = new System.Windows.Forms.TabPage();
            this.dataGridViewMultiImageInfo = new System.Windows.Forms.DataGridView();
            this.panelMultiImageConfirmViewport = new System.Windows.Forms.Panel();
            this.pictureBoxMultiImageConfirm = new System.Windows.Forms.PictureBox();
            this.openFileDialogImage = new System.Windows.Forms.OpenFileDialog();
            this.menuStripMain.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageImageViewer.SuspendLayout();
            this.panelImageViewport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.tabPageBinarization.SuspendLayout();
            this.tabPageBinarization2.SuspendLayout();
            this.panelDualThresholdOriginal.SuspendLayout();
            this.panelDualThresholdOriginalViewport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDualThresholdOriginal)).BeginInit();
            this.panelDualThresholdPreview.SuspendLayout();
            this.panelDualThresholdPreviewViewport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDualThresholdPreview)).BeginInit();
            this.panelDualThresholdControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDualThresholdLower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDualThresholdLower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDualThresholdUpper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDualThresholdUpper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDualThresholdErode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDualThresholdDilate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDualThresholdOpen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDualThresholdClose)).BeginInit();
            this.panelDualThresholdActions.SuspendLayout();
            this.panelBinaryOriginal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBinaryOriginal)).BeginInit();
            this.panelActivePreprocess.SuspendLayout();
            this.panelActiveViewport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxActivePreprocess)).BeginInit();
            this.panelPreprocess1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreprocess1)).BeginInit();
            this.panelPreprocess2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreprocess2)).BeginInit();
            this.panelPreprocess3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreprocess3)).BeginInit();
            this.panelPreprocess4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreprocess4)).BeginInit();
            this.tabControlPreprocess.SuspendLayout();
            this.tabPagePreprocess1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericThreshold1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericErode1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDilate1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOpen1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericClose1)).BeginInit();
            this.tabPagePreprocess2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericThreshold2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericErode2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDilate2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOpen2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericClose2)).BeginInit();
            this.tabPagePreprocess3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericThreshold3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericErode3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDilate3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOpen3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericClose3)).BeginInit();
            this.tabPagePreprocess4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericThreshold4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericErode4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDilate4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOpen4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericClose4)).BeginInit();
            this.panelPreprocessActions.SuspendLayout();
            this.tabPageReferenceCorner.SuspendLayout();
            this.panelReferenceCornerControls.SuspendLayout();
            this.panelReferencePreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxReferencePreview)).BeginInit();
            this.tabPageContinuousInspection.SuspendLayout();
            this.panelContinuousInspection.SuspendLayout();
            this.groupBoxContinuousInspection1.SuspendLayout();
            this.panelContinuousInspectionPreview1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxContinuousInspection1)).BeginInit();
            this.groupBoxContinuousInspection2.SuspendLayout();
            this.panelContinuousInspectionPreview2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxContinuousInspection2)).BeginInit();
            this.groupBoxContinuousInspection3.SuspendLayout();
            this.panelContinuousInspectionPreview3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxContinuousInspection3)).BeginInit();
            this.tabPageMultiImageConfirm.SuspendLayout();
            this.groupBoxMultiImagePreviewSource.SuspendLayout();
            this.panelMultiImageInfo.SuspendLayout();
            this.tabControlMultiImageInfo.SuspendLayout();
            this.tabPageMultiImageRawData.SuspendLayout();
            this.tabPageMultiImageJudgementResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMultiImageInfo)).BeginInit();
            this.panelMultiImageConfirmViewport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMultiImageConfirm)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.menuStripMain.Font = new System.Drawing.Font("Microsoft JhengHei UI", 11F);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuEdit,
            this.menuView,
            this.menuHelp});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Padding = new System.Windows.Forms.Padding(18, 8, 0, 8);
            this.menuStripMain.Size = new System.Drawing.Size(1280, 39);
            this.menuStripMain.TabIndex = 2;
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOpenImage,
            this.menuFileSeparator,
            this.menuExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(51, 23);
            this.menuFile.Text = "檔案";
            // 
            // menuOpenImage
            // 
            this.menuOpenImage.Name = "menuOpenImage";
            this.menuOpenImage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuOpenImage.Size = new System.Drawing.Size(195, 24);
            this.menuOpenImage.Text = "讀取圖片";
            this.menuOpenImage.Click += new System.EventHandler(this.LoadImageButton_Click);
            // 
            // menuFileSeparator
            // 
            this.menuFileSeparator.Name = "menuFileSeparator";
            this.menuFileSeparator.Size = new System.Drawing.Size(192, 6);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(195, 24);
            this.menuExit.Text = "結束";
            this.menuExit.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // menuEdit
            // 
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(51, 23);
            this.menuEdit.Text = "編輯";
            // 
            // menuView
            // 
            this.menuView.Name = "menuView";
            this.menuView.Size = new System.Drawing.Size(51, 23);
            this.menuView.Text = "檢視";
            // 
            // menuHelp
            // 
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(51, 23);
            this.menuHelp.Text = "說明";
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(245)))));
            this.panelSidebar.Controls.Add(this.labelAppName);
            this.panelSidebar.Controls.Add(this.labelNavigation);
            this.panelSidebar.Controls.Add(this.buttonLoadImage);
            this.panelSidebar.Controls.Add(this.buttonReferenceCorner);
            this.panelSidebar.Controls.Add(this.buttonMeasureDistance);
            this.panelSidebar.Controls.Add(this.buttonMultiImageConfirm);
            this.panelSidebar.Controls.Add(this.buttonInnerSettings);
            this.panelSidebar.Controls.Add(this.buttonJudgementCriteria);
            this.panelSidebar.Controls.Add(this.buttonDetectionParameterSummary);
            this.panelSidebar.Controls.Add(this.buttonContinuousInspection);
            this.panelSidebar.Controls.Add(this.labelOpenCvStatus);
            this.panelSidebar.Location = new System.Drawing.Point(0, 42);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(240, 758);
            this.panelSidebar.TabIndex = 1;
            // 
            // labelAppName
            // 
            this.labelAppName.AutoSize = true;
            this.labelAppName.Font = new System.Drawing.Font("Microsoft JhengHei UI", 16F, System.Drawing.FontStyle.Bold);
            this.labelAppName.Location = new System.Drawing.Point(20, 25);
            this.labelAppName.Name = "labelAppName";
            this.labelAppName.Size = new System.Drawing.Size(133, 28);
            this.labelAppName.TabIndex = 0;
            this.labelAppName.Text = "AOI Viewer";
            // 
            // labelNavigation
            // 
            this.labelNavigation.AutoSize = true;
            this.labelNavigation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(149)))), ((int)(((byte)(153)))));
            this.labelNavigation.Location = new System.Drawing.Point(20, 83);
            this.labelNavigation.Name = "labelNavigation";
            this.labelNavigation.Size = new System.Drawing.Size(53, 12);
            this.labelNavigation.TabIndex = 1;
            this.labelNavigation.Text = "工作項目";
            // 
            // buttonLoadImage
            // 
            this.buttonLoadImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonLoadImage.FlatAppearance.BorderSize = 0;
            this.buttonLoadImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLoadImage.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F);
            this.buttonLoadImage.Location = new System.Drawing.Point(16, 116);
            this.buttonLoadImage.Name = "buttonLoadImage";
            this.buttonLoadImage.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.buttonLoadImage.Size = new System.Drawing.Size(208, 48);
            this.buttonLoadImage.TabIndex = 2;
            this.buttonLoadImage.Text = "+   圖片檢視";
            this.buttonLoadImage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLoadImage.UseVisualStyleBackColor = false;
            this.buttonLoadImage.Click += new System.EventHandler(this.SidebarImageViewerButton_Click);
            // 
            // buttonReferenceCorner
            // 
            this.buttonReferenceCorner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonReferenceCorner.FlatAppearance.BorderSize = 0;
            this.buttonReferenceCorner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReferenceCorner.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F);
            this.buttonReferenceCorner.Location = new System.Drawing.Point(16, 172);
            this.buttonReferenceCorner.Name = "buttonReferenceCorner";
            this.buttonReferenceCorner.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.buttonReferenceCorner.Size = new System.Drawing.Size(208, 48);
            this.buttonReferenceCorner.TabIndex = 3;
            this.buttonReferenceCorner.Text = "+   參考角點";
            this.buttonReferenceCorner.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonReferenceCorner.UseVisualStyleBackColor = false;
            this.buttonReferenceCorner.Click += new System.EventHandler(this.ReferenceCornerButton_Click);
            // 
            // buttonMeasureDistance
            // 
            this.buttonMeasureDistance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonMeasureDistance.FlatAppearance.BorderSize = 0;
            this.buttonMeasureDistance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMeasureDistance.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F);
            this.buttonMeasureDistance.Location = new System.Drawing.Point(16, 228);
            this.buttonMeasureDistance.Name = "buttonMeasureDistance";
            this.buttonMeasureDistance.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.buttonMeasureDistance.Size = new System.Drawing.Size(208, 48);
            this.buttonMeasureDistance.TabIndex = 4;
            this.buttonMeasureDistance.Text = "+   框選量測的距離";
            this.buttonMeasureDistance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMeasureDistance.UseVisualStyleBackColor = false;
            this.buttonMeasureDistance.Click += new System.EventHandler(this.MeasureDistanceButton_Click);
            // 
            // buttonMultiImageConfirm
            // 
            this.buttonMultiImageConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonMultiImageConfirm.FlatAppearance.BorderSize = 0;
            this.buttonMultiImageConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMultiImageConfirm.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F);
            this.buttonMultiImageConfirm.Location = new System.Drawing.Point(16, 284);
            this.buttonMultiImageConfirm.Name = "buttonMultiImageConfirm";
            this.buttonMultiImageConfirm.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.buttonMultiImageConfirm.Size = new System.Drawing.Size(208, 48);
            this.buttonMultiImageConfirm.TabIndex = 5;
            this.buttonMultiImageConfirm.Text = "+   多影像確認結果";
            this.buttonMultiImageConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMultiImageConfirm.UseVisualStyleBackColor = false;
            this.buttonMultiImageConfirm.Click += new System.EventHandler(this.MultiImageConfirmButton_Click);
            // 
            // buttonInnerSettings
            // 
            this.buttonInnerSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonInnerSettings.FlatAppearance.BorderSize = 0;
            this.buttonInnerSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonInnerSettings.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F);
            this.buttonInnerSettings.Location = new System.Drawing.Point(16, 340);
            this.buttonInnerSettings.Name = "buttonInnerSettings";
            this.buttonInnerSettings.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.buttonInnerSettings.Size = new System.Drawing.Size(208, 48);
            this.buttonInnerSettings.TabIndex = 6;
            this.buttonInnerSettings.Text = "+   內部參數";
            this.buttonInnerSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonInnerSettings.UseVisualStyleBackColor = false;
            this.buttonInnerSettings.Click += new System.EventHandler(this.InnerSettingsButton_Click);
            // 
            // buttonJudgementCriteria
            // 
            this.buttonJudgementCriteria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonJudgementCriteria.FlatAppearance.BorderSize = 0;
            this.buttonJudgementCriteria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonJudgementCriteria.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F);
            this.buttonJudgementCriteria.Location = new System.Drawing.Point(16, 396);
            this.buttonJudgementCriteria.Name = "buttonJudgementCriteria";
            this.buttonJudgementCriteria.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.buttonJudgementCriteria.Size = new System.Drawing.Size(208, 48);
            this.buttonJudgementCriteria.TabIndex = 7;
            this.buttonJudgementCriteria.Text = "+   良品判斷條件";
            this.buttonJudgementCriteria.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonJudgementCriteria.UseVisualStyleBackColor = false;
            this.buttonJudgementCriteria.Click += new System.EventHandler(this.JudgementCriteriaButton_Click);
            // 
            // buttonDetectionParameterSummary
            // 
            this.buttonDetectionParameterSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonDetectionParameterSummary.FlatAppearance.BorderSize = 0;
            this.buttonDetectionParameterSummary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDetectionParameterSummary.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F);
            this.buttonDetectionParameterSummary.Location = new System.Drawing.Point(16, 452);
            this.buttonDetectionParameterSummary.Name = "buttonDetectionParameterSummary";
            this.buttonDetectionParameterSummary.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.buttonDetectionParameterSummary.Size = new System.Drawing.Size(208, 48);
            this.buttonDetectionParameterSummary.TabIndex = 8;
            this.buttonDetectionParameterSummary.Text = "+   檢測參數整理";
            this.buttonDetectionParameterSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDetectionParameterSummary.UseVisualStyleBackColor = false;
            this.buttonDetectionParameterSummary.Click += new System.EventHandler(this.DetectionParameterSummaryButton_Click);
            // 
            // buttonContinuousInspection
            // 
            this.buttonContinuousInspection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonContinuousInspection.FlatAppearance.BorderSize = 0;
            this.buttonContinuousInspection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonContinuousInspection.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F);
            this.buttonContinuousInspection.Location = new System.Drawing.Point(16, 508);
            this.buttonContinuousInspection.Name = "buttonContinuousInspection";
            this.buttonContinuousInspection.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.buttonContinuousInspection.Size = new System.Drawing.Size(208, 48);
            this.buttonContinuousInspection.TabIndex = 9;
            this.buttonContinuousInspection.Text = "+   連續檢測";
            this.buttonContinuousInspection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonContinuousInspection.UseVisualStyleBackColor = false;
            this.buttonContinuousInspection.Click += new System.EventHandler(this.ContinuousInspectionButton_Click);
            // 
            // labelOpenCvStatus
            // 
            this.labelOpenCvStatus.AutoSize = true;
            this.labelOpenCvStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(115)))), ((int)(((byte)(120)))));
            this.labelOpenCvStatus.Location = new System.Drawing.Point(24, 706);
            this.labelOpenCvStatus.Name = "labelOpenCvStatus";
            this.labelOpenCvStatus.Size = new System.Drawing.Size(109, 12);
            this.labelOpenCvStatus.TabIndex = 5;
            this.labelOpenCvStatus.Text = "OpenCV 4.13 已就緒";
            // 
            // buttonLoadImageInViewer
            // 
            this.buttonLoadImageInViewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonLoadImageInViewer.FlatAppearance.BorderSize = 0;
            this.buttonLoadImageInViewer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLoadImageInViewer.Location = new System.Drawing.Point(856, 575);
            this.buttonLoadImageInViewer.Name = "buttonLoadImageInViewer";
            this.buttonLoadImageInViewer.Size = new System.Drawing.Size(168, 40);
            this.buttonLoadImageInViewer.TabIndex = 2;
            this.buttonLoadImageInViewer.Text = "讀取圖片";
            this.buttonLoadImageInViewer.UseVisualStyleBackColor = false;
            this.buttonLoadImageInViewer.Click += new System.EventHandler(this.LoadImageButton_Click);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelMain.Controls.Add(this.panelHeader);
            this.panelMain.Controls.Add(this.tabControlMain);
            this.panelMain.Location = new System.Drawing.Point(240, 42);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1040, 758);
            this.panelMain.TabIndex = 0;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.labelTitle);
            this.panelHeader.Controls.Add(this.labelImageInfo);
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1040, 72);
            this.panelHeader.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft JhengHei UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelTitle.Location = new System.Drawing.Point(28, 23);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(127, 24);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "AOI 影像工具";
            // 
            // labelImageInfo
            // 
            this.labelImageInfo.AutoSize = true;
            this.labelImageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(129)))), ((int)(((byte)(134)))));
            this.labelImageInfo.Location = new System.Drawing.Point(190, 28);
            this.labelImageInfo.Name = "labelImageInfo";
            this.labelImageInfo.Size = new System.Drawing.Size(77, 12);
            this.labelImageInfo.TabIndex = 1;
            this.labelImageInfo.Text = "尚未讀取圖片";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageImageViewer);
            this.tabControlMain.Controls.Add(this.tabPageBinarization);
            this.tabControlMain.Controls.Add(this.tabPageBinarization2);
            this.tabControlMain.Controls.Add(this.tabPageReferenceCorner);
            this.tabControlMain.Controls.Add(this.tabPageMeasureDistance);
            this.tabControlMain.Controls.Add(this.tabPageMultiImageConfirm);
            this.tabControlMain.Controls.Add(this.tabPageInnerSettings);
            this.tabControlMain.Controls.Add(this.tabPageJudgementCriteria);
            this.tabControlMain.Controls.Add(this.tabPageDetectionParameterSummary);
            this.tabControlMain.Controls.Add(this.tabPageContinuousInspection);
            this.tabControlMain.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F);
            this.tabControlMain.Location = new System.Drawing.Point(0, 72);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1040, 686);
            this.tabControlMain.TabIndex = 1;
            // 
            // tabPageImageViewer
            // 
            this.tabPageImageViewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.tabPageImageViewer.Controls.Add(this.labelWorkspace);
            this.tabPageImageViewer.Controls.Add(this.panelImageViewport);
            this.tabPageImageViewer.Controls.Add(this.buttonLoadImageInViewer);
            this.tabPageImageViewer.Location = new System.Drawing.Point(4, 26);
            this.tabPageImageViewer.Name = "tabPageImageViewer";
            this.tabPageImageViewer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageImageViewer.Size = new System.Drawing.Size(1032, 656);
            this.tabPageImageViewer.TabIndex = 0;
            this.tabPageImageViewer.Text = "圖片檢視";
            // 
            // labelWorkspace
            // 
            this.labelWorkspace.AutoSize = true;
            this.labelWorkspace.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(134)))), ((int)(((byte)(138)))));
            this.labelWorkspace.Location = new System.Drawing.Point(31, 21);
            this.labelWorkspace.Name = "labelWorkspace";
            this.labelWorkspace.Size = new System.Drawing.Size(64, 18);
            this.labelWorkspace.TabIndex = 0;
            this.labelWorkspace.Text = "原始圖片";
            // 
            // panelImageViewport
            // 
            this.panelImageViewport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.panelImageViewport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelImageViewport.Controls.Add(this.pictureBoxImage);
            this.panelImageViewport.Location = new System.Drawing.Point(28, 46);
            this.panelImageViewport.Name = "panelImageViewport";
            this.panelImageViewport.Size = new System.Drawing.Size(820, 570);
            this.panelImageViewport.TabIndex = 1;
            this.panelImageViewport.MouseEnter += new System.EventHandler(this.ImageViewport_MouseEnter);
            this.panelImageViewport.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.PictureBoxImage_MouseWheel);
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.pictureBoxImage.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(818, 568);
            this.pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxImage.TabIndex = 0;
            this.pictureBoxImage.TabStop = false;
            this.pictureBoxImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBoxImage_MouseDown);
            this.pictureBoxImage.MouseEnter += new System.EventHandler(this.ImageViewport_MouseEnter);
            this.pictureBoxImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBoxImage_MouseMove);
            this.pictureBoxImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBoxImage_MouseUp);
            this.pictureBoxImage.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.PictureBoxImage_MouseWheel);
            // 
            // tabPageBinarization
            // 
            this.tabPageBinarization.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.tabPageBinarization.Controls.Add(this.panelBinaryOriginal);
            this.tabPageBinarization.Controls.Add(this.panelActivePreprocess);
            this.tabPageBinarization.Controls.Add(this.panelPreprocess1);
            this.tabPageBinarization.Controls.Add(this.panelPreprocess2);
            this.tabPageBinarization.Controls.Add(this.panelPreprocess3);
            this.tabPageBinarization.Controls.Add(this.panelPreprocess4);
            this.tabPageBinarization.Controls.Add(this.tabControlPreprocess);
            this.tabPageBinarization.Controls.Add(this.panelPreprocessActions);
            this.tabPageBinarization.Location = new System.Drawing.Point(4, 26);
            this.tabPageBinarization.Name = "tabPageBinarization";
            this.tabPageBinarization.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBinarization.Size = new System.Drawing.Size(1032, 656);
            this.tabPageBinarization.TabIndex = 1;
            this.tabPageBinarization.Text = "二值化處理";
            // 
            // tabPageBinarization2
            // 
            this.tabPageBinarization2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.tabPageBinarization2.Controls.Add(this.panelDualThresholdOriginal);
            this.tabPageBinarization2.Controls.Add(this.panelDualThresholdPreview);
            this.tabPageBinarization2.Controls.Add(this.panelDualThresholdControls);
            this.tabPageBinarization2.Controls.Add(this.panelDualThresholdActions);
            this.tabPageBinarization2.Location = new System.Drawing.Point(4, 26);
            this.tabPageBinarization2.Name = "tabPageBinarization2";
            this.tabPageBinarization2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBinarization2.Size = new System.Drawing.Size(1032, 656);
            this.tabPageBinarization2.TabIndex = 6;
            this.tabPageBinarization2.Text = "二值化處理-2";
            // 
            // panelDualThresholdOriginal
            // 
            this.panelDualThresholdOriginal.BackColor = System.Drawing.Color.White;
            this.panelDualThresholdOriginal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDualThresholdOriginal.Controls.Add(this.labelDualThresholdOriginal);
            this.panelDualThresholdOriginal.Controls.Add(this.panelDualThresholdOriginalViewport);
            this.panelDualThresholdOriginal.Location = new System.Drawing.Point(20, 20);
            this.panelDualThresholdOriginal.Name = "panelDualThresholdOriginal";
            this.panelDualThresholdOriginal.Size = new System.Drawing.Size(300, 280);
            this.panelDualThresholdOriginal.TabIndex = 0;
            // 
            // labelDualThresholdOriginal
            // 
            this.labelDualThresholdOriginal.AutoSize = true;
            this.labelDualThresholdOriginal.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelDualThresholdOriginal.Location = new System.Drawing.Point(10, 8);
            this.labelDualThresholdOriginal.Name = "labelDualThresholdOriginal";
            this.labelDualThresholdOriginal.Size = new System.Drawing.Size(64, 18);
            this.labelDualThresholdOriginal.TabIndex = 0;
            this.labelDualThresholdOriginal.Text = "原始影像";
            // 
            // panelDualThresholdOriginalViewport
            // 
            this.panelDualThresholdOriginalViewport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(236)))));
            this.panelDualThresholdOriginalViewport.Controls.Add(this.pictureBoxDualThresholdOriginal);
            this.panelDualThresholdOriginalViewport.Location = new System.Drawing.Point(10, 34);
            this.panelDualThresholdOriginalViewport.Name = "panelDualThresholdOriginalViewport";
            this.panelDualThresholdOriginalViewport.Size = new System.Drawing.Size(278, 234);
            this.panelDualThresholdOriginalViewport.TabIndex = 1;
            this.panelDualThresholdOriginalViewport.TabStop = true;
            // 
            // pictureBoxDualThresholdOriginal
            // 
            this.pictureBoxDualThresholdOriginal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(236)))));
            this.pictureBoxDualThresholdOriginal.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxDualThresholdOriginal.Name = "pictureBoxDualThresholdOriginal";
            this.pictureBoxDualThresholdOriginal.Size = new System.Drawing.Size(278, 234);
            this.pictureBoxDualThresholdOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxDualThresholdOriginal.TabIndex = 0;
            this.pictureBoxDualThresholdOriginal.TabStop = false;
            // 
            // panelDualThresholdPreview
            // 
            this.panelDualThresholdPreview.BackColor = System.Drawing.Color.White;
            this.panelDualThresholdPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDualThresholdPreview.Controls.Add(this.labelDualThresholdPreview);
            this.panelDualThresholdPreview.Controls.Add(this.panelDualThresholdPreviewViewport);
            this.panelDualThresholdPreview.Location = new System.Drawing.Point(366, 20);
            this.panelDualThresholdPreview.Name = "panelDualThresholdPreview";
            this.panelDualThresholdPreview.Size = new System.Drawing.Size(300, 280);
            this.panelDualThresholdPreview.TabIndex = 1;
            // 
            // labelDualThresholdPreview
            // 
            this.labelDualThresholdPreview.AutoSize = true;
            this.labelDualThresholdPreview.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelDualThresholdPreview.Location = new System.Drawing.Point(10, 8);
            this.labelDualThresholdPreview.Name = "labelDualThresholdPreview";
            this.labelDualThresholdPreview.Size = new System.Drawing.Size(286, 18);
            this.labelDualThresholdPreview.TabIndex = 0;
            this.labelDualThresholdPreview.Text = "雙門檻結果｜滾輪縮放、左鍵拖曳、右鍵看原圖";
            // 
            // panelDualThresholdPreviewViewport
            // 
            this.panelDualThresholdPreviewViewport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(236)))));
            this.panelDualThresholdPreviewViewport.Controls.Add(this.pictureBoxDualThresholdPreview);
            this.panelDualThresholdPreviewViewport.Location = new System.Drawing.Point(10, 34);
            this.panelDualThresholdPreviewViewport.Name = "panelDualThresholdPreviewViewport";
            this.panelDualThresholdPreviewViewport.Size = new System.Drawing.Size(278, 234);
            this.panelDualThresholdPreviewViewport.TabIndex = 1;
            this.panelDualThresholdPreviewViewport.TabStop = true;
            // 
            // pictureBoxDualThresholdPreview
            // 
            this.pictureBoxDualThresholdPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(236)))));
            this.pictureBoxDualThresholdPreview.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxDualThresholdPreview.Name = "pictureBoxDualThresholdPreview";
            this.pictureBoxDualThresholdPreview.Size = new System.Drawing.Size(278, 234);
            this.pictureBoxDualThresholdPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxDualThresholdPreview.TabIndex = 0;
            this.pictureBoxDualThresholdPreview.TabStop = false;
            // 
            // panelDualThresholdControls
            // 
            this.panelDualThresholdControls.BackColor = System.Drawing.Color.White;
            this.panelDualThresholdControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDualThresholdControls.Controls.Add(this.labelDualThresholdSettings);
            this.panelDualThresholdControls.Controls.Add(this.checkBoxDualThresholdEnabled);
            this.panelDualThresholdControls.Controls.Add(this.labelDualThresholdLower);
            this.panelDualThresholdControls.Controls.Add(this.trackBarDualThresholdLower);
            this.panelDualThresholdControls.Controls.Add(this.numericDualThresholdLower);
            this.panelDualThresholdControls.Controls.Add(this.labelDualThresholdUpper);
            this.panelDualThresholdControls.Controls.Add(this.trackBarDualThresholdUpper);
            this.panelDualThresholdControls.Controls.Add(this.numericDualThresholdUpper);
            this.panelDualThresholdControls.Controls.Add(this.labelDualThresholdErode);
            this.panelDualThresholdControls.Controls.Add(this.numericDualThresholdErode);
            this.panelDualThresholdControls.Controls.Add(this.labelDualThresholdDilate);
            this.panelDualThresholdControls.Controls.Add(this.numericDualThresholdDilate);
            this.panelDualThresholdControls.Controls.Add(this.labelDualThresholdOpen);
            this.panelDualThresholdControls.Controls.Add(this.numericDualThresholdOpen);
            this.panelDualThresholdControls.Controls.Add(this.labelDualThresholdClose);
            this.panelDualThresholdControls.Controls.Add(this.numericDualThresholdClose);
            this.panelDualThresholdControls.Location = new System.Drawing.Point(712, 20);
            this.panelDualThresholdControls.Name = "panelDualThresholdControls";
            this.panelDualThresholdControls.Size = new System.Drawing.Size(300, 280);
            this.panelDualThresholdControls.TabIndex = 2;
            // 
            // labelDualThresholdSettings
            // 
            this.labelDualThresholdSettings.AutoSize = true;
            this.labelDualThresholdSettings.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelDualThresholdSettings.Location = new System.Drawing.Point(12, 10);
            this.labelDualThresholdSettings.Name = "labelDualThresholdSettings";
            this.labelDualThresholdSettings.Size = new System.Drawing.Size(78, 18);
            this.labelDualThresholdSettings.TabIndex = 0;
            this.labelDualThresholdSettings.Text = "雙門檻設定";
            // 
            // checkBoxDualThresholdEnabled
            // 
            this.checkBoxDualThresholdEnabled.AutoSize = true;
            this.checkBoxDualThresholdEnabled.Checked = true;
            this.checkBoxDualThresholdEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDualThresholdEnabled.Location = new System.Drawing.Point(16, 40);
            this.checkBoxDualThresholdEnabled.Name = "checkBoxDualThresholdEnabled";
            this.checkBoxDualThresholdEnabled.Size = new System.Drawing.Size(54, 22);
            this.checkBoxDualThresholdEnabled.TabIndex = 1;
            this.checkBoxDualThresholdEnabled.Text = "啟用";
            this.checkBoxDualThresholdEnabled.UseVisualStyleBackColor = true;
            // 
            // labelDualThresholdLower
            // 
            this.labelDualThresholdLower.AutoSize = true;
            this.labelDualThresholdLower.Location = new System.Drawing.Point(16, 74);
            this.labelDualThresholdLower.Name = "labelDualThresholdLower";
            this.labelDualThresholdLower.Size = new System.Drawing.Size(50, 18);
            this.labelDualThresholdLower.TabIndex = 2;
            this.labelDualThresholdLower.Text = "下門檻";
            // 
            // trackBarDualThresholdLower
            // 
            this.trackBarDualThresholdLower.AutoSize = false;
            this.trackBarDualThresholdLower.Location = new System.Drawing.Point(86, 68);
            this.trackBarDualThresholdLower.Maximum = 255;
            this.trackBarDualThresholdLower.Name = "trackBarDualThresholdLower";
            this.trackBarDualThresholdLower.Size = new System.Drawing.Size(140, 30);
            this.trackBarDualThresholdLower.TabIndex = 3;
            this.trackBarDualThresholdLower.TickFrequency = 5;
            this.trackBarDualThresholdLower.Value = 80;
            // 
            // numericDualThresholdLower
            // 
            this.numericDualThresholdLower.Location = new System.Drawing.Point(232, 70);
            this.numericDualThresholdLower.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericDualThresholdLower.Name = "numericDualThresholdLower";
            this.numericDualThresholdLower.Size = new System.Drawing.Size(52, 25);
            this.numericDualThresholdLower.TabIndex = 4;
            this.numericDualThresholdLower.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // labelDualThresholdUpper
            // 
            this.labelDualThresholdUpper.AutoSize = true;
            this.labelDualThresholdUpper.Location = new System.Drawing.Point(16, 114);
            this.labelDualThresholdUpper.Name = "labelDualThresholdUpper";
            this.labelDualThresholdUpper.Size = new System.Drawing.Size(50, 18);
            this.labelDualThresholdUpper.TabIndex = 5;
            this.labelDualThresholdUpper.Text = "上門檻";
            // 
            // trackBarDualThresholdUpper
            // 
            this.trackBarDualThresholdUpper.AutoSize = false;
            this.trackBarDualThresholdUpper.Location = new System.Drawing.Point(86, 108);
            this.trackBarDualThresholdUpper.Maximum = 255;
            this.trackBarDualThresholdUpper.Name = "trackBarDualThresholdUpper";
            this.trackBarDualThresholdUpper.Size = new System.Drawing.Size(140, 30);
            this.trackBarDualThresholdUpper.TabIndex = 6;
            this.trackBarDualThresholdUpper.TickFrequency = 5;
            this.trackBarDualThresholdUpper.Value = 180;
            // 
            // numericDualThresholdUpper
            // 
            this.numericDualThresholdUpper.Location = new System.Drawing.Point(232, 110);
            this.numericDualThresholdUpper.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericDualThresholdUpper.Name = "numericDualThresholdUpper";
            this.numericDualThresholdUpper.Size = new System.Drawing.Size(52, 25);
            this.numericDualThresholdUpper.TabIndex = 7;
            this.numericDualThresholdUpper.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            // 
            // labelDualThresholdErode
            // 
            this.labelDualThresholdErode.AutoSize = true;
            this.labelDualThresholdErode.Location = new System.Drawing.Point(16, 160);
            this.labelDualThresholdErode.Name = "labelDualThresholdErode";
            this.labelDualThresholdErode.Size = new System.Drawing.Size(36, 18);
            this.labelDualThresholdErode.TabIndex = 8;
            this.labelDualThresholdErode.Text = "侵蝕";
            // 
            // numericDualThresholdErode
            // 
            this.numericDualThresholdErode.Location = new System.Drawing.Point(86, 156);
            this.numericDualThresholdErode.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericDualThresholdErode.Name = "numericDualThresholdErode";
            this.numericDualThresholdErode.Size = new System.Drawing.Size(52, 25);
            this.numericDualThresholdErode.TabIndex = 9;
            // 
            // labelDualThresholdDilate
            // 
            this.labelDualThresholdDilate.AutoSize = true;
            this.labelDualThresholdDilate.Location = new System.Drawing.Point(156, 160);
            this.labelDualThresholdDilate.Name = "labelDualThresholdDilate";
            this.labelDualThresholdDilate.Size = new System.Drawing.Size(36, 18);
            this.labelDualThresholdDilate.TabIndex = 10;
            this.labelDualThresholdDilate.Text = "膨脹";
            // 
            // numericDualThresholdDilate
            // 
            this.numericDualThresholdDilate.Location = new System.Drawing.Point(226, 156);
            this.numericDualThresholdDilate.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericDualThresholdDilate.Name = "numericDualThresholdDilate";
            this.numericDualThresholdDilate.Size = new System.Drawing.Size(52, 25);
            this.numericDualThresholdDilate.TabIndex = 11;
            // 
            // labelDualThresholdOpen
            // 
            this.labelDualThresholdOpen.AutoSize = true;
            this.labelDualThresholdOpen.Location = new System.Drawing.Point(16, 200);
            this.labelDualThresholdOpen.Name = "labelDualThresholdOpen";
            this.labelDualThresholdOpen.Size = new System.Drawing.Size(50, 18);
            this.labelDualThresholdOpen.TabIndex = 12;
            this.labelDualThresholdOpen.Text = "開運算";
            // 
            // numericDualThresholdOpen
            // 
            this.numericDualThresholdOpen.Location = new System.Drawing.Point(86, 196);
            this.numericDualThresholdOpen.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericDualThresholdOpen.Name = "numericDualThresholdOpen";
            this.numericDualThresholdOpen.Size = new System.Drawing.Size(52, 25);
            this.numericDualThresholdOpen.TabIndex = 13;
            // 
            // labelDualThresholdClose
            // 
            this.labelDualThresholdClose.AutoSize = true;
            this.labelDualThresholdClose.Location = new System.Drawing.Point(156, 200);
            this.labelDualThresholdClose.Name = "labelDualThresholdClose";
            this.labelDualThresholdClose.Size = new System.Drawing.Size(50, 18);
            this.labelDualThresholdClose.TabIndex = 14;
            this.labelDualThresholdClose.Text = "閉運算";
            // 
            // numericDualThresholdClose
            // 
            this.numericDualThresholdClose.Location = new System.Drawing.Point(226, 196);
            this.numericDualThresholdClose.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericDualThresholdClose.Name = "numericDualThresholdClose";
            this.numericDualThresholdClose.Size = new System.Drawing.Size(52, 25);
            this.numericDualThresholdClose.TabIndex = 15;
            // 
            // panelDualThresholdActions
            // 
            this.panelDualThresholdActions.BackColor = System.Drawing.Color.Transparent;
            this.panelDualThresholdActions.Controls.Add(this.buttonDualThresholdLoadSettings);
            this.panelDualThresholdActions.Controls.Add(this.buttonDualThresholdSaveSettings);
            this.panelDualThresholdActions.Location = new System.Drawing.Point(20, 574);
            this.panelDualThresholdActions.Name = "panelDualThresholdActions";
            this.panelDualThresholdActions.Size = new System.Drawing.Size(370, 58);
            this.panelDualThresholdActions.TabIndex = 3;
            // 
            // buttonDualThresholdLoadSettings
            // 
            this.buttonDualThresholdLoadSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonDualThresholdLoadSettings.FlatAppearance.BorderSize = 0;
            this.buttonDualThresholdLoadSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDualThresholdLoadSettings.Location = new System.Drawing.Point(12, 9);
            this.buttonDualThresholdLoadSettings.Name = "buttonDualThresholdLoadSettings";
            this.buttonDualThresholdLoadSettings.Size = new System.Drawing.Size(168, 40);
            this.buttonDualThresholdLoadSettings.TabIndex = 0;
            this.buttonDualThresholdLoadSettings.Text = "讀取設定";
            this.buttonDualThresholdLoadSettings.UseVisualStyleBackColor = false;
            // 
            // buttonDualThresholdSaveSettings
            // 
            this.buttonDualThresholdSaveSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonDualThresholdSaveSettings.FlatAppearance.BorderSize = 0;
            this.buttonDualThresholdSaveSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDualThresholdSaveSettings.Location = new System.Drawing.Point(190, 9);
            this.buttonDualThresholdSaveSettings.Name = "buttonDualThresholdSaveSettings";
            this.buttonDualThresholdSaveSettings.Size = new System.Drawing.Size(168, 40);
            this.buttonDualThresholdSaveSettings.TabIndex = 1;
            this.buttonDualThresholdSaveSettings.Text = "儲存目前設定";
            this.buttonDualThresholdSaveSettings.UseVisualStyleBackColor = false;
            // 
            // panelBinaryOriginal
            // 
            this.panelBinaryOriginal.BackColor = System.Drawing.Color.White;
            this.panelBinaryOriginal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBinaryOriginal.Controls.Add(this.labelBinaryOriginal);
            this.panelBinaryOriginal.Controls.Add(this.pictureBoxBinaryOriginal);
            this.panelBinaryOriginal.Location = new System.Drawing.Point(20, 20);
            this.panelBinaryOriginal.Name = "panelBinaryOriginal";
            this.panelBinaryOriginal.Size = new System.Drawing.Size(300, 280);
            this.panelBinaryOriginal.TabIndex = 0;
            // 
            // labelBinaryOriginal
            // 
            this.labelBinaryOriginal.AutoSize = true;
            this.labelBinaryOriginal.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelBinaryOriginal.Location = new System.Drawing.Point(10, 8);
            this.labelBinaryOriginal.Name = "labelBinaryOriginal";
            this.labelBinaryOriginal.Size = new System.Drawing.Size(36, 18);
            this.labelBinaryOriginal.TabIndex = 0;
            this.labelBinaryOriginal.Text = "原圖";
            // 
            // pictureBoxBinaryOriginal
            // 
            this.pictureBoxBinaryOriginal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(236)))));
            this.pictureBoxBinaryOriginal.Location = new System.Drawing.Point(10, 34);
            this.pictureBoxBinaryOriginal.Name = "pictureBoxBinaryOriginal";
            this.pictureBoxBinaryOriginal.Size = new System.Drawing.Size(278, 234);
            this.pictureBoxBinaryOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxBinaryOriginal.TabIndex = 1;
            this.pictureBoxBinaryOriginal.TabStop = false;
            // 
            // panelActivePreprocess
            // 
            this.panelActivePreprocess.BackColor = System.Drawing.Color.White;
            this.panelActivePreprocess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelActivePreprocess.Controls.Add(this.labelActivePreprocess);
            this.panelActivePreprocess.Controls.Add(this.panelActiveViewport);
            this.panelActivePreprocess.Location = new System.Drawing.Point(366, 20);
            this.panelActivePreprocess.Name = "panelActivePreprocess";
            this.panelActivePreprocess.Size = new System.Drawing.Size(300, 280);
            this.panelActivePreprocess.TabIndex = 1;
            // 
            // labelActivePreprocess
            // 
            this.labelActivePreprocess.AutoSize = true;
            this.labelActivePreprocess.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelActivePreprocess.Location = new System.Drawing.Point(10, 8);
            this.labelActivePreprocess.Name = "labelActivePreprocess";
            this.labelActivePreprocess.Size = new System.Drawing.Size(286, 18);
            this.labelActivePreprocess.TabIndex = 0;
            this.labelActivePreprocess.Text = "前處理 1｜滾輪縮放、左鍵拖曳、右鍵看原圖";
            // 
            // panelActiveViewport
            // 
            this.panelActiveViewport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(236)))));
            this.panelActiveViewport.Controls.Add(this.pictureBoxActivePreprocess);
            this.panelActiveViewport.Location = new System.Drawing.Point(10, 34);
            this.panelActiveViewport.Name = "panelActiveViewport";
            this.panelActiveViewport.Size = new System.Drawing.Size(278, 234);
            this.panelActiveViewport.TabIndex = 1;
            this.panelActiveViewport.MouseEnter += new System.EventHandler(this.ActivePreview_MouseEnter);
            this.panelActiveViewport.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ActivePreprocess_MouseWheel);
            // 
            // pictureBoxActivePreprocess
            // 
            this.pictureBoxActivePreprocess.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(236)))));
            this.pictureBoxActivePreprocess.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxActivePreprocess.Name = "pictureBoxActivePreprocess";
            this.pictureBoxActivePreprocess.Size = new System.Drawing.Size(278, 234);
            this.pictureBoxActivePreprocess.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxActivePreprocess.TabIndex = 0;
            this.pictureBoxActivePreprocess.TabStop = false;
            this.pictureBoxActivePreprocess.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ActivePreprocess_MouseDown);
            this.pictureBoxActivePreprocess.MouseEnter += new System.EventHandler(this.ActivePreview_MouseEnter);
            this.pictureBoxActivePreprocess.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ActivePreprocess_MouseMove);
            this.pictureBoxActivePreprocess.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ActivePreprocess_MouseUp);
            this.pictureBoxActivePreprocess.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ActivePreprocess_MouseWheel);
            // 
            // panelPreprocess1
            // 
            this.panelPreprocess1.BackColor = System.Drawing.Color.White;
            this.panelPreprocess1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPreprocess1.Controls.Add(this.labelPreprocess1Title);
            this.panelPreprocess1.Controls.Add(this.pictureBoxPreprocess1);
            this.panelPreprocess1.Location = new System.Drawing.Point(20, 330);
            this.panelPreprocess1.Name = "panelPreprocess1";
            this.panelPreprocess1.Size = new System.Drawing.Size(230, 220);
            this.panelPreprocess1.TabIndex = 2;
            this.panelPreprocess1.Tag = 0;
            this.panelPreprocess1.Click += new System.EventHandler(this.PreprocessThumbnail_Click);
            // 
            // labelPreprocess1Title
            // 
            this.labelPreprocess1Title.AutoSize = true;
            this.labelPreprocess1Title.Location = new System.Drawing.Point(8, 6);
            this.labelPreprocess1Title.Name = "labelPreprocess1Title";
            this.labelPreprocess1Title.Size = new System.Drawing.Size(162, 18);
            this.labelPreprocess1Title.TabIndex = 0;
            this.labelPreprocess1Title.Text = "處理 1  白物件  Gray > T";
            // 
            // pictureBoxPreprocess1
            // 
            this.pictureBoxPreprocess1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(236)))));
            this.pictureBoxPreprocess1.Location = new System.Drawing.Point(8, 28);
            this.pictureBoxPreprocess1.Name = "pictureBoxPreprocess1";
            this.pictureBoxPreprocess1.Size = new System.Drawing.Size(212, 180);
            this.pictureBoxPreprocess1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPreprocess1.TabIndex = 1;
            this.pictureBoxPreprocess1.TabStop = false;
            this.pictureBoxPreprocess1.Tag = 0;
            this.pictureBoxPreprocess1.Click += new System.EventHandler(this.PreprocessThumbnail_Click);
            // 
            // panelPreprocess2
            // 
            this.panelPreprocess2.BackColor = System.Drawing.Color.White;
            this.panelPreprocess2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPreprocess2.Controls.Add(this.labelPreprocess2Title);
            this.panelPreprocess2.Controls.Add(this.pictureBoxPreprocess2);
            this.panelPreprocess2.Location = new System.Drawing.Point(270, 330);
            this.panelPreprocess2.Name = "panelPreprocess2";
            this.panelPreprocess2.Size = new System.Drawing.Size(230, 220);
            this.panelPreprocess2.TabIndex = 3;
            this.panelPreprocess2.Tag = 1;
            this.panelPreprocess2.Click += new System.EventHandler(this.PreprocessThumbnail_Click);
            // 
            // labelPreprocess2Title
            // 
            this.labelPreprocess2Title.AutoSize = true;
            this.labelPreprocess2Title.Location = new System.Drawing.Point(8, 6);
            this.labelPreprocess2Title.Name = "labelPreprocess2Title";
            this.labelPreprocess2Title.Size = new System.Drawing.Size(162, 18);
            this.labelPreprocess2Title.TabIndex = 0;
            this.labelPreprocess2Title.Text = "處理 2  白物件  Gray > T";
            // 
            // pictureBoxPreprocess2
            // 
            this.pictureBoxPreprocess2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(236)))));
            this.pictureBoxPreprocess2.Location = new System.Drawing.Point(8, 28);
            this.pictureBoxPreprocess2.Name = "pictureBoxPreprocess2";
            this.pictureBoxPreprocess2.Size = new System.Drawing.Size(212, 180);
            this.pictureBoxPreprocess2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPreprocess2.TabIndex = 1;
            this.pictureBoxPreprocess2.TabStop = false;
            this.pictureBoxPreprocess2.Tag = 1;
            this.pictureBoxPreprocess2.Click += new System.EventHandler(this.PreprocessThumbnail_Click);
            // 
            // panelPreprocess3
            // 
            this.panelPreprocess3.BackColor = System.Drawing.Color.White;
            this.panelPreprocess3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPreprocess3.Controls.Add(this.labelPreprocess3Title);
            this.panelPreprocess3.Controls.Add(this.pictureBoxPreprocess3);
            this.panelPreprocess3.Location = new System.Drawing.Point(520, 330);
            this.panelPreprocess3.Name = "panelPreprocess3";
            this.panelPreprocess3.Size = new System.Drawing.Size(230, 220);
            this.panelPreprocess3.TabIndex = 4;
            this.panelPreprocess3.Tag = 2;
            this.panelPreprocess3.Click += new System.EventHandler(this.PreprocessThumbnail_Click);
            // 
            // labelPreprocess3Title
            // 
            this.labelPreprocess3Title.AutoSize = true;
            this.labelPreprocess3Title.Location = new System.Drawing.Point(8, 6);
            this.labelPreprocess3Title.Name = "labelPreprocess3Title";
            this.labelPreprocess3Title.Size = new System.Drawing.Size(162, 18);
            this.labelPreprocess3Title.TabIndex = 0;
            this.labelPreprocess3Title.Text = "處理 3  黑物件  Gray < T";
            // 
            // pictureBoxPreprocess3
            // 
            this.pictureBoxPreprocess3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(236)))));
            this.pictureBoxPreprocess3.Location = new System.Drawing.Point(8, 28);
            this.pictureBoxPreprocess3.Name = "pictureBoxPreprocess3";
            this.pictureBoxPreprocess3.Size = new System.Drawing.Size(212, 180);
            this.pictureBoxPreprocess3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPreprocess3.TabIndex = 1;
            this.pictureBoxPreprocess3.TabStop = false;
            this.pictureBoxPreprocess3.Tag = 2;
            this.pictureBoxPreprocess3.Click += new System.EventHandler(this.PreprocessThumbnail_Click);
            // 
            // panelPreprocess4
            // 
            this.panelPreprocess4.BackColor = System.Drawing.Color.White;
            this.panelPreprocess4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPreprocess4.Controls.Add(this.labelPreprocess4Title);
            this.panelPreprocess4.Controls.Add(this.pictureBoxPreprocess4);
            this.panelPreprocess4.Location = new System.Drawing.Point(770, 330);
            this.panelPreprocess4.Name = "panelPreprocess4";
            this.panelPreprocess4.Size = new System.Drawing.Size(230, 220);
            this.panelPreprocess4.TabIndex = 5;
            this.panelPreprocess4.Tag = 3;
            this.panelPreprocess4.Click += new System.EventHandler(this.PreprocessThumbnail_Click);
            // 
            // labelPreprocess4Title
            // 
            this.labelPreprocess4Title.AutoSize = true;
            this.labelPreprocess4Title.Location = new System.Drawing.Point(8, 6);
            this.labelPreprocess4Title.Name = "labelPreprocess4Title";
            this.labelPreprocess4Title.Size = new System.Drawing.Size(162, 18);
            this.labelPreprocess4Title.TabIndex = 0;
            this.labelPreprocess4Title.Text = "處理 4  黑物件  Gray < T";
            // 
            // pictureBoxPreprocess4
            // 
            this.pictureBoxPreprocess4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(236)))));
            this.pictureBoxPreprocess4.Location = new System.Drawing.Point(8, 28);
            this.pictureBoxPreprocess4.Name = "pictureBoxPreprocess4";
            this.pictureBoxPreprocess4.Size = new System.Drawing.Size(212, 180);
            this.pictureBoxPreprocess4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPreprocess4.TabIndex = 1;
            this.pictureBoxPreprocess4.TabStop = false;
            this.pictureBoxPreprocess4.Tag = 3;
            this.pictureBoxPreprocess4.Click += new System.EventHandler(this.PreprocessThumbnail_Click);
            // 
            // tabControlPreprocess
            // 
            this.tabControlPreprocess.Controls.Add(this.tabPagePreprocess1);
            this.tabControlPreprocess.Controls.Add(this.tabPagePreprocess2);
            this.tabControlPreprocess.Controls.Add(this.tabPagePreprocess3);
            this.tabControlPreprocess.Controls.Add(this.tabPagePreprocess4);
            this.tabControlPreprocess.ItemSize = new System.Drawing.Size(136, 24);
            this.tabControlPreprocess.Location = new System.Drawing.Point(712, 20);
            this.tabControlPreprocess.Multiline = true;
            this.tabControlPreprocess.Name = "tabControlPreprocess";
            this.tabControlPreprocess.SelectedIndex = 0;
            this.tabControlPreprocess.Size = new System.Drawing.Size(300, 280);
            this.tabControlPreprocess.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlPreprocess.TabIndex = 6;
            // 
            // tabPagePreprocess1
            // 
            this.tabPagePreprocess1.BackColor = System.Drawing.Color.White;
            this.tabPagePreprocess1.Controls.Add(this.checkBoxPreprocess1Enabled);
            this.tabPagePreprocess1.Controls.Add(this.labelThreshold1);
            this.tabPagePreprocess1.Controls.Add(this.trackBarThreshold1);
            this.tabPagePreprocess1.Controls.Add(this.numericThreshold1);
            this.tabPagePreprocess1.Controls.Add(this.labelErode1);
            this.tabPagePreprocess1.Controls.Add(this.numericErode1);
            this.tabPagePreprocess1.Controls.Add(this.labelDilate1);
            this.tabPagePreprocess1.Controls.Add(this.numericDilate1);
            this.tabPagePreprocess1.Controls.Add(this.labelOpen1);
            this.tabPagePreprocess1.Controls.Add(this.numericOpen1);
            this.tabPagePreprocess1.Controls.Add(this.labelClose1);
            this.tabPagePreprocess1.Controls.Add(this.numericClose1);
            this.tabPagePreprocess1.Location = new System.Drawing.Point(4, 52);
            this.tabPagePreprocess1.Name = "tabPagePreprocess1";
            this.tabPagePreprocess1.Size = new System.Drawing.Size(292, 224);
            this.tabPagePreprocess1.TabIndex = 0;
            this.tabPagePreprocess1.Text = "前處理 1 - 白物件";
            // 
            // checkBoxPreprocess1Enabled
            // 
            this.checkBoxPreprocess1Enabled.AutoSize = true;
            this.checkBoxPreprocess1Enabled.Checked = true;
            this.checkBoxPreprocess1Enabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPreprocess1Enabled.Location = new System.Drawing.Point(16, 15);
            this.checkBoxPreprocess1Enabled.Name = "checkBoxPreprocess1Enabled";
            this.checkBoxPreprocess1Enabled.Size = new System.Drawing.Size(125, 22);
            this.checkBoxPreprocess1Enabled.TabIndex = 0;
            this.checkBoxPreprocess1Enabled.Text = "啟用此組前處理";
            this.checkBoxPreprocess1Enabled.CheckedChanged += new System.EventHandler(this.PreprocessEnabled_CheckedChanged);
            // 
            // labelThreshold1
            // 
            this.labelThreshold1.AutoSize = true;
            this.labelThreshold1.Location = new System.Drawing.Point(16, 54);
            this.labelThreshold1.Name = "labelThreshold1";
            this.labelThreshold1.Size = new System.Drawing.Size(75, 18);
            this.labelThreshold1.TabIndex = 1;
            this.labelThreshold1.Text = "Threshold";
            // 
            // trackBarThreshold1
            // 
            this.trackBarThreshold1.Location = new System.Drawing.Point(12, 70);
            this.trackBarThreshold1.Maximum = 255;
            this.trackBarThreshold1.Name = "trackBarThreshold1";
            this.trackBarThreshold1.Size = new System.Drawing.Size(180, 45);
            this.trackBarThreshold1.TabIndex = 2;
            this.trackBarThreshold1.TickFrequency = 32;
            this.trackBarThreshold1.Value = 128;
            this.trackBarThreshold1.Scroll += new System.EventHandler(this.ThresholdTrackBar_Scroll);
            // 
            // numericThreshold1
            // 
            this.numericThreshold1.Location = new System.Drawing.Point(200, 70);
            this.numericThreshold1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericThreshold1.Name = "numericThreshold1";
            this.numericThreshold1.Size = new System.Drawing.Size(72, 24);
            this.numericThreshold1.TabIndex = 3;
            this.numericThreshold1.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.numericThreshold1.ValueChanged += new System.EventHandler(this.ThresholdValue_ValueChanged);
            // 
            // labelErode1
            // 
            this.labelErode1.AutoSize = true;
            this.labelErode1.Location = new System.Drawing.Point(16, 130);
            this.labelErode1.Name = "labelErode1";
            this.labelErode1.Size = new System.Drawing.Size(47, 18);
            this.labelErode1.TabIndex = 4;
            this.labelErode1.Text = "Erode";
            // 
            // numericErode1
            // 
            this.numericErode1.Location = new System.Drawing.Point(88, 125);
            this.numericErode1.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericErode1.Name = "numericErode1";
            this.numericErode1.Size = new System.Drawing.Size(60, 24);
            this.numericErode1.TabIndex = 5;
            this.numericErode1.ValueChanged += new System.EventHandler(this.MorphologyValue_ValueChanged);
            // 
            // labelDilate1
            // 
            this.labelDilate1.AutoSize = true;
            this.labelDilate1.Location = new System.Drawing.Point(160, 130);
            this.labelDilate1.Name = "labelDilate1";
            this.labelDilate1.Size = new System.Drawing.Size(48, 18);
            this.labelDilate1.TabIndex = 6;
            this.labelDilate1.Text = "Dilate";
            // 
            // numericDilate1
            // 
            this.numericDilate1.Location = new System.Drawing.Point(224, 125);
            this.numericDilate1.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericDilate1.Name = "numericDilate1";
            this.numericDilate1.Size = new System.Drawing.Size(60, 24);
            this.numericDilate1.TabIndex = 7;
            this.numericDilate1.ValueChanged += new System.EventHandler(this.MorphologyValue_ValueChanged);
            // 
            // labelOpen1
            // 
            this.labelOpen1.AutoSize = true;
            this.labelOpen1.Location = new System.Drawing.Point(16, 175);
            this.labelOpen1.Name = "labelOpen1";
            this.labelOpen1.Size = new System.Drawing.Size(45, 18);
            this.labelOpen1.TabIndex = 8;
            this.labelOpen1.Text = "Open";
            // 
            // numericOpen1
            // 
            this.numericOpen1.Location = new System.Drawing.Point(88, 170);
            this.numericOpen1.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericOpen1.Name = "numericOpen1";
            this.numericOpen1.Size = new System.Drawing.Size(60, 24);
            this.numericOpen1.TabIndex = 9;
            this.numericOpen1.ValueChanged += new System.EventHandler(this.MorphologyValue_ValueChanged);
            // 
            // labelClose1
            // 
            this.labelClose1.AutoSize = true;
            this.labelClose1.Location = new System.Drawing.Point(160, 175);
            this.labelClose1.Name = "labelClose1";
            this.labelClose1.Size = new System.Drawing.Size(44, 18);
            this.labelClose1.TabIndex = 10;
            this.labelClose1.Text = "Close";
            // 
            // numericClose1
            // 
            this.numericClose1.Location = new System.Drawing.Point(224, 170);
            this.numericClose1.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericClose1.Name = "numericClose1";
            this.numericClose1.Size = new System.Drawing.Size(60, 24);
            this.numericClose1.TabIndex = 11;
            this.numericClose1.ValueChanged += new System.EventHandler(this.MorphologyValue_ValueChanged);
            // 
            // tabPagePreprocess2
            // 
            this.tabPagePreprocess2.BackColor = System.Drawing.Color.White;
            this.tabPagePreprocess2.Controls.Add(this.checkBoxPreprocess2Enabled);
            this.tabPagePreprocess2.Controls.Add(this.labelThreshold2);
            this.tabPagePreprocess2.Controls.Add(this.trackBarThreshold2);
            this.tabPagePreprocess2.Controls.Add(this.numericThreshold2);
            this.tabPagePreprocess2.Controls.Add(this.labelErode2);
            this.tabPagePreprocess2.Controls.Add(this.numericErode2);
            this.tabPagePreprocess2.Controls.Add(this.labelDilate2);
            this.tabPagePreprocess2.Controls.Add(this.numericDilate2);
            this.tabPagePreprocess2.Controls.Add(this.labelOpen2);
            this.tabPagePreprocess2.Controls.Add(this.numericOpen2);
            this.tabPagePreprocess2.Controls.Add(this.labelClose2);
            this.tabPagePreprocess2.Controls.Add(this.numericClose2);
            this.tabPagePreprocess2.Location = new System.Drawing.Point(4, 52);
            this.tabPagePreprocess2.Name = "tabPagePreprocess2";
            this.tabPagePreprocess2.Size = new System.Drawing.Size(292, 224);
            this.tabPagePreprocess2.TabIndex = 1;
            this.tabPagePreprocess2.Text = "前處理 2 - 白物件";
            // 
            // checkBoxPreprocess2Enabled
            // 
            this.checkBoxPreprocess2Enabled.AutoSize = true;
            this.checkBoxPreprocess2Enabled.Checked = true;
            this.checkBoxPreprocess2Enabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPreprocess2Enabled.Location = new System.Drawing.Point(16, 15);
            this.checkBoxPreprocess2Enabled.Name = "checkBoxPreprocess2Enabled";
            this.checkBoxPreprocess2Enabled.Size = new System.Drawing.Size(125, 22);
            this.checkBoxPreprocess2Enabled.TabIndex = 0;
            this.checkBoxPreprocess2Enabled.Text = "啟用此組前處理";
            this.checkBoxPreprocess2Enabled.CheckedChanged += new System.EventHandler(this.PreprocessEnabled_CheckedChanged);
            // 
            // labelThreshold2
            // 
            this.labelThreshold2.AutoSize = true;
            this.labelThreshold2.Location = new System.Drawing.Point(16, 54);
            this.labelThreshold2.Name = "labelThreshold2";
            this.labelThreshold2.Size = new System.Drawing.Size(75, 18);
            this.labelThreshold2.TabIndex = 1;
            this.labelThreshold2.Text = "Threshold";
            // 
            // trackBarThreshold2
            // 
            this.trackBarThreshold2.Location = new System.Drawing.Point(12, 70);
            this.trackBarThreshold2.Maximum = 255;
            this.trackBarThreshold2.Name = "trackBarThreshold2";
            this.trackBarThreshold2.Size = new System.Drawing.Size(180, 45);
            this.trackBarThreshold2.TabIndex = 2;
            this.trackBarThreshold2.TickFrequency = 32;
            this.trackBarThreshold2.Value = 128;
            this.trackBarThreshold2.Scroll += new System.EventHandler(this.ThresholdTrackBar_Scroll);
            // 
            // numericThreshold2
            // 
            this.numericThreshold2.Location = new System.Drawing.Point(200, 70);
            this.numericThreshold2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericThreshold2.Name = "numericThreshold2";
            this.numericThreshold2.Size = new System.Drawing.Size(72, 24);
            this.numericThreshold2.TabIndex = 3;
            this.numericThreshold2.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.numericThreshold2.ValueChanged += new System.EventHandler(this.ThresholdValue_ValueChanged);
            // 
            // labelErode2
            // 
            this.labelErode2.AutoSize = true;
            this.labelErode2.Location = new System.Drawing.Point(16, 130);
            this.labelErode2.Name = "labelErode2";
            this.labelErode2.Size = new System.Drawing.Size(47, 18);
            this.labelErode2.TabIndex = 4;
            this.labelErode2.Text = "Erode";
            // 
            // numericErode2
            // 
            this.numericErode2.Location = new System.Drawing.Point(88, 125);
            this.numericErode2.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericErode2.Name = "numericErode2";
            this.numericErode2.Size = new System.Drawing.Size(60, 24);
            this.numericErode2.TabIndex = 5;
            this.numericErode2.ValueChanged += new System.EventHandler(this.MorphologyValue_ValueChanged);
            // 
            // labelDilate2
            // 
            this.labelDilate2.AutoSize = true;
            this.labelDilate2.Location = new System.Drawing.Point(160, 130);
            this.labelDilate2.Name = "labelDilate2";
            this.labelDilate2.Size = new System.Drawing.Size(48, 18);
            this.labelDilate2.TabIndex = 6;
            this.labelDilate2.Text = "Dilate";
            // 
            // numericDilate2
            // 
            this.numericDilate2.Location = new System.Drawing.Point(224, 125);
            this.numericDilate2.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericDilate2.Name = "numericDilate2";
            this.numericDilate2.Size = new System.Drawing.Size(60, 24);
            this.numericDilate2.TabIndex = 7;
            this.numericDilate2.ValueChanged += new System.EventHandler(this.MorphologyValue_ValueChanged);
            // 
            // labelOpen2
            // 
            this.labelOpen2.AutoSize = true;
            this.labelOpen2.Location = new System.Drawing.Point(16, 175);
            this.labelOpen2.Name = "labelOpen2";
            this.labelOpen2.Size = new System.Drawing.Size(45, 18);
            this.labelOpen2.TabIndex = 8;
            this.labelOpen2.Text = "Open";
            // 
            // numericOpen2
            // 
            this.numericOpen2.Location = new System.Drawing.Point(88, 170);
            this.numericOpen2.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericOpen2.Name = "numericOpen2";
            this.numericOpen2.Size = new System.Drawing.Size(60, 24);
            this.numericOpen2.TabIndex = 9;
            this.numericOpen2.ValueChanged += new System.EventHandler(this.MorphologyValue_ValueChanged);
            // 
            // labelClose2
            // 
            this.labelClose2.AutoSize = true;
            this.labelClose2.Location = new System.Drawing.Point(160, 175);
            this.labelClose2.Name = "labelClose2";
            this.labelClose2.Size = new System.Drawing.Size(44, 18);
            this.labelClose2.TabIndex = 10;
            this.labelClose2.Text = "Close";
            // 
            // numericClose2
            // 
            this.numericClose2.Location = new System.Drawing.Point(224, 170);
            this.numericClose2.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericClose2.Name = "numericClose2";
            this.numericClose2.Size = new System.Drawing.Size(60, 24);
            this.numericClose2.TabIndex = 11;
            this.numericClose2.ValueChanged += new System.EventHandler(this.MorphologyValue_ValueChanged);
            // 
            // tabPagePreprocess3
            // 
            this.tabPagePreprocess3.BackColor = System.Drawing.Color.White;
            this.tabPagePreprocess3.Controls.Add(this.checkBoxPreprocess3Enabled);
            this.tabPagePreprocess3.Controls.Add(this.labelThreshold3);
            this.tabPagePreprocess3.Controls.Add(this.trackBarThreshold3);
            this.tabPagePreprocess3.Controls.Add(this.numericThreshold3);
            this.tabPagePreprocess3.Controls.Add(this.labelErode3);
            this.tabPagePreprocess3.Controls.Add(this.numericErode3);
            this.tabPagePreprocess3.Controls.Add(this.labelDilate3);
            this.tabPagePreprocess3.Controls.Add(this.numericDilate3);
            this.tabPagePreprocess3.Controls.Add(this.labelOpen3);
            this.tabPagePreprocess3.Controls.Add(this.numericOpen3);
            this.tabPagePreprocess3.Controls.Add(this.labelClose3);
            this.tabPagePreprocess3.Controls.Add(this.numericClose3);
            this.tabPagePreprocess3.Location = new System.Drawing.Point(4, 52);
            this.tabPagePreprocess3.Name = "tabPagePreprocess3";
            this.tabPagePreprocess3.Size = new System.Drawing.Size(292, 224);
            this.tabPagePreprocess3.TabIndex = 2;
            this.tabPagePreprocess3.Text = "前處理 3 - 黑物件";
            // 
            // checkBoxPreprocess3Enabled
            // 
            this.checkBoxPreprocess3Enabled.AutoSize = true;
            this.checkBoxPreprocess3Enabled.Checked = true;
            this.checkBoxPreprocess3Enabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPreprocess3Enabled.Location = new System.Drawing.Point(16, 15);
            this.checkBoxPreprocess3Enabled.Name = "checkBoxPreprocess3Enabled";
            this.checkBoxPreprocess3Enabled.Size = new System.Drawing.Size(125, 22);
            this.checkBoxPreprocess3Enabled.TabIndex = 0;
            this.checkBoxPreprocess3Enabled.Text = "啟用此組前處理";
            this.checkBoxPreprocess3Enabled.CheckedChanged += new System.EventHandler(this.PreprocessEnabled_CheckedChanged);
            // 
            // labelThreshold3
            // 
            this.labelThreshold3.AutoSize = true;
            this.labelThreshold3.Location = new System.Drawing.Point(16, 54);
            this.labelThreshold3.Name = "labelThreshold3";
            this.labelThreshold3.Size = new System.Drawing.Size(75, 18);
            this.labelThreshold3.TabIndex = 1;
            this.labelThreshold3.Text = "Threshold";
            // 
            // trackBarThreshold3
            // 
            this.trackBarThreshold3.Location = new System.Drawing.Point(12, 70);
            this.trackBarThreshold3.Maximum = 255;
            this.trackBarThreshold3.Name = "trackBarThreshold3";
            this.trackBarThreshold3.Size = new System.Drawing.Size(180, 45);
            this.trackBarThreshold3.TabIndex = 2;
            this.trackBarThreshold3.TickFrequency = 32;
            this.trackBarThreshold3.Value = 128;
            this.trackBarThreshold3.Scroll += new System.EventHandler(this.ThresholdTrackBar_Scroll);
            // 
            // numericThreshold3
            // 
            this.numericThreshold3.Location = new System.Drawing.Point(200, 70);
            this.numericThreshold3.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericThreshold3.Name = "numericThreshold3";
            this.numericThreshold3.Size = new System.Drawing.Size(72, 24);
            this.numericThreshold3.TabIndex = 3;
            this.numericThreshold3.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.numericThreshold3.ValueChanged += new System.EventHandler(this.ThresholdValue_ValueChanged);
            // 
            // labelErode3
            // 
            this.labelErode3.AutoSize = true;
            this.labelErode3.Location = new System.Drawing.Point(16, 130);
            this.labelErode3.Name = "labelErode3";
            this.labelErode3.Size = new System.Drawing.Size(47, 18);
            this.labelErode3.TabIndex = 4;
            this.labelErode3.Text = "Erode";
            // 
            // numericErode3
            // 
            this.numericErode3.Location = new System.Drawing.Point(88, 125);
            this.numericErode3.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericErode3.Name = "numericErode3";
            this.numericErode3.Size = new System.Drawing.Size(60, 24);
            this.numericErode3.TabIndex = 5;
            this.numericErode3.ValueChanged += new System.EventHandler(this.MorphologyValue_ValueChanged);
            // 
            // labelDilate3
            // 
            this.labelDilate3.AutoSize = true;
            this.labelDilate3.Location = new System.Drawing.Point(160, 130);
            this.labelDilate3.Name = "labelDilate3";
            this.labelDilate3.Size = new System.Drawing.Size(48, 18);
            this.labelDilate3.TabIndex = 6;
            this.labelDilate3.Text = "Dilate";
            // 
            // numericDilate3
            // 
            this.numericDilate3.Location = new System.Drawing.Point(224, 125);
            this.numericDilate3.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericDilate3.Name = "numericDilate3";
            this.numericDilate3.Size = new System.Drawing.Size(60, 24);
            this.numericDilate3.TabIndex = 7;
            this.numericDilate3.ValueChanged += new System.EventHandler(this.MorphologyValue_ValueChanged);
            // 
            // labelOpen3
            // 
            this.labelOpen3.AutoSize = true;
            this.labelOpen3.Location = new System.Drawing.Point(16, 175);
            this.labelOpen3.Name = "labelOpen3";
            this.labelOpen3.Size = new System.Drawing.Size(45, 18);
            this.labelOpen3.TabIndex = 8;
            this.labelOpen3.Text = "Open";
            // 
            // numericOpen3
            // 
            this.numericOpen3.Location = new System.Drawing.Point(88, 170);
            this.numericOpen3.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericOpen3.Name = "numericOpen3";
            this.numericOpen3.Size = new System.Drawing.Size(60, 24);
            this.numericOpen3.TabIndex = 9;
            this.numericOpen3.ValueChanged += new System.EventHandler(this.MorphologyValue_ValueChanged);
            // 
            // labelClose3
            // 
            this.labelClose3.AutoSize = true;
            this.labelClose3.Location = new System.Drawing.Point(160, 175);
            this.labelClose3.Name = "labelClose3";
            this.labelClose3.Size = new System.Drawing.Size(44, 18);
            this.labelClose3.TabIndex = 10;
            this.labelClose3.Text = "Close";
            // 
            // numericClose3
            // 
            this.numericClose3.Location = new System.Drawing.Point(224, 170);
            this.numericClose3.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericClose3.Name = "numericClose3";
            this.numericClose3.Size = new System.Drawing.Size(60, 24);
            this.numericClose3.TabIndex = 11;
            this.numericClose3.ValueChanged += new System.EventHandler(this.MorphologyValue_ValueChanged);
            // 
            // tabPagePreprocess4
            // 
            this.tabPagePreprocess4.BackColor = System.Drawing.Color.White;
            this.tabPagePreprocess4.Controls.Add(this.checkBoxPreprocess4Enabled);
            this.tabPagePreprocess4.Controls.Add(this.labelThreshold4);
            this.tabPagePreprocess4.Controls.Add(this.trackBarThreshold4);
            this.tabPagePreprocess4.Controls.Add(this.numericThreshold4);
            this.tabPagePreprocess4.Controls.Add(this.labelErode4);
            this.tabPagePreprocess4.Controls.Add(this.numericErode4);
            this.tabPagePreprocess4.Controls.Add(this.labelDilate4);
            this.tabPagePreprocess4.Controls.Add(this.numericDilate4);
            this.tabPagePreprocess4.Controls.Add(this.labelOpen4);
            this.tabPagePreprocess4.Controls.Add(this.numericOpen4);
            this.tabPagePreprocess4.Controls.Add(this.labelClose4);
            this.tabPagePreprocess4.Controls.Add(this.numericClose4);
            this.tabPagePreprocess4.Location = new System.Drawing.Point(4, 52);
            this.tabPagePreprocess4.Name = "tabPagePreprocess4";
            this.tabPagePreprocess4.Size = new System.Drawing.Size(292, 224);
            this.tabPagePreprocess4.TabIndex = 3;
            this.tabPagePreprocess4.Text = "前處理 4 - 黑物件";
            // 
            // checkBoxPreprocess4Enabled
            // 
            this.checkBoxPreprocess4Enabled.AutoSize = true;
            this.checkBoxPreprocess4Enabled.Checked = true;
            this.checkBoxPreprocess4Enabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPreprocess4Enabled.Location = new System.Drawing.Point(16, 15);
            this.checkBoxPreprocess4Enabled.Name = "checkBoxPreprocess4Enabled";
            this.checkBoxPreprocess4Enabled.Size = new System.Drawing.Size(125, 22);
            this.checkBoxPreprocess4Enabled.TabIndex = 0;
            this.checkBoxPreprocess4Enabled.Text = "啟用此組前處理";
            this.checkBoxPreprocess4Enabled.CheckedChanged += new System.EventHandler(this.PreprocessEnabled_CheckedChanged);
            // 
            // labelThreshold4
            // 
            this.labelThreshold4.AutoSize = true;
            this.labelThreshold4.Location = new System.Drawing.Point(16, 54);
            this.labelThreshold4.Name = "labelThreshold4";
            this.labelThreshold4.Size = new System.Drawing.Size(75, 18);
            this.labelThreshold4.TabIndex = 1;
            this.labelThreshold4.Text = "Threshold";
            // 
            // trackBarThreshold4
            // 
            this.trackBarThreshold4.Location = new System.Drawing.Point(12, 70);
            this.trackBarThreshold4.Maximum = 255;
            this.trackBarThreshold4.Name = "trackBarThreshold4";
            this.trackBarThreshold4.Size = new System.Drawing.Size(180, 45);
            this.trackBarThreshold4.TabIndex = 2;
            this.trackBarThreshold4.TickFrequency = 32;
            this.trackBarThreshold4.Value = 128;
            this.trackBarThreshold4.Scroll += new System.EventHandler(this.ThresholdTrackBar_Scroll);
            // 
            // numericThreshold4
            // 
            this.numericThreshold4.Location = new System.Drawing.Point(200, 70);
            this.numericThreshold4.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericThreshold4.Name = "numericThreshold4";
            this.numericThreshold4.Size = new System.Drawing.Size(72, 24);
            this.numericThreshold4.TabIndex = 3;
            this.numericThreshold4.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.numericThreshold4.ValueChanged += new System.EventHandler(this.ThresholdValue_ValueChanged);
            // 
            // labelErode4
            // 
            this.labelErode4.AutoSize = true;
            this.labelErode4.Location = new System.Drawing.Point(16, 130);
            this.labelErode4.Name = "labelErode4";
            this.labelErode4.Size = new System.Drawing.Size(47, 18);
            this.labelErode4.TabIndex = 4;
            this.labelErode4.Text = "Erode";
            // 
            // numericErode4
            // 
            this.numericErode4.Location = new System.Drawing.Point(88, 125);
            this.numericErode4.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericErode4.Name = "numericErode4";
            this.numericErode4.Size = new System.Drawing.Size(60, 24);
            this.numericErode4.TabIndex = 5;
            this.numericErode4.ValueChanged += new System.EventHandler(this.MorphologyValue_ValueChanged);
            // 
            // labelDilate4
            // 
            this.labelDilate4.AutoSize = true;
            this.labelDilate4.Location = new System.Drawing.Point(160, 130);
            this.labelDilate4.Name = "labelDilate4";
            this.labelDilate4.Size = new System.Drawing.Size(48, 18);
            this.labelDilate4.TabIndex = 6;
            this.labelDilate4.Text = "Dilate";
            // 
            // numericDilate4
            // 
            this.numericDilate4.Location = new System.Drawing.Point(224, 125);
            this.numericDilate4.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericDilate4.Name = "numericDilate4";
            this.numericDilate4.Size = new System.Drawing.Size(60, 24);
            this.numericDilate4.TabIndex = 7;
            this.numericDilate4.ValueChanged += new System.EventHandler(this.MorphologyValue_ValueChanged);
            // 
            // labelOpen4
            // 
            this.labelOpen4.AutoSize = true;
            this.labelOpen4.Location = new System.Drawing.Point(16, 175);
            this.labelOpen4.Name = "labelOpen4";
            this.labelOpen4.Size = new System.Drawing.Size(45, 18);
            this.labelOpen4.TabIndex = 8;
            this.labelOpen4.Text = "Open";
            // 
            // numericOpen4
            // 
            this.numericOpen4.Location = new System.Drawing.Point(88, 170);
            this.numericOpen4.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericOpen4.Name = "numericOpen4";
            this.numericOpen4.Size = new System.Drawing.Size(60, 24);
            this.numericOpen4.TabIndex = 9;
            this.numericOpen4.ValueChanged += new System.EventHandler(this.MorphologyValue_ValueChanged);
            // 
            // labelClose4
            // 
            this.labelClose4.AutoSize = true;
            this.labelClose4.Location = new System.Drawing.Point(160, 175);
            this.labelClose4.Name = "labelClose4";
            this.labelClose4.Size = new System.Drawing.Size(44, 18);
            this.labelClose4.TabIndex = 10;
            this.labelClose4.Text = "Close";
            // 
            // numericClose4
            // 
            this.numericClose4.Location = new System.Drawing.Point(224, 170);
            this.numericClose4.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericClose4.Name = "numericClose4";
            this.numericClose4.Size = new System.Drawing.Size(60, 24);
            this.numericClose4.TabIndex = 11;
            this.numericClose4.ValueChanged += new System.EventHandler(this.MorphologyValue_ValueChanged);
            // 
            // panelPreprocessActions
            // 
            this.panelPreprocessActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.panelPreprocessActions.Controls.Add(this.buttonLoadSavedSettings);
            this.panelPreprocessActions.Controls.Add(this.buttonSaveCurrentSettings);
            this.panelPreprocessActions.Location = new System.Drawing.Point(20, 566);
            this.panelPreprocessActions.Name = "panelPreprocessActions";
            this.panelPreprocessActions.Size = new System.Drawing.Size(980, 56);
            this.panelPreprocessActions.TabIndex = 7;
            // 
            // buttonLoadSavedSettings
            // 
            this.buttonLoadSavedSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonLoadSavedSettings.FlatAppearance.BorderSize = 0;
            this.buttonLoadSavedSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLoadSavedSettings.Location = new System.Drawing.Point(12, 9);
            this.buttonLoadSavedSettings.Name = "buttonLoadSavedSettings";
            this.buttonLoadSavedSettings.Size = new System.Drawing.Size(168, 40);
            this.buttonLoadSavedSettings.TabIndex = 0;
            this.buttonLoadSavedSettings.Text = "讀取設定";
            this.buttonLoadSavedSettings.UseVisualStyleBackColor = false;
            this.buttonLoadSavedSettings.Click += new System.EventHandler(this.ButtonLoadSavedSettings_Click);
            // 
            // buttonSaveCurrentSettings
            // 
            this.buttonSaveCurrentSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonSaveCurrentSettings.FlatAppearance.BorderSize = 0;
            this.buttonSaveCurrentSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveCurrentSettings.Location = new System.Drawing.Point(190, 9);
            this.buttonSaveCurrentSettings.Name = "buttonSaveCurrentSettings";
            this.buttonSaveCurrentSettings.Size = new System.Drawing.Size(168, 40);
            this.buttonSaveCurrentSettings.TabIndex = 1;
            this.buttonSaveCurrentSettings.Text = "儲存目前設定";
            this.buttonSaveCurrentSettings.UseVisualStyleBackColor = false;
            this.buttonSaveCurrentSettings.Click += new System.EventHandler(this.ButtonSaveCurrentSettings_Click);
            // 
            // tabPageReferenceCorner
            // 
            this.tabPageReferenceCorner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.tabPageReferenceCorner.Controls.Add(this.panelReferenceCornerControls);
            this.tabPageReferenceCorner.Controls.Add(this.panelReferencePreview);
            this.tabPageReferenceCorner.Controls.Add(this.labelReferenceCornerStatus);
            this.tabPageReferenceCorner.Location = new System.Drawing.Point(4, 26);
            this.tabPageReferenceCorner.Name = "tabPageReferenceCorner";
            this.tabPageReferenceCorner.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageReferenceCorner.Size = new System.Drawing.Size(1032, 656);
            this.tabPageReferenceCorner.TabIndex = 0;
            this.tabPageReferenceCorner.Text = "參考角點";
            // 
            // panelReferenceCornerControls
            // 
            this.panelReferenceCornerControls.BackColor = System.Drawing.Color.White;
            this.panelReferenceCornerControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelReferenceCornerControls.Controls.Add(this.checkBoxReferenceCornerEnabled);
            this.panelReferenceCornerControls.Controls.Add(this.labelReferenceSource);
            this.panelReferenceCornerControls.Controls.Add(this.comboBoxReferenceSource);
            this.panelReferenceCornerControls.Controls.Add(this.buttonSaveReferenceRoi);
            this.panelReferenceCornerControls.Location = new System.Drawing.Point(20, 20);
            this.panelReferenceCornerControls.Name = "panelReferenceCornerControls";
            this.panelReferenceCornerControls.Size = new System.Drawing.Size(330, 228);
            this.panelReferenceCornerControls.TabIndex = 0;
            // 
            // checkBoxReferenceCornerEnabled
            // 
            this.checkBoxReferenceCornerEnabled.AutoSize = true;
            this.checkBoxReferenceCornerEnabled.Location = new System.Drawing.Point(16, 16);
            this.checkBoxReferenceCornerEnabled.Name = "checkBoxReferenceCornerEnabled";
            this.checkBoxReferenceCornerEnabled.Size = new System.Drawing.Size(139, 22);
            this.checkBoxReferenceCornerEnabled.TabIndex = 0;
            this.checkBoxReferenceCornerEnabled.Text = "啟用參考角點搜尋";
            this.checkBoxReferenceCornerEnabled.CheckedChanged += new System.EventHandler(this.ReferenceCornerEnabled_CheckedChanged);
            // 
            // labelReferenceSource
            // 
            this.labelReferenceSource.AutoSize = true;
            this.labelReferenceSource.Location = new System.Drawing.Point(16, 56);
            this.labelReferenceSource.Name = "labelReferenceSource";
            this.labelReferenceSource.Size = new System.Drawing.Size(106, 18);
            this.labelReferenceSource.TabIndex = 1;
            this.labelReferenceSource.Text = "前處理影像來源";
            // 
            // comboBoxReferenceSource
            // 
            this.comboBoxReferenceSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReferenceSource.FormattingEnabled = true;
            this.comboBoxReferenceSource.Location = new System.Drawing.Point(16, 78);
            this.comboBoxReferenceSource.Name = "comboBoxReferenceSource";
            this.comboBoxReferenceSource.Size = new System.Drawing.Size(280, 25);
            this.comboBoxReferenceSource.TabIndex = 2;
            this.comboBoxReferenceSource.SelectedIndexChanged += new System.EventHandler(this.ReferenceSource_SelectedIndexChanged);
            // 
            // buttonSaveReferenceRoi
            // 
            this.buttonSaveReferenceRoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonSaveReferenceRoi.FlatAppearance.BorderSize = 0;
            this.buttonSaveReferenceRoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveReferenceRoi.Location = new System.Drawing.Point(16, 122);
            this.buttonSaveReferenceRoi.Name = "buttonSaveReferenceRoi";
            this.buttonSaveReferenceRoi.Size = new System.Drawing.Size(280, 40);
            this.buttonSaveReferenceRoi.TabIndex = 3;
            this.buttonSaveReferenceRoi.Text = "保存 ROI 範圍";
            this.buttonSaveReferenceRoi.UseVisualStyleBackColor = false;
            this.buttonSaveReferenceRoi.Click += new System.EventHandler(this.SaveReferenceRoi_Click);
            // 
            // panelReferencePreview
            // 
            this.panelReferencePreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.panelReferencePreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelReferencePreview.Controls.Add(this.pictureBoxReferencePreview);
            this.panelReferencePreview.Location = new System.Drawing.Point(20, 264);
            this.panelReferencePreview.Name = "panelReferencePreview";
            this.panelReferencePreview.Size = new System.Drawing.Size(980, 352);
            this.panelReferencePreview.TabIndex = 1;
            this.panelReferencePreview.MouseEnter += new System.EventHandler(this.ReferencePreview_MouseEnter);
            this.panelReferencePreview.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ReferencePreview_MouseWheel);
            // 
            // pictureBoxReferencePreview
            // 
            this.pictureBoxReferencePreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.pictureBoxReferencePreview.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxReferencePreview.Name = "pictureBoxReferencePreview";
            this.pictureBoxReferencePreview.Size = new System.Drawing.Size(978, 350);
            this.pictureBoxReferencePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxReferencePreview.TabIndex = 0;
            this.pictureBoxReferencePreview.TabStop = false;
            this.pictureBoxReferencePreview.Paint += new System.Windows.Forms.PaintEventHandler(this.ReferencePreview_Paint);
            this.pictureBoxReferencePreview.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ReferencePreview_MouseDown);
            this.pictureBoxReferencePreview.MouseEnter += new System.EventHandler(this.ReferencePreview_MouseEnter);
            this.pictureBoxReferencePreview.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ReferencePreview_MouseMove);
            this.pictureBoxReferencePreview.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ReferencePreview_MouseUp);
            this.pictureBoxReferencePreview.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ReferencePreview_MouseWheel);
            // 
            // labelReferenceCornerStatus
            // 
            this.labelReferenceCornerStatus.AutoSize = true;
            this.labelReferenceCornerStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(134)))), ((int)(((byte)(138)))));
            this.labelReferenceCornerStatus.Location = new System.Drawing.Point(383, 39);
            this.labelReferenceCornerStatus.Name = "labelReferenceCornerStatus";
            this.labelReferenceCornerStatus.Size = new System.Drawing.Size(540, 18);
            this.labelReferenceCornerStatus.TabIndex = 2;
            this.labelReferenceCornerStatus.Text = "勾選後選擇基礎前處理影像，直接在大圖上左鍵拖曳框選 ROI，並讓標的物盡量靠中。";
            // 
            // tabPageInnerSettings
            // 
            this.tabPageInnerSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.tabPageInnerSettings.Controls.Add(this.labelInnerSettingsTitle);
            this.tabPageInnerSettings.Controls.Add(this.panelInnerSettings);
            this.tabPageInnerSettings.Location = new System.Drawing.Point(4, 26);
            this.tabPageInnerSettings.Name = "tabPageInnerSettings";
            this.tabPageInnerSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInnerSettings.Size = new System.Drawing.Size(1032, 656);
            this.tabPageInnerSettings.TabIndex = 4;
            this.tabPageInnerSettings.Text = "內部參數";
            // 
            // labelInnerSettingsTitle
            // 
            this.labelInnerSettingsTitle.AutoSize = true;
            this.labelInnerSettingsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(134)))), ((int)(((byte)(138)))));
            this.labelInnerSettingsTitle.Location = new System.Drawing.Point(31, 21);
            this.labelInnerSettingsTitle.Name = "labelInnerSettingsTitle";
            this.labelInnerSettingsTitle.Size = new System.Drawing.Size(64, 18);
            this.labelInnerSettingsTitle.TabIndex = 0;
            this.labelInnerSettingsTitle.Text = "內部參數";
            // 
            // panelInnerSettings
            // 
            this.panelInnerSettings.BackColor = System.Drawing.Color.White;
            this.panelInnerSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInnerSettings.Controls.Add(this.labelInnerCcdXPrecision);
            this.panelInnerSettings.Controls.Add(this.numericInnerCcdXPrecision);
            this.panelInnerSettings.Controls.Add(this.labelInnerCcdYPrecision);
            this.panelInnerSettings.Controls.Add(this.numericInnerCcdYPrecision);
            this.panelInnerSettings.Controls.Add(this.labelInnerMeasurementScaleFactor);
            this.panelInnerSettings.Controls.Add(this.numericInnerMeasurementScaleFactor);
            this.panelInnerSettings.Controls.Add(this.buttonSaveInnerSettings);
            this.panelInnerSettings.Location = new System.Drawing.Point(28, 46);
            this.panelInnerSettings.Name = "panelInnerSettings";
            this.panelInnerSettings.Size = new System.Drawing.Size(420, 230);
            this.panelInnerSettings.TabIndex = 1;
            // 
            // labelInnerCcdXPrecision
            // 
            this.labelInnerCcdXPrecision.AutoSize = true;
            this.labelInnerCcdXPrecision.Location = new System.Drawing.Point(24, 32);
            this.labelInnerCcdXPrecision.Name = "labelInnerCcdXPrecision";
            this.labelInnerCcdXPrecision.Size = new System.Drawing.Size(87, 18);
            this.labelInnerCcdXPrecision.TabIndex = 0;
            this.labelInnerCcdXPrecision.Text = "CCD X向精度";
            // 
            // numericInnerCcdXPrecision
            // 
            this.numericInnerCcdXPrecision.DecimalPlaces = 4;
            this.numericInnerCcdXPrecision.Increment = new decimal(new int[] {1, 0, 0, 262144});
            this.numericInnerCcdXPrecision.Location = new System.Drawing.Point(160, 28);
            this.numericInnerCcdXPrecision.Maximum = new decimal(new int[] {1000, 0, 0, 0});
            this.numericInnerCcdXPrecision.Name = "numericInnerCcdXPrecision";
            this.numericInnerCcdXPrecision.Size = new System.Drawing.Size(180, 25);
            this.numericInnerCcdXPrecision.TabIndex = 1;
            this.numericInnerCcdXPrecision.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelInnerCcdYPrecision
            // 
            this.labelInnerCcdYPrecision.AutoSize = true;
            this.labelInnerCcdYPrecision.Location = new System.Drawing.Point(24, 78);
            this.labelInnerCcdYPrecision.Name = "labelInnerCcdYPrecision";
            this.labelInnerCcdYPrecision.Size = new System.Drawing.Size(86, 18);
            this.labelInnerCcdYPrecision.TabIndex = 2;
            this.labelInnerCcdYPrecision.Text = "CCD Y向精度";
            // 
            // numericInnerCcdYPrecision
            // 
            this.numericInnerCcdYPrecision.DecimalPlaces = 4;
            this.numericInnerCcdYPrecision.Increment = new decimal(new int[] {1, 0, 0, 262144});
            this.numericInnerCcdYPrecision.Location = new System.Drawing.Point(160, 74);
            this.numericInnerCcdYPrecision.Maximum = new decimal(new int[] {1000, 0, 0, 0});
            this.numericInnerCcdYPrecision.Name = "numericInnerCcdYPrecision";
            this.numericInnerCcdYPrecision.Size = new System.Drawing.Size(180, 25);
            this.numericInnerCcdYPrecision.TabIndex = 3;
            this.numericInnerCcdYPrecision.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelInnerMeasurementScaleFactor
            // 
            this.labelInnerMeasurementScaleFactor.AutoSize = true;
            this.labelInnerMeasurementScaleFactor.Location = new System.Drawing.Point(24, 124);
            this.labelInnerMeasurementScaleFactor.Name = "labelInnerMeasurementScaleFactor";
            this.labelInnerMeasurementScaleFactor.Size = new System.Drawing.Size(103, 18);
            this.labelInnerMeasurementScaleFactor.TabIndex = 4;
            this.labelInnerMeasurementScaleFactor.Text = "計算值倍率";
            // 
            // numericInnerMeasurementScaleFactor
            // 
            this.numericInnerMeasurementScaleFactor.DecimalPlaces = 4;
            this.numericInnerMeasurementScaleFactor.Increment = new decimal(new int[] {1, 0, 0, 262144});
            this.numericInnerMeasurementScaleFactor.Location = new System.Drawing.Point(160, 120);
            this.numericInnerMeasurementScaleFactor.Maximum = new decimal(new int[] {1000, 0, 0, 0});
            this.numericInnerMeasurementScaleFactor.Minimum = new decimal(new int[] {1, 0, 0, 262144});
            this.numericInnerMeasurementScaleFactor.Name = "numericInnerMeasurementScaleFactor";
            this.numericInnerMeasurementScaleFactor.Size = new System.Drawing.Size(180, 25);
            this.numericInnerMeasurementScaleFactor.TabIndex = 5;
            this.numericInnerMeasurementScaleFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // buttonSaveInnerSettings
            // 
            this.buttonSaveInnerSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonSaveInnerSettings.FlatAppearance.BorderSize = 0;
            this.buttonSaveInnerSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveInnerSettings.Location = new System.Drawing.Point(24, 198);
            this.buttonSaveInnerSettings.Name = "buttonSaveInnerSettings";
            this.buttonSaveInnerSettings.Size = new System.Drawing.Size(120, 36);
            this.buttonSaveInnerSettings.TabIndex = 6;
            this.buttonSaveInnerSettings.Text = "儲存";
            this.buttonSaveInnerSettings.UseVisualStyleBackColor = false;
            this.buttonSaveInnerSettings.Click += new System.EventHandler(this.SaveInnerSettingsButton_Click);
            // 
            // tabPageJudgementCriteria
            // 
            this.tabPageJudgementCriteria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.tabPageJudgementCriteria.Controls.Add(this.labelJudgementCriteriaTitle);
            this.tabPageJudgementCriteria.Controls.Add(this.buttonJudgementSyntaxHelp);
            this.tabPageJudgementCriteria.Controls.Add(this.panelJudgementCriteria);
            this.tabPageJudgementCriteria.Location = new System.Drawing.Point(4, 26);
            this.tabPageJudgementCriteria.Name = "tabPageJudgementCriteria";
            this.tabPageJudgementCriteria.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageJudgementCriteria.Size = new System.Drawing.Size(1032, 656);
            this.tabPageJudgementCriteria.TabIndex = 5;
            this.tabPageJudgementCriteria.Text = "良品判斷條件";
            // 
            // tabPageDetectionParameterSummary
            // 
            this.tabPageDetectionParameterSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.tabPageDetectionParameterSummary.Controls.Add(this.panelDetectionParameterSummary);
            this.tabPageDetectionParameterSummary.Location = new System.Drawing.Point(4, 26);
            this.tabPageDetectionParameterSummary.Name = "tabPageDetectionParameterSummary";
            this.tabPageDetectionParameterSummary.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDetectionParameterSummary.Size = new System.Drawing.Size(1032, 656);
            this.tabPageDetectionParameterSummary.TabIndex = 6;
            this.tabPageDetectionParameterSummary.Text = "檢測參數整理";
            // 
            // tabPageContinuousInspection
            // 
            this.tabPageContinuousInspection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.tabPageContinuousInspection.Controls.Add(this.panelContinuousInspection);
            this.tabPageContinuousInspection.Location = new System.Drawing.Point(4, 26);
            this.tabPageContinuousInspection.Name = "tabPageContinuousInspection";
            this.tabPageContinuousInspection.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageContinuousInspection.Size = new System.Drawing.Size(1032, 656);
            this.tabPageContinuousInspection.TabIndex = 7;
            this.tabPageContinuousInspection.Text = "連續檢測";
            // 
            // buttonContinuousInspectionResetYield
            // 
            this.buttonContinuousInspectionResetYield.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom)));
            this.buttonContinuousInspectionResetYield.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonContinuousInspectionResetYield.FlatAppearance.BorderSize = 0;
            this.buttonContinuousInspectionResetYield.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonContinuousInspectionResetYield.Location = new System.Drawing.Point(842, 607);
            this.buttonContinuousInspectionResetYield.Name = "buttonContinuousInspectionResetYield";
            this.buttonContinuousInspectionResetYield.Size = new System.Drawing.Size(156, 30);
            this.buttonContinuousInspectionResetYield.TabIndex = 8;
            this.buttonContinuousInspectionResetYield.Text = "歸零良率計算";
            this.buttonContinuousInspectionResetYield.UseVisualStyleBackColor = false;
            // 
            // panelContinuousInspection
            // 
            this.panelContinuousInspection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelContinuousInspection.Controls.Add(this.buttonContinuousInspectionResetYield);
            this.panelContinuousInspection.Controls.Add(this.groupBoxContinuousInspection3);
            this.panelContinuousInspection.Controls.Add(this.groupBoxContinuousInspection2);
            this.panelContinuousInspection.Controls.Add(this.groupBoxContinuousInspection1);
            this.panelContinuousInspection.Controls.Add(this.comboBoxContinuousInspectionMainParameter);
            this.panelContinuousInspection.Controls.Add(this.labelContinuousInspectionMainParameter);
            this.panelContinuousInspection.Controls.Add(this.labelContinuousInspectionTitle);
            this.panelContinuousInspection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContinuousInspection.Name = "panelContinuousInspection";
            this.panelContinuousInspection.Size = new System.Drawing.Size(1026, 650);
            this.panelContinuousInspection.TabIndex = 0;
            // 
            // 
            // labelContinuousInspectionTitle
            // 
            this.labelContinuousInspectionTitle.AutoSize = true;
            this.labelContinuousInspectionTitle.Font = new System.Drawing.Font("Microsoft JhengHei UI", 16F, System.Drawing.FontStyle.Bold);
            this.labelContinuousInspectionTitle.Location = new System.Drawing.Point(26, 28);
            this.labelContinuousInspectionTitle.Name = "labelContinuousInspectionTitle";
            this.labelContinuousInspectionTitle.Size = new System.Drawing.Size(100, 28);
            this.labelContinuousInspectionTitle.TabIndex = 0;
            this.labelContinuousInspectionTitle.Text = "連續檢測";
            // 
            // labelContinuousInspectionMainParameter
            // 
            this.labelContinuousInspectionMainParameter.AutoSize = true;
            this.labelContinuousInspectionMainParameter.Location = new System.Drawing.Point(30, 74);
            this.labelContinuousInspectionMainParameter.Name = "labelContinuousInspectionMainParameter";
            this.labelContinuousInspectionMainParameter.Size = new System.Drawing.Size(62, 18);
            this.labelContinuousInspectionMainParameter.TabIndex = 1;
            this.labelContinuousInspectionMainParameter.Text = "主參數";
            // 
            // comboBoxContinuousInspectionMainParameter
            // 
            this.comboBoxContinuousInspectionMainParameter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxContinuousInspectionMainParameter.FormattingEnabled = true;
            this.comboBoxContinuousInspectionMainParameter.Location = new System.Drawing.Point(98, 70);
            this.comboBoxContinuousInspectionMainParameter.Name = "comboBoxContinuousInspectionMainParameter";
            this.comboBoxContinuousInspectionMainParameter.Size = new System.Drawing.Size(240, 26);
            this.comboBoxContinuousInspectionMainParameter.TabIndex = 2;
            // 
            // groupBoxContinuousInspection1
            // 
            this.groupBoxContinuousInspection1.Controls.Add(this.checkBoxContinuousInspectionSaveOriginalImage1);
            this.groupBoxContinuousInspection1.Controls.Add(this.labelContinuousInspectionYield1);
            this.groupBoxContinuousInspection1.Controls.Add(this.buttonContinuousInspectionJudge1);
            this.groupBoxContinuousInspection1.Controls.Add(this.labelContinuousInspectionResult1);
            this.groupBoxContinuousInspection1.Controls.Add(this.buttonContinuousInspectionLoadImage1);
            this.groupBoxContinuousInspection1.Controls.Add(this.panelContinuousInspectionPreview1);
            this.groupBoxContinuousInspection1.Controls.Add(this.labelContinuousInspectionSubParameter1);
            this.groupBoxContinuousInspection1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F);
            this.groupBoxContinuousInspection1.Location = new System.Drawing.Point(24, 118);
            this.groupBoxContinuousInspection1.Name = "groupBoxContinuousInspection1";
            this.groupBoxContinuousInspection1.Size = new System.Drawing.Size(292, 510);
            this.groupBoxContinuousInspection1.TabIndex = 3;
            this.groupBoxContinuousInspection1.TabStop = false;
            this.groupBoxContinuousInspection1.Text = "子參數 1";
            // 
            // checkBoxContinuousInspectionSaveOriginalImage1
            // 
            this.checkBoxContinuousInspectionSaveOriginalImage1.AutoSize = true;
            this.checkBoxContinuousInspectionSaveOriginalImage1.Location = new System.Drawing.Point(16, 489);
            this.checkBoxContinuousInspectionSaveOriginalImage1.Name = "checkBoxContinuousInspectionSaveOriginalImage1";
            this.checkBoxContinuousInspectionSaveOriginalImage1.Size = new System.Drawing.Size(125, 24);
            this.checkBoxContinuousInspectionSaveOriginalImage1.TabIndex = 6;
            this.checkBoxContinuousInspectionSaveOriginalImage1.Text = "保存原始影像";
            this.checkBoxContinuousInspectionSaveOriginalImage1.UseVisualStyleBackColor = true;
            // 
            // labelContinuousInspectionYield1
            // 
            this.labelContinuousInspectionYield1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelContinuousInspectionYield1.Location = new System.Drawing.Point(16, 450);
            this.labelContinuousInspectionYield1.Name = "labelContinuousInspectionYield1";
            this.labelContinuousInspectionYield1.Size = new System.Drawing.Size(258, 32);
            this.labelContinuousInspectionYield1.TabIndex = 5;
            this.labelContinuousInspectionYield1.Text = "良率 0/0(0%)";
            this.labelContinuousInspectionYield1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonContinuousInspectionJudge1
            // 
            this.buttonContinuousInspectionJudge1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonContinuousInspectionJudge1.FlatAppearance.BorderSize = 0;
            this.buttonContinuousInspectionJudge1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonContinuousInspectionJudge1.Location = new System.Drawing.Point(146, 400);
            this.buttonContinuousInspectionJudge1.Name = "buttonContinuousInspectionJudge1";
            this.buttonContinuousInspectionJudge1.Size = new System.Drawing.Size(128, 36);
            this.buttonContinuousInspectionJudge1.TabIndex = 4;
            this.buttonContinuousInspectionJudge1.Text = "判斷";
            this.buttonContinuousInspectionJudge1.UseVisualStyleBackColor = false;
            // 
            // labelContinuousInspectionResult1
            // 
            this.labelContinuousInspectionResult1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelContinuousInspectionResult1.Location = new System.Drawing.Point(16, 346);
            this.labelContinuousInspectionResult1.Name = "labelContinuousInspectionResult1";
            this.labelContinuousInspectionResult1.Size = new System.Drawing.Size(258, 40);
            this.labelContinuousInspectionResult1.TabIndex = 3;
            this.labelContinuousInspectionResult1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonContinuousInspectionLoadImage1
            // 
            this.buttonContinuousInspectionLoadImage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonContinuousInspectionLoadImage1.FlatAppearance.BorderSize = 0;
            this.buttonContinuousInspectionLoadImage1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonContinuousInspectionLoadImage1.Location = new System.Drawing.Point(16, 400);
            this.buttonContinuousInspectionLoadImage1.Name = "buttonContinuousInspectionLoadImage1";
            this.buttonContinuousInspectionLoadImage1.Size = new System.Drawing.Size(120, 36);
            this.buttonContinuousInspectionLoadImage1.TabIndex = 2;
            this.buttonContinuousInspectionLoadImage1.Text = "讀取圖";
            this.buttonContinuousInspectionLoadImage1.UseVisualStyleBackColor = false;
            // 
            // panelContinuousInspectionPreview1
            // 
            this.panelContinuousInspectionPreview1.BackColor = System.Drawing.Color.White;
            this.panelContinuousInspectionPreview1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelContinuousInspectionPreview1.Controls.Add(this.pictureBoxContinuousInspection1);
            this.panelContinuousInspectionPreview1.Location = new System.Drawing.Point(16, 82);
            this.panelContinuousInspectionPreview1.Name = "panelContinuousInspectionPreview1";
            this.panelContinuousInspectionPreview1.Size = new System.Drawing.Size(258, 250);
            this.panelContinuousInspectionPreview1.TabIndex = 1;
            // 
            // pictureBoxContinuousInspection1
            // 
            this.pictureBoxContinuousInspection1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(236)))));
            this.pictureBoxContinuousInspection1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxContinuousInspection1.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxContinuousInspection1.Name = "pictureBoxContinuousInspection1";
            this.pictureBoxContinuousInspection1.Size = new System.Drawing.Size(256, 248);
            this.pictureBoxContinuousInspection1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxContinuousInspection1.TabIndex = 0;
            this.pictureBoxContinuousInspection1.TabStop = false;
            // 
            // labelContinuousInspectionSubParameter1
            // 
            this.labelContinuousInspectionSubParameter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelContinuousInspectionSubParameter1.Location = new System.Drawing.Point(16, 34);
            this.labelContinuousInspectionSubParameter1.Name = "labelContinuousInspectionSubParameter1";
            this.labelContinuousInspectionSubParameter1.Size = new System.Drawing.Size(258, 36);
            this.labelContinuousInspectionSubParameter1.TabIndex = 0;
            this.labelContinuousInspectionSubParameter1.Text = "未設定子參數";
            this.labelContinuousInspectionSubParameter1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBoxContinuousInspection2
            // 
            this.groupBoxContinuousInspection2.Controls.Add(this.checkBoxContinuousInspectionSaveOriginalImage2);
            this.groupBoxContinuousInspection2.Controls.Add(this.labelContinuousInspectionYield2);
            this.groupBoxContinuousInspection2.Controls.Add(this.buttonContinuousInspectionJudge2);
            this.groupBoxContinuousInspection2.Controls.Add(this.labelContinuousInspectionResult2);
            this.groupBoxContinuousInspection2.Controls.Add(this.buttonContinuousInspectionLoadImage2);
            this.groupBoxContinuousInspection2.Controls.Add(this.panelContinuousInspectionPreview2);
            this.groupBoxContinuousInspection2.Controls.Add(this.labelContinuousInspectionSubParameter2);
            this.groupBoxContinuousInspection2.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F);
            this.groupBoxContinuousInspection2.Location = new System.Drawing.Point(344, 118);
            this.groupBoxContinuousInspection2.Name = "groupBoxContinuousInspection2";
            this.groupBoxContinuousInspection2.Size = new System.Drawing.Size(292, 510);
            this.groupBoxContinuousInspection2.TabIndex = 4;
            this.groupBoxContinuousInspection2.TabStop = false;
            this.groupBoxContinuousInspection2.Text = "子參數 2";
            // 
            // checkBoxContinuousInspectionSaveOriginalImage2
            // 
            this.checkBoxContinuousInspectionSaveOriginalImage2.AutoSize = true;
            this.checkBoxContinuousInspectionSaveOriginalImage2.Location = new System.Drawing.Point(16, 489);
            this.checkBoxContinuousInspectionSaveOriginalImage2.Name = "checkBoxContinuousInspectionSaveOriginalImage2";
            this.checkBoxContinuousInspectionSaveOriginalImage2.Size = new System.Drawing.Size(125, 24);
            this.checkBoxContinuousInspectionSaveOriginalImage2.TabIndex = 6;
            this.checkBoxContinuousInspectionSaveOriginalImage2.Text = "保存原始影像";
            this.checkBoxContinuousInspectionSaveOriginalImage2.UseVisualStyleBackColor = true;
            // 
            // labelContinuousInspectionYield2
            // 
            this.labelContinuousInspectionYield2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelContinuousInspectionYield2.Location = new System.Drawing.Point(16, 450);
            this.labelContinuousInspectionYield2.Name = "labelContinuousInspectionYield2";
            this.labelContinuousInspectionYield2.Size = new System.Drawing.Size(258, 32);
            this.labelContinuousInspectionYield2.TabIndex = 5;
            this.labelContinuousInspectionYield2.Text = "良率 0/0(0%)";
            this.labelContinuousInspectionYield2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonContinuousInspectionJudge2
            // 
            this.buttonContinuousInspectionJudge2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonContinuousInspectionJudge2.FlatAppearance.BorderSize = 0;
            this.buttonContinuousInspectionJudge2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonContinuousInspectionJudge2.Location = new System.Drawing.Point(146, 400);
            this.buttonContinuousInspectionJudge2.Name = "buttonContinuousInspectionJudge2";
            this.buttonContinuousInspectionJudge2.Size = new System.Drawing.Size(128, 36);
            this.buttonContinuousInspectionJudge2.TabIndex = 4;
            this.buttonContinuousInspectionJudge2.Text = "判斷";
            this.buttonContinuousInspectionJudge2.UseVisualStyleBackColor = false;
            // 
            // labelContinuousInspectionResult2
            // 
            this.labelContinuousInspectionResult2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelContinuousInspectionResult2.Location = new System.Drawing.Point(16, 346);
            this.labelContinuousInspectionResult2.Name = "labelContinuousInspectionResult2";
            this.labelContinuousInspectionResult2.Size = new System.Drawing.Size(258, 40);
            this.labelContinuousInspectionResult2.TabIndex = 3;
            this.labelContinuousInspectionResult2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonContinuousInspectionLoadImage2
            // 
            this.buttonContinuousInspectionLoadImage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonContinuousInspectionLoadImage2.FlatAppearance.BorderSize = 0;
            this.buttonContinuousInspectionLoadImage2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonContinuousInspectionLoadImage2.Location = new System.Drawing.Point(16, 400);
            this.buttonContinuousInspectionLoadImage2.Name = "buttonContinuousInspectionLoadImage2";
            this.buttonContinuousInspectionLoadImage2.Size = new System.Drawing.Size(120, 36);
            this.buttonContinuousInspectionLoadImage2.TabIndex = 2;
            this.buttonContinuousInspectionLoadImage2.Text = "讀取圖";
            this.buttonContinuousInspectionLoadImage2.UseVisualStyleBackColor = false;
            // 
            // panelContinuousInspectionPreview2
            // 
            this.panelContinuousInspectionPreview2.BackColor = System.Drawing.Color.White;
            this.panelContinuousInspectionPreview2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelContinuousInspectionPreview2.Controls.Add(this.pictureBoxContinuousInspection2);
            this.panelContinuousInspectionPreview2.Location = new System.Drawing.Point(16, 82);
            this.panelContinuousInspectionPreview2.Name = "panelContinuousInspectionPreview2";
            this.panelContinuousInspectionPreview2.Size = new System.Drawing.Size(258, 250);
            this.panelContinuousInspectionPreview2.TabIndex = 1;
            // 
            // pictureBoxContinuousInspection2
            // 
            this.pictureBoxContinuousInspection2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(236)))));
            this.pictureBoxContinuousInspection2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxContinuousInspection2.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxContinuousInspection2.Name = "pictureBoxContinuousInspection2";
            this.pictureBoxContinuousInspection2.Size = new System.Drawing.Size(256, 248);
            this.pictureBoxContinuousInspection2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxContinuousInspection2.TabIndex = 0;
            this.pictureBoxContinuousInspection2.TabStop = false;
            // 
            // labelContinuousInspectionSubParameter2
            // 
            this.labelContinuousInspectionSubParameter2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelContinuousInspectionSubParameter2.Location = new System.Drawing.Point(16, 34);
            this.labelContinuousInspectionSubParameter2.Name = "labelContinuousInspectionSubParameter2";
            this.labelContinuousInspectionSubParameter2.Size = new System.Drawing.Size(258, 36);
            this.labelContinuousInspectionSubParameter2.TabIndex = 0;
            this.labelContinuousInspectionSubParameter2.Text = "未設定子參數";
            this.labelContinuousInspectionSubParameter2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBoxContinuousInspection3
            // 
            this.groupBoxContinuousInspection3.Controls.Add(this.checkBoxContinuousInspectionSaveOriginalImage3);
            this.groupBoxContinuousInspection3.Controls.Add(this.labelContinuousInspectionYield3);
            this.groupBoxContinuousInspection3.Controls.Add(this.buttonContinuousInspectionJudge3);
            this.groupBoxContinuousInspection3.Controls.Add(this.labelContinuousInspectionResult3);
            this.groupBoxContinuousInspection3.Controls.Add(this.buttonContinuousInspectionLoadImage3);
            this.groupBoxContinuousInspection3.Controls.Add(this.panelContinuousInspectionPreview3);
            this.groupBoxContinuousInspection3.Controls.Add(this.labelContinuousInspectionSubParameter3);
            this.groupBoxContinuousInspection3.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F);
            this.groupBoxContinuousInspection3.Location = new System.Drawing.Point(664, 118);
            this.groupBoxContinuousInspection3.Name = "groupBoxContinuousInspection3";
            this.groupBoxContinuousInspection3.Size = new System.Drawing.Size(292, 510);
            this.groupBoxContinuousInspection3.TabIndex = 5;
            this.groupBoxContinuousInspection3.TabStop = false;
            this.groupBoxContinuousInspection3.Text = "子參數 3";
            // 
            // checkBoxContinuousInspectionSaveOriginalImage3
            // 
            this.checkBoxContinuousInspectionSaveOriginalImage3.AutoSize = true;
            this.checkBoxContinuousInspectionSaveOriginalImage3.Location = new System.Drawing.Point(16, 489);
            this.checkBoxContinuousInspectionSaveOriginalImage3.Name = "checkBoxContinuousInspectionSaveOriginalImage3";
            this.checkBoxContinuousInspectionSaveOriginalImage3.Size = new System.Drawing.Size(125, 24);
            this.checkBoxContinuousInspectionSaveOriginalImage3.TabIndex = 6;
            this.checkBoxContinuousInspectionSaveOriginalImage3.Text = "保存原始影像";
            this.checkBoxContinuousInspectionSaveOriginalImage3.UseVisualStyleBackColor = true;
            // 
            // labelContinuousInspectionYield3
            // 
            this.labelContinuousInspectionYield3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelContinuousInspectionYield3.Location = new System.Drawing.Point(16, 450);
            this.labelContinuousInspectionYield3.Name = "labelContinuousInspectionYield3";
            this.labelContinuousInspectionYield3.Size = new System.Drawing.Size(258, 32);
            this.labelContinuousInspectionYield3.TabIndex = 5;
            this.labelContinuousInspectionYield3.Text = "良率 0/0(0%)";
            this.labelContinuousInspectionYield3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonContinuousInspectionJudge3
            // 
            this.buttonContinuousInspectionJudge3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonContinuousInspectionJudge3.FlatAppearance.BorderSize = 0;
            this.buttonContinuousInspectionJudge3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonContinuousInspectionJudge3.Location = new System.Drawing.Point(146, 400);
            this.buttonContinuousInspectionJudge3.Name = "buttonContinuousInspectionJudge3";
            this.buttonContinuousInspectionJudge3.Size = new System.Drawing.Size(128, 36);
            this.buttonContinuousInspectionJudge3.TabIndex = 4;
            this.buttonContinuousInspectionJudge3.Text = "判斷";
            this.buttonContinuousInspectionJudge3.UseVisualStyleBackColor = false;
            // 
            // labelContinuousInspectionResult3
            // 
            this.labelContinuousInspectionResult3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelContinuousInspectionResult3.Location = new System.Drawing.Point(16, 346);
            this.labelContinuousInspectionResult3.Name = "labelContinuousInspectionResult3";
            this.labelContinuousInspectionResult3.Size = new System.Drawing.Size(258, 40);
            this.labelContinuousInspectionResult3.TabIndex = 3;
            this.labelContinuousInspectionResult3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonContinuousInspectionLoadImage3
            // 
            this.buttonContinuousInspectionLoadImage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonContinuousInspectionLoadImage3.FlatAppearance.BorderSize = 0;
            this.buttonContinuousInspectionLoadImage3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonContinuousInspectionLoadImage3.Location = new System.Drawing.Point(16, 400);
            this.buttonContinuousInspectionLoadImage3.Name = "buttonContinuousInspectionLoadImage3";
            this.buttonContinuousInspectionLoadImage3.Size = new System.Drawing.Size(120, 36);
            this.buttonContinuousInspectionLoadImage3.TabIndex = 2;
            this.buttonContinuousInspectionLoadImage3.Text = "讀取圖";
            this.buttonContinuousInspectionLoadImage3.UseVisualStyleBackColor = false;
            // 
            // panelContinuousInspectionPreview3
            // 
            this.panelContinuousInspectionPreview3.BackColor = System.Drawing.Color.White;
            this.panelContinuousInspectionPreview3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelContinuousInspectionPreview3.Controls.Add(this.pictureBoxContinuousInspection3);
            this.panelContinuousInspectionPreview3.Location = new System.Drawing.Point(16, 82);
            this.panelContinuousInspectionPreview3.Name = "panelContinuousInspectionPreview3";
            this.panelContinuousInspectionPreview3.Size = new System.Drawing.Size(258, 250);
            this.panelContinuousInspectionPreview3.TabIndex = 1;
            // 
            // pictureBoxContinuousInspection3
            // 
            this.pictureBoxContinuousInspection3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(236)))));
            this.pictureBoxContinuousInspection3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxContinuousInspection3.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxContinuousInspection3.Name = "pictureBoxContinuousInspection3";
            this.pictureBoxContinuousInspection3.Size = new System.Drawing.Size(256, 248);
            this.pictureBoxContinuousInspection3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxContinuousInspection3.TabIndex = 0;
            this.pictureBoxContinuousInspection3.TabStop = false;
            // 
            // labelContinuousInspectionSubParameter3
            // 
            this.labelContinuousInspectionSubParameter3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelContinuousInspectionSubParameter3.Location = new System.Drawing.Point(16, 34);
            this.labelContinuousInspectionSubParameter3.Name = "labelContinuousInspectionSubParameter3";
            this.labelContinuousInspectionSubParameter3.Size = new System.Drawing.Size(258, 36);
            this.labelContinuousInspectionSubParameter3.TabIndex = 0;
            this.labelContinuousInspectionSubParameter3.Text = "未設定子參數";
            this.labelContinuousInspectionSubParameter3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelContinuousInspectionTitle.Text = "連續檢測";
            // 
            // panelDetectionParameterSummary
            // 
            this.panelDetectionParameterSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelDetectionParameterSummary.Controls.Add(this.buttonDetectionSaveParameterReference);
            this.panelDetectionParameterSummary.Controls.Add(this.groupBoxDetectionSubParameter3List);
            this.panelDetectionParameterSummary.Controls.Add(this.groupBoxDetectionSubParameter2List);
            this.panelDetectionParameterSummary.Controls.Add(this.groupBoxDetectionSubParameter1List);
            this.panelDetectionParameterSummary.Controls.Add(this.groupBoxDetectionMainParameterList);
            this.panelDetectionParameterSummary.Controls.Add(this.groupBoxDetectionParameterCreate);
            this.panelDetectionParameterSummary.Location = new System.Drawing.Point(0, 0);
            this.panelDetectionParameterSummary.Name = "panelDetectionParameterSummary";
            this.panelDetectionParameterSummary.Size = new System.Drawing.Size(1026, 650);
            this.panelDetectionParameterSummary.TabIndex = 0;
            // 
            // groupBoxDetectionParameterCreate
            // 
            this.groupBoxDetectionParameterCreate.Controls.Add(this.buttonDetectionMainParameterConfirm);
            this.groupBoxDetectionParameterCreate.Controls.Add(this.textBoxDetectionMainParameterName);
            this.groupBoxDetectionParameterCreate.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F);
            this.groupBoxDetectionParameterCreate.Location = new System.Drawing.Point(24, 24);
            this.groupBoxDetectionParameterCreate.Name = "groupBoxDetectionParameterCreate";
            this.groupBoxDetectionParameterCreate.Size = new System.Drawing.Size(180, 140);
            this.groupBoxDetectionParameterCreate.TabIndex = 0;
            this.groupBoxDetectionParameterCreate.TabStop = false;
            this.groupBoxDetectionParameterCreate.Text = "建立主參數";
            // 
            // buttonDetectionMainParameterConfirm
            // 
            this.buttonDetectionMainParameterConfirm.BackColor = System.Drawing.Color.White;
            this.buttonDetectionMainParameterConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDetectionMainParameterConfirm.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F);
            this.buttonDetectionMainParameterConfirm.Location = new System.Drawing.Point(47, 86);
            this.buttonDetectionMainParameterConfirm.Name = "buttonDetectionMainParameterConfirm";
            this.buttonDetectionMainParameterConfirm.Size = new System.Drawing.Size(86, 30);
            this.buttonDetectionMainParameterConfirm.TabIndex = 1;
            this.buttonDetectionMainParameterConfirm.Text = "確認";
            this.buttonDetectionMainParameterConfirm.UseVisualStyleBackColor = false;
            // 
            // textBoxDetectionMainParameterName
            // 
            this.textBoxDetectionMainParameterName.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F);
            this.textBoxDetectionMainParameterName.Location = new System.Drawing.Point(20, 38);
            this.textBoxDetectionMainParameterName.Name = "textBoxDetectionMainParameterName";
            this.textBoxDetectionMainParameterName.Size = new System.Drawing.Size(138, 29);
            this.textBoxDetectionMainParameterName.TabIndex = 0;
            // 
            // groupBoxDetectionMainParameterList
            // 
            this.groupBoxDetectionMainParameterList.Controls.Add(this.buttonDetectionMainParameterSaveOrder);
            this.groupBoxDetectionMainParameterList.Controls.Add(this.buttonDetectionMainParameterMoveDown);
            this.groupBoxDetectionMainParameterList.Controls.Add(this.buttonDetectionMainParameterMoveUp);
            this.groupBoxDetectionMainParameterList.Controls.Add(this.listBoxDetectionMainParameter);
            this.groupBoxDetectionMainParameterList.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F);
            this.groupBoxDetectionMainParameterList.Location = new System.Drawing.Point(236, 24);
            this.groupBoxDetectionMainParameterList.Name = "groupBoxDetectionMainParameterList";
            this.groupBoxDetectionMainParameterList.Size = new System.Drawing.Size(180, 280);
            this.groupBoxDetectionMainParameterList.TabIndex = 1;
            this.groupBoxDetectionMainParameterList.TabStop = false;
            this.groupBoxDetectionMainParameterList.Text = "主參數";
            // 
            // listBoxDetectionMainParameter
            // 
            this.listBoxDetectionMainParameter.FormattingEnabled = true;
            this.listBoxDetectionMainParameter.ItemHeight = 18;
            this.listBoxDetectionMainParameter.Location = new System.Drawing.Point(12, 28);
            this.listBoxDetectionMainParameter.Name = "listBoxDetectionMainParameter";
            this.listBoxDetectionMainParameter.Size = new System.Drawing.Size(156, 202);
            this.listBoxDetectionMainParameter.TabIndex = 0;
            // 
            // buttonDetectionMainParameterMoveUp
            // 
            this.buttonDetectionMainParameterMoveUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDetectionMainParameterMoveUp.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.buttonDetectionMainParameterMoveUp.Location = new System.Drawing.Point(12, 238);
            this.buttonDetectionMainParameterMoveUp.Name = "buttonDetectionMainParameterMoveUp";
            this.buttonDetectionMainParameterMoveUp.Size = new System.Drawing.Size(34, 28);
            this.buttonDetectionMainParameterMoveUp.TabIndex = 1;
            this.buttonDetectionMainParameterMoveUp.Text = "↑";
            this.buttonDetectionMainParameterMoveUp.UseVisualStyleBackColor = true;
            // 
            // buttonDetectionMainParameterMoveDown
            // 
            this.buttonDetectionMainParameterMoveDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDetectionMainParameterMoveDown.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.buttonDetectionMainParameterMoveDown.Location = new System.Drawing.Point(52, 238);
            this.buttonDetectionMainParameterMoveDown.Name = "buttonDetectionMainParameterMoveDown";
            this.buttonDetectionMainParameterMoveDown.Size = new System.Drawing.Size(34, 28);
            this.buttonDetectionMainParameterMoveDown.TabIndex = 2;
            this.buttonDetectionMainParameterMoveDown.Text = "↓";
            this.buttonDetectionMainParameterMoveDown.UseVisualStyleBackColor = true;
            // 
            // buttonDetectionMainParameterSaveOrder
            // 
            this.buttonDetectionMainParameterSaveOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDetectionMainParameterSaveOrder.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.buttonDetectionMainParameterSaveOrder.Location = new System.Drawing.Point(92, 238);
            this.buttonDetectionMainParameterSaveOrder.Name = "buttonDetectionMainParameterSaveOrder";
            this.buttonDetectionMainParameterSaveOrder.Size = new System.Drawing.Size(76, 28);
            this.buttonDetectionMainParameterSaveOrder.TabIndex = 3;
            this.buttonDetectionMainParameterSaveOrder.Text = "保存順序";
            this.buttonDetectionMainParameterSaveOrder.UseVisualStyleBackColor = true;
            // 
            // groupBoxDetectionSubParameter1List
            // 
            this.groupBoxDetectionSubParameter1List.Controls.Add(this.checkBoxDetectionSubParameter1Enabled);
            this.groupBoxDetectionSubParameter1List.Controls.Add(this.buttonDetectionSubParameter1SaveOrder);
            this.groupBoxDetectionSubParameter1List.Controls.Add(this.buttonDetectionSubParameter1MoveDown);
            this.groupBoxDetectionSubParameter1List.Controls.Add(this.buttonDetectionSubParameter1MoveUp);
            this.groupBoxDetectionSubParameter1List.Controls.Add(this.listBoxDetectionSubParameter1);
            this.groupBoxDetectionSubParameter1List.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F);
            this.groupBoxDetectionSubParameter1List.Location = new System.Drawing.Point(430, 24);
            this.groupBoxDetectionSubParameter1List.Name = "groupBoxDetectionSubParameter1List";
            this.groupBoxDetectionSubParameter1List.Size = new System.Drawing.Size(180, 280);
            this.groupBoxDetectionSubParameter1List.TabIndex = 2;
            this.groupBoxDetectionSubParameter1List.TabStop = false;
            this.groupBoxDetectionSubParameter1List.Text = "子參數1";
            // 
            // listBoxDetectionSubParameter1
            // 
            this.listBoxDetectionSubParameter1.FormattingEnabled = true;
            this.listBoxDetectionSubParameter1.ItemHeight = 18;
            this.listBoxDetectionSubParameter1.Location = new System.Drawing.Point(12, 28);
            this.listBoxDetectionSubParameter1.Name = "listBoxDetectionSubParameter1";
            this.listBoxDetectionSubParameter1.Size = new System.Drawing.Size(156, 202);
            this.listBoxDetectionSubParameter1.TabIndex = 0;
            // 
            // buttonDetectionSubParameter1MoveUp
            // 
            this.buttonDetectionSubParameter1MoveUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDetectionSubParameter1MoveUp.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.buttonDetectionSubParameter1MoveUp.Location = new System.Drawing.Point(12, 238);
            this.buttonDetectionSubParameter1MoveUp.Name = "buttonDetectionSubParameter1MoveUp";
            this.buttonDetectionSubParameter1MoveUp.Size = new System.Drawing.Size(34, 28);
            this.buttonDetectionSubParameter1MoveUp.TabIndex = 1;
            this.buttonDetectionSubParameter1MoveUp.Text = "↑";
            this.buttonDetectionSubParameter1MoveUp.UseVisualStyleBackColor = true;
            // 
            // buttonDetectionSubParameter1MoveDown
            // 
            this.buttonDetectionSubParameter1MoveDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDetectionSubParameter1MoveDown.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.buttonDetectionSubParameter1MoveDown.Location = new System.Drawing.Point(52, 238);
            this.buttonDetectionSubParameter1MoveDown.Name = "buttonDetectionSubParameter1MoveDown";
            this.buttonDetectionSubParameter1MoveDown.Size = new System.Drawing.Size(34, 28);
            this.buttonDetectionSubParameter1MoveDown.TabIndex = 2;
            this.buttonDetectionSubParameter1MoveDown.Text = "↓";
            this.buttonDetectionSubParameter1MoveDown.UseVisualStyleBackColor = true;
            // 
            // buttonDetectionSubParameter1SaveOrder
            // 
            this.buttonDetectionSubParameter1SaveOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDetectionSubParameter1SaveOrder.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.buttonDetectionSubParameter1SaveOrder.Location = new System.Drawing.Point(92, 238);
            this.buttonDetectionSubParameter1SaveOrder.Name = "buttonDetectionSubParameter1SaveOrder";
            this.buttonDetectionSubParameter1SaveOrder.Size = new System.Drawing.Size(76, 28);
            this.buttonDetectionSubParameter1SaveOrder.TabIndex = 3;
            this.buttonDetectionSubParameter1SaveOrder.Text = "保存順序";
            this.buttonDetectionSubParameter1SaveOrder.UseVisualStyleBackColor = true;
            // 
            // checkBoxDetectionSubParameter1Enabled
            // 
            this.checkBoxDetectionSubParameter1Enabled.AutoSize = true;
            this.checkBoxDetectionSubParameter1Enabled.Location = new System.Drawing.Point(140, 6);
            this.checkBoxDetectionSubParameter1Enabled.Name = "checkBoxDetectionSubParameter1Enabled";
            this.checkBoxDetectionSubParameter1Enabled.Size = new System.Drawing.Size(15, 14);
            this.checkBoxDetectionSubParameter1Enabled.TabIndex = 4;
            this.checkBoxDetectionSubParameter1Enabled.UseVisualStyleBackColor = true;
            // 
            // groupBoxDetectionSubParameter2List
            // 
            this.groupBoxDetectionSubParameter2List.Controls.Add(this.checkBoxDetectionSubParameter2Enabled);
            this.groupBoxDetectionSubParameter2List.Controls.Add(this.buttonDetectionSubParameter2SaveOrder);
            this.groupBoxDetectionSubParameter2List.Controls.Add(this.buttonDetectionSubParameter2MoveDown);
            this.groupBoxDetectionSubParameter2List.Controls.Add(this.buttonDetectionSubParameter2MoveUp);
            this.groupBoxDetectionSubParameter2List.Controls.Add(this.listBoxDetectionSubParameter2);
            this.groupBoxDetectionSubParameter2List.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F);
            this.groupBoxDetectionSubParameter2List.Location = new System.Drawing.Point(624, 24);
            this.groupBoxDetectionSubParameter2List.Name = "groupBoxDetectionSubParameter2List";
            this.groupBoxDetectionSubParameter2List.Size = new System.Drawing.Size(180, 280);
            this.groupBoxDetectionSubParameter2List.TabIndex = 3;
            this.groupBoxDetectionSubParameter2List.TabStop = false;
            this.groupBoxDetectionSubParameter2List.Text = "子參數2";
            // 
            // listBoxDetectionSubParameter2
            // 
            this.listBoxDetectionSubParameter2.FormattingEnabled = true;
            this.listBoxDetectionSubParameter2.ItemHeight = 18;
            this.listBoxDetectionSubParameter2.Location = new System.Drawing.Point(12, 28);
            this.listBoxDetectionSubParameter2.Name = "listBoxDetectionSubParameter2";
            this.listBoxDetectionSubParameter2.Size = new System.Drawing.Size(156, 202);
            this.listBoxDetectionSubParameter2.TabIndex = 0;
            // 
            // buttonDetectionSubParameter2MoveUp
            // 
            this.buttonDetectionSubParameter2MoveUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDetectionSubParameter2MoveUp.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.buttonDetectionSubParameter2MoveUp.Location = new System.Drawing.Point(12, 238);
            this.buttonDetectionSubParameter2MoveUp.Name = "buttonDetectionSubParameter2MoveUp";
            this.buttonDetectionSubParameter2MoveUp.Size = new System.Drawing.Size(34, 28);
            this.buttonDetectionSubParameter2MoveUp.TabIndex = 1;
            this.buttonDetectionSubParameter2MoveUp.Text = "↑";
            this.buttonDetectionSubParameter2MoveUp.UseVisualStyleBackColor = true;
            // 
            // buttonDetectionSubParameter2MoveDown
            // 
            this.buttonDetectionSubParameter2MoveDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDetectionSubParameter2MoveDown.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.buttonDetectionSubParameter2MoveDown.Location = new System.Drawing.Point(52, 238);
            this.buttonDetectionSubParameter2MoveDown.Name = "buttonDetectionSubParameter2MoveDown";
            this.buttonDetectionSubParameter2MoveDown.Size = new System.Drawing.Size(34, 28);
            this.buttonDetectionSubParameter2MoveDown.TabIndex = 2;
            this.buttonDetectionSubParameter2MoveDown.Text = "↓";
            this.buttonDetectionSubParameter2MoveDown.UseVisualStyleBackColor = true;
            // 
            // buttonDetectionSubParameter2SaveOrder
            // 
            this.buttonDetectionSubParameter2SaveOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDetectionSubParameter2SaveOrder.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.buttonDetectionSubParameter2SaveOrder.Location = new System.Drawing.Point(92, 238);
            this.buttonDetectionSubParameter2SaveOrder.Name = "buttonDetectionSubParameter2SaveOrder";
            this.buttonDetectionSubParameter2SaveOrder.Size = new System.Drawing.Size(76, 28);
            this.buttonDetectionSubParameter2SaveOrder.TabIndex = 3;
            this.buttonDetectionSubParameter2SaveOrder.Text = "保存順序";
            this.buttonDetectionSubParameter2SaveOrder.UseVisualStyleBackColor = true;
            // 
            // checkBoxDetectionSubParameter2Enabled
            // 
            this.checkBoxDetectionSubParameter2Enabled.AutoSize = true;
            this.checkBoxDetectionSubParameter2Enabled.Location = new System.Drawing.Point(140, 6);
            this.checkBoxDetectionSubParameter2Enabled.Name = "checkBoxDetectionSubParameter2Enabled";
            this.checkBoxDetectionSubParameter2Enabled.Size = new System.Drawing.Size(15, 14);
            this.checkBoxDetectionSubParameter2Enabled.TabIndex = 4;
            this.checkBoxDetectionSubParameter2Enabled.UseVisualStyleBackColor = true;
            // 
            // groupBoxDetectionSubParameter3List
            // 
            this.groupBoxDetectionSubParameter3List.Controls.Add(this.checkBoxDetectionSubParameter3Enabled);
            this.groupBoxDetectionSubParameter3List.Controls.Add(this.buttonDetectionSubParameter3SaveOrder);
            this.groupBoxDetectionSubParameter3List.Controls.Add(this.buttonDetectionSubParameter3MoveDown);
            this.groupBoxDetectionSubParameter3List.Controls.Add(this.buttonDetectionSubParameter3MoveUp);
            this.groupBoxDetectionSubParameter3List.Controls.Add(this.listBoxDetectionSubParameter3);
            this.groupBoxDetectionSubParameter3List.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F);
            this.groupBoxDetectionSubParameter3List.Location = new System.Drawing.Point(818, 24);
            this.groupBoxDetectionSubParameter3List.Name = "groupBoxDetectionSubParameter3List";
            this.groupBoxDetectionSubParameter3List.Size = new System.Drawing.Size(180, 280);
            this.groupBoxDetectionSubParameter3List.TabIndex = 4;
            this.groupBoxDetectionSubParameter3List.TabStop = false;
            this.groupBoxDetectionSubParameter3List.Text = "子參數3";
            // 
            // listBoxDetectionSubParameter3
            // 
            this.listBoxDetectionSubParameter3.FormattingEnabled = true;
            this.listBoxDetectionSubParameter3.ItemHeight = 18;
            this.listBoxDetectionSubParameter3.Location = new System.Drawing.Point(12, 28);
            this.listBoxDetectionSubParameter3.Name = "listBoxDetectionSubParameter3";
            this.listBoxDetectionSubParameter3.Size = new System.Drawing.Size(156, 202);
            this.listBoxDetectionSubParameter3.TabIndex = 0;
            // 
            // buttonDetectionSubParameter3MoveUp
            // 
            this.buttonDetectionSubParameter3MoveUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDetectionSubParameter3MoveUp.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.buttonDetectionSubParameter3MoveUp.Location = new System.Drawing.Point(12, 238);
            this.buttonDetectionSubParameter3MoveUp.Name = "buttonDetectionSubParameter3MoveUp";
            this.buttonDetectionSubParameter3MoveUp.Size = new System.Drawing.Size(34, 28);
            this.buttonDetectionSubParameter3MoveUp.TabIndex = 1;
            this.buttonDetectionSubParameter3MoveUp.Text = "↑";
            this.buttonDetectionSubParameter3MoveUp.UseVisualStyleBackColor = true;
            // 
            // buttonDetectionSubParameter3MoveDown
            // 
            this.buttonDetectionSubParameter3MoveDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDetectionSubParameter3MoveDown.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.buttonDetectionSubParameter3MoveDown.Location = new System.Drawing.Point(52, 238);
            this.buttonDetectionSubParameter3MoveDown.Name = "buttonDetectionSubParameter3MoveDown";
            this.buttonDetectionSubParameter3MoveDown.Size = new System.Drawing.Size(34, 28);
            this.buttonDetectionSubParameter3MoveDown.TabIndex = 2;
            this.buttonDetectionSubParameter3MoveDown.Text = "↓";
            this.buttonDetectionSubParameter3MoveDown.UseVisualStyleBackColor = true;
            // 
            // buttonDetectionSubParameter3SaveOrder
            // 
            this.buttonDetectionSubParameter3SaveOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDetectionSubParameter3SaveOrder.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.buttonDetectionSubParameter3SaveOrder.Location = new System.Drawing.Point(92, 238);
            this.buttonDetectionSubParameter3SaveOrder.Name = "buttonDetectionSubParameter3SaveOrder";
            this.buttonDetectionSubParameter3SaveOrder.Size = new System.Drawing.Size(76, 28);
            this.buttonDetectionSubParameter3SaveOrder.TabIndex = 3;
            this.buttonDetectionSubParameter3SaveOrder.Text = "保存順序";
            this.buttonDetectionSubParameter3SaveOrder.UseVisualStyleBackColor = true;
            // 
            // checkBoxDetectionSubParameter3Enabled
            // 
            this.checkBoxDetectionSubParameter3Enabled.AutoSize = true;
            this.checkBoxDetectionSubParameter3Enabled.Location = new System.Drawing.Point(140, 6);
            this.checkBoxDetectionSubParameter3Enabled.Name = "checkBoxDetectionSubParameter3Enabled";
            this.checkBoxDetectionSubParameter3Enabled.Size = new System.Drawing.Size(15, 14);
            this.checkBoxDetectionSubParameter3Enabled.TabIndex = 4;
            this.checkBoxDetectionSubParameter3Enabled.UseVisualStyleBackColor = true;
            // 
            // buttonDetectionSaveParameterReference
            // 
            this.buttonDetectionSaveParameterReference.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDetectionSaveParameterReference.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.buttonDetectionSaveParameterReference.Location = new System.Drawing.Point(854, 320);
            this.buttonDetectionSaveParameterReference.Name = "buttonDetectionSaveParameterReference";
            this.buttonDetectionSaveParameterReference.Size = new System.Drawing.Size(144, 32);
            this.buttonDetectionSaveParameterReference.TabIndex = 5;
            this.buttonDetectionSaveParameterReference.Text = "儲存關聯";
            this.buttonDetectionSaveParameterReference.UseVisualStyleBackColor = true;
            // 
            // labelJudgementCriteriaTitle
            // 
            this.labelJudgementCriteriaTitle.AutoSize = true;
            this.labelJudgementCriteriaTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(134)))), ((int)(((byte)(138)))));
            this.labelJudgementCriteriaTitle.Location = new System.Drawing.Point(31, 21);
            this.labelJudgementCriteriaTitle.Name = "labelJudgementCriteriaTitle";
            this.labelJudgementCriteriaTitle.Size = new System.Drawing.Size(96, 18);
            this.labelJudgementCriteriaTitle.TabIndex = 0;
            this.labelJudgementCriteriaTitle.Text = "良品判斷條件";
            // 
            // buttonJudgementSyntaxHelp
            // 
            this.buttonJudgementSyntaxHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonJudgementSyntaxHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonJudgementSyntaxHelp.FlatAppearance.BorderSize = 0;
            this.buttonJudgementSyntaxHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonJudgementSyntaxHelp.Location = new System.Drawing.Point(930, 16);
            this.buttonJudgementSyntaxHelp.Name = "buttonJudgementSyntaxHelp";
            this.buttonJudgementSyntaxHelp.Size = new System.Drawing.Size(72, 28);
            this.buttonJudgementSyntaxHelp.TabIndex = 1;
            this.buttonJudgementSyntaxHelp.Text = "語法";
            this.buttonJudgementSyntaxHelp.UseVisualStyleBackColor = false;
            this.buttonJudgementSyntaxHelp.Click += new System.EventHandler(this.JudgementSyntaxHelpButton_Click);
            // 
            // panelJudgementCriteria
            // 
            this.panelJudgementCriteria.BackColor = System.Drawing.Color.White;
            this.panelJudgementCriteria.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelJudgementCriteria.Controls.Add(this.labelJudgementName);
            this.panelJudgementCriteria.Controls.Add(this.textBoxJudgementName);
            this.panelJudgementCriteria.Controls.Add(this.labelJudgementCalculation);
            this.panelJudgementCriteria.Controls.Add(this.textBoxJudgementCalculation);
            this.panelJudgementCriteria.Controls.Add(this.labelJudgementSpec);
            this.panelJudgementCriteria.Controls.Add(this.textBoxJudgementSpec);
            this.panelJudgementCriteria.Controls.Add(this.labelJudgementCalculationB);
            this.panelJudgementCriteria.Controls.Add(this.textBoxJudgementCalculationB);
            this.panelJudgementCriteria.Controls.Add(this.labelJudgementSpecB);
            this.panelJudgementCriteria.Controls.Add(this.textBoxJudgementSpecB);
            this.panelJudgementCriteria.Controls.Add(this.buttonJudgementAdd);
            this.panelJudgementCriteria.Controls.Add(this.buttonJudgementReset);
            this.panelJudgementCriteria.Controls.Add(this.buttonJudgementSave);
            this.panelJudgementCriteria.Controls.Add(this.buttonJudgementMoveUp);
            this.panelJudgementCriteria.Controls.Add(this.buttonJudgementMoveDown);
            this.panelJudgementCriteria.Controls.Add(this.dataGridViewJudgementCriteria);
            this.panelJudgementCriteria.Location = new System.Drawing.Point(28, 46);
            this.panelJudgementCriteria.Name = "panelJudgementCriteria";
            this.panelJudgementCriteria.Size = new System.Drawing.Size(976, 560);
            this.panelJudgementCriteria.TabIndex = 1;
            // 
            // labelJudgementName
            // 
            this.labelJudgementName.AutoSize = true;
            this.labelJudgementName.Location = new System.Drawing.Point(24, 20);
            this.labelJudgementName.Name = "labelJudgementName";
            this.labelJudgementName.Size = new System.Drawing.Size(56, 18);
            this.labelJudgementName.TabIndex = 0;
            this.labelJudgementName.Text = "規則名稱";
            // 
            // textBoxJudgementName
            // 
            this.textBoxJudgementName.Location = new System.Drawing.Point(160, 16);
            this.textBoxJudgementName.Name = "textBoxJudgementName";
            this.textBoxJudgementName.Size = new System.Drawing.Size(260, 25);
            this.textBoxJudgementName.TabIndex = 1;
            // 
            // labelJudgementCalculation
            // 
            this.labelJudgementCalculation.AutoSize = true;
            this.labelJudgementCalculation.Location = new System.Drawing.Point(24, 58);
            this.labelJudgementCalculation.Name = "labelJudgementCalculation";
            this.labelJudgementCalculation.Size = new System.Drawing.Size(76, 18);
            this.labelJudgementCalculation.TabIndex = 2;
            this.labelJudgementCalculation.Text = "A 規計算式";
            // 
            // textBoxJudgementCalculation
            // 
            this.textBoxJudgementCalculation.Location = new System.Drawing.Point(160, 54);
            this.textBoxJudgementCalculation.Name = "textBoxJudgementCalculation";
            this.textBoxJudgementCalculation.Size = new System.Drawing.Size(260, 25);
            this.textBoxJudgementCalculation.TabIndex = 3;
            // 
            // labelJudgementSpec
            // 
            this.labelJudgementSpec.AutoSize = true;
            this.labelJudgementSpec.Location = new System.Drawing.Point(24, 96);
            this.labelJudgementSpec.Name = "labelJudgementSpec";
            this.labelJudgementSpec.Size = new System.Drawing.Size(62, 18);
            this.labelJudgementSpec.TabIndex = 4;
            this.labelJudgementSpec.Text = "A 規規格";
            // 
            // textBoxJudgementSpec
            // 
            this.textBoxJudgementSpec.Location = new System.Drawing.Point(160, 92);
            this.textBoxJudgementSpec.Name = "textBoxJudgementSpec";
            this.textBoxJudgementSpec.Size = new System.Drawing.Size(260, 25);
            this.textBoxJudgementSpec.TabIndex = 5;
            // 
            // labelJudgementCalculationB
            // 
            this.labelJudgementCalculationB.AutoSize = true;
            this.labelJudgementCalculationB.Location = new System.Drawing.Point(24, 134);
            this.labelJudgementCalculationB.Name = "labelJudgementCalculationB";
            this.labelJudgementCalculationB.Size = new System.Drawing.Size(76, 18);
            this.labelJudgementCalculationB.TabIndex = 6;
            this.labelJudgementCalculationB.Text = "B 規計算式";
            // 
            // textBoxJudgementCalculationB
            // 
            this.textBoxJudgementCalculationB.Location = new System.Drawing.Point(160, 130);
            this.textBoxJudgementCalculationB.Name = "textBoxJudgementCalculationB";
            this.textBoxJudgementCalculationB.Size = new System.Drawing.Size(260, 25);
            this.textBoxJudgementCalculationB.TabIndex = 7;
            // 
            // labelJudgementSpecB
            // 
            this.labelJudgementSpecB.AutoSize = true;
            this.labelJudgementSpecB.Location = new System.Drawing.Point(24, 172);
            this.labelJudgementSpecB.Name = "labelJudgementSpecB";
            this.labelJudgementSpecB.Size = new System.Drawing.Size(62, 18);
            this.labelJudgementSpecB.TabIndex = 8;
            this.labelJudgementSpecB.Text = "B 規規格";
            // 
            // textBoxJudgementSpecB
            // 
            this.textBoxJudgementSpecB.Location = new System.Drawing.Point(160, 168);
            this.textBoxJudgementSpecB.Name = "textBoxJudgementSpecB";
            this.textBoxJudgementSpecB.Size = new System.Drawing.Size(260, 25);
            this.textBoxJudgementSpecB.TabIndex = 9;
            // 
            // buttonJudgementAdd
            // 
            this.buttonJudgementAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonJudgementAdd.FlatAppearance.BorderSize = 0;
            this.buttonJudgementAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonJudgementAdd.Location = new System.Drawing.Point(160, 208);
            this.buttonJudgementAdd.Name = "buttonJudgementAdd";
            this.buttonJudgementAdd.Size = new System.Drawing.Size(100, 36);
            this.buttonJudgementAdd.TabIndex = 10;
            this.buttonJudgementAdd.Text = "新增";
            this.buttonJudgementAdd.UseVisualStyleBackColor = false;
            this.buttonJudgementAdd.Click += new System.EventHandler(this.JudgementAddButton_Click);
            // 
            // buttonJudgementReset
            // 
            this.buttonJudgementReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonJudgementReset.FlatAppearance.BorderSize = 0;
            this.buttonJudgementReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonJudgementReset.Location = new System.Drawing.Point(272, 208);
            this.buttonJudgementReset.Name = "buttonJudgementReset";
            this.buttonJudgementReset.Size = new System.Drawing.Size(100, 36);
            this.buttonJudgementReset.TabIndex = 11;
            this.buttonJudgementReset.Text = "重新輸入";
            this.buttonJudgementReset.UseVisualStyleBackColor = false;
            this.buttonJudgementReset.Click += new System.EventHandler(this.JudgementResetButton_Click);
            // 
            // buttonJudgementSave
            // 
            this.buttonJudgementSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonJudgementSave.FlatAppearance.BorderSize = 0;
            this.buttonJudgementSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonJudgementSave.Location = new System.Drawing.Point(800, 470);
            this.buttonJudgementSave.Name = "buttonJudgementSave";
            this.buttonJudgementSave.Size = new System.Drawing.Size(140, 36);
            this.buttonJudgementSave.TabIndex = 12;
            this.buttonJudgementSave.Text = "儲存判斷條件";
            this.buttonJudgementSave.UseVisualStyleBackColor = false;
            this.buttonJudgementSave.Click += new System.EventHandler(this.JudgementSaveButton_Click);
            // 
            // buttonJudgementMoveUp
            // 
            this.buttonJudgementMoveUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonJudgementMoveUp.FlatAppearance.BorderSize = 0;
            this.buttonJudgementMoveUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonJudgementMoveUp.Location = new System.Drawing.Point(650, 470);
            this.buttonJudgementMoveUp.Name = "buttonJudgementMoveUp";
            this.buttonJudgementMoveUp.Size = new System.Drawing.Size(68, 36);
            this.buttonJudgementMoveUp.TabIndex = 13;
            this.buttonJudgementMoveUp.Text = "↑";
            this.buttonJudgementMoveUp.UseVisualStyleBackColor = false;
            this.buttonJudgementMoveUp.Click += new System.EventHandler(this.JudgementMoveUpButton_Click);
            // 
            // buttonJudgementMoveDown
            // 
            this.buttonJudgementMoveDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonJudgementMoveDown.FlatAppearance.BorderSize = 0;
            this.buttonJudgementMoveDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonJudgementMoveDown.Location = new System.Drawing.Point(724, 470);
            this.buttonJudgementMoveDown.Name = "buttonJudgementMoveDown";
            this.buttonJudgementMoveDown.Size = new System.Drawing.Size(68, 36);
            this.buttonJudgementMoveDown.TabIndex = 14;
            this.buttonJudgementMoveDown.Text = "↓";
            this.buttonJudgementMoveDown.UseVisualStyleBackColor = false;
            this.buttonJudgementMoveDown.Click += new System.EventHandler(this.JudgementMoveDownButton_Click);
            // 
            // dataGridViewJudgementCriteria
            // 
            this.dataGridViewJudgementCriteria.AllowUserToAddRows = false;
            this.dataGridViewJudgementCriteria.AllowUserToDeleteRows = false;
            this.dataGridViewJudgementCriteria.AllowUserToResizeRows = false;
            this.dataGridViewJudgementCriteria.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewJudgementCriteria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewJudgementCriteria.Location = new System.Drawing.Point(24, 256);
            this.dataGridViewJudgementCriteria.Name = "dataGridViewJudgementCriteria";
            this.dataGridViewJudgementCriteria.ReadOnly = true;
            this.dataGridViewJudgementCriteria.RowHeadersVisible = false;
            this.dataGridViewJudgementCriteria.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewJudgementCriteria.Size = new System.Drawing.Size(916, 214);
            this.dataGridViewJudgementCriteria.TabIndex = 15;
            // 
            // tabPageMeasureDistance
            // 
            this.tabPageMeasureDistance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.tabPageMeasureDistance.Controls.Add(this.panelMeasureSource);
            this.tabPageMeasureDistance.Controls.Add(this.dataGridViewMeasureRecords);
            this.tabPageMeasureDistance.Controls.Add(this.buttonSaveMeasureRecords);
            this.tabPageMeasureDistance.Controls.Add(this.panelMeasurePreview);
            this.tabPageMeasureDistance.Location = new System.Drawing.Point(4, 26);
            this.tabPageMeasureDistance.Name = "tabPageMeasureDistance";
            this.tabPageMeasureDistance.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMeasureDistance.Size = new System.Drawing.Size(1032, 656);
            this.tabPageMeasureDistance.TabIndex = 2;
            this.tabPageMeasureDistance.Text = "框選量測的距離";
            // 
            // panelMeasureSource
            // 
            this.panelMeasureSource.BackColor = System.Drawing.Color.White;
            this.panelMeasureSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMeasureSource.Controls.Add(this.labelMeasureSource);
            this.panelMeasureSource.Controls.Add(this.comboBoxMeasureSource);
            this.panelMeasureSource.Controls.Add(this.buttonParallelMeasure);
            this.panelMeasureSource.Controls.Add(this.buttonPerpendicularMeasure);
            this.panelMeasureSource.Controls.Add(this.buttonSaveMeasurePoint);
            this.panelMeasureSource.Controls.Add(this.buttonClearMeasurePoint);
            this.panelMeasureSource.Controls.Add(this.labelMeasureStatus);
            this.panelMeasureSource.Location = new System.Drawing.Point(20, 20);
            this.panelMeasureSource.Name = "panelMeasureSource";
            this.panelMeasureSource.Size = new System.Drawing.Size(330, 238);
            this.panelMeasureSource.TabIndex = 0;
            // 
            // labelMeasureSource
            // 
            this.labelMeasureSource.AutoSize = true;
            this.labelMeasureSource.Location = new System.Drawing.Point(16, 16);
            this.labelMeasureSource.Name = "labelMeasureSource";
            this.labelMeasureSource.Size = new System.Drawing.Size(88, 18);
            this.labelMeasureSource.TabIndex = 0;
            this.labelMeasureSource.Text = "前處理影像來源";
            // 
            // comboBoxMeasureSource
            // 
            this.comboBoxMeasureSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMeasureSource.FormattingEnabled = true;
            this.comboBoxMeasureSource.Location = new System.Drawing.Point(16, 42);
            this.comboBoxMeasureSource.Name = "comboBoxMeasureSource";
            this.comboBoxMeasureSource.Size = new System.Drawing.Size(280, 25);
            this.comboBoxMeasureSource.TabIndex = 1;
            // 
            // buttonParallelMeasure
            // 
            this.buttonParallelMeasure.Location = new System.Drawing.Point(16, 76);
            this.buttonParallelMeasure.Name = "buttonParallelMeasure";
            this.buttonParallelMeasure.Size = new System.Drawing.Size(136, 36);
            this.buttonParallelMeasure.TabIndex = 2;
            this.buttonParallelMeasure.Text = "平行";
            this.buttonParallelMeasure.UseVisualStyleBackColor = true;
            // 
            // buttonPerpendicularMeasure
            // 
            this.buttonPerpendicularMeasure.Location = new System.Drawing.Point(160, 76);
            this.buttonPerpendicularMeasure.Name = "buttonPerpendicularMeasure";
            this.buttonPerpendicularMeasure.Size = new System.Drawing.Size(136, 36);
            this.buttonPerpendicularMeasure.TabIndex = 3;
            this.buttonPerpendicularMeasure.Text = "垂直";
            this.buttonPerpendicularMeasure.UseVisualStyleBackColor = true;
            // 
            // buttonSaveMeasurePoint
            // 
            this.buttonSaveMeasurePoint.Location = new System.Drawing.Point(16, 126);
            this.buttonSaveMeasurePoint.Name = "buttonSaveMeasurePoint";
            this.buttonSaveMeasurePoint.Size = new System.Drawing.Size(280, 40);
            this.buttonSaveMeasurePoint.TabIndex = 4;
            this.buttonSaveMeasurePoint.Text = "保存量測點";
            this.buttonSaveMeasurePoint.UseVisualStyleBackColor = true;
            // 
            // buttonClearMeasurePoint
            // 
            this.buttonClearMeasurePoint.Location = new System.Drawing.Point(16, 172);
            this.buttonClearMeasurePoint.Name = "buttonClearMeasurePoint";
            this.buttonClearMeasurePoint.Size = new System.Drawing.Size(280, 40);
            this.buttonClearMeasurePoint.TabIndex = 5;
            this.buttonClearMeasurePoint.Text = "清除量測點";
            this.buttonClearMeasurePoint.UseVisualStyleBackColor = true;
            // 
            // labelMeasureStatus
            // 
            this.labelMeasureStatus.AutoSize = true;
            this.labelMeasureStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(134)))), ((int)(((byte)(138)))));
            this.labelMeasureStatus.Location = new System.Drawing.Point(16, 220);
            this.labelMeasureStatus.Name = "labelMeasureStatus";
            this.labelMeasureStatus.Size = new System.Drawing.Size(188, 18);
            this.labelMeasureStatus.TabIndex = 6;
            this.labelMeasureStatus.Text = "點兩下影像建立兩點量測";
            // 
            // panelMeasurePreview
            // 
            this.panelMeasurePreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.panelMeasurePreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMeasurePreview.Controls.Add(this.pictureBoxMeasurePreview);
            this.panelMeasurePreview.Location = new System.Drawing.Point(20, 264);
            this.panelMeasurePreview.Name = "panelMeasurePreview";
            this.panelMeasurePreview.Size = new System.Drawing.Size(992, 352);
            this.panelMeasurePreview.TabIndex = 1;
            // 
            // pictureBoxMeasurePreview
            // 
            this.pictureBoxMeasurePreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.pictureBoxMeasurePreview.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxMeasurePreview.Name = "pictureBoxMeasurePreview";
            this.pictureBoxMeasurePreview.Size = new System.Drawing.Size(990, 350);
            this.pictureBoxMeasurePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxMeasurePreview.TabIndex = 0;
            this.pictureBoxMeasurePreview.TabStop = false;
            // 
            // dataGridViewMeasureRecords
            // 
            this.dataGridViewMeasureRecords.Location = new System.Drawing.Point(366, 20);
            this.dataGridViewMeasureRecords.Name = "dataGridViewMeasureRecords";
            this.dataGridViewMeasureRecords.Size = new System.Drawing.Size(646, 178);
            this.dataGridViewMeasureRecords.TabIndex = 2;
            // 
            // buttonSaveMeasureRecords
            // 
            this.buttonSaveMeasureRecords.Location = new System.Drawing.Point(366, 206);
            this.buttonSaveMeasureRecords.Name = "buttonSaveMeasureRecords";
            this.buttonSaveMeasureRecords.Size = new System.Drawing.Size(646, 42);
            this.buttonSaveMeasureRecords.TabIndex = 3;
            this.buttonSaveMeasureRecords.Text = "保存紀錄";
            this.buttonSaveMeasureRecords.UseVisualStyleBackColor = true;
            // 
            // tabPageMultiImageConfirm
            // 
            this.tabPageMultiImageConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.tabPageMultiImageConfirm.Controls.Add(this.buttonLoadMultiImageFolder);
            this.tabPageMultiImageConfirm.Controls.Add(this.groupBoxMultiImagePreviewSource);
            this.tabPageMultiImageConfirm.Controls.Add(this.labelMultiImageStatus);
            this.tabPageMultiImageConfirm.Controls.Add(this.buttonMultiImagePrev);
            this.tabPageMultiImageConfirm.Controls.Add(this.buttonMultiImageNext);
            this.tabPageMultiImageConfirm.Controls.Add(this.buttonMultiImageLineSequence);
            this.tabPageMultiImageConfirm.Controls.Add(this.comboBoxMultiImageLineDisplayMode);
            this.tabPageMultiImageConfirm.Controls.Add(this.panelMultiImageInfo);
            this.tabPageMultiImageConfirm.Controls.Add(this.panelMultiImageConfirmViewport);
            this.tabPageMultiImageConfirm.Location = new System.Drawing.Point(4, 26);
            this.tabPageMultiImageConfirm.Name = "tabPageMultiImageConfirm";
            this.tabPageMultiImageConfirm.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMultiImageConfirm.Size = new System.Drawing.Size(1032, 656);
            this.tabPageMultiImageConfirm.TabIndex = 3;
            this.tabPageMultiImageConfirm.Text = "多影像確認結果";
            // 
            // buttonLoadMultiImageFolder
            // 
            this.buttonLoadMultiImageFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonLoadMultiImageFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLoadMultiImageFolder.Location = new System.Drawing.Point(20, 432);
            this.buttonLoadMultiImageFolder.Name = "buttonLoadMultiImageFolder";
            this.buttonLoadMultiImageFolder.Size = new System.Drawing.Size(180, 36);
            this.buttonLoadMultiImageFolder.TabIndex = 1;
            this.buttonLoadMultiImageFolder.Text = "讀取資料夾";
            this.buttonLoadMultiImageFolder.UseVisualStyleBackColor = false;
            this.buttonLoadMultiImageFolder.Click += new System.EventHandler(this.LoadMultiImageFolder_Click);
            // 
            // groupBoxMultiImagePreviewSource
            // 
            this.groupBoxMultiImagePreviewSource.Controls.Add(this.comboBoxMultiImagePreviewSource);
            this.groupBoxMultiImagePreviewSource.Controls.Add(this.buttonLoadMultiImagePreprocess);
            this.groupBoxMultiImagePreviewSource.Controls.Add(this.buttonLoadMultiImageOriginal);
            this.groupBoxMultiImagePreviewSource.Location = new System.Drawing.Point(20, 476);
            this.groupBoxMultiImagePreviewSource.Name = "groupBoxMultiImagePreviewSource";
            this.groupBoxMultiImagePreviewSource.Size = new System.Drawing.Size(440, 126);
            this.groupBoxMultiImagePreviewSource.TabIndex = 5;
            this.groupBoxMultiImagePreviewSource.TabStop = false;
            this.groupBoxMultiImagePreviewSource.Text = "預覽來源";
            // 
            // comboBoxMultiImagePreviewSource
            // 
            this.comboBoxMultiImagePreviewSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMultiImagePreviewSource.FormattingEnabled = true;
            this.comboBoxMultiImagePreviewSource.Location = new System.Drawing.Point(16, 28);
            this.comboBoxMultiImagePreviewSource.Name = "comboBoxMultiImagePreviewSource";
            this.comboBoxMultiImagePreviewSource.Size = new System.Drawing.Size(202, 25);
            this.comboBoxMultiImagePreviewSource.TabIndex = 0;
            this.comboBoxMultiImagePreviewSource.SelectedIndexChanged += new System.EventHandler(this.MultiImagePreviewSource_SelectedIndexChanged);
            // 
            // buttonLoadMultiImagePreprocess
            // 
            this.buttonLoadMultiImagePreprocess.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonLoadMultiImagePreprocess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLoadMultiImagePreprocess.Location = new System.Drawing.Point(16, 66);
            this.buttonLoadMultiImagePreprocess.Name = "buttonLoadMultiImagePreprocess";
            this.buttonLoadMultiImagePreprocess.Size = new System.Drawing.Size(202, 36);
            this.buttonLoadMultiImagePreprocess.TabIndex = 1;
            this.buttonLoadMultiImagePreprocess.Text = "讀取前處理影像";
            this.buttonLoadMultiImagePreprocess.UseVisualStyleBackColor = false;
            this.buttonLoadMultiImagePreprocess.Click += new System.EventHandler(this.LoadMultiImagePreprocess_Click);
            // 
            // buttonLoadMultiImageOriginal
            // 
            this.buttonLoadMultiImageOriginal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonLoadMultiImageOriginal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLoadMultiImageOriginal.Location = new System.Drawing.Point(230, 28);
            this.buttonLoadMultiImageOriginal.Name = "buttonLoadMultiImageOriginal";
            this.buttonLoadMultiImageOriginal.Size = new System.Drawing.Size(188, 74);
            this.buttonLoadMultiImageOriginal.TabIndex = 2;
            this.buttonLoadMultiImageOriginal.Text = "原始影像";
            this.buttonLoadMultiImageOriginal.UseVisualStyleBackColor = false;
            this.buttonLoadMultiImageOriginal.Click += new System.EventHandler(this.LoadMultiImageOriginal_Click);
            // 
            // labelMultiImageStatus
            // 
            this.labelMultiImageStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelMultiImageStatus.Location = new System.Drawing.Point(290, 432);
            this.labelMultiImageStatus.Name = "labelMultiImageStatus";
            this.labelMultiImageStatus.Size = new System.Drawing.Size(160, 36);
            this.labelMultiImageStatus.TabIndex = 2;
            this.labelMultiImageStatus.Text = "0 / 0";
            this.labelMultiImageStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonMultiImagePrev
            // 
            this.buttonMultiImagePrev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonMultiImagePrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMultiImagePrev.Location = new System.Drawing.Point(578, 432);
            this.buttonMultiImagePrev.Name = "buttonMultiImagePrev";
            this.buttonMultiImagePrev.Size = new System.Drawing.Size(44, 36);
            this.buttonMultiImagePrev.TabIndex = 3;
            this.buttonMultiImagePrev.Text = "←";
            this.buttonMultiImagePrev.UseVisualStyleBackColor = false;
            this.buttonMultiImagePrev.Click += new System.EventHandler(this.MultiImageConfirmPrev_Click);
            // 
            // buttonMultiImageNext
            // 
            this.buttonMultiImageNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonMultiImageNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMultiImageNext.Location = new System.Drawing.Point(628, 432);
            this.buttonMultiImageNext.Name = "buttonMultiImageNext";
            this.buttonMultiImageNext.Size = new System.Drawing.Size(44, 36);
            this.buttonMultiImageNext.TabIndex = 4;
            this.buttonMultiImageNext.Text = "→";
            this.buttonMultiImageNext.UseVisualStyleBackColor = false;
            this.buttonMultiImageNext.Click += new System.EventHandler(this.MultiImageConfirmNext_Click);
            // 
            // buttonMultiImageLineSequence
            // 
            this.buttonMultiImageLineSequence.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.buttonMultiImageLineSequence.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMultiImageLineSequence.Location = new System.Drawing.Point(578, 476);
            this.buttonMultiImageLineSequence.Name = "buttonMultiImageLineSequence";
            this.buttonMultiImageLineSequence.Size = new System.Drawing.Size(94, 36);
            this.buttonMultiImageLineSequence.TabIndex = 7;
            this.buttonMultiImageLineSequence.Text = "線序";
            this.buttonMultiImageLineSequence.UseVisualStyleBackColor = false;
            this.buttonMultiImageLineSequence.Click += new System.EventHandler(this.MultiImageLineSequence_Click);
            // 
            // comboBoxMultiImageLineDisplayMode
            // 
            this.comboBoxMultiImageLineDisplayMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMultiImageLineDisplayMode.FormattingEnabled = true;
            this.comboBoxMultiImageLineDisplayMode.Location = new System.Drawing.Point(578, 520);
            this.comboBoxMultiImageLineDisplayMode.Name = "comboBoxMultiImageLineDisplayMode";
            this.comboBoxMultiImageLineDisplayMode.Size = new System.Drawing.Size(210, 25);
            this.comboBoxMultiImageLineDisplayMode.TabIndex = 8;
            this.comboBoxMultiImageLineDisplayMode.SelectedIndexChanged += new System.EventHandler(this.MultiImageLineDisplayMode_SelectedIndexChanged);
            // 
            // panelMultiImageInfo
            // 
            this.panelMultiImageInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.panelMultiImageInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMultiImageInfo.Controls.Add(this.tabControlMultiImageInfo);
            this.panelMultiImageInfo.Location = new System.Drawing.Point(690, 20);
            this.panelMultiImageInfo.Name = "panelMultiImageInfo";
            this.panelMultiImageInfo.Size = new System.Drawing.Size(326, 400);
            this.panelMultiImageInfo.TabIndex = 9;
            //
            // tabControlMultiImageInfo
            //
            this.tabControlMultiImageInfo.Controls.Add(this.tabPageMultiImageRawData);
            this.tabControlMultiImageInfo.Controls.Add(this.tabPageMultiImageJudgementResult);
            this.tabControlMultiImageInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMultiImageInfo.Location = new System.Drawing.Point(0, 0);
            this.tabControlMultiImageInfo.Name = "tabControlMultiImageInfo";
            this.tabControlMultiImageInfo.SelectedIndex = 0;
            this.tabControlMultiImageInfo.Size = new System.Drawing.Size(324, 398);
            this.tabControlMultiImageInfo.TabIndex = 0;
            //
            // tabPageMultiImageRawData
            //
            this.tabPageMultiImageRawData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.tabPageMultiImageRawData.Controls.Add(this.dataGridViewMultiImageInfo);
            this.tabPageMultiImageRawData.Location = new System.Drawing.Point(4, 26);
            this.tabPageMultiImageRawData.Name = "tabPageMultiImageRawData";
            this.tabPageMultiImageRawData.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMultiImageRawData.Size = new System.Drawing.Size(316, 368);
            this.tabPageMultiImageRawData.TabIndex = 0;
            this.tabPageMultiImageRawData.Text = "原始資料";
            //
            // tabPageMultiImageJudgementResult
            //
            this.tabPageMultiImageJudgementResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.tabPageMultiImageJudgementResult.Location = new System.Drawing.Point(4, 26);
            this.tabPageMultiImageJudgementResult.Name = "tabPageMultiImageJudgementResult";
            this.tabPageMultiImageJudgementResult.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMultiImageJudgementResult.Size = new System.Drawing.Size(316, 368);
            this.tabPageMultiImageJudgementResult.TabIndex = 1;
            this.tabPageMultiImageJudgementResult.Text = "判定結果";
            // 
            // dataGridViewMultiImageInfo
            // 
            this.dataGridViewMultiImageInfo.AllowUserToAddRows = false;
            this.dataGridViewMultiImageInfo.AllowUserToDeleteRows = false;
            this.dataGridViewMultiImageInfo.AllowUserToResizeColumns = false;
            this.dataGridViewMultiImageInfo.AllowUserToResizeRows = false;
            this.dataGridViewMultiImageInfo.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.dataGridViewMultiImageInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewMultiImageInfo.ColumnHeadersVisible = false;
            this.dataGridViewMultiImageInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMultiImageInfo.MultiSelect = false;
            this.dataGridViewMultiImageInfo.Name = "dataGridViewMultiImageInfo";
            this.dataGridViewMultiImageInfo.ReadOnly = true;
            this.dataGridViewMultiImageInfo.RowHeadersVisible = false;
            this.dataGridViewMultiImageInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewMultiImageInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMultiImageInfo.Size = new System.Drawing.Size(310, 362);
            this.dataGridViewMultiImageInfo.TabIndex = 0;
            // 
            // panelMultiImageConfirmViewport
            // 
            this.panelMultiImageConfirmViewport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.panelMultiImageConfirmViewport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMultiImageConfirmViewport.Controls.Add(this.pictureBoxMultiImageConfirm);
            this.panelMultiImageConfirmViewport.Location = new System.Drawing.Point(20, 20);
            this.panelMultiImageConfirmViewport.Name = "panelMultiImageConfirmViewport";
            this.panelMultiImageConfirmViewport.Size = new System.Drawing.Size(652, 400);
            this.panelMultiImageConfirmViewport.TabIndex = 0;
            this.panelMultiImageConfirmViewport.TabStop = true;
            // 
            // pictureBoxMultiImageConfirm
            // 
            this.pictureBoxMultiImageConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.pictureBoxMultiImageConfirm.Location = new System.Drawing.Point(20, 20);
            this.pictureBoxMultiImageConfirm.Name = "pictureBoxMultiImageConfirm";
            this.pictureBoxMultiImageConfirm.Size = new System.Drawing.Size(496, 306);
            this.pictureBoxMultiImageConfirm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxMultiImageConfirm.TabIndex = 0;
            this.pictureBoxMultiImageConfirm.TabStop = false;
            // 
            // openFileDialogImage
            // 
            this.openFileDialogImage.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff|All Files|*.*";
            this.openFileDialogImage.Title = "選擇圖片";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.menuStripMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStripMain;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AOI Image Viewer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageImageViewer.ResumeLayout(false);
            this.tabPageImageViewer.PerformLayout();
            this.panelImageViewport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.tabPageBinarization.ResumeLayout(false);
            this.tabPageBinarization2.ResumeLayout(false);
            this.panelDualThresholdOriginal.ResumeLayout(false);
            this.panelDualThresholdOriginal.PerformLayout();
            this.panelDualThresholdOriginalViewport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDualThresholdOriginal)).EndInit();
            this.panelDualThresholdPreview.ResumeLayout(false);
            this.panelDualThresholdPreview.PerformLayout();
            this.panelDualThresholdPreviewViewport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDualThresholdPreview)).EndInit();
            this.panelDualThresholdControls.ResumeLayout(false);
            this.panelDualThresholdControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDualThresholdLower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDualThresholdLower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDualThresholdUpper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDualThresholdUpper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDualThresholdErode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDualThresholdDilate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDualThresholdOpen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDualThresholdClose)).EndInit();
            this.panelDualThresholdActions.ResumeLayout(false);
            this.panelBinaryOriginal.ResumeLayout(false);
            this.panelBinaryOriginal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBinaryOriginal)).EndInit();
            this.panelActivePreprocess.ResumeLayout(false);
            this.panelActivePreprocess.PerformLayout();
            this.panelActiveViewport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxActivePreprocess)).EndInit();
            this.panelPreprocess1.ResumeLayout(false);
            this.panelPreprocess1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreprocess1)).EndInit();
            this.panelPreprocess2.ResumeLayout(false);
            this.panelPreprocess2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreprocess2)).EndInit();
            this.panelPreprocess3.ResumeLayout(false);
            this.panelPreprocess3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreprocess3)).EndInit();
            this.panelPreprocess4.ResumeLayout(false);
            this.panelPreprocess4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreprocess4)).EndInit();
            this.tabControlPreprocess.ResumeLayout(false);
            this.tabPagePreprocess1.ResumeLayout(false);
            this.tabPagePreprocess1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericThreshold1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericErode1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDilate1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOpen1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericClose1)).EndInit();
            this.tabPagePreprocess2.ResumeLayout(false);
            this.tabPagePreprocess2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericThreshold2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericErode2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDilate2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOpen2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericClose2)).EndInit();
            this.tabPagePreprocess3.ResumeLayout(false);
            this.tabPagePreprocess3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericThreshold3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericErode3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDilate3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOpen3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericClose3)).EndInit();
            this.tabPagePreprocess4.ResumeLayout(false);
            this.tabPagePreprocess4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericThreshold4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericErode4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDilate4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOpen4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericClose4)).EndInit();
            this.panelPreprocessActions.ResumeLayout(false);
            this.tabPageReferenceCorner.ResumeLayout(false);
            this.tabPageReferenceCorner.PerformLayout();
            this.panelReferenceCornerControls.ResumeLayout(false);
            this.panelReferenceCornerControls.PerformLayout();
            this.panelReferencePreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxReferencePreview)).EndInit();
            this.tabPageContinuousInspection.ResumeLayout(false);
            this.panelContinuousInspection.ResumeLayout(false);
            this.panelContinuousInspection.PerformLayout();
            this.groupBoxContinuousInspection1.ResumeLayout(false);
            this.panelContinuousInspectionPreview1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxContinuousInspection1)).EndInit();
            this.groupBoxContinuousInspection2.ResumeLayout(false);
            this.panelContinuousInspectionPreview2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxContinuousInspection2)).EndInit();
            this.groupBoxContinuousInspection3.ResumeLayout(false);
            this.panelContinuousInspectionPreview3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxContinuousInspection3)).EndInit();
            this.tabPageInnerSettings.ResumeLayout(false);
            this.tabPageInnerSettings.PerformLayout();
            this.panelInnerSettings.ResumeLayout(false);
            this.panelInnerSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericInnerCcdXPrecision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericInnerCcdYPrecision)).EndInit();
            this.tabPageJudgementCriteria.ResumeLayout(false);
            this.tabPageJudgementCriteria.PerformLayout();
            this.panelJudgementCriteria.ResumeLayout(false);
            this.panelJudgementCriteria.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewJudgementCriteria)).EndInit();
            this.tabPageDetectionParameterSummary.ResumeLayout(false);
            this.tabPageDetectionParameterSummary.PerformLayout();
            this.panelDetectionParameterSummary.ResumeLayout(false);
            this.panelDetectionParameterSummary.PerformLayout();
            this.tabPageMultiImageConfirm.ResumeLayout(false);
            this.groupBoxMultiImagePreviewSource.ResumeLayout(false);
            this.tabControlMultiImageInfo.ResumeLayout(false);
            this.tabPageMultiImageRawData.ResumeLayout(false);
            this.tabPageMultiImageJudgementResult.ResumeLayout(false);
            this.panelMultiImageInfo.ResumeLayout(false);
            this.tabControlMultiImageInfo.ResumeLayout(false);
            this.tabPageMultiImageRawData.ResumeLayout(false);
            this.tabPageMultiImageJudgementResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMultiImageInfo)).EndInit();
            this.panelMultiImageConfirmViewport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMultiImageConfirm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    }
}
