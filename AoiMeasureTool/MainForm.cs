using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AoiMeasureTool
{
    public partial class MainForm : Form
    {
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
                using (var sourceImage = Image.FromFile(openFileDialogImage.FileName))
                {
                    var displayImage = new Bitmap(sourceImage);
                    var oldImage = pictureBoxImage.Image;
                    pictureBoxImage.Image = displayImage;
                    oldImage?.Dispose();

                    labelImageInfo.Text = string.Format(
                        "{0}    {1} x {2} px",
                        Path.GetFileName(openFileDialogImage.FileName),
                        displayImage.Width,
                        displayImage.Height);
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
        }
    }
}
