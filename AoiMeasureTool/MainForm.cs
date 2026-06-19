using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OpenCvSharp.Extensions;
using Cv2 = OpenCvSharp.Cv2;
using CvMat = OpenCvSharp.Mat;
using ImreadModes = OpenCvSharp.ImreadModes;

namespace AoiMeasureTool
{
    public partial class MainForm : Form
    {
        private CvMat _sourceImage;
        private float _imageScale = 1f;
        private float _fitScale = 1f;
        private bool _isDraggingImage;
        private Point _lastMousePosition;

        public MainForm()
        {
            InitializeComponent();
        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
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

                    var oldImage = pictureBoxImage.Image;
                    pictureBoxImage.Image = displayImage;
                    displayImage = null;
                    oldImage?.Dispose();
                    FitImageToViewport();

                    _sourceImage?.Dispose();
                    _sourceImage = loadedImage;
                    loadedImage = null;

                    labelImageInfo.Text = string.Format(
                        "{0}    {1} x {2} px",
                        Path.GetFileName(openFileDialogImage.FileName),
                        _sourceImage.Width,
                        _sourceImage.Height);
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
            _sourceImage?.Dispose();
            _sourceImage = null;
        }
    }
}
