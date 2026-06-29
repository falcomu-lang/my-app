using System.Windows.Forms;

namespace AoiMeasureTool
{
    internal static class PreprocessProfileApplier
    {
        public static void ApplySnapshots(
            PreprocessSnapshot[] snapshots,
            CheckBox[] enabledChecks,
            TrackBar[] thresholdTrackBars,
            NumericUpDown[] thresholdInputs,
            NumericUpDown[] erodeInputs,
            NumericUpDown[] dilateInputs,
            NumericUpDown[] openInputs,
            NumericUpDown[] closeInputs)
        {
            if (snapshots == null)
            {
                return;
            }

            for (var i = 0; i < 4; i++)
            {
                if (snapshots[i] == null)
                {
                    continue;
                }

                enabledChecks[i].Checked = snapshots[i].Enabled;
                thresholdInputs[i].Value = snapshots[i].Threshold;
                thresholdTrackBars[i].Value = snapshots[i].Threshold;
                erodeInputs[i].Value = snapshots[i].ErodeIterations;
                dilateInputs[i].Value = snapshots[i].DilateIterations;
                openInputs[i].Value = snapshots[i].OpenIterations;
                closeInputs[i].Value = snapshots[i].CloseIterations;
            }
        }

        public static PreprocessSnapshot[] CaptureSnapshots(
            CheckBox[] enabledChecks,
            NumericUpDown[] thresholdInputs,
            NumericUpDown[] erodeInputs,
            NumericUpDown[] dilateInputs,
            NumericUpDown[] openInputs,
            NumericUpDown[] closeInputs)
        {
            var snapshots = new PreprocessSnapshot[4];

            for (var i = 0; i < 4; i++)
            {
                snapshots[i] = new PreprocessSnapshot
                {
                    Enabled = enabledChecks[i].Checked,
                    Threshold = (int)thresholdInputs[i].Value,
                    ErodeIterations = (int)erodeInputs[i].Value,
                    DilateIterations = (int)dilateInputs[i].Value,
                    OpenIterations = (int)openInputs[i].Value,
                    CloseIterations = (int)closeInputs[i].Value
                };
            }

            return snapshots;
        }

        public static PreprocessParam CreateParam(
            int index,
            CheckBox[] enabledChecks,
            NumericUpDown[] thresholdInputs,
            NumericUpDown[] erodeInputs,
            NumericUpDown[] dilateInputs,
            NumericUpDown[] openInputs,
            NumericUpDown[] closeInputs)
        {
            return new PreprocessParam
            {
                Enabled = enabledChecks[index].Checked,
                WhiteObject = index < 2,
                Threshold = (int)thresholdInputs[index].Value,
                UpperThreshold = (int)thresholdInputs[index].Value,
                UseDualThreshold = false,
                ErodeIterations = (int)erodeInputs[index].Value,
                DilateIterations = (int)dilateInputs[index].Value,
                OpenIterations = (int)openInputs[index].Value,
                CloseIterations = (int)closeInputs[index].Value
            };
        }
    }
}
