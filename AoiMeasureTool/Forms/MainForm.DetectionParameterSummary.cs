using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace AoiMeasureTool
{
    public partial class MainForm
    {
        private void InitializeDetectionParameterSummaryControls()
        {
            _tabPageDetectionParameterSummary = tabPageDetectionParameterSummary;
            _textBoxDetectionMainParameterName = textBoxDetectionMainParameterName;
            _buttonDetectionMainParameterConfirm = buttonDetectionMainParameterConfirm;
            _listBoxDetectionMainParameter = listBoxDetectionMainParameter;

            if (_buttonDetectionMainParameterConfirm != null)
            {
                _buttonDetectionMainParameterConfirm.Click += DetectionMainParameterConfirmButton_Click;
            }

            LoadDetectionParameterReferenceList();
        }

        private void DetectionMainParameterConfirmButton_Click(object sender, EventArgs e)
        {
            var parameterName = _textBoxDetectionMainParameterName == null
                ? string.Empty
                : _textBoxDetectionMainParameterName.Text.Trim();

            if (string.IsNullOrWhiteSpace(parameterName))
            {
                return;
            }

            foreach (var existingName in _detectionMainParameters)
            {
                if (string.Equals(existingName, parameterName, StringComparison.OrdinalIgnoreCase))
                {
                    if (_listBoxDetectionMainParameter != null)
                    {
                        _listBoxDetectionMainParameter.SelectedItem = existingName;
                    }

                    if (_textBoxDetectionMainParameterName != null)
                    {
                        _textBoxDetectionMainParameterName.SelectAll();
                        _textBoxDetectionMainParameterName.Focus();
                    }

                    return;
                }
            }

            _detectionMainParameters.Add(parameterName);
            SaveDetectionParameterReferenceList();
            RefreshDetectionMainParameterList();

            if (_textBoxDetectionMainParameterName != null)
            {
                _textBoxDetectionMainParameterName.Clear();
                _textBoxDetectionMainParameterName.Focus();
            }
        }

        private void LoadDetectionParameterReferenceList()
        {
            _detectionMainParameters.Clear();
            _detectionMainParameters.AddRange(
                _parameterReferenceListRepository.Load(GetParameterReferenceListPath()));
            RefreshDetectionMainParameterList();
        }

        private void SaveDetectionParameterReferenceList()
        {
            _parameterReferenceListRepository.Save(
                GetParameterReferenceListPath(),
                _detectionMainParameters);
        }

        private void RefreshDetectionMainParameterList()
        {
            if (_listBoxDetectionMainParameter == null)
            {
                return;
            }

            _listBoxDetectionMainParameter.BeginUpdate();
            try
            {
                _listBoxDetectionMainParameter.Items.Clear();
                foreach (var parameterName in _detectionMainParameters)
                {
                    _listBoxDetectionMainParameter.Items.Add(parameterName);
                }
            }
            finally
            {
                _listBoxDetectionMainParameter.EndUpdate();
            }
        }

        private string GetParameterReferenceListPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "parameterReferenceList.ini");
        }
    }
}
