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
        private System.Windows.Forms.Label labelOpenCvStatus;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelImageInfo;
        private System.Windows.Forms.Label labelWorkspace;
        private System.Windows.Forms.Panel panelImageViewport;
        private System.Windows.Forms.PictureBox pictureBoxImage;
        private System.Windows.Forms.Label labelPreprocessResults;
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
            this.components = new System.ComponentModel.Container();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenImage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.labelOpenCvStatus = new System.Windows.Forms.Label();
            this.buttonLoadImage = new System.Windows.Forms.Button();
            this.labelNavigation = new System.Windows.Forms.Label();
            this.labelAppName = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.tabControlPreprocess = new System.Windows.Forms.TabControl();
            this.tabPagePreprocess1 = new System.Windows.Forms.TabPage();
            this.tabPagePreprocess2 = new System.Windows.Forms.TabPage();
            this.tabPagePreprocess3 = new System.Windows.Forms.TabPage();
            this.tabPagePreprocess4 = new System.Windows.Forms.TabPage();
            this.panelPreprocess1 = new System.Windows.Forms.Panel();
            this.panelPreprocess2 = new System.Windows.Forms.Panel();
            this.panelPreprocess3 = new System.Windows.Forms.Panel();
            this.panelPreprocess4 = new System.Windows.Forms.Panel();
            this.pictureBoxPreprocess1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxPreprocess2 = new System.Windows.Forms.PictureBox();
            this.pictureBoxPreprocess3 = new System.Windows.Forms.PictureBox();
            this.pictureBoxPreprocess4 = new System.Windows.Forms.PictureBox();
            this.labelPreprocess1Title = new System.Windows.Forms.Label();
            this.labelPreprocess2Title = new System.Windows.Forms.Label();
            this.labelPreprocess3Title = new System.Windows.Forms.Label();
            this.labelPreprocess4Title = new System.Windows.Forms.Label();
            this.labelPreprocessResults = new System.Windows.Forms.Label();
            this.labelWorkspace = new System.Windows.Forms.Label();
            this.panelImageViewport = new System.Windows.Forms.Panel();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelImageInfo = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.openFileDialogImage = new System.Windows.Forms.OpenFileDialog();
            this.checkBoxPreprocess1Enabled = new System.Windows.Forms.CheckBox();
            this.checkBoxPreprocess2Enabled = new System.Windows.Forms.CheckBox();
            this.checkBoxPreprocess3Enabled = new System.Windows.Forms.CheckBox();
            this.checkBoxPreprocess4Enabled = new System.Windows.Forms.CheckBox();
            this.trackBarThreshold1 = new System.Windows.Forms.TrackBar();
            this.trackBarThreshold2 = new System.Windows.Forms.TrackBar();
            this.trackBarThreshold3 = new System.Windows.Forms.TrackBar();
            this.trackBarThreshold4 = new System.Windows.Forms.TrackBar();
            this.numericThreshold1 = CreateNumericUpDown(0, 255, 128);
            this.numericThreshold2 = CreateNumericUpDown(0, 255, 128);
            this.numericThreshold3 = CreateNumericUpDown(0, 255, 128);
            this.numericThreshold4 = CreateNumericUpDown(0, 255, 128);
            this.numericErode1 = CreateNumericUpDown(0, 20, 0);
            this.numericErode2 = CreateNumericUpDown(0, 20, 0);
            this.numericErode3 = CreateNumericUpDown(0, 20, 0);
            this.numericErode4 = CreateNumericUpDown(0, 20, 0);
            this.numericDilate1 = CreateNumericUpDown(0, 20, 0);
            this.numericDilate2 = CreateNumericUpDown(0, 20, 0);
            this.numericDilate3 = CreateNumericUpDown(0, 20, 0);
            this.numericDilate4 = CreateNumericUpDown(0, 20, 0);
            this.numericOpen1 = CreateNumericUpDown(0, 20, 0);
            this.numericOpen2 = CreateNumericUpDown(0, 20, 0);
            this.numericOpen3 = CreateNumericUpDown(0, 20, 0);
            this.numericOpen4 = CreateNumericUpDown(0, 20, 0);
            this.numericClose1 = CreateNumericUpDown(0, 20, 0);
            this.numericClose2 = CreateNumericUpDown(0, 20, 0);
            this.numericClose3 = CreateNumericUpDown(0, 20, 0);
            this.numericClose4 = CreateNumericUpDown(0, 20, 0);
            this.menuStripMain.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelImageViewport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.tabControlPreprocess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreprocess1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreprocess2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreprocess3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreprocess4)).BeginInit();
            this.SuspendLayout();

            ConfigureMenu();
            ConfigureSidebar();
            ConfigureMainWorkspace();
            ConfigureOriginalImageArea();
            ConfigurePreprocessResults();
            ConfigurePreprocessTabs();

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.menuStripMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStripMain;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AOI Image Viewer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelImageViewport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.tabControlPreprocess.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreprocess1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreprocess2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreprocess3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreprocess4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void ConfigureMenu()
        {
            this.menuStripMain.BackColor = System.Drawing.Color.FromArgb(248, 247, 247);
            this.menuStripMain.Font = new System.Drawing.Font("Microsoft JhengHei UI", 11F);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuFile, this.menuEdit, this.menuView, this.menuHelp });
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Padding = new System.Windows.Forms.Padding(18, 8, 0, 8);
            this.menuStripMain.Size = new System.Drawing.Size(1280, 42);
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuOpenImage, this.menuFileSeparator, this.menuExit });
            this.menuFile.Text = "檔案";
            this.menuOpenImage.Text = "讀取圖片";
            this.menuOpenImage.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
            this.menuOpenImage.Click += new System.EventHandler(this.LoadImageButton_Click);
            this.menuExit.Text = "結束";
            this.menuExit.Click += new System.EventHandler(this.CloseButton_Click);
            this.menuEdit.Text = "編輯";
            this.menuView.Text = "檢視";
            this.menuHelp.Text = "說明";
        }

        private void ConfigureSidebar()
        {
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(239, 243, 245);
            this.panelSidebar.Controls.Add(this.labelOpenCvStatus);
            this.panelSidebar.Controls.Add(this.buttonLoadImage);
            this.panelSidebar.Controls.Add(this.labelNavigation);
            this.panelSidebar.Controls.Add(this.labelAppName);
            this.panelSidebar.Location = new System.Drawing.Point(0, 42);
            this.panelSidebar.Size = new System.Drawing.Size(240, 758);
            this.labelAppName.AutoSize = true;
            this.labelAppName.Font = new System.Drawing.Font("Microsoft JhengHei UI", 16F, System.Drawing.FontStyle.Bold);
            this.labelAppName.Location = new System.Drawing.Point(20, 25);
            this.labelAppName.Text = "AOI Viewer";
            this.labelNavigation.AutoSize = true;
            this.labelNavigation.ForeColor = System.Drawing.Color.FromArgb(145, 149, 153);
            this.labelNavigation.Location = new System.Drawing.Point(20, 83);
            this.labelNavigation.Text = "工作項目";
            this.buttonLoadImage.BackColor = System.Drawing.Color.FromArgb(224, 228, 231);
            this.buttonLoadImage.FlatAppearance.BorderSize = 0;
            this.buttonLoadImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLoadImage.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F);
            this.buttonLoadImage.Location = new System.Drawing.Point(16, 116);
            this.buttonLoadImage.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.buttonLoadImage.Size = new System.Drawing.Size(208, 48);
            this.buttonLoadImage.Text = "+   讀取圖片";
            this.buttonLoadImage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLoadImage.Click += new System.EventHandler(this.LoadImageButton_Click);
            this.labelOpenCvStatus.AutoSize = true;
            this.labelOpenCvStatus.ForeColor = System.Drawing.Color.FromArgb(110, 115, 120);
            this.labelOpenCvStatus.Location = new System.Drawing.Point(24, 706);
            this.labelOpenCvStatus.Text = "OpenCV 4.13 已就緒";
        }

        private void ConfigureMainWorkspace()
        {
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.panelMain.Controls.Add(this.tabControlPreprocess);
            this.panelMain.Controls.Add(this.panelPreprocess1);
            this.panelMain.Controls.Add(this.panelPreprocess2);
            this.panelMain.Controls.Add(this.panelPreprocess3);
            this.panelMain.Controls.Add(this.panelPreprocess4);
            this.panelMain.Controls.Add(this.labelPreprocessResults);
            this.panelMain.Controls.Add(this.labelWorkspace);
            this.panelMain.Controls.Add(this.panelImageViewport);
            this.panelMain.Controls.Add(this.panelHeader);
            this.panelMain.Location = new System.Drawing.Point(240, 42);
            this.panelMain.Size = new System.Drawing.Size(1040, 758);
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.labelImageInfo);
            this.panelHeader.Controls.Add(this.labelTitle);
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Size = new System.Drawing.Size(1040, 72);
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft JhengHei UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelTitle.Location = new System.Drawing.Point(28, 23);
            this.labelTitle.Text = "影像前處理";
            this.labelImageInfo.AutoSize = true;
            this.labelImageInfo.ForeColor = System.Drawing.Color.FromArgb(125, 129, 134);
            this.labelImageInfo.Location = new System.Drawing.Point(210, 28);
            this.labelImageInfo.Text = "尚未讀取圖片";
        }

        private void ConfigureOriginalImageArea()
        {
            this.labelWorkspace.AutoSize = true;
            this.labelWorkspace.ForeColor = System.Drawing.Color.FromArgb(130, 134, 138);
            this.labelWorkspace.Location = new System.Drawing.Point(32, 88);
            this.labelWorkspace.Text = "原始圖片";
            this.panelImageViewport.BackColor = System.Drawing.Color.FromArgb(239, 241, 243);
            this.panelImageViewport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelImageViewport.Controls.Add(this.pictureBoxImage);
            this.panelImageViewport.Location = new System.Drawing.Point(32, 118);
            this.panelImageViewport.Size = new System.Drawing.Size(500, 380);
            this.panelImageViewport.MouseEnter += new System.EventHandler(this.ImageViewport_MouseEnter);
            this.panelImageViewport.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.PictureBoxImage_MouseWheel);
            this.pictureBoxImage.BackColor = System.Drawing.Color.FromArgb(239, 241, 243);
            this.pictureBoxImage.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxImage.Size = new System.Drawing.Size(498, 378);
            this.pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBoxImage_MouseDown);
            this.pictureBoxImage.MouseEnter += new System.EventHandler(this.ImageViewport_MouseEnter);
            this.pictureBoxImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBoxImage_MouseMove);
            this.pictureBoxImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBoxImage_MouseUp);
            this.pictureBoxImage.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.PictureBoxImage_MouseWheel);
        }

        private void ConfigurePreprocessResults()
        {
            this.labelPreprocessResults.AutoSize = true;
            this.labelPreprocessResults.ForeColor = System.Drawing.Color.FromArgb(130, 134, 138);
            this.labelPreprocessResults.Location = new System.Drawing.Point(556, 88);
            this.labelPreprocessResults.Text = "二值化結果";
            ConfigurePreprocessPanel(this.panelPreprocess1, this.pictureBoxPreprocess1, this.labelPreprocess1Title, 556, 118, "1  白物件  Gray > T");
            ConfigurePreprocessPanel(this.panelPreprocess2, this.pictureBoxPreprocess2, this.labelPreprocess2Title, 786, 118, "2  白物件  Gray > T");
            ConfigurePreprocessPanel(this.panelPreprocess3, this.pictureBoxPreprocess3, this.labelPreprocess3Title, 556, 312, "3  黑物件  Gray < T");
            ConfigurePreprocessPanel(this.panelPreprocess4, this.pictureBoxPreprocess4, this.labelPreprocess4Title, 786, 312, "4  黑物件  Gray < T");
        }

        private static void ConfigurePreprocessPanel(System.Windows.Forms.Panel panel, System.Windows.Forms.PictureBox pictureBox, System.Windows.Forms.Label title, int x, int y, string text)
        {
            panel.BackColor = System.Drawing.Color.White;
            panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panel.Location = new System.Drawing.Point(x, y);
            panel.Size = new System.Drawing.Size(218, 186);
            panel.Controls.Add(title);
            panel.Controls.Add(pictureBox);
            title.AutoSize = true;
            title.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            title.Location = new System.Drawing.Point(8, 6);
            title.Text = text;
            pictureBox.BackColor = System.Drawing.Color.FromArgb(232, 234, 236);
            pictureBox.Location = new System.Drawing.Point(8, 28);
            pictureBox.Size = new System.Drawing.Size(200, 148);
            pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        }

        private void ConfigurePreprocessTabs()
        {
            this.tabControlPreprocess.Controls.Add(this.tabPagePreprocess1);
            this.tabControlPreprocess.Controls.Add(this.tabPagePreprocess2);
            this.tabControlPreprocess.Controls.Add(this.tabPagePreprocess3);
            this.tabControlPreprocess.Controls.Add(this.tabPagePreprocess4);
            this.tabControlPreprocess.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F);
            this.tabControlPreprocess.Location = new System.Drawing.Point(32, 520);
            this.tabControlPreprocess.Size = new System.Drawing.Size(976, 206);
            ConfigurePreprocessTab(this.tabPagePreprocess1, "前處理 1 - 白物件", this.checkBoxPreprocess1Enabled, this.trackBarThreshold1, this.numericThreshold1, this.numericErode1, this.numericDilate1, this.numericOpen1, this.numericClose1);
            ConfigurePreprocessTab(this.tabPagePreprocess2, "前處理 2 - 白物件", this.checkBoxPreprocess2Enabled, this.trackBarThreshold2, this.numericThreshold2, this.numericErode2, this.numericDilate2, this.numericOpen2, this.numericClose2);
            ConfigurePreprocessTab(this.tabPagePreprocess3, "前處理 3 - 黑物件", this.checkBoxPreprocess3Enabled, this.trackBarThreshold3, this.numericThreshold3, this.numericErode3, this.numericDilate3, this.numericOpen3, this.numericClose3);
            ConfigurePreprocessTab(this.tabPagePreprocess4, "前處理 4 - 黑物件", this.checkBoxPreprocess4Enabled, this.trackBarThreshold4, this.numericThreshold4, this.numericErode4, this.numericDilate4, this.numericOpen4, this.numericClose4);
        }

        private void ConfigurePreprocessTab(System.Windows.Forms.TabPage page, string title, System.Windows.Forms.CheckBox enabled, System.Windows.Forms.TrackBar thresholdBar, System.Windows.Forms.NumericUpDown thresholdValue, System.Windows.Forms.NumericUpDown erode, System.Windows.Forms.NumericUpDown dilate, System.Windows.Forms.NumericUpDown open, System.Windows.Forms.NumericUpDown close)
        {
            page.Text = title;
            page.BackColor = System.Drawing.Color.White;
            enabled.AutoSize = true;
            enabled.Checked = true;
            enabled.Location = new System.Drawing.Point(16, 15);
            enabled.Text = "啟用此組前處理";
            enabled.CheckedChanged += new System.EventHandler(this.PreprocessEnabled_CheckedChanged);
            page.Controls.Add(enabled);
            var thresholdLabel = new System.Windows.Forms.Label { AutoSize = true, Location = new System.Drawing.Point(16, 54), Text = "Threshold" };
            page.Controls.Add(thresholdLabel);
            thresholdBar.Location = new System.Drawing.Point(100, 43);
            thresholdBar.Maximum = 255;
            thresholdBar.TickFrequency = 32;
            thresholdBar.Value = 128;
            thresholdBar.Size = new System.Drawing.Size(430, 45);
            thresholdBar.Scroll += new System.EventHandler(this.ThresholdTrackBar_Scroll);
            page.Controls.Add(thresholdBar);
            thresholdValue.Location = new System.Drawing.Point(540, 48);
            thresholdValue.Size = new System.Drawing.Size(72, 25);
            thresholdValue.ValueChanged += new System.EventHandler(this.ThresholdValue_ValueChanged);
            page.Controls.Add(thresholdValue);
            AddMorphControl(page, "Erode", erode, 16);
            AddMorphControl(page, "Dilate", dilate, 180);
            AddMorphControl(page, "Open", open, 344);
            AddMorphControl(page, "Close", close, 508);
        }

        private void AddMorphControl(System.Windows.Forms.TabPage page, string text, System.Windows.Forms.NumericUpDown input, int x)
        {
            var label = new System.Windows.Forms.Label { AutoSize = true, Location = new System.Drawing.Point(x, 111), Text = text };
            page.Controls.Add(label);
            input.Location = new System.Drawing.Point(x + 65, 106);
            input.Size = new System.Drawing.Size(72, 25);
            input.ValueChanged += new System.EventHandler(this.MorphologyValue_ValueChanged);
            page.Controls.Add(input);
        }

        private static System.Windows.Forms.NumericUpDown CreateNumericUpDown(int minimum, int maximum, int value)
        {
            return new System.Windows.Forms.NumericUpDown
            {
                Minimum = minimum,
                Maximum = maximum,
                Value = value
            };
        }
    }
}
