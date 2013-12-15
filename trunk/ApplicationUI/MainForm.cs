using System;
using System.Windows.Forms;
using ApplicationLogic;
using ApplicationLogic.Interfaces;

namespace ApplicationUI
{
    public partial class MainForm : Form, IApplicationUI
    {
        private readonly AppController mAppController;

        public MainForm()
        {
            InitializeComponent();
            Show();

            mAppController = new AppController(this);
            mAppController.SetUpRenderingWindow(MainWindow.Handle, MainWindow.Width, MainWindow.Height);
        }

        public void Start() 
        {
            mAppController.Start();
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



    }
}
