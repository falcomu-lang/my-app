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
        private System.Windows.Forms.PictureBox pictureBoxImage;
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
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.labelWorkspace = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelImageInfo = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.openFileDialogImage = new System.Windows.Forms.OpenFileDialog();
            this.menuStripMain.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            //
            // menuStripMain
            //
            this.menuStripMain.BackColor = System.Drawing.Color.FromArgb(248, 247, 247);
            this.menuStripMain.Font = new System.Drawing.Font("Microsoft JhengHei UI", 11F);
            this.menuStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuEdit,
            this.menuView,
            this.menuHelp});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Padding = new System.Windows.Forms.Padding(18, 8, 0, 8);
            this.menuStripMain.Size = new System.Drawing.Size(1920, 42);
            this.menuStripMain.TabIndex = 0;
            //
            // menuFile
            //
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOpenImage,
            this.menuFileSeparator,
            this.menuExit});
            this.menuFile.ForeColor = System.Drawing.Color.FromArgb(78, 82, 87);
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(53, 26);
            this.menuFile.Text = "檔案";
            //
            // menuOpenImage
            //
            this.menuOpenImage.Name = "menuOpenImage";
            this.menuOpenImage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuOpenImage.Size = new System.Drawing.Size(220, 26);
            this.menuOpenImage.Text = "讀取圖片";
            this.menuOpenImage.Click += new System.EventHandler(this.LoadImageButton_Click);
            //
            // menuFileSeparator
            //
            this.menuFileSeparator.Name = "menuFileSeparator";
            this.menuFileSeparator.Size = new System.Drawing.Size(217, 6);
            //
            // menuExit
            //
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(220, 26);
            this.menuExit.Text = "結束";
            this.menuExit.Click += new System.EventHandler(this.CloseButton_Click);
            //
            // menuEdit
            //
            this.menuEdit.ForeColor = System.Drawing.Color.FromArgb(125, 128, 132);
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(53, 26);
            this.menuEdit.Text = "編輯";
            //
            // menuView
            //
            this.menuView.ForeColor = System.Drawing.Color.FromArgb(125, 128, 132);
            this.menuView.Name = "menuView";
            this.menuView.Size = new System.Drawing.Size(53, 26);
            this.menuView.Text = "檢視";
            //
            // menuHelp
            //
            this.menuHelp.ForeColor = System.Drawing.Color.FromArgb(125, 128, 132);
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(53, 26);
            this.menuHelp.Text = "說明";
            //
            // panelSidebar
            //
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(239, 243, 245);
            this.panelSidebar.Controls.Add(this.labelOpenCvStatus);
            this.panelSidebar.Controls.Add(this.buttonLoadImage);
            this.panelSidebar.Controls.Add(this.labelNavigation);
            this.panelSidebar.Controls.Add(this.labelAppName);
            this.panelSidebar.Location = new System.Drawing.Point(0, 42);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(300, 1038);
            this.panelSidebar.TabIndex = 1;
            //
            // labelOpenCvStatus
            //
            this.labelOpenCvStatus.AutoSize = true;
            this.labelOpenCvStatus.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F);
            this.labelOpenCvStatus.ForeColor = System.Drawing.Color.FromArgb(110, 115, 120);
            this.labelOpenCvStatus.Location = new System.Drawing.Point(24, 986);
            this.labelOpenCvStatus.Name = "labelOpenCvStatus";
            this.labelOpenCvStatus.Size = new System.Drawing.Size(147, 18);
            this.labelOpenCvStatus.TabIndex = 3;
            this.labelOpenCvStatus.Text = "OpenCV 4.13 已就緒";
            //
            // buttonLoadImage
            //
            this.buttonLoadImage.BackColor = System.Drawing.Color.FromArgb(224, 228, 231);
            this.buttonLoadImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLoadImage.FlatAppearance.BorderSize = 0;
            this.buttonLoadImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLoadImage.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F);
            this.buttonLoadImage.ForeColor = System.Drawing.Color.FromArgb(42, 45, 49);
            this.buttonLoadImage.Location = new System.Drawing.Point(16, 116);
            this.buttonLoadImage.Name = "buttonLoadImage";
            this.buttonLoadImage.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.buttonLoadImage.Size = new System.Drawing.Size(268, 48);
            this.buttonLoadImage.TabIndex = 2;
            this.buttonLoadImage.Text = "+   讀取圖片";
            this.buttonLoadImage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLoadImage.UseVisualStyleBackColor = false;
            this.buttonLoadImage.Click += new System.EventHandler(this.LoadImageButton_Click);
            //
            // labelNavigation
            //
            this.labelNavigation.AutoSize = true;
            this.labelNavigation.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F);
            this.labelNavigation.ForeColor = System.Drawing.Color.FromArgb(145, 149, 153);
            this.labelNavigation.Location = new System.Drawing.Point(20, 83);
            this.labelNavigation.Name = "labelNavigation";
            this.labelNavigation.Size = new System.Drawing.Size(64, 18);
            this.labelNavigation.TabIndex = 1;
            this.labelNavigation.Text = "工作項目";
            //
            // labelAppName
            //
            this.labelAppName.AutoSize = true;
            this.labelAppName.Font = new System.Drawing.Font("Microsoft JhengHei UI", 16F, System.Drawing.FontStyle.Bold);
            this.labelAppName.ForeColor = System.Drawing.Color.FromArgb(36, 39, 43);
            this.labelAppName.Location = new System.Drawing.Point(20, 25);
            this.labelAppName.Name = "labelAppName";
            this.labelAppName.Size = new System.Drawing.Size(153, 28);
            this.labelAppName.TabIndex = 0;
            this.labelAppName.Text = "AOI Viewer";
            //
            // panelMain
            //
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.panelMain.Controls.Add(this.pictureBoxImage);
            this.panelMain.Controls.Add(this.labelWorkspace);
            this.panelMain.Controls.Add(this.panelHeader);
            this.panelMain.Location = new System.Drawing.Point(300, 42);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1620, 1038);
            this.panelMain.TabIndex = 2;
            //
            // pictureBoxImage
            //
            this.pictureBoxImage.BackColor = System.Drawing.Color.FromArgb(239, 241, 243);
            this.pictureBoxImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxImage.Location = new System.Drawing.Point(32, 118);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(1556, 888);
            this.pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxImage.TabIndex = 2;
            this.pictureBoxImage.TabStop = false;
            //
            // labelWorkspace
            //
            this.labelWorkspace.AutoSize = true;
            this.labelWorkspace.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F);
            this.labelWorkspace.ForeColor = System.Drawing.Color.FromArgb(130, 134, 138);
            this.labelWorkspace.Location = new System.Drawing.Point(32, 88);
            this.labelWorkspace.Name = "labelWorkspace";
            this.labelWorkspace.Size = new System.Drawing.Size(64, 18);
            this.labelWorkspace.TabIndex = 1;
            this.labelWorkspace.Text = "圖片預覽";
            //
            // panelHeader
            //
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.labelImageInfo);
            this.panelHeader.Controls.Add(this.labelTitle);
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1620, 72);
            this.panelHeader.TabIndex = 0;
            //
            // labelImageInfo
            //
            this.labelImageInfo.AutoSize = true;
            this.labelImageInfo.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F);
            this.labelImageInfo.ForeColor = System.Drawing.Color.FromArgb(125, 129, 134);
            this.labelImageInfo.Location = new System.Drawing.Point(240, 28);
            this.labelImageInfo.Name = "labelImageInfo";
            this.labelImageInfo.Size = new System.Drawing.Size(106, 18);
            this.labelImageInfo.TabIndex = 1;
            this.labelImageInfo.Text = "尚未讀取圖片";
            //
            // labelTitle
            //
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft JhengHei UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(35, 38, 42);
            this.labelTitle.Location = new System.Drawing.Point(28, 23);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(125, 24);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "影像檢視器";
            //
            // openFileDialogImage
            //
            this.openFileDialogImage.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff|All Files|*.*";
            this.openFileDialogImage.Title = "選擇圖片";
            //
            // MainForm
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.ClientSize = new System.Drawing.Size(1920, 1080);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
