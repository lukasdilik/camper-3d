using System.Windows.Forms;

namespace ApplicationUI
{
    public partial class FullPreviewForm : Form
    {
        public FullPreviewForm()
        {
            InitializeComponent();
        }

        private void FullPreviewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        public PictureBox GetPictureBox()
        {
            return fullPreview_pictureBox;
        }
    }
}
