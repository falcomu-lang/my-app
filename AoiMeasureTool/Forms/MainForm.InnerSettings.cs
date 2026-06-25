using System;
using System.Drawing;
using System.Windows.Forms;

namespace AoiMeasureTool
{
    public partial class MainForm
    {
        private Button _buttonInnerSettings;

        private void InitializeInnerSettingsControls()
        {
            EnsureInnerSettingsSidebarButton();
            EnsureInnerSettingsTab();
            ApplyInnerSettings(_innerSettings);
        }

        private void EnsureInnerSettingsSidebarButton()
        {
            if (panelSidebar == null || _buttonInnerSettings != null)
            {
                return;
            }

            _buttonInnerSettings = new Button
            {
                Name = "buttonInnerSettings",
                BackColor = Color.FromArgb(224, 228, 231),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Microsoft JhengHei UI", 12F),
                Location = new Point(16, 340),
                Padding = new Padding(14, 0, 0, 0),
                Size = new Size(208, 48),
                TabIndex = 6,
                Text = "+   內部參數",
                TextAlign = ContentAlignment.MiddleLeft,
                UseVisualStyleBackColor = false
            };
            _buttonInnerSettings.FlatAppearance.BorderSize = 0;
            _buttonInnerSettings.Click += InnerSettingsButton_Click;

            panelSidebar.Controls.Add(_buttonInnerSettings);
            _buttonInnerSettings.BringToFront();
            labelOpenCvStatus?.BringToFront();
        }

        private void EnsureInnerSettingsTab()
        {
            if (_tabPageInnerSettings != null)
            {
                return;
            }

            _tabPageInnerSettings = new TabPage
            {
                Name = "tabPageInnerSettings",
                Text = "內部參數",
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(3),
                Size = new Size(1032, 656)
            };

            var labelTitle = new Label
            {
                AutoSize = true,
                ForeColor = Color.FromArgb(130, 134, 138),
                Location = new Point(31, 21),
                Name = "labelInnerSettingsTitle",
                Size = new Size(64, 18),
                Text = "內部參數"
            };

            var panelSettings = new Panel
            {
                Name = "panelInnerSettings",
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(28, 46),
                Size = new Size(420, 190)
            };

            _labelInnerCcdXPrecision = new Label
            {
                AutoSize = true,
                Location = new Point(24, 32),
                Name = "labelInnerCcdXPrecision",
                Text = "CCD X向精度"
            };

            _numericInnerCcdXPrecision = new NumericUpDown
            {
                Name = "numericInnerCcdXPrecision",
                DecimalPlaces = 6,
                Increment = 0.000001M,
                Minimum = 0,
                Maximum = 1000,
                Location = new Point(160, 28),
                Size = new Size(180, 25),
                TextAlign = HorizontalAlignment.Right
            };

            _labelInnerCcdYPrecision = new Label
            {
                AutoSize = true,
                Location = new Point(24, 78),
                Name = "labelInnerCcdYPrecision",
                Text = "CCD Y向精度"
            };

            _numericInnerCcdYPrecision = new NumericUpDown
            {
                Name = "numericInnerCcdYPrecision",
                DecimalPlaces = 6,
                Increment = 0.000001M,
                Minimum = 0,
                Maximum = 1000,
                Location = new Point(160, 74),
                Size = new Size(180, 25),
                TextAlign = HorizontalAlignment.Right
            };

            _buttonSaveInnerSettings = new Button
            {
                Name = "buttonSaveInnerSettings",
                BackColor = Color.FromArgb(224, 228, 231),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(24, 126),
                Size = new Size(120, 36),
                TabIndex = 2,
                Text = "儲存",
                UseVisualStyleBackColor = false
            };
            _buttonSaveInnerSettings.FlatAppearance.BorderSize = 0;
            _buttonSaveInnerSettings.Click += SaveInnerSettingsButton_Click;

            panelSettings.Controls.Add(_labelInnerCcdXPrecision);
            panelSettings.Controls.Add(_numericInnerCcdXPrecision);
            panelSettings.Controls.Add(_labelInnerCcdYPrecision);
            panelSettings.Controls.Add(_numericInnerCcdYPrecision);
            panelSettings.Controls.Add(_buttonSaveInnerSettings);

            _tabPageInnerSettings.Controls.Add(labelTitle);
            _tabPageInnerSettings.Controls.Add(panelSettings);
            tabControlMain.Controls.Add(_tabPageInnerSettings);
        }

        private void SaveInnerSettingsButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveInnerSettings();
                MessageBox.Show(this, "內部參數已儲存。", "內部參數", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "無法儲存內部參數。\r\n\r\n" + ex.Message, "內部參數", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
