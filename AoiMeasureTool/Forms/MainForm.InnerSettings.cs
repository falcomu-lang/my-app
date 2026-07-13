using System;
using System.Windows.Forms;

namespace AoiMeasureTool
{
    public partial class MainForm
    {
        private void InitializeInnerSettingsControls()
        {
            _tabPageInnerSettings = tabPageInnerSettings;
            _buttonSaveInnerSettings = buttonSaveInnerSettings;

            _innerCameraGroupBoxes[0] = groupBoxInnerCamera1;
            _innerCameraGroupBoxes[1] = groupBoxInnerCamera2;
            _innerCameraGroupBoxes[2] = groupBoxInnerCamera3;

            _innerCameraNameTextBoxes[0] = textBoxInnerCamera1Name;
            _innerCameraNameTextBoxes[1] = textBoxInnerCamera2Name;
            _innerCameraNameTextBoxes[2] = textBoxInnerCamera3Name;

            _innerCameraUsageTextBoxes[0] = textBoxInnerCamera1Usage;
            _innerCameraUsageTextBoxes[1] = textBoxInnerCamera2Usage;
            _innerCameraUsageTextBoxes[2] = textBoxInnerCamera3Usage;

            _innerCameraCcdXPrecisions[0] = numericInnerCcdXPrecision;
            _innerCameraCcdXPrecisions[1] = numericInnerCamera2CcdXPrecision;
            _innerCameraCcdXPrecisions[2] = numericInnerCamera3CcdXPrecision;

            _innerCameraCcdYPrecisions[0] = numericInnerCcdYPrecision;
            _innerCameraCcdYPrecisions[1] = numericInnerCamera2CcdYPrecision;
            _innerCameraCcdYPrecisions[2] = numericInnerCamera3CcdYPrecision;

            _innerCameraMeasurementScaleFactors[0] = numericInnerMeasurementScaleFactor;
            _innerCameraMeasurementScaleFactors[1] = numericInnerCamera2MeasurementScaleFactor;
            _innerCameraMeasurementScaleFactors[2] = numericInnerCamera3MeasurementScaleFactor;

            ApplyInnerSettings(_innerSettings);
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
                MessageBox.Show(this, "儲存內部參數時發生錯誤。\r\n\r\n" + ex.Message, "內部參數", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
