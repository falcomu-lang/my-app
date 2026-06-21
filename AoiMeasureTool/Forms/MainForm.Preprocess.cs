using System;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp.Extensions;

namespace AoiMeasureTool
{
    public partial class MainForm
    {
        private void InitializePreprocessControls()
        {
            _preprocessPictureBoxes = new[]
            {
                pictureBoxPreprocess1,
                pictureBoxPreprocess2,
                pictureBoxPreprocess3,
                pictureBoxPreprocess4
            };

            _preprocessEnabledChecks = new[]
            {
                checkBoxPreprocess1Enabled,
                checkBoxPreprocess2Enabled,
                checkBoxPreprocess3Enabled,
                checkBoxPreprocess4Enabled
            };

            _thresholdTrackBars = new[] { trackBarThreshold1, trackBarThreshold2, trackBarThreshold3, trackBarThreshold4 };
            _thresholdInputs = new[] { numericThreshold1, numericThreshold2, numericThreshold3, numericThreshold4 };
            _erodeInputs = new[] { numericErode1, numericErode2, numericErode3, numericErode4 };
            _dilateInputs = new[] { numericDilate1, numericDilate2, numericDilate3, numericDilate4 };
            _openInputs = new[] { numericOpen1, numericOpen2, numericOpen3, numericOpen4 };
            _closeInputs = new[] { numericClose1, numericClose2, numericClose3, numericClose4 };

            for (var i = 0; i < 4; i++)
            {
                SetPreprocessControlsEnabled(i, _preprocessEnabledChecks[i].Checked);
            }
        }

        private PreprocessParam CreatePreprocessParam(int index)
        {
            return PreprocessProfileApplier.CreateParam(
                index,
                _preprocessEnabledChecks,
                _thresholdInputs,
                _erodeInputs,
                _dilateInputs,
                _openInputs,
                _closeInputs);
        }

        private bool TryGetReferenceCornerPreprocessParam(out PreprocessParam preprocessParam)
        {
            preprocessParam = null;
            if (_preprocessEnabledChecks == null)
            {
                return false;
            }

            if (_referenceSourceIndex < 0 || _referenceSourceIndex >= _preprocessEnabledChecks.Length)
            {
                return false;
            }

            var selectedParam = CreatePreprocessParam(_referenceSourceIndex);
            if (selectedParam == null || !selectedParam.Enabled)
            {
                return false;
            }

            preprocessParam = selectedParam;
            return true;
        }

        private void UpdateAllPreprocessImages()
        {
            if (_preprocessPictureBoxes == null)
            {
                return;
            }

            for (var i = 0; i < 4; i++)
            {
                UpdatePreprocessImage(i);
            }

            UpdateReferenceCornerPreview();
        }

        private void UpdatePreprocessImage(int index, bool preserveActiveView = false)
        {
            DisposePreprocessImage(index);

            if (_grayImage == null || _grayImage.Empty() || !_preprocessEnabledChecks[index].Checked)
            {
                SetPictureBoxImage(_preprocessPictureBoxes[index], null);
                if (index == _selectedPreprocessIndex)
                {
                    UpdateActivePreprocessPreview(false);
                }
                return;
            }

            _preprocessImages[index] = PreprocessPipelineService.Build(_grayImage, CreatePreprocessParam(index));
            SetPictureBoxImage(_preprocessPictureBoxes[index], BitmapConverter.ToBitmap(_preprocessImages[index]));
            if (index == _selectedPreprocessIndex)
            {
                UpdateActivePreprocessPreview(preserveActiveView);
            }
        }

        private void PreprocessThumbnail_Click(object sender, EventArgs e)
        {
            var control = sender as Control;
            if (control == null || control.Tag == null)
            {
                return;
            }

            _selectedPreprocessIndex = Convert.ToInt32(control.Tag);
            tabControlPreprocess.SelectedIndex = _selectedPreprocessIndex;
            UpdateActivePreprocessPreview(false);
        }

        private void UpdateActivePreprocessPreview(bool preserveView = false)
        {
            if (_preprocessPictureBoxes == null || _showingOriginalInActivePreview)
            {
                return;
            }

            var sourceImage = _preprocessPictureBoxes[_selectedPreprocessIndex].Image;
            if (sourceImage == null)
            {
                SetActivePreviewImage(null);
            }
            else if (preserveView && pictureBoxActivePreprocess.Image != null)
            {
                var savedScale = _activeImageScale;
                var savedLeft = pictureBoxActivePreprocess.Left;
                var savedTop = pictureBoxActivePreprocess.Top;
                SetPictureBoxImage(pictureBoxActivePreprocess, new Bitmap(sourceImage));
                _activeImageScale = savedScale;
                pictureBoxActivePreprocess.Size = new Size(
                    Math.Max(1, (int)Math.Round(pictureBoxActivePreprocess.Image.Width * _activeImageScale)),
                    Math.Max(1, (int)Math.Round(pictureBoxActivePreprocess.Image.Height * _activeImageScale)));
                pictureBoxActivePreprocess.Left = savedLeft;
                pictureBoxActivePreprocess.Top = savedTop;
                ConstrainActiveImagePosition();
            }
            else
            {
                SetActivePreviewImage(new Bitmap(sourceImage));
            }

            labelActivePreprocess.Text = string.Format("前處理 {0}", _selectedPreprocessIndex + 1);
        }

        private void ActivePreprocess_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && pictureBoxBinaryOriginal.Image != null)
            {
                _savedActiveImageScale = _activeImageScale;
                _savedActiveImageLeft = pictureBoxActivePreprocess.Left;
                _savedActiveImageTop = pictureBoxActivePreprocess.Top;
                _showingOriginalInActivePreview = true;
                SetActivePreviewImageNoFit(new Bitmap(pictureBoxBinaryOriginal.Image));
                return;
            }

            if (e.Button != MouseButtons.Left || pictureBoxActivePreprocess.Image == null)
            {
                return;
            }

            _isDraggingActiveImage = true;
            _lastActiveMousePosition = panelActiveViewport.PointToClient(pictureBoxActivePreprocess.PointToScreen(e.Location));
            pictureBoxActivePreprocess.Cursor = Cursors.SizeAll;
            pictureBoxActivePreprocess.Capture = true;
        }

        private void ActivePreprocess_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDraggingActiveImage)
            {
                return;
            }

            var currentPosition = panelActiveViewport.PointToClient(pictureBoxActivePreprocess.PointToScreen(e.Location));
            pictureBoxActivePreprocess.Left += currentPosition.X - _lastActiveMousePosition.X;
            pictureBoxActivePreprocess.Top += currentPosition.Y - _lastActiveMousePosition.Y;
            _lastActiveMousePosition = currentPosition;
            ConstrainActiveImagePosition();
        }

        private void ActivePreprocess_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _showingOriginalInActivePreview = false;
                RestoreActivePreviewView();
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                _isDraggingActiveImage = false;
                pictureBoxActivePreprocess.Cursor = Cursors.Default;
                pictureBoxActivePreprocess.Capture = false;
            }
        }

        private void ActivePreprocess_MouseWheel(object sender, MouseEventArgs e)
        {
            if (pictureBoxActivePreprocess.Image == null)
            {
                return;
            }

            var sourceControl = (Control)sender;
            var mousePosition = panelActiveViewport.PointToClient(sourceControl.PointToScreen(e.Location));
            PreprocessPreviewService.ZoomAt(
                pictureBoxActivePreprocess,
                panelActiveViewport,
                mousePosition,
                e.Delta,
                ref _activeImageScale,
                _activeFitScale);
        }

        private void ActivePreview_MouseEnter(object sender, EventArgs e)
        {
            pictureBoxActivePreprocess.Focus();
        }

        private void SetActivePreviewImage(Bitmap image)
        {
            SetPictureBoxImage(pictureBoxActivePreprocess, image);
            FitActiveImageToViewport();
        }

        private void SetActivePreviewImageNoFit(Bitmap image)
        {
            var oldImage = pictureBoxActivePreprocess.Image;
            pictureBoxActivePreprocess.Image = image;
            oldImage?.Dispose();
        }

        private void RestoreActivePreviewView()
        {
            if (_preprocessPictureBoxes == null)
            {
                return;
            }

            var sourceImage = _preprocessPictureBoxes[_selectedPreprocessIndex].Image;
            if (sourceImage == null)
            {
                SetActivePreviewImage(null);
                return;
            }

            SetPictureBoxImage(pictureBoxActivePreprocess, new Bitmap(sourceImage));
            _activeImageScale = _savedActiveImageScale;
            pictureBoxActivePreprocess.Size = new Size(
                Math.Max(1, (int)Math.Round(pictureBoxActivePreprocess.Image.Width * _activeImageScale)),
                Math.Max(1, (int)Math.Round(pictureBoxActivePreprocess.Image.Height * _activeImageScale)));
            pictureBoxActivePreprocess.Left = _savedActiveImageLeft;
            pictureBoxActivePreprocess.Top = _savedActiveImageTop;
            ConstrainActiveImagePosition();
        }

        private void FitActiveImageToViewport()
        {
            if (pictureBoxActivePreprocess.Image == null)
            {
                pictureBoxActivePreprocess.Location = Point.Empty;
                pictureBoxActivePreprocess.Size = panelActiveViewport.ClientSize;
                _activeImageScale = 1f;
                _activeFitScale = 1f;
                return;
            }

            PreprocessPreviewService.FitToViewport(
                pictureBoxActivePreprocess,
                panelActiveViewport,
                ref _activeImageScale,
                ref _activeFitScale);
        }

        private void ConstrainActiveImagePosition()
        {
            PreprocessPreviewService.ConstrainPosition(pictureBoxActivePreprocess, panelActiveViewport);
        }

        private void DisposePreprocessImage(int index)
        {
            _preprocessImages[index]?.Dispose();
            _preprocessImages[index] = null;
        }

        private void PreprocessEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (_preprocessEnabledChecks == null)
            {
                return;
            }

            var index = Array.IndexOf(_preprocessEnabledChecks, sender as CheckBox);
            if (index < 0)
            {
                return;
            }

            SetPreprocessControlsEnabled(index, _preprocessEnabledChecks[index].Checked);
            UpdatePreprocessImage(index, false);
        }

        private void SetPreprocessControlsEnabled(int index, bool enabled)
        {
            _thresholdTrackBars[index].Enabled = enabled;
            _thresholdInputs[index].Enabled = enabled;
            _erodeInputs[index].Enabled = enabled;
            _dilateInputs[index].Enabled = enabled;
            _openInputs[index].Enabled = enabled;
            _closeInputs[index].Enabled = enabled;
        }

        private void ThresholdTrackBar_Scroll(object sender, EventArgs e)
        {
            if (_synchronizingThreshold || _thresholdTrackBars == null)
            {
                return;
            }

            var index = Array.IndexOf(_thresholdTrackBars, sender as TrackBar);
            if (index < 0)
            {
                return;
            }

            _synchronizingThreshold = true;
            _thresholdInputs[index].Value = _thresholdTrackBars[index].Value;
            _synchronizingThreshold = false;
            UpdatePreprocessImage(index, true);
        }

        private void ThresholdValue_ValueChanged(object sender, EventArgs e)
        {
            if (_synchronizingThreshold || _thresholdInputs == null)
            {
                return;
            }

            var index = Array.IndexOf(_thresholdInputs, sender as NumericUpDown);
            if (index < 0)
            {
                return;
            }

            _synchronizingThreshold = true;
            _thresholdTrackBars[index].Value = (int)_thresholdInputs[index].Value;
            _synchronizingThreshold = false;
            UpdatePreprocessImage(index, true);
        }

        private void MorphologyValue_ValueChanged(object sender, EventArgs e)
        {
            if (_erodeInputs == null)
            {
                return;
            }

            var input = sender as NumericUpDown;
            var index = Array.IndexOf(_erodeInputs, input);
            if (index < 0) index = Array.IndexOf(_dilateInputs, input);
            if (index < 0) index = Array.IndexOf(_openInputs, input);
            if (index < 0) index = Array.IndexOf(_closeInputs, input);
            if (index >= 0)
            {
                UpdatePreprocessImage(index, true);
            }
        }
    }
}
