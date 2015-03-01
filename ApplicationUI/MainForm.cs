using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using ApplicationLogic;
using ApplicationLogic.Interfaces;
using ApplicationLogic.Scene;
using Mogre;

namespace ApplicationUI
{
    public partial class MainForm : IApplicationUI
    {
        private readonly string ValueDelimiter = ";";
        private readonly AppController mAppController;
        private SecurityCameraProperties ActualCameraProperties;
        private bool isMainWindowActive = true;
        public MainForm()
        {
            InitializeComponent();
            Focus();
            mAppController = new AppController(this);
            mAppController.SetUpRenderingWindow(MainWindow.Handle, MainWindow.Width, MainWindow.Height);
            CameraProperties_panel.Hide();
            AvailableModels_combo.SelectedIndex = 1;
        }

        private void MainForm_Disposed(object sender, EventArgs e)
        {
            if (mAppController != null)
            {
                mAppController.Shutdown();
            }
                
        }

        #region Keyboard Form events

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if(isMainWindowActive)
     	        mAppController.KeyDown(e.KeyCode);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (isMainWindowActive)
                mAppController.KeyUp(e.KeyCode);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (isMainWindowActive)
                mAppController.KeyPress(e.KeyChar);
        }

        #endregion

        #region Mouse MainWindows events

        private void MainWindow_MouseLeave(object sender, EventArgs e)
        {
        }

        private void MainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (isMainWindowActive)
                mAppController.MouseDown(e);
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
                if (isMainWindowActive)
                    mAppController.MouseMove(e);
        }

        private void MainWindow_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMainWindowActive)
                mAppController.MouseUp(e);
        }

        private void MainWindow_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (isMainWindowActive)
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

        public int GetSelectedModelIndex()
        {
            return AvailableModels_combo.SelectedIndex;
        }

        public void SendMessage(string msg)
        {
            Log_textBox.AppendText(msg+Environment.NewLine);
        }

        public void ShowAvailableModels(List<string> models)
        {
            if (models != null) AvailableModels_combo.Items.AddRange(models.ToArray());
        }

        public void UpdateStatusBarInfo(string info)
        {
            CameraCoords_label.Text = info;
        }

        public void AddCamera(SecurityCameraProperties cameraProperties)
        {
            Camera_listBox.Items.Add(cameraProperties.Name);
            Camera_listBox.SelectedItem = cameraProperties.Name;
            FillCameraProperties(cameraProperties);
        }

        public void RemoveCamera(string cameraName)
        {
            Camera_listBox.Items.Remove(cameraName);
        }

        public void CameraSelected(SecurityCameraProperties cameraProperties)
        {
            Camera_listBox.SelectedIndex = Camera_listBox.FindString(cameraProperties.Name);
            UpdateCameraProperties(cameraProperties);
        }

        public void UpdateCameraProperties(SecurityCameraProperties cameraProperties)
        {
            FillCameraProperties(cameraProperties);
        }

        public Size GetCameraPreviewDimension()
        {
            return new Size(CameraView_pictureBox.Width,CameraView_pictureBox.Height);
        }

        public void UpdateCameraView(string cameraName, Bitmap bmp)
        {
            CameraView_label.Text = cameraName;
            CameraView_pictureBox.Image = bmp;
            CameraView_pictureBox.Invalidate();
        }

        private void FillCameraProperties(SecurityCameraProperties properties)
        {
            ActualCameraProperties = properties;
            Name_textBox.Text = properties.Name;
            Position_textBox.Text = String.Format("{0:f2};{1:f2};{2:f2}", properties.Position.x, properties.Position.y, properties.Position.z);
            Direction_textBox.Text = String.Format("{0:f2};{1:f2};{2:f2}", properties.Direction.x, properties.Direction.y, properties.Direction.z);
            AspectRatio_textBox.Text = properties.AspectRatio.ToString();
            FOVy_textBox.Text = properties.FOVy.ValueDegrees.ToString();
            Resolution_textBox.Text = String.Format("{0:f2};{1:f2}", properties.Resolution.x, properties.Resolution.y);
            Rotation_textBox.Text = properties.Rotation.ToString();

        }

        private void MainWindow_MouseClick(object sender, MouseEventArgs e)
        {
            Activate();
        }

        private void MainWindow_SizeChanged(object sender, EventArgs e)
        {
           mAppController.Resize(MainWindow.Width, MainWindow.Height);
        }

        public void LogMessage(string msg)
        {
            if (ActiveForm == null || ActiveForm.Disposing) return;

            try
            {
                Log_textBox.AppendText(msg);
                Log_textBox.AppendText("-------------------------------------");

            }
            catch (Exception e)
            {
                Log_textBox.AppendText(e.ToString());
                Log_textBox.AppendText("-------------------------------------");
            }

        }

        private void Camera_listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Camera_listBox.SelectedIndex > -1)
            {
                if (Camera_listBox.Items.Count > 0)
                {
                    CameraProperties_panel.Show();
                }
                else
                {
                    CameraProperties_panel.Hide();
                }
                mAppController.SelectCamera((string) Camera_listBox.Items[Camera_listBox.SelectedIndex]);
            }
        }

        private void Update_btn_Click(object sender, EventArgs e)
        {
            var newProperties = new SecurityCameraProperties();
            SetNewName(ref newProperties);
            SetNewPosition(ref newProperties);
            SetNewDirection(ref newProperties);
            SetNewAspectRatio(ref newProperties);
            SetNewFOVy(ref newProperties);
            mAppController.UpdateCameraProperties(newProperties);
        }

        private void SetNewName(ref SecurityCameraProperties newProperties)
        {
            newProperties.Name = !string.IsNullOrEmpty(Name_textBox.Text) ? Name_textBox.Text : ActualCameraProperties.Name;
            if (string.IsNullOrEmpty(Name_textBox.Text))
            {
                Log_textBox.AppendText("Name cannot be empty");
            }
        }

        private void SetNewPosition(ref SecurityCameraProperties newProperties)
        {
            try
            {
                newProperties.Position = VectorFromString(Position_textBox.Text);
            }
            catch (Exception e)
            {
                newProperties.Position = ActualCameraProperties.Position;
                Log_textBox.AppendText("Position data invalid: " + e);
            }
        }

        private void SetNewDirection(ref SecurityCameraProperties newProperties)
        {
            try
            {
                newProperties.Direction = VectorFromString(Direction_textBox.Text);
            }
            catch (Exception e)
            {
                newProperties.Direction = ActualCameraProperties.Direction;
                Log_textBox.AppendText("Direction data invalid: " + e);
            }
        }

        private void SetNewAspectRatio(ref SecurityCameraProperties newProperties)
        {
            try
            {
                var aspRatioStr = AspectRatio_textBox.Text.Replace(',', '.');
                newProperties.AspectRatio = float.Parse(aspRatioStr, CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                newProperties.AspectRatio = ActualCameraProperties.AspectRatio;
                Log_textBox.AppendText("AspectRatio data invalid: "+e);
            }
        }

        private void SetNewFOVy(ref SecurityCameraProperties newProperties)
        {
            try
            {
                var degStr = FOVy_textBox.Text.Replace(',', '.');
                var deg = new Degree(float.Parse(degStr, CultureInfo.InvariantCulture));
                if (deg < 0 || deg > 180) throw new Exception("Value must be in range 0:180 degree");
                newProperties.FOVy = deg;
            }
            catch (Exception e)
            {
                newProperties.FOVy = ActualCameraProperties.FOVy;
                Log_textBox.AppendText("FieldOfView data invalid: " + e);
            }
        }

        private Vector3 VectorFromString(string value)
        {
            try
            {
                value = value.Replace(',','.');
                var temp = value.Split(ValueDelimiter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var x = temp[0];
                var y = temp[1];
                var z = temp[2];
                return new Vector3(float.Parse(x, CultureInfo.InvariantCulture), float.Parse(y, CultureInfo.InvariantCulture), float.Parse(z, CultureInfo.InvariantCulture));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mAppController.Start();
        }

        private void LeftPanel_MouseEnter(object sender, EventArgs e)
        {
            isMainWindowActive = false;
        }

        private void MainWindow_MouseEnter(object sender, EventArgs e)
        {
            isMainWindowActive = true;
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Delete_btn_Click(object sender, EventArgs e)
        {
            mAppController.DeleteSelectedCamera();
        }
    }
}
