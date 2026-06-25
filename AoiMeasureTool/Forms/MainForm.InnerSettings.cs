using System;
using System.Windows.Forms;

namespace AoiMeasureTool
{
    public partial class MainForm
    {
        private void InitializeInnerSettingsControls()
        {
            _tabPageInnerSettings = tabPageInnerSettings;
            _numericInnerCcdXPrecision = numericInnerCcdXPrecision;
            _numericInnerCcdYPrecision = numericInnerCcdYPrecision;
            _labelInnerCcdXPrecision = labelInnerCcdXPrecision;
            _labelInnerCcdYPrecision = labelInnerCcdYPrecision;
            _buttonSaveInnerSettings = buttonSaveInnerSettings;
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
                MessageBox.Show(this, "無法儲存內部參數。\r\n\r\n" + ex.Message, "內部參數", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
