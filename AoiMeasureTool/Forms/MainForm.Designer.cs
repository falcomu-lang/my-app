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
        private System.Windows.Forms.Label labelOpenCvStatus;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelImageInfo;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageImageViewer;
        private System.Windows.Forms.TabPage tabPageBinarization;
        private System.Windows.Forms.TabPage tabPageReferenceCorner;
        private System.Windows.Forms.TabPage tabPageMeasureDistance;
        private System.Windows.Forms.TabPage tabPageMultiImageConfirm;
        private System.Windows.Forms.Label labelWorkspace;
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
            this.tabPageMultiImageConfirm.SuspendLayout();
            this.groupBoxMultiImagePreviewSource.SuspendLayout();
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
            this.tabControlMain.Controls.Add(this.tabPageReferenceCorner);
            this.tabControlMain.Controls.Add(this.tabPageMeasureDistance);
            this.tabControlMain.Controls.Add(this.tabPageMultiImageConfirm);
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
            this.tabPageMultiImageConfirm.ResumeLayout(false);
            this.groupBoxMultiImagePreviewSource.ResumeLayout(false);
            this.panelMultiImageConfirmViewport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMultiImageConfirm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    }
}
