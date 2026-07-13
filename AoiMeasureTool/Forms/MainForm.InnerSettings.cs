using System;
using System.Drawing;
using System.Windows.Forms;

namespace AoiMeasureTool
{
    public partial class MainForm
    {
        private void InitializeInnerSettingsControls()
        {
            _tabPageInnerSettings = tabPageInnerSettings;
            _buttonSaveInnerSettings = buttonSaveInnerSettings;

            if (panelInnerSettings != null)
            {
                panelInnerSettings.Controls.Clear();
                panelInnerSettings.Size = new Size(950, 280);
                CreateInnerSettingsBlocks(panelInnerSettings);
            }

            ApplyInnerSettings(_innerSettings);
        }

        private void CreateInnerSettingsBlocks(Control host)
        {
            var blockWidth = 290;
            var blockHeight = 220;
            var left = 18;
            var top = 16;
            var gap = 18;

            for (var i = 0; i < 3; i++)
            {
                var group = new GroupBox
                {
                    Location = new Point(left + i * (blockWidth + gap), top),
                    Size = new Size(blockWidth, blockHeight),
                    Text = "內部參數 " + (i + 1)
                };

                var labelCamera = new Label { AutoSize = true, Location = new Point(16, 32), Text = "攝影機 / 用途" };
                var textCamera = new TextBox { Location = new Point(16, 54), Size = new Size(250, 25) };
                var labelUsage = new Label { AutoSize = true, Location = new Point(16, 88), Text = "用途" };
                var textUsage = new TextBox { Location = new Point(16, 110), Size = new Size(250, 25) };
                var labelX = new Label { AutoSize = true, Location = new Point(16, 144), Text = "CCD X 精度" };
                var numX = CreateInnerNumeric(new Point(120, 140));
                var labelY = new Label { AutoSize = true, Location = new Point(16, 176), Text = "CCD Y 精度" };
                var numY = CreateInnerNumeric(new Point(120, 172));
                var labelScale = new Label { AutoSize = true, Location = new Point(16, 208), Text = "量測倍率" };
                var numScale = CreateInnerNumeric(new Point(120, 204));

                if (i == 0)
                {
                    _innerCameraNameTextBoxes[i] = textCamera;
                    _innerCameraUsageTextBoxes[i] = textUsage;
                    _innerCameraCcdXPrecisions[i] = numX;
                    _innerCameraCcdYPrecisions[i] = numY;
                    _innerCameraMeasurementScaleFactors[i] = numScale;
                }
                else
                {
                    _innerCameraNameTextBoxes[i] = textCamera;
                    _innerCameraUsageTextBoxes[i] = textUsage;
                    _innerCameraCcdXPrecisions[i] = numX;
                    _innerCameraCcdYPrecisions[i] = numY;
                    _innerCameraMeasurementScaleFactors[i] = numScale;
                }

                group.Controls.Add(labelCamera);
                group.Controls.Add(textCamera);
                group.Controls.Add(labelUsage);
                group.Controls.Add(textUsage);
                group.Controls.Add(labelX);
                group.Controls.Add(numX);
                group.Controls.Add(labelY);
                group.Controls.Add(numY);
                group.Controls.Add(labelScale);
                group.Controls.Add(numScale);
                host.Controls.Add(group);
                _innerCameraGroupBoxes[i] = group;
            }

            if (_buttonSaveInnerSettings != null)
            {
                _buttonSaveInnerSettings.Location = new Point(28, 248);
                host.Controls.Add(_buttonSaveInnerSettings);
                _buttonSaveInnerSettings.BringToFront();
            }
        }

        private NumericUpDown CreateInnerNumeric(Point location)
        {
            return new NumericUpDown
            {
                DecimalPlaces = 5,
                Increment = new decimal(new int[] { 1, 0, 0, 131072 }),
                Location = location,
                Maximum = new decimal(new int[] { 1000, 0, 0, 0 }),
                Minimum = new decimal(new int[] { 1, 0, 0, 262144 }),
                Size = new Size(130, 25),
                TextAlign = HorizontalAlignment.Right
            };
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
