using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ApplicationLogic;
using ApplicationLogic.Interfaces;

namespace ApplicationUI
{
    public partial class MainForm : IApplicationUI
    {
        private readonly AppController mAppController;

        public MainForm()
        {
            InitializeComponent();

            mAppController = new AppController(this);
            mAppController.SetUpRenderingWindow(MainWindow.Handle, MainWindow.Width, MainWindow.Height);
        }

        private void MainForm_Disposed(object sender, EventArgs e)
        {
            if (mAppController != null)
            {
                mAppController.Dispose();
            }
                
        }

        #region Keyboard Form events

        protected override void OnKeyDown(KeyEventArgs e)
        {
 	         mAppController.KeyDown(e.KeyCode);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            mAppController.KeyUp(e.KeyCode);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            mAppController.KeyPress(e.KeyChar);
        }

        #endregion

        #region Mouse MainWindows events

        private void MainWindow_MouseLeave(object sender, EventArgs e)
        {
            //TODO
        }

        private void MainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            mAppController.MouseDown(e);
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
                mAppController.MouseMove(e);
        }

        private void MainWindow_MouseUp(object sender, MouseEventArgs e)
        {
            mAppController.MouseUp(e);
        }

        private void MainWindow_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            mAppController.MouseDoubleClick(e);
        }

        #endregion

        private void AddFile_btn_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
            }
        }

        private void LoadSelectedModel()
        {
            var selected = AvailableModels_combo.SelectedIndex;
            
            if (selected == -1) return;
            
            var selectedItem = AvailableModels_combo.SelectedItem;
            mAppController.LoadModel(selectedItem.ToString());
        }

        private void Start_btn_Click(object sender, EventArgs e)
        {
            LoadSelectedModel();
            mAppController.Start();
        }

        public void SendMessage(string msg)
        {
            Log_textBox.AppendText(msg);
        }

        public void ShowAvailableModels(List<string> models)
        {
            if (models != null) AvailableModels_combo.Items.AddRange(models.ToArray());
        }

        public void UpdateCameraCoordinates(float x, float y, float z)
        {
            CameraCoords_label.Text = string.Format("[{0};{1};{2};]", x, y, z);
        }

        public void ExceptionOccured(Exception e)
        {
            StatusLabel.Text = e.Message;
        }


    }
}
