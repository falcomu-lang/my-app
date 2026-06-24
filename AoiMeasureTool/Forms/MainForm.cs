using System;
using System.Drawing;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using OpenCvSharp.Extensions;
using Cv2 = OpenCvSharp.Cv2;
using ColorConversionCodes = OpenCvSharp.ColorConversionCodes;
using CvMat = OpenCvSharp.Mat;
using ImreadModes = OpenCvSharp.ImreadModes;

namespace AoiMeasureTool
{
    public partial class MainForm : Form
    {
        private CvMat _sourceImage;
        private CvMat _grayImage;
        private readonly CvMat[] _preprocessImages = new CvMat[4];
        private readonly string _settingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "setting.ini");
        private readonly IniSettingsRepository _settingsRepository = new IniSettingsRepository();
        private string _lastImagePath;
        private string _activeProductKey;
        private readonly System.Collections.Generic.Dictionary<string, PreprocessSnapshot[]> _productProfiles =
            new System.Collections.Generic.Dictionary<string, PreprocessSnapshot[]>(System.StringComparer.OrdinalIgnoreCase);
        private readonly System.Collections.Generic.Dictionary<string, ReferenceCornerSnapshot> _referenceCornerProfiles =
            new System.Collections.Generic.Dictionary<string, ReferenceCornerSnapshot>(System.StringComparer.OrdinalIgnoreCase);
        private readonly System.Collections.Generic.Dictionary<string, List<MeasureRecord>> _measureProfiles =
            new System.Collections.Generic.Dictionary<string, List<MeasureRecord>>(System.StringComparer.OrdinalIgnoreCase);
        private readonly ProductProfileService _productProfileService;
        private PictureBox[] _preprocessPictureBoxes;
        private CheckBox[] _preprocessEnabledChecks;
        private TrackBar[] _thresholdTrackBars;
        private NumericUpDown[] _thresholdInputs;
        private NumericUpDown[] _erodeInputs;
        private NumericUpDown[] _dilateInputs;
        private NumericUpDown[] _openInputs;
        private NumericUpDown[] _closeInputs;
        private bool _synchronizingThreshold;
        private int _selectedPreprocessIndex;
        private bool _referenceCornerEnabled;
        private int _referenceSourceIndex;
        private bool _referenceRoiSaved;
        private bool _referenceCornerFound;
        private bool _isSelectingReferenceRoi;
        private Point _referenceRoiStart;
        private Rectangle _referenceRoiRectangle;
        private ReferenceCornerCandidate _referenceCornerCandidate;
        private bool _referencePreviewPanning;
        private float _referenceImageScale = 1f;
        private float _referenceFitScale = 1f;
        private Point _lastReferenceMousePosition;
        private float _activeImageScale = 1f;
        private float _activeFitScale = 1f;
        private bool _isDraggingActiveImage;
        private Point _lastActiveMousePosition;
        private bool _measurePreviewPanning;
        private float _measureImageScale = 1f;
        private float _measureFitScale = 1f;
        private Point _lastMeasureMousePosition;
        private bool _multiImageConfirmPanning;
        private float _multiImageConfirmImageScale = 1f;
        private float _multiImageConfirmFitScale = 1f;
        private float _multiImageConfirmOffsetX = 0f;
        private float _multiImageConfirmOffsetY = 0f;
        private Point _lastMultiImageConfirmMousePosition;
        private bool _showingOriginalInActivePreview;
        private bool _suppressPersistOnProductSwitch;
        private bool _isApplyingProductState;
        private bool _isApplyingReferenceCornerState;
        private float _savedActiveImageScale = 1f;
        private int _savedActiveImageLeft;
        private int _savedActiveImageTop;
        private float _imageScale = 1f;
        private float _fitScale = 1f;
        private bool _isDraggingImage;
        private Point _lastMousePosition;
        private TabPage _tabPageMeasureDistance;
        private TabPage _tabPageMultiImageConfirm;
        private Panel _panelMeasurePreview;
        private Panel _panelMultiImageConfirmViewport;
        private PictureBox _pictureBoxMultiImageConfirm;
        private PictureBox _pictureBoxMeasurePreview;
        private Button _buttonLoadMultiImageFolder;
        private Button _buttonMultiImagePrev;
        private Button _buttonMultiImageNext;
        private DataGridView _dataGridViewMeasureRecords;
        private Button _buttonSaveMeasurePoint;
        private Button _buttonClearMeasurePoint;
        private Button _buttonSaveMeasureRecords;
        private Button _buttonParallelMeasure;
        private Button _buttonPerpendicularMeasure;
        private ComboBox _comboBoxMeasureSource;
        private Label _labelMeasureStatus;
        private ContextMenuStrip _measureRecordMenu;
        private ToolStripMenuItem _measureEditMenuItem;
        private ToolStripMenuItem _measureDeleteMenuItem;
        private System.Windows.Forms.Timer _measureBlinkTimer;
        private MeasureRecord _measureBlinkRecord;
        private int _measureBlinkRemainingTicks;
        private bool _isMeasureSelecting;
        private readonly List<Point> _measurePoints = new List<Point>(2);
        private readonly List<MeasureRecord> _measureRecords = new List<MeasureRecord>();
        private MeasureRecord _editingMeasureRecord;
        private DataGridViewRow _editingMeasureRow;
        private bool _isEditingMeasureRecord;
        private bool _measureSourceAvailable;
        private MeasureDirectionMode _measureDirectionMode = MeasureDirectionMode.None;
        private readonly List<string> _multiImageConfirmImagePaths = new List<string>();
        private int _multiImageConfirmImageIndex = -1;
        private Bitmap _multiImageConfirmBitmap;
        private Size _multiImageConfirmSourceImageSize = Size.Empty;
        private bool _multiImageConfirmShowingPreprocess;
        private string _multiImageConfirmProductKey;

        public MainForm()
        {
            _productProfileService = new ProductProfileService(_productProfiles, _referenceCornerProfiles, _measureProfiles);
            InitializeComponent();
            ShowMainWorkspaceTabs();
            InitializePreprocessControls();
            InitializeReferenceCornerControls();
            InitializeMeasureDistanceControls();
            EnableDoubleBuffering();
            LoadSavedAppSettings();
            LoadLastImageIfAvailable();
        }

        private void EnableDoubleBuffering()
        {
            SetControlDoubleBuffered(_panelMultiImageConfirmViewport, true);
            SetControlDoubleBuffered(_tabPageMultiImageConfirm, true);
        }

        private static void SetControlDoubleBuffered(Control control, bool enabled)
        {
            if (control == null)
            {
                return;
            }

            var property = typeof(Control).GetProperty(
                "DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            property?.SetValue(control, enabled, null);
        }

        private void SidebarImageViewerButton_Click(object sender, EventArgs e)
        {
            ShowMainWorkspaceTabs();
            tabControlMain.SelectedTab = tabPageImageViewer;
        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            ShowMainWorkspaceTabs();

            if (_sourceImage != null && !_sourceImage.Empty())
            {
                PersistActiveProductProfile();
            }

            if (openFileDialogImage.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                CvMat loadedImage = null;
                Bitmap displayImage = null;

                try
                {
                    loadedImage = Cv2.ImRead(openFileDialogImage.FileName, ImreadModes.Color);
                    if (loadedImage.Empty())
                    {
                        throw new InvalidOperationException("OpenCV could not decode this image.");
                    }

                    displayImage = BitmapConverter.ToBitmap(loadedImage);
                    var binaryOriginalImage = new Bitmap(displayImage);

                    var oldImage = pictureBoxImage.Image;
                    pictureBoxImage.Image = displayImage;
                    displayImage = null;
                    oldImage?.Dispose();
                    SetPictureBoxImage(pictureBoxBinaryOriginal, binaryOriginalImage);
                    FitImageToViewport();

                    _sourceImage?.Dispose();
                    _sourceImage = loadedImage;
                    loadedImage = null;

                    _grayImage?.Dispose();
                    _grayImage = new CvMat();
                    Cv2.CvtColor(_sourceImage, _grayImage, ColorConversionCodes.BGR2GRAY);
                    UpdateAllPreprocessImages();

                    var productKey = GetProductKeyFromImagePath(openFileDialogImage.FileName);
                    SwitchActiveProduct(productKey);
                    RefreshMeasureDistancePreview();

                    labelImageInfo.Text = string.Format(
                        "{0}    {1} x {2} px",
                        Path.GetFileName(openFileDialogImage.FileName),
                        _sourceImage.Width,
                        _sourceImage.Height);

                    _lastImagePath = openFileDialogImage.FileName;
                }
                finally
                {
                    displayImage?.Dispose();
                    loadedImage?.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    "Unable to load image.\r\n\r\n" + ex.Message,
                    "Load Image",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ReferenceCornerButton_Click(object sender, EventArgs e)
        {
            ShowReferenceCornerWorkspace();
        }

        private void MeasureDistanceButton_Click(object sender, EventArgs e)
        {
            ShowMeasureDistanceWorkspace();
        }

        private void MultiImageConfirmButton_Click(object sender, EventArgs e)
        {
            ShowMultiImageConfirmWorkspace();
        }

        private void ShowMainWorkspaceTabs()
        {
            if (tabControlMain == null)
            {
                return;
            }

            tabControlMain.TabPages.Clear();
            tabControlMain.TabPages.Add(tabPageImageViewer);
            tabControlMain.TabPages.Add(tabPageBinarization);

            tabControlMain.SelectedTab = tabPageImageViewer;
        }

        private void ApplySnapshots(PreprocessSnapshot[] snapshots)
        {
            PreprocessProfileApplier.ApplySnapshots(
                snapshots,
                _preprocessEnabledChecks,
                _thresholdTrackBars,
                _thresholdInputs,
                _erodeInputs,
                _dilateInputs,
                _openInputs,
                _closeInputs);

            if (snapshots != null)
            {
                for (var i = 0; i < 4; i++)
                {
                    if (snapshots[i] != null)
                    {
                        SetPreprocessControlsEnabled(i, snapshots[i].Enabled);
                    }
                }
            }

            UpdateAllPreprocessImages();
        }

        private static string GetProductKeyFromImagePath(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
            {
                return "DEFAULT";
            }

            var fileName = Path.GetFileNameWithoutExtension(imagePath);
            return string.IsNullOrWhiteSpace(fileName) ? "DEFAULT" : fileName.Trim();
        }

        private PreprocessSnapshot[] CaptureCurrentSnapshots()
        {
            return PreprocessProfileApplier.CaptureSnapshots(
                _preprocessEnabledChecks,
                _thresholdInputs,
                _erodeInputs,
                _dilateInputs,
                _openInputs,
                _closeInputs);
        }

        private PreprocessSnapshot[] GetPreprocessSnapshotsForProduct(string productKey)
        {
            productKey = string.IsNullOrWhiteSpace(productKey) ? "DEFAULT" : productKey;

            if (string.Equals(GetCurrentProductKeyOrDefault(), productKey, System.StringComparison.OrdinalIgnoreCase))
            {
                return CaptureCurrentSnapshots();
            }

            return _productProfileService.GetOrCreateState(productKey).PreprocessSnapshots;
        }

        private ReferenceCornerSnapshot GetReferenceCornerSnapshotForProduct(string productKey)
        {
            productKey = string.IsNullOrWhiteSpace(productKey) ? "DEFAULT" : productKey;

            if (string.Equals(GetCurrentProductKeyOrDefault(), productKey, System.StringComparison.OrdinalIgnoreCase))
            {
                return CaptureCurrentReferenceCornerSnapshot();
            }

            return _productProfileService.GetOrCreateState(productKey).ReferenceCornerSnapshot;
        }

        private void PersistActiveProductProfile()
        {
            _productProfileService.PersistProduct(
                GetCurrentProductKeyOrDefault(),
                CaptureCurrentSnapshots(),
                CaptureCurrentReferenceCornerSnapshot(),
                _measureRecords);
        }

        private string GetCurrentProductKeyOrDefault()
        {
            if (!string.IsNullOrWhiteSpace(_activeProductKey))
            {
                return _activeProductKey;
            }

            if (!string.IsNullOrWhiteSpace(_lastImagePath))
            {
                return GetProductKeyFromImagePath(_lastImagePath);
            }

            return "DEFAULT";
        }

        private void SwitchActiveProduct(string productKey)
        {
            productKey = string.IsNullOrWhiteSpace(productKey) ? "DEFAULT" : productKey;

            if (!string.IsNullOrWhiteSpace(_activeProductKey) &&
                string.Equals(_activeProductKey, productKey, System.StringComparison.OrdinalIgnoreCase))
            {
                ApplyProductState(_productProfileService.GetOrCreateState(productKey));
                UpdateReferenceCornerPreview();
                return;
            }

            if (!_suppressPersistOnProductSwitch)
            {
                PersistActiveProductProfile();
            }
            _isApplyingProductState = true;
            try
            {
                _activeProductKey = productKey;
                ApplyProductState(_productProfileService.GetOrCreateState(productKey));
                UpdateReferenceCornerPreview();
            }
            finally
            {
                _isApplyingProductState = false;
            }
        }

        private void ApplyProductState(ProductProfileState state)
        {
            if (state == null)
            {
                return;
            }

            ApplyProductProfile(state.PreprocessSnapshots);
            ApplyReferenceCornerProfile(state.ReferenceCornerSnapshot);
            ApplyMeasureProfile(state.MeasureRecords);
        }

        private void ApplyProductProfile(PreprocessSnapshot[] snapshots)
        {
            ApplySnapshots(ProfileDataCloner.CloneSnapshots(snapshots));
        }

        private void ApplyReferenceCornerProfile(ReferenceCornerSnapshot snapshot)
        {
            ApplyReferenceCornerSnapshot(snapshot);
        }

        private void ApplyMeasureProfile(List<MeasureRecord> records)
        {
            ApplyMeasureRecords(CloneMeasureRecords(records));
        }

        private void LoadSavedAppSettings()
        {
            try
            {
                var loadedData = _settingsRepository.Load(_settingsPath);
                _productProfileService.ReplaceAll(loadedData);

                _lastImagePath = loadedData.LastImagePath;
                _activeProductKey = string.IsNullOrWhiteSpace(loadedData.ActiveProductKey) ? null : loadedData.ActiveProductKey;

                if (!_referenceCornerProfiles.ContainsKey("DEFAULT"))
                {
                    _referenceCornerProfiles["DEFAULT"] = new ReferenceCornerSnapshot
                    {
                        Enabled = false,
                        SourceIndex = 0,
                        Roi = Rectangle.Empty,
                        RoiSaved = false
                    };
                }

                if (!_measureProfiles.ContainsKey("DEFAULT"))
                {
                    _measureProfiles["DEFAULT"] = new List<MeasureRecord>();
                }

                if (string.IsNullOrWhiteSpace(_activeProductKey))
                {
                    _activeProductKey = !string.IsNullOrWhiteSpace(_lastImagePath)
                        ? GetProductKeyFromImagePath(_lastImagePath)
                        : "DEFAULT";
                }

                ApplyProductState(_productProfileService.GetOrCreateState(GetCurrentProductKeyOrDefault()));
            }
            catch
            {
                // Ignore malformed settings and keep defaults.
            }
        }

        private void SaveCurrentAppSettings()
        {
            try
            {
                PersistActiveProductProfile();
                var settingsData = new AppSettingsData
                {
                    LastImagePath = _lastImagePath,
                    ActiveProductKey = _activeProductKey
                };
                _productProfileService.ExportTo(settingsData);
                _settingsRepository.Save(_settingsPath, settingsData, GetCurrentProductKeyOrDefault());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Unable to save settings.\r\n\r\n" + ex.Message, "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLastImageIfAvailable()
        {
            if (string.IsNullOrWhiteSpace(_lastImagePath) || !File.Exists(_lastImagePath))
            {
                return;
            }

            try
            {
                var image = Cv2.ImRead(_lastImagePath, ImreadModes.Color);
                if (image.Empty())
                {
                    return;
                }

                var bitmap = BitmapConverter.ToBitmap(image);
                var oldImage = pictureBoxImage.Image;
                pictureBoxImage.Image = bitmap;
                oldImage?.Dispose();
                FitImageToViewport();

                _sourceImage?.Dispose();
                _sourceImage = image;
                image = null;

                _grayImage?.Dispose();
                _grayImage = new CvMat();
                Cv2.CvtColor(_sourceImage, _grayImage, ColorConversionCodes.BGR2GRAY);
                UpdateAllPreprocessImages();

                _suppressPersistOnProductSwitch = true;
                try
                {
                    SwitchActiveProduct(GetProductKeyFromImagePath(_lastImagePath));
                }
                finally
                {
                    _suppressPersistOnProductSwitch = false;
                }
                ApplyProductState(_productProfileService.GetOrCreateState(GetCurrentProductKeyOrDefault()));
                RefreshMeasureDistancePreview();

                SetPictureBoxImage(pictureBoxBinaryOriginal, new Bitmap(bitmap));
                labelImageInfo.Text = string.Format(
                    "{0}    {1} x {2} px",
                    Path.GetFileName(_lastImagePath),
                    _sourceImage.Width,
                    _sourceImage.Height);
            }
            catch
            {
                // Ignore auto-load errors.
            }
        }

        private void ButtonLoadSavedSettings_Click(object sender, EventArgs e)
        {
            LoadSavedAppSettings();
        }

        private void ButtonSaveCurrentSettings_Click(object sender, EventArgs e)
        {
            SaveCurrentAppSettings();
            MessageBox.Show(this, "Settings saved.", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void SetPictureBoxImage(PictureBox pictureBox, Bitmap image)
        {
            var oldImage = pictureBox.Image;
            pictureBox.Image = image;
            oldImage?.Dispose();
        }

        private void FitImageToViewport()
        {
            if (pictureBoxImage.Image == null)
            {
                return;
            }

            var scaleX = panelImageViewport.ClientSize.Width / (float)pictureBoxImage.Image.Width;
            var scaleY = panelImageViewport.ClientSize.Height / (float)pictureBoxImage.Image.Height;
            _fitScale = Math.Min(scaleX, scaleY);
            _imageScale = _fitScale;
            ApplyImageTransform(true);
        }

        private void PictureBoxImage_MouseWheel(object sender, MouseEventArgs e)
        {
            if (pictureBoxImage.Image == null)
            {
                return;
            }

            var sourceControl = (Control)sender;
            var mousePosition = panelImageViewport.PointToClient(sourceControl.PointToScreen(e.Location));
            var imageX = (mousePosition.X - pictureBoxImage.Left) / _imageScale;
            var imageY = (mousePosition.Y - pictureBoxImage.Top) / _imageScale;
            var zoomFactor = e.Delta > 0 ? 1.15f : 1f / 1.15f;
            var minimumScale = _fitScale * 0.25f;
            var maximumScale = _fitScale * 20f;

            _imageScale = Math.Max(minimumScale, Math.Min(maximumScale, _imageScale * zoomFactor));
            var newWidth = Math.Max(1, (int)Math.Round(pictureBoxImage.Image.Width * _imageScale));
            var newHeight = Math.Max(1, (int)Math.Round(pictureBoxImage.Image.Height * _imageScale));
            pictureBoxImage.Size = new Size(newWidth, newHeight);
            pictureBoxImage.Left = (int)Math.Round(mousePosition.X - imageX * _imageScale);
            pictureBoxImage.Top = (int)Math.Round(mousePosition.Y - imageY * _imageScale);
            ConstrainImagePosition();
        }

        private void PictureBoxImage_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || pictureBoxImage.Image == null)
            {
                return;
            }

            _isDraggingImage = true;
            _lastMousePosition = panelImageViewport.PointToClient(pictureBoxImage.PointToScreen(e.Location));
            pictureBoxImage.Cursor = Cursors.SizeAll;
            pictureBoxImage.Capture = true;
        }

        private void PictureBoxImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDraggingImage)
            {
                return;
            }

            var currentPosition = panelImageViewport.PointToClient(pictureBoxImage.PointToScreen(e.Location));
            pictureBoxImage.Left += currentPosition.X - _lastMousePosition.X;
            pictureBoxImage.Top += currentPosition.Y - _lastMousePosition.Y;
            _lastMousePosition = currentPosition;
            ConstrainImagePosition();
        }

        private void PictureBoxImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            _isDraggingImage = false;
            pictureBoxImage.Cursor = Cursors.Default;
            pictureBoxImage.Capture = false;
        }

        private void ImageViewport_MouseEnter(object sender, EventArgs e)
        {
            pictureBoxImage.Focus();
        }

        private void ApplyImageTransform(bool centerImage)
        {
            var width = Math.Max(1, (int)Math.Round(pictureBoxImage.Image.Width * _imageScale));
            var height = Math.Max(1, (int)Math.Round(pictureBoxImage.Image.Height * _imageScale));
            pictureBoxImage.Size = new Size(width, height);

            if (centerImage)
            {
                pictureBoxImage.Left = (panelImageViewport.ClientSize.Width - width) / 2;
                pictureBoxImage.Top = (panelImageViewport.ClientSize.Height - height) / 2;
            }

            ConstrainImagePosition();
        }

        private void ConstrainImagePosition()
        {
            pictureBoxImage.Left = pictureBoxImage.Width <= panelImageViewport.ClientSize.Width
                ? (panelImageViewport.ClientSize.Width - pictureBoxImage.Width) / 2
                : Math.Max(panelImageViewport.ClientSize.Width - pictureBoxImage.Width, Math.Min(0, pictureBoxImage.Left));

            pictureBoxImage.Top = pictureBoxImage.Height <= panelImageViewport.ClientSize.Height
                ? (panelImageViewport.ClientSize.Height - pictureBoxImage.Height) / 2
                : Math.Max(panelImageViewport.ClientSize.Height - pictureBoxImage.Height, Math.Min(0, pictureBoxImage.Top));
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            PersistActiveProductProfile();
            SaveCurrentAppSettings();

            pictureBoxImage.Image?.Dispose();
            pictureBoxImage.Image = null;
            SetPictureBoxImage(pictureBoxBinaryOriginal, null);
            SetPictureBoxImage(pictureBoxActivePreprocess, null);
            _sourceImage?.Dispose();
            _sourceImage = null;
            _grayImage?.Dispose();
            _grayImage = null;

            for (var i = 0; i < _preprocessImages.Length; i++)
            {
                DisposePreprocessImage(i);
                if (_preprocessPictureBoxes != null)
                {
                    SetPictureBoxImage(_preprocessPictureBoxes[i], null);
                }
            }
        }
    }
}
