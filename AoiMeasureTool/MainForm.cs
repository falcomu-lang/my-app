using System;
using System.Drawing;
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
        private string _lastImagePath;
        private string _activeProductKey;
        private readonly System.Collections.Generic.Dictionary<string, PreprocessSnapshot[]> _productProfiles =
            new System.Collections.Generic.Dictionary<string, PreprocessSnapshot[]>(System.StringComparer.OrdinalIgnoreCase);
        private readonly System.Collections.Generic.Dictionary<string, ReferenceCornerSnapshot> _referenceCornerProfiles =
            new System.Collections.Generic.Dictionary<string, ReferenceCornerSnapshot>(System.StringComparer.OrdinalIgnoreCase);
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
        private Rectangle? _referenceCornerCandidateRectangle;
        private bool _referencePreviewPanning;
        private float _referenceImageScale = 1f;
        private float _referenceFitScale = 1f;
        private Point _lastReferenceMousePosition;
        private float _activeImageScale = 1f;
        private float _activeFitScale = 1f;
        private bool _isDraggingActiveImage;
        private Point _lastActiveMousePosition;
        private bool _showingOriginalInActivePreview;
        private float _savedActiveImageScale = 1f;
        private int _savedActiveImageLeft;
        private int _savedActiveImageTop;
        private float _imageScale = 1f;
        private float _fitScale = 1f;
        private bool _isDraggingImage;
        private Point _lastMousePosition;

        public MainForm()
        {
            InitializeComponent();
            ShowMainWorkspaceTabs();
            InitializePreprocessControls();
            InitializeReferenceCornerControls();
            LoadSavedAppSettings();
            LoadLastImageIfAvailable();
        }

        private void SidebarImageViewerButton_Click(object sender, EventArgs e)
        {
            ShowMainWorkspaceTabs();
            tabControlMain.SelectedTab = tabPageImageViewer;
        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            ShowMainWorkspaceTabs();

            PersistActiveProductProfile();

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
                    _activeProductKey = productKey;
                    ApplyProductProfile(productKey);

                    labelImageInfo.Text = string.Format(
                        "{0}    {1} x {2} px",
                        Path.GetFileName(openFileDialogImage.FileName),
                        _sourceImage.Width,
                        _sourceImage.Height);

                    _lastImagePath = openFileDialogImage.FileName;
                    SaveCurrentAppSettings();
                    UpdateReferenceCornerPreview();
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

        private void ShowReferenceCornerWorkspace()
        {
            if (tabControlMain == null)
            {
                return;
            }

            tabControlMain.TabPages.Clear();
            tabControlMain.TabPages.Add(tabPageReferenceCorner);

            tabControlMain.SelectedTab = tabPageReferenceCorner;
            if (_referenceRoiSaved)
            {
                RefreshReferenceCornerCandidate();
            }
            UpdateReferenceCornerPreview();
        }

        private void ApplySnapshots(PreprocessSnapshot[] snapshots)
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

                _preprocessEnabledChecks[i].Checked = snapshots[i].Enabled;
                _thresholdInputs[i].Value = snapshots[i].Threshold;
                _thresholdTrackBars[i].Value = snapshots[i].Threshold;
                _erodeInputs[i].Value = snapshots[i].ErodeIterations;
                _dilateInputs[i].Value = snapshots[i].DilateIterations;
                _openInputs[i].Value = snapshots[i].OpenIterations;
                _closeInputs[i].Value = snapshots[i].CloseIterations;
                SetPreprocessControlsEnabled(i, snapshots[i].Enabled);
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
            var snapshots = new PreprocessSnapshot[4];

            for (var i = 0; i < 4; i++)
            {
                snapshots[i] = new PreprocessSnapshot
                {
                    Enabled = _preprocessEnabledChecks[i].Checked,
                    Threshold = (int)_thresholdInputs[i].Value,
                    ErodeIterations = (int)_erodeInputs[i].Value,
                    DilateIterations = (int)_dilateInputs[i].Value,
                    OpenIterations = (int)_openInputs[i].Value,
                    CloseIterations = (int)_closeInputs[i].Value
                };
            }

            return snapshots;
        }

        private ReferenceCornerSnapshot CaptureCurrentReferenceCornerSnapshot()
        {
            return new ReferenceCornerSnapshot
            {
                Enabled = _referenceCornerEnabled,
                SourceIndex = _referenceSourceIndex,
                Roi = _referenceRoiRectangle,
                RoiSaved = _referenceRoiSaved,
                CornerFound = _referenceCornerFound
            };
        }

        private void ApplyReferenceCornerSnapshot(ReferenceCornerSnapshot snapshot)
        {
            if (snapshot == null)
            {
                return;
            }

            _referenceCornerEnabled = snapshot.Enabled;
            _referenceSourceIndex = Math.Max(0, Math.Min(3, snapshot.SourceIndex));
            _referenceRoiRectangle = NormalizeRectangle(snapshot.Roi);
            _referenceRoiSaved = snapshot.RoiSaved && _referenceRoiRectangle.Width > 0 && _referenceRoiRectangle.Height > 0;
            _referenceCornerFound = snapshot.CornerFound && _referenceRoiSaved;
            ApplyReferenceCornerUiState();
        }

        private static ReferenceCornerSnapshot CloneReferenceCornerSnapshot(ReferenceCornerSnapshot snapshot)
        {
            if (snapshot == null)
            {
                return null;
            }

            return new ReferenceCornerSnapshot
            {
                Enabled = snapshot.Enabled,
                SourceIndex = snapshot.SourceIndex,
                Roi = snapshot.Roi,
                RoiSaved = snapshot.RoiSaved,
                CornerFound = snapshot.CornerFound
            };
        }

        private void PersistActiveProductProfile()
        {
            if (string.IsNullOrWhiteSpace(_activeProductKey))
            {
                return;
            }

            _productProfiles[_activeProductKey] = CloneSnapshots(CaptureCurrentSnapshots());
            _referenceCornerProfiles[_activeProductKey] = CloneReferenceCornerSnapshot(CaptureCurrentReferenceCornerSnapshot());
        }

        private void ApplyProductProfile(string productKey)
        {
            if (string.IsNullOrWhiteSpace(productKey))
            {
                productKey = "DEFAULT";
            }

            PreprocessSnapshot[] snapshots;
            if (!_productProfiles.TryGetValue(productKey, out snapshots))
            {
                snapshots = CreateDefaultPreprocessSnapshots();
                _productProfiles[productKey] = snapshots;
                ApplySnapshots(CloneSnapshots(snapshots));
                SaveCurrentAppSettings();
                return;
            }

            ApplySnapshots(CloneSnapshots(snapshots));
        }

        private void ApplyReferenceCornerProfile(string productKey)
        {
            if (string.IsNullOrWhiteSpace(productKey))
            {
                productKey = "DEFAULT";
            }

            ReferenceCornerSnapshot snapshot;
            if (!_referenceCornerProfiles.TryGetValue(productKey, out snapshot))
            {
                snapshot = CreateDefaultReferenceCornerSnapshot();
                _referenceCornerProfiles[productKey] = CloneReferenceCornerSnapshot(snapshot);
                ApplyReferenceCornerSnapshot(snapshot);
                return;
            }

            ApplyReferenceCornerSnapshot(snapshot);
        }

        private static PreprocessSnapshot[] CreateDefaultPreprocessSnapshots()
        {
            var snapshots = new PreprocessSnapshot[4];
            for (var i = 0; i < 4; i++)
            {
                snapshots[i] = new PreprocessSnapshot
                {
                    Enabled = false,
                    Threshold = 128,
                    ErodeIterations = 0,
                    DilateIterations = 0,
                    OpenIterations = 0,
                    CloseIterations = 0
                };
            }

            return snapshots;
        }

        private static ReferenceCornerSnapshot CreateDefaultReferenceCornerSnapshot()
        {
            return new ReferenceCornerSnapshot
            {
                Enabled = false,
                SourceIndex = 0,
                Roi = Rectangle.Empty,
                RoiSaved = false,
                CornerFound = false
            };
        }

        private static PreprocessSnapshot[] CloneSnapshots(PreprocessSnapshot[] snapshots)
        {
            if (snapshots == null)
            {
                return null;
            }

            var cloned = new PreprocessSnapshot[snapshots.Length];
            for (var i = 0; i < snapshots.Length; i++)
            {
                if (snapshots[i] == null)
                {
                    continue;
                }

                cloned[i] = new PreprocessSnapshot
                {
                    Enabled = snapshots[i].Enabled,
                    Threshold = snapshots[i].Threshold,
                    ErodeIterations = snapshots[i].ErodeIterations,
                    DilateIterations = snapshots[i].DilateIterations,
                    OpenIterations = snapshots[i].OpenIterations,
                    CloseIterations = snapshots[i].CloseIterations
                };
            }

            return cloned;
        }

        private void LoadSavedAppSettings()
        {
            if (!File.Exists(_settingsPath))
            {
                return;
            }

            try
            {
                var lines = File.ReadAllLines(_settingsPath);
                string lastImagePath = null;
                string activeProductKey = null;
                string currentSection = null;
                var productSnapshotsBySection = new System.Collections.Generic.Dictionary<string, PreprocessSnapshot[]>(System.StringComparer.OrdinalIgnoreCase);
                var referenceSnapshotsBySection = new System.Collections.Generic.Dictionary<string, ReferenceCornerSnapshot>(System.StringComparer.OrdinalIgnoreCase);

                foreach (var rawLine in lines)
                {
                    var line = rawLine.Trim();
                    if (line.Length == 0 || line.StartsWith(";") || line.StartsWith("#"))
                    {
                        continue;
                    }

                    if (line.StartsWith("ImagePath=", System.StringComparison.OrdinalIgnoreCase))
                    {
                        lastImagePath = line.Substring("ImagePath=".Length).Trim();
                        continue;
                    }

                    if (line.StartsWith("ActiveProductKey=", System.StringComparison.OrdinalIgnoreCase))
                    {
                        activeProductKey = line.Substring("ActiveProductKey=".Length).Trim();
                        continue;
                    }

                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        currentSection = line.Substring(1, line.Length - 2).Trim();
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(currentSection))
                    {
                        continue;
                    }

                    var equalsIndex = line.IndexOf('=');
                    if (equalsIndex <= 0)
                    {
                        continue;
                    }

                    var name = line.Substring(0, equalsIndex).Trim();
                    var value = line.Substring(equalsIndex + 1).Trim();

                    if (name.StartsWith("Preprocess", System.StringComparison.OrdinalIgnoreCase))
                    {
                        var suffix = name.Substring("Preprocess".Length);
                        var digitEnd = 0;
                        while (digitEnd < suffix.Length && char.IsDigit(suffix[digitEnd]))
                        {
                            digitEnd++;
                        }

                        var numberText = digitEnd > 0 ? suffix.Substring(0, digitEnd) : null;
                        var propertyName = digitEnd < suffix.Length ? suffix.Substring(digitEnd) : string.Empty;
                        int preprocessIndex;
                        if (!int.TryParse(numberText, out preprocessIndex))
                        {
                            continue;
                        }

                        preprocessIndex -= 1;
                        if (preprocessIndex < 0 || preprocessIndex >= 4)
                        {
                            continue;
                        }

                        PreprocessSnapshot[] productSnapshots;
                        if (!productSnapshotsBySection.TryGetValue(currentSection, out productSnapshots))
                        {
                            productSnapshots = new PreprocessSnapshot[4];
                            productSnapshotsBySection[currentSection] = productSnapshots;
                        }

                        if (productSnapshots[preprocessIndex] == null)
                        {
                            productSnapshots[preprocessIndex] = new PreprocessSnapshot();
                        }

                        var snapshot = productSnapshots[preprocessIndex];
                        if (propertyName.Equals("Enabled", System.StringComparison.OrdinalIgnoreCase))
                        {
                            snapshot.Enabled = bool.Parse(value);
                        }
                        else if (propertyName.Equals("Threshold", System.StringComparison.OrdinalIgnoreCase))
                        {
                            snapshot.Threshold = int.Parse(value);
                        }
                        else if (propertyName.Equals("Erode", System.StringComparison.OrdinalIgnoreCase))
                        {
                            snapshot.ErodeIterations = int.Parse(value);
                        }
                        else if (propertyName.Equals("Dilate", System.StringComparison.OrdinalIgnoreCase))
                        {
                            snapshot.DilateIterations = int.Parse(value);
                        }
                        else if (propertyName.Equals("Open", System.StringComparison.OrdinalIgnoreCase))
                        {
                            snapshot.OpenIterations = int.Parse(value);
                        }
                        else if (propertyName.Equals("Close", System.StringComparison.OrdinalIgnoreCase))
                        {
                            snapshot.CloseIterations = int.Parse(value);
                        }

                        continue;
                    }

                    if (!name.StartsWith("Reference", System.StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    ReferenceCornerSnapshot referenceSnapshot;
                    if (!referenceSnapshotsBySection.TryGetValue(currentSection, out referenceSnapshot))
                    {
                        referenceSnapshot = new ReferenceCornerSnapshot
                        {
                            Enabled = false,
                            SourceIndex = 0,
                            Roi = Rectangle.Empty,
                            RoiSaved = false
                        };
                        referenceSnapshotsBySection[currentSection] = referenceSnapshot;
                    }

                    if (name.Equals("ReferenceCornerEnabled", System.StringComparison.OrdinalIgnoreCase))
                    {
                        referenceSnapshot.Enabled = bool.Parse(value);
                    }
                    else if (name.Equals("ReferenceSourceIndex", System.StringComparison.OrdinalIgnoreCase))
                    {
                        referenceSnapshot.SourceIndex = int.Parse(value);
                    }
                    else if (name.Equals("ReferenceRoiX", System.StringComparison.OrdinalIgnoreCase))
                    {
                        referenceSnapshot.Roi = new Rectangle(int.Parse(value), referenceSnapshot.Roi.Y, referenceSnapshot.Roi.Width, referenceSnapshot.Roi.Height);
                    }
                    else if (name.Equals("ReferenceRoiY", System.StringComparison.OrdinalIgnoreCase))
                    {
                        referenceSnapshot.Roi = new Rectangle(referenceSnapshot.Roi.X, int.Parse(value), referenceSnapshot.Roi.Width, referenceSnapshot.Roi.Height);
                    }
                    else if (name.Equals("ReferenceRoiWidth", System.StringComparison.OrdinalIgnoreCase))
                    {
                        referenceSnapshot.Roi = new Rectangle(referenceSnapshot.Roi.X, referenceSnapshot.Roi.Y, int.Parse(value), referenceSnapshot.Roi.Height);
                    }
                    else if (name.Equals("ReferenceRoiHeight", System.StringComparison.OrdinalIgnoreCase))
                    {
                        referenceSnapshot.Roi = new Rectangle(referenceSnapshot.Roi.X, referenceSnapshot.Roi.Y, referenceSnapshot.Roi.Width, int.Parse(value));
                    }
                    else if (name.Equals("ReferenceRoiSaved", System.StringComparison.OrdinalIgnoreCase))
                    {
                        referenceSnapshot.RoiSaved = bool.Parse(value);
                    }
                }

                _productProfiles.Clear();
                foreach (var entry in productSnapshotsBySection)
                {
                    _productProfiles[entry.Key] = CloneSnapshots(entry.Value);
                }

                _referenceCornerProfiles.Clear();
                foreach (var entry in referenceSnapshotsBySection)
                {
                    _referenceCornerProfiles[entry.Key] = CloneReferenceCornerSnapshot(entry.Value);
                }

                _lastImagePath = lastImagePath;
                _activeProductKey = string.IsNullOrWhiteSpace(activeProductKey) ? null : activeProductKey;

                if (!string.IsNullOrWhiteSpace(_lastImagePath) && File.Exists(_lastImagePath))
                {
                    _activeProductKey = GetProductKeyFromImagePath(_lastImagePath);
                    ApplyProductProfile(_activeProductKey);
                    ApplyReferenceCornerProfile(_activeProductKey);
                }
                else if (!_referenceCornerProfiles.ContainsKey("DEFAULT"))
                {
                    _referenceCornerProfiles["DEFAULT"] = new ReferenceCornerSnapshot
                    {
                        Enabled = false,
                        SourceIndex = 0,
                        Roi = Rectangle.Empty,
                        RoiSaved = false
                    };
                }
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
                var directory = Path.GetDirectoryName(_settingsPath);
                if (!string.IsNullOrEmpty(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var writer = new StreamWriter(_settingsPath, false))
                {
                    PersistActiveProductProfile();

                    if (!string.IsNullOrWhiteSpace(_lastImagePath))
                    {
                        writer.WriteLine("ImagePath=" + _lastImagePath);
                    }

                    if (!string.IsNullOrWhiteSpace(_activeProductKey))
                    {
                        writer.WriteLine("ActiveProductKey=" + _activeProductKey);
                    }

                    foreach (var productProfile in _productProfiles)
                    {
                        writer.WriteLine("[" + productProfile.Key + "]");
                        for (var i = 0; i < 4; i++)
                        {
                            var snapshot = productProfile.Value[i];
                            if (snapshot == null)
                            {
                                continue;
                            }

                            writer.WriteLine("Preprocess" + (i + 1) + "Enabled=" + snapshot.Enabled);
                            writer.WriteLine("Preprocess" + (i + 1) + "Threshold=" + snapshot.Threshold);
                            writer.WriteLine("Preprocess" + (i + 1) + "Erode=" + snapshot.ErodeIterations);
                            writer.WriteLine("Preprocess" + (i + 1) + "Dilate=" + snapshot.DilateIterations);
                            writer.WriteLine("Preprocess" + (i + 1) + "Open=" + snapshot.OpenIterations);
                            writer.WriteLine("Preprocess" + (i + 1) + "Close=" + snapshot.CloseIterations);
                        }

                        ReferenceCornerSnapshot referenceSnapshot;
                        if (!_referenceCornerProfiles.TryGetValue(productProfile.Key, out referenceSnapshot))
                        {
                            referenceSnapshot = new ReferenceCornerSnapshot
                            {
                                Enabled = false,
                                SourceIndex = 0,
                                Roi = Rectangle.Empty,
                                RoiSaved = false
                            };
                        }

                        writer.WriteLine("ReferenceCornerEnabled=" + referenceSnapshot.Enabled);
                        writer.WriteLine("ReferenceSourceIndex=" + referenceSnapshot.SourceIndex);
                        writer.WriteLine("ReferenceRoiX=" + referenceSnapshot.Roi.X);
                        writer.WriteLine("ReferenceRoiY=" + referenceSnapshot.Roi.Y);
                        writer.WriteLine("ReferenceRoiWidth=" + referenceSnapshot.Roi.Width);
                        writer.WriteLine("ReferenceRoiHeight=" + referenceSnapshot.Roi.Height);
                        writer.WriteLine("ReferenceRoiSaved=" + referenceSnapshot.RoiSaved);
                        writer.WriteLine(string.Empty);
                    }
                }
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
                UpdateReferenceCornerPreview();

                SetPictureBoxImage(pictureBoxBinaryOriginal, new Bitmap(bitmap));
                labelImageInfo.Text = string.Format(
                    "{0}    {1} x {2} px",
                    Path.GetFileName(_lastImagePath),
                    _sourceImage.Width,
                    _sourceImage.Height);

                _activeProductKey = GetProductKeyFromImagePath(_lastImagePath);
                ApplyProductProfile(_activeProductKey);
                ApplyReferenceCornerProfile(_activeProductKey);
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

        private void InitializeReferenceCornerControls()
        {
            comboBoxReferenceSource.Items.Clear();
            comboBoxReferenceSource.Items.Add("前處理影像 1");
            comboBoxReferenceSource.Items.Add("前處理影像 2");
            comboBoxReferenceSource.Items.Add("前處理影像 3");
            comboBoxReferenceSource.Items.Add("前處理影像 4");
            comboBoxReferenceSource.SelectedIndex = 0;
            _referenceSourceIndex = 0;
            _referenceCornerEnabled = false;
            ApplyReferenceCornerUiState();
        }

        private void ApplyReferenceCornerUiState()
        {
            if (labelReferenceCornerStatus != null)
            {
                labelReferenceCornerStatus.Visible = false;
            }

            if (checkBoxReferenceCornerEnabled != null)
            {
                checkBoxReferenceCornerEnabled.Checked = _referenceCornerEnabled;
            }

            if (comboBoxReferenceSource != null && comboBoxReferenceSource.Items.Count > 0)
            {
                comboBoxReferenceSource.SelectedIndex = Math.Max(0, Math.Min(comboBoxReferenceSource.Items.Count - 1, _referenceSourceIndex));
                comboBoxReferenceSource.Enabled = _referenceCornerEnabled;
            }

            if (_referenceRoiSaved)
            {
                RefreshReferenceCornerCandidate();
            }
            UpdateReferenceCornerPreview();
            UpdateReferenceRoiSaveButtonState();
        }

        private void ReferenceCornerEnabled_CheckedChanged(object sender, EventArgs e)
        {
            _referenceCornerEnabled = checkBoxReferenceCornerEnabled.Checked;
            PersistReferenceCornerState();
            ApplyReferenceCornerUiState();
        }

        private void ReferenceSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxReferenceSource.SelectedIndex < 0)
            {
                return;
            }

            _referenceSourceIndex = comboBoxReferenceSource.SelectedIndex;
            _referenceRoiRectangle = Rectangle.Empty;
            _referenceRoiSaved = false;
            _referenceCornerFound = false;
            _referenceCornerCandidateRectangle = null;
            PersistReferenceCornerState();
            UpdateReferenceCornerPreview();
            UpdateReferenceRoiSaveButtonState();
        }

        private void UpdateReferenceCornerPreview()
        {
            if (pictureBoxReferencePreview == null)
            {
                return;
            }

            var image = GetSelectedReferencePreviewBitmap();
            SetPictureBoxImage(pictureBoxReferencePreview, image);
            FitReferenceImageToViewport();
            pictureBoxReferencePreview.Invalidate();
        }

        private void PersistReferenceCornerState()
        {
            if (string.IsNullOrWhiteSpace(_activeProductKey))
            {
                return;
            }

            _referenceCornerProfiles[_activeProductKey] = CloneReferenceCornerSnapshot(CaptureCurrentReferenceCornerSnapshot());
            SaveCurrentAppSettings();
        }

        private void UpdateReferenceCornerFoundState(bool found, bool previousFound)
        {
            _referenceCornerFound = found;
            if (found != previousFound)
            {
                PersistReferenceCornerState();
            }
        }

        private static void DrawReferencePoint(Graphics graphics, Brush brush, Pen outlinePen, int x, int y)
        {
            const int radius = 6;
            graphics.FillEllipse(brush, x - radius, y - radius, radius * 2, radius * 2);
            graphics.DrawEllipse(outlinePen, x - radius, y - radius, radius * 2, radius * 2);
        }

        private Bitmap GetSelectedReferencePreviewBitmap()
        {
            if (_preprocessPictureBoxes == null || _referenceSourceIndex < 0 || _referenceSourceIndex >= _preprocessPictureBoxes.Length)
            {
                return null;
            }

            var source = _preprocessPictureBoxes[_referenceSourceIndex].Image;
            if (source == null)
            {
                return null;
            }

            var preview = new Bitmap(source);
            var annotatedPreview = TryAnnotateReferenceCornerPreview(preview);
            preview.Dispose();
            return annotatedPreview;
        }

        private Bitmap TryAnnotateReferenceCornerPreview(Bitmap sourceBitmap)
        {
            if (sourceBitmap == null || _referenceRoiRectangle.Width <= 0 || _referenceRoiRectangle.Height <= 0)
            {
                return sourceBitmap == null ? null : new Bitmap(sourceBitmap);
            }

            using (var sourceMat = BitmapConverter.ToMat(sourceBitmap))
            using (var grayMat = new CvMat())
            using (var binaryMat = new CvMat())
            {
                if (sourceMat.Empty())
                {
                    return new Bitmap(sourceBitmap);
                }

                if (sourceMat.Channels() == 1)
                {
                    sourceMat.CopyTo(grayMat);
                }
                else
                {
                    Cv2.CvtColor(sourceMat, grayMat, ColorConversionCodes.BGR2GRAY);
                }

                Cv2.Threshold(grayMat, binaryMat, 0, 255, OpenCvSharp.ThresholdTypes.Binary | OpenCvSharp.ThresholdTypes.Otsu);
            }

            return new Bitmap(sourceBitmap);
        }

        private void RefreshReferenceCornerCandidate()
        {
            _referenceCornerCandidateRectangle = null;
            var previousFound = _referenceCornerFound;

            if (_preprocessPictureBoxes == null || _referenceSourceIndex < 0 || _referenceSourceIndex >= _preprocessPictureBoxes.Length)
            {
                UpdateReferenceCornerFoundState(false, previousFound);
                return;
            }

            var source = _preprocessPictureBoxes[_referenceSourceIndex].Image;
            if (source == null || _referenceRoiRectangle.Width <= 0 || _referenceRoiRectangle.Height <= 0)
            {
                UpdateReferenceCornerFoundState(false, previousFound);
                return;
            }

            using (var sourceBitmap = new Bitmap(source))
            using (var sourceMat = BitmapConverter.ToMat(sourceBitmap))
            using (var grayMat = new CvMat())
            using (var binaryMat = new CvMat())
            {
                if (sourceMat.Empty())
                {
                    return;
                }

                if (sourceMat.Channels() == 1)
                {
                    sourceMat.CopyTo(grayMat);
                }
                else
                {
                    Cv2.CvtColor(sourceMat, grayMat, ColorConversionCodes.BGR2GRAY);
                }

                Cv2.Threshold(grayMat, binaryMat, 0, 255, OpenCvSharp.ThresholdTypes.Binary | OpenCvSharp.ThresholdTypes.Otsu);

                var roiCenter = new Point(
                    _referenceRoiRectangle.Left + _referenceRoiRectangle.Width / 2,
                    _referenceRoiRectangle.Top + _referenceRoiRectangle.Height / 2);

                var bestRect = FindReferenceCornerCandidate(binaryMat, _referenceRoiRectangle, roiCenter);
                _referenceCornerCandidateRectangle = bestRect;
                UpdateReferenceCornerFoundState(bestRect.HasValue, previousFound);
            }
        }

        private static Rectangle? FindReferenceCornerCandidate(CvMat binaryMat, Rectangle roi, Point roiCenter)
        {
            if (binaryMat == null || binaryMat.Empty())
            {
                return null;
            }

            using (var labels = new CvMat())
            using (var stats = new CvMat())
            using (var centroids = new CvMat())
            {
                var componentCount = Cv2.ConnectedComponentsWithStats(binaryMat, labels, stats, centroids);
                Rectangle? bestRect = null;
                var bestDistance = double.MaxValue;
                for (var i = 1; i < componentCount; i++)
                {
                    var left = stats.At<int>(i, 0);
                    var top = stats.At<int>(i, 1);
                    var width = stats.At<int>(i, 2);
                    var height = stats.At<int>(i, 3);
                    var rect = new Rectangle(left, top, width, height);

                    if (!IsRectangleFullyInsideRoi(rect, roi))
                    {
                        continue;
                    }

                    var centerX = centroids.At<double>(i, 0);
                    var centerY = centroids.At<double>(i, 1);
                    var dx = centerX - roiCenter.X;
                    var dy = centerY - roiCenter.Y;
                    var distance = dx * dx + dy * dy;

                    if (distance < bestDistance)
                    {
                        bestDistance = distance;
                        bestRect = rect;
                    }
                }

                return bestRect;
            }
        }

        private static bool IsRectangleFullyInsideRoi(Rectangle rect, Rectangle roi)
        {
            return rect.Left >= roi.Left
                && rect.Top >= roi.Top
                && rect.Right <= roi.Right
                && rect.Bottom <= roi.Bottom;
        }

        private void DrawReferenceCornerOverlayOnPreview(Graphics graphics, Rectangle rect)
        {
            if (pictureBoxReferencePreview.Image == null || pictureBoxReferencePreview.Width <= 0 || pictureBoxReferencePreview.Height <= 0)
            {
                return;
            }

            var imageWidth = pictureBoxReferencePreview.Image.Width;
            var imageHeight = pictureBoxReferencePreview.Image.Height;
            if (imageWidth <= 0 || imageHeight <= 0)
            {
                return;
            }

            var drawRect = GetReferenceDisplayRectangle(rect);

            using (var boxPen = new Pen(Color.OrangeRed, 3f))
            using (var pointBrush = new SolidBrush(Color.Yellow))
            using (var pointOutlinePen = new Pen(Color.Black, 2f))
            {
                graphics.DrawRectangle(boxPen, drawRect);
                DrawReferencePoint(graphics, pointBrush, pointOutlinePen, drawRect.Left + 2, drawRect.Top + 2);
                DrawReferencePoint(graphics, pointBrush, pointOutlinePen, drawRect.Right - 3, drawRect.Top + 2);
                DrawReferencePoint(graphics, pointBrush, pointOutlinePen, drawRect.Left + drawRect.Width / 2, drawRect.Top + drawRect.Height / 2);
            }
        }


        private Point GetReferenceImagePoint(Point displayPoint)
        {
            if (pictureBoxReferencePreview.Image == null || _referenceImageScale <= 0)
            {
                return displayPoint;
            }

            return new Point(
                (int)Math.Round(displayPoint.X / _referenceImageScale),
                (int)Math.Round(displayPoint.Y / _referenceImageScale));
        }

        private Rectangle GetReferenceDisplayRectangle(Rectangle imageRect)
        {
            if (pictureBoxReferencePreview.Image == null || _referenceImageScale <= 0)
            {
                return imageRect;
            }

            return new Rectangle(
                (int)Math.Round(imageRect.X * _referenceImageScale),
                (int)Math.Round(imageRect.Y * _referenceImageScale),
                Math.Max(1, (int)Math.Round(imageRect.Width * _referenceImageScale)),
                Math.Max(1, (int)Math.Round(imageRect.Height * _referenceImageScale)));
        }

        private void FitReferenceImageToViewport()
        {
            if (pictureBoxReferencePreview.Image == null)
            {
                pictureBoxReferencePreview.Location = Point.Empty;
                pictureBoxReferencePreview.Size = panelReferencePreview.ClientSize;
                _referenceImageScale = 1f;
                _referenceFitScale = 1f;
                return;
            }

            var scaleX = panelReferencePreview.ClientSize.Width / (float)pictureBoxReferencePreview.Image.Width;
            var scaleY = panelReferencePreview.ClientSize.Height / (float)pictureBoxReferencePreview.Image.Height;
            _referenceFitScale = Math.Min(scaleX, scaleY);
            _referenceImageScale = _referenceFitScale;
            pictureBoxReferencePreview.Size = new Size(
                Math.Max(1, (int)Math.Round(pictureBoxReferencePreview.Image.Width * _referenceImageScale)),
                Math.Max(1, (int)Math.Round(pictureBoxReferencePreview.Image.Height * _referenceImageScale)));
            pictureBoxReferencePreview.Left = (panelReferencePreview.ClientSize.Width - pictureBoxReferencePreview.Width) / 2;
            pictureBoxReferencePreview.Top = (panelReferencePreview.ClientSize.Height - pictureBoxReferencePreview.Height) / 2;
        }

        private void ReferencePreview_MouseEnter(object sender, EventArgs e)
        {
            pictureBoxReferencePreview.Focus();
        }

        private void ReferencePreview_MouseWheel(object sender, MouseEventArgs e)
        {
            if (pictureBoxReferencePreview.Image == null)
            {
                return;
            }

            var sourceControl = (Control)sender;
            var mousePosition = panelReferencePreview.PointToClient(sourceControl.PointToScreen(e.Location));
            var imageX = (mousePosition.X - pictureBoxReferencePreview.Left) / _referenceImageScale;
            var imageY = (mousePosition.Y - pictureBoxReferencePreview.Top) / _referenceImageScale;
            var zoomFactor = e.Delta > 0 ? 1.15f : 1f / 1.15f;
            var minimumScale = _referenceFitScale * 0.25f;
            var maximumScale = _referenceFitScale * 20f;
            _referenceImageScale = Math.Max(minimumScale, Math.Min(maximumScale, _referenceImageScale * zoomFactor));
            pictureBoxReferencePreview.Size = new Size(
                Math.Max(1, (int)Math.Round(pictureBoxReferencePreview.Image.Width * _referenceImageScale)),
                Math.Max(1, (int)Math.Round(pictureBoxReferencePreview.Image.Height * _referenceImageScale)));
            pictureBoxReferencePreview.Left = (int)Math.Round(mousePosition.X - imageX * _referenceImageScale);
            pictureBoxReferencePreview.Top = (int)Math.Round(mousePosition.Y - imageY * _referenceImageScale);
            ConstrainReferenceImagePosition();
            pictureBoxReferencePreview.Invalidate();
        }

        private void ReferencePreview_MouseDown(object sender, MouseEventArgs e)
        {
            if (pictureBoxReferencePreview.Image == null)
            {
                return;
            }
            if (e.Button == MouseButtons.Left && _referenceCornerEnabled)
            {
                _isSelectingReferenceRoi = true;
                _referenceRoiStart = GetReferenceImagePoint(e.Location);
                _referenceRoiRectangle = new Rectangle(_referenceRoiStart, Size.Empty);
                _referenceRoiSaved = false;
                _referenceCornerFound = false;
                _referenceCornerCandidateRectangle = null;
                PersistReferenceCornerState();
                if (labelReferenceCornerStatus != null)
                {
                    labelReferenceCornerStatus.Visible = false;
                }
                pictureBoxReferencePreview.Capture = true;
                UpdateReferenceRoiSaveButtonState();
                pictureBoxReferencePreview.Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                _referencePreviewPanning = true;
                _lastReferenceMousePosition = panelReferencePreview.PointToClient(pictureBoxReferencePreview.PointToScreen(e.Location));
                pictureBoxReferencePreview.Cursor = Cursors.SizeAll;
                pictureBoxReferencePreview.Capture = true;
            }
        }
        private void ReferencePreview_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isSelectingReferenceRoi)
            {
                var currentPoint = GetReferenceImagePoint(e.Location);
                _referenceRoiRectangle = NormalizeRectangle(new Rectangle(
                    _referenceRoiStart,
                    new Size(currentPoint.X - _referenceRoiStart.X, currentPoint.Y - _referenceRoiStart.Y)));
                PersistReferenceCornerState();
                pictureBoxReferencePreview.Invalidate();
                return;
            }
            if (!_referencePreviewPanning)
            {
                return;
            }
            var currentPosition = panelReferencePreview.PointToClient(pictureBoxReferencePreview.PointToScreen(e.Location));
            pictureBoxReferencePreview.Left += currentPosition.X - _lastReferenceMousePosition.X;
            pictureBoxReferencePreview.Top += currentPosition.Y - _lastReferenceMousePosition.Y;
            _lastReferenceMousePosition = currentPosition;
            ConstrainReferenceImagePosition();
            pictureBoxReferencePreview.Invalidate();
        }
        private void ReferencePreview_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isSelectingReferenceRoi = false;
                pictureBoxReferencePreview.Capture = false;
                if (labelReferenceCornerStatus != null)
                {
                    labelReferenceCornerStatus.Visible = false;
                }
                pictureBoxReferencePreview.Invalidate();
                UpdateReferenceRoiSaveButtonState();
                return;
            }
            if (e.Button == MouseButtons.Right)
            {
                _referencePreviewPanning = false;
                pictureBoxReferencePreview.Cursor = Cursors.Default;
                pictureBoxReferencePreview.Capture = false;
            }
        }
        private void ReferencePreview_Paint(object sender, PaintEventArgs e)
        {
            if (_referenceRoiRectangle.Width <= 0 || _referenceRoiRectangle.Height <= 0)
            {
                return;
            }
            using (var pen = new Pen(Color.LimeGreen, 2f))
            using (var brush = new SolidBrush(Color.FromArgb(45, Color.LimeGreen)))
            {
                var displayRoi = GetReferenceDisplayRectangle(_referenceRoiRectangle);
                e.Graphics.FillRectangle(brush, displayRoi);
                e.Graphics.DrawRectangle(pen, displayRoi);
            }
            if (_referenceCornerCandidateRectangle.HasValue)
            {
                DrawReferenceCornerOverlayOnPreview(e.Graphics, _referenceCornerCandidateRectangle.Value);
            }
        }
        private void SaveReferenceRoi_Click(object sender, EventArgs e)
        {
            if (_referenceRoiRectangle.Width <= 0 || _referenceRoiRectangle.Height <= 0)
            {
                MessageBox.Show(this, "請先框選 ROI，再保存。", "Reference Corner", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            RefreshReferenceCornerCandidate();
            _referenceRoiSaved = true;
            PersistReferenceCornerState();
            UpdateReferenceRoiSaveButtonState();
            if (labelReferenceCornerStatus != null)
            {
                labelReferenceCornerStatus.Visible = false;
            }
            pictureBoxReferencePreview.Invalidate();
        }
        private void UpdateReferenceRoiSaveButtonState()
        {
            if (buttonSaveReferenceRoi == null)
            {
                return;
            }
            buttonSaveReferenceRoi.Enabled = _referenceCornerEnabled && _referenceRoiRectangle.Width > 0 && _referenceRoiRectangle.Height > 0;
            buttonSaveReferenceRoi.Text = _referenceRoiSaved ? "重新保存 ROI 範圍" : "保存 ROI 範圍";
        }
        private void ConstrainReferenceImagePosition()
        {
            pictureBoxReferencePreview.Left = pictureBoxReferencePreview.Width <= panelReferencePreview.ClientSize.Width
                ? (panelReferencePreview.ClientSize.Width - pictureBoxReferencePreview.Width) / 2
                : Math.Max(panelReferencePreview.ClientSize.Width - pictureBoxReferencePreview.Width, Math.Min(0, pictureBoxReferencePreview.Left));
            pictureBoxReferencePreview.Top = pictureBoxReferencePreview.Height <= panelReferencePreview.ClientSize.Height
                ? (panelReferencePreview.ClientSize.Height - pictureBoxReferencePreview.Height) / 2
                : Math.Max(panelReferencePreview.ClientSize.Height - pictureBoxReferencePreview.Height, Math.Min(0, pictureBoxReferencePreview.Top));
        }
        private static Rectangle NormalizeRectangle(Rectangle rect)
        {
            var x = rect.Width < 0 ? rect.Right : rect.Left;
            var y = rect.Height < 0 ? rect.Bottom : rect.Top;
            return new Rectangle(x, y, Math.Abs(rect.Width), Math.Abs(rect.Height));
        }

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
            return new PreprocessParam
            {
                Enabled = _preprocessEnabledChecks[index].Checked,
                WhiteObject = index < 2,
                Threshold = (int)_thresholdInputs[index].Value,
                ErodeIterations = (int)_erodeInputs[index].Value,
                DilateIterations = (int)_dilateInputs[index].Value,
                OpenIterations = (int)_openInputs[index].Value,
                CloseIterations = (int)_closeInputs[index].Value
            };
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

            _preprocessImages[index] = ImageProcessor.Preprocess(_grayImage, CreatePreprocessParam(index));
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
            var imageX = (mousePosition.X - pictureBoxActivePreprocess.Left) / _activeImageScale;
            var imageY = (mousePosition.Y - pictureBoxActivePreprocess.Top) / _activeImageScale;
            var zoomFactor = e.Delta > 0 ? 1.15f : 1f / 1.15f;
            var minimumScale = _activeFitScale * 0.25f;
            var maximumScale = _activeFitScale * 20f;
            _activeImageScale = Math.Max(minimumScale, Math.Min(maximumScale, _activeImageScale * zoomFactor));

            var width = Math.Max(1, (int)Math.Round(pictureBoxActivePreprocess.Image.Width * _activeImageScale));
            var height = Math.Max(1, (int)Math.Round(pictureBoxActivePreprocess.Image.Height * _activeImageScale));
            pictureBoxActivePreprocess.Size = new Size(width, height);
            pictureBoxActivePreprocess.Left = (int)Math.Round(mousePosition.X - imageX * _activeImageScale);
            pictureBoxActivePreprocess.Top = (int)Math.Round(mousePosition.Y - imageY * _activeImageScale);
            ConstrainActiveImagePosition();
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

            var scaleX = panelActiveViewport.ClientSize.Width / (float)pictureBoxActivePreprocess.Image.Width;
            var scaleY = panelActiveViewport.ClientSize.Height / (float)pictureBoxActivePreprocess.Image.Height;
            _activeFitScale = Math.Min(scaleX, scaleY);
            _activeImageScale = _activeFitScale;
            var width = Math.Max(1, (int)Math.Round(pictureBoxActivePreprocess.Image.Width * _activeImageScale));
            var height = Math.Max(1, (int)Math.Round(pictureBoxActivePreprocess.Image.Height * _activeImageScale));
            pictureBoxActivePreprocess.Size = new Size(width, height);
            pictureBoxActivePreprocess.Left = (panelActiveViewport.ClientSize.Width - width) / 2;
            pictureBoxActivePreprocess.Top = (panelActiveViewport.ClientSize.Height - height) / 2;
        }

        private void ConstrainActiveImagePosition()
        {
            pictureBoxActivePreprocess.Left = pictureBoxActivePreprocess.Width <= panelActiveViewport.ClientSize.Width
                ? (panelActiveViewport.ClientSize.Width - pictureBoxActivePreprocess.Width) / 2
                : Math.Max(panelActiveViewport.ClientSize.Width - pictureBoxActivePreprocess.Width, Math.Min(0, pictureBoxActivePreprocess.Left));

            pictureBoxActivePreprocess.Top = pictureBoxActivePreprocess.Height <= panelActiveViewport.ClientSize.Height
                ? (panelActiveViewport.ClientSize.Height - pictureBoxActivePreprocess.Height) / 2
                : Math.Max(panelActiveViewport.ClientSize.Height - pictureBoxActivePreprocess.Height, Math.Min(0, pictureBoxActivePreprocess.Top));
        }

        private static void SetPictureBoxImage(PictureBox pictureBox, Bitmap image)
        {
            var oldImage = pictureBox.Image;
            pictureBox.Image = image;
            oldImage?.Dispose();
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
            if (index >= 0) UpdatePreprocessImage(index, true);
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

        private sealed class PreprocessSnapshot
        {
            public bool Enabled { get; set; }
            public int Threshold { get; set; }
            public int ErodeIterations { get; set; }
            public int DilateIterations { get; set; }
            public int OpenIterations { get; set; }
            public int CloseIterations { get; set; }
        }

        private sealed class ReferenceCornerSnapshot
        {
            public bool Enabled { get; set; }
            public int SourceIndex { get; set; }
            public Rectangle Roi { get; set; }
            public bool RoiSaved { get; set; }
            public bool CornerFound { get; set; }
        }
    }
}
