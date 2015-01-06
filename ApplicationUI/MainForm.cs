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
            //preselect first model
            AvailableModels_combo.SelectedIndex = 1;
            LoadSelectedModel();
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
            Log_textBox.AppendText(msg+Environment.NewLine);
        }

        public void ShowAvailableModels(List<string> models)
        {
            if (models != null) AvailableModels_combo.Items.AddRange(models.ToArray());
        }

        public void UpdateCameraInformation(string info)
        {
            CameraCoords_label.Text = info;
        }

        public void ExceptionOccured(Exception e)
        {
            StatusLabel.Text = e.Message;
        }

        private void AvailableModels_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSelectedModel();
        }

        private void MainWindow_MouseClick(object sender, MouseEventArgs e)
        {
            Activate();
        }

        private void MainWindow_SizeChanged(object sender, EventArgs e)
        {
           // mAppController.Resize(MainWindow.Width, MainWindow.Height);
        }


    }
}
