using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ApplicationLogic;
using ApplicationLogic.Interfaces;

namespace ApplicationUI
{
    public partial class MainForm : Form, IApplicationUI
    {

        private string mFileName = null;
        private readonly AppController mAppController;

        public MainForm()
        {
            InitializeComponent();
            Focus();
            mAppController = new AppController(this);
            mAppController.SetUpRenderingWindow(MainWindow.Handle, MainWindow.Width, MainWindow.Height);
        }

        private void MainForm_Disposed(object sender, EventArgs e)
        {
            if (mAppController != null)
                mAppController.Dispose();
        }

        #region Keyboard Form events

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            mAppController.KeyDown(e);
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            mAppController.KeyPress(e);
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            mAppController.KeyUp(e);
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

        private void LoadModel_btn_Click(object sender, EventArgs e)
        {
            var selected = AvailableModels_combo.SelectedIndex;
            if (selected != -1)
            {
                var selectedItem = AvailableModels_combo.SelectedItem;
                mAppController.LoadModel(selectedItem.ToString());
            }
        }

        private void Start_btn_Click(object sender, EventArgs e)
        {
            Focus();
            mAppController.Start();
        }

        public void ModelSucessfullyLoaded(string modeFileName)
        {
            StatusLabel.Text = "Model successfully loaded" + modeFileName;
        }

        public void ShowAvailableModels(List<string> models)
        {
            if (models != null) AvailableModels_combo.Items.AddRange(models.ToArray());
        }

        public void ExceptionOccured(Exception e)
        {
            StatusLabel.Text = e.Message;
        }
    }
}
