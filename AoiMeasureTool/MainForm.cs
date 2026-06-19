using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace AoiMeasureTool
{
    public partial class MainForm : Form
    {
        private Mat _sourceImage;

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
                Mat loadedImage = null;
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

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            pictureBoxImage.Image?.Dispose();
            pictureBoxImage.Image = null;
            _sourceImage?.Dispose();
            _sourceImage = null;
        }
    }
}
