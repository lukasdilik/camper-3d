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
        private readonly AppController mAppController;
        private SecurityCameraProperties ActualCameraProperties;
        private bool isMainWindowActive = true;
        private LibraryForm mLibraryForm;

        public MainForm()
        {
            InitializeComponent();
   
            Focus();
            mAppController = new AppController(this);
            mAppController.SetUpRenderingWindow(MainWindow.Handle, MainWindow.Width, MainWindow.Height);

            mLibraryForm = new LibraryForm(mAppController);
            mLibraryForm.Hide();
            mLibraryForm.FormClosed += LibraryFormOnFormClosed;
            mLibraryForm.Closed += mLibraryForm_Closed;

            CameraProperties_panel.Hide();
            SecurityCameras_comboBox.Hide();

            if (AvailableModels_combo.Items.Count > 0)
            {
                AvailableModels_combo.SelectedIndex = 0;    
            }
        }

        private void LibraryFormOnFormClosed(object sender, FormClosedEventArgs formClosedEventArgs)
        {
            mAppController.GetAvailableModels();
        }

        private void mLibraryForm_Closed(object sender, EventArgs e)
        {
            mAppController.GetAvailableModels();
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

        public String GetSelectedModelName()
        {
            return (string) AvailableModels_combo.SelectedItem;
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

        public void ModelRemoved(string modelName)
        {
            addedModels_listBox.Items.Remove(modelName);
            if (addedModels_listBox.Items.Count > 0)
            {
                var firstItem = (string) addedModels_listBox.Items[0];
                mAppController.SelectModel(firstItem);
            }
        }

        public void CameraAdded(SecurityCameraProperties cameraProperties)
        {
            SecurityCameras_comboBox.Items.Add(cameraProperties.Name);
            SecurityCameras_comboBox.SelectedItem = cameraProperties.Name;
            FillCameraProperties(cameraProperties);
        }

        public void ModelAdded(ModelProperties modelProperties)
        {
            addedModels_listBox.Items.Add(string.Format("{0}", modelProperties.Name));
            mAppController.SelectModel(modelProperties.Name);
        }

        public void ModelSelected(ModelProperties modelProperties)
        {
            addedModels_listBox.SelectedItem = modelProperties.Name;
            ModelPosition_textBox.Text = String.Format("{0:f2};{1:f2};{2:f2}", modelProperties.Position.x, modelProperties.Position.y, modelProperties.Position.z);
            ModelFileName_label.Text = modelProperties.FileName;
        }

        public void CameraRemoved(string cameraName)
        {
            SecurityCameras_comboBox.Items.Remove(cameraName);
        }

        public void CameraSelected(SecurityCameraProperties cameraProperties)
        {
            SecurityCameras_comboBox.SelectedIndex = SecurityCameras_comboBox.FindString(cameraProperties.Name);
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
            CameraView_pictureBox.Image = bmp;
            CameraView_pictureBox.Invalidate();
        }

        private void FillCameraProperties(SecurityCameraProperties properties)
        {
            ActualCameraProperties = properties;
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

        private void Update_btn_Click(object sender, EventArgs e)
        {
            var newProperties = new SecurityCameraProperties();
            SetNewPosition(ref newProperties);
            SetNewDirection(ref newProperties);
            SetNewAspectRatio(ref newProperties);
            SetNewFOVy(ref newProperties);
            mAppController.UpdateCameraProperties(newProperties);
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
                var temp = value.Split(ApplicationUIResources.ValueDelimiter.ToCharArray(),StringSplitOptions.RemoveEmptyEntries);
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

        private void DeactivateMainWindow(object sender, EventArgs e)
        {
            isMainWindowActive = false;
            mAppController.StopMovement();
        }

        private void ActivateMainWindow(object sender, EventArgs e)
        {
            isMainWindowActive = true;
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

        private void SecurityCameras_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SecurityCameras_comboBox.SelectedIndex > -1)
            {
                if (SecurityCameras_comboBox.Items.Count > 0)
                {
                    CameraProperties_panel.Show();
                    SecurityCameras_comboBox.Show();
                }
                else
                {
                    SecurityCameras_comboBox.Hide();
                    CameraProperties_panel.Hide();
                }
                mAppController.SelectCamera((string)SecurityCameras_comboBox.Items[SecurityCameras_comboBox.SelectedIndex]);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mAppController.Destroy();
        }

        private void showLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mLibraryForm.Show();
        }

        private void Mode_tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Mode_tabControl.SelectedTab.Name == "Cameras_tab")
            {
                mAppController.SetCameraMode();
            }else if (Mode_tabControl.SelectedTab.Name == "Lights_tab")
            {
                mAppController.SetLightMode();
            }
            else if (Mode_tabControl.SelectedTab.Name == "Models_tab")
            {
                mAppController.SetModelMode();
            }
        }

        private void addedModels_listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedModelName = (string)addedModels_listBox.SelectedItem;
            mAppController.SelectModel(selectedModelName);
        }

        private void ModelApply_btn_Click(object sender, EventArgs e)
        {
            TryMoveSelectedModel();
            TryRotateModel();
            TryScaleModel();
            ModelScale_textBox.Text = "";
            ModelRotate_textBox.Text = "";
        }

        private void TryMoveSelectedModel()
        {
            try
            {
                var newPosition = VectorFromString(ModelPosition_textBox.Text);
                mAppController.SetNewPositionToSelectedModel(newPosition);
            }
            catch (Exception exception)
            {
                Log_textBox.AppendText("Position data invalid: " + exception);
            }
        }

        private void TryRotateModel()
        {
            var degStr = ModelRotate_textBox.Text;
            if (string.IsNullOrEmpty(degStr)) return;
            try
            {
                degStr = degStr.Replace(',', '.');
                var deg = new Degree(float.Parse(degStr, CultureInfo.InvariantCulture));
                if (deg < 0 || deg > 360) throw new Exception("Value must be in range 0:360 degree");
                mAppController.RotateSelectedModel(deg);
            }
            catch (Exception exception)
            {
                Log_textBox.AppendText("Rotate Model data invalid: " + exception);
            }
        }

        private void TryScaleModel()
        {
            string factorStr = ModelScale_textBox.Text;
            if(string.IsNullOrEmpty(factorStr)) return;
            try
            {
                factorStr = factorStr.Replace(',', '.');
                var factor = float.Parse(factorStr, CultureInfo.InvariantCulture);
                mAppController.ScaleSelectedModel(factor);
            }
            catch (Exception exception)
            {
                Log_textBox.AppendText("Scale data invalid: " + exception);
            }
        }

        private void Mode_tabControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.Handled = true;
            }
        }

        private void deleteModel_btn_Click(object sender, EventArgs e)
        {
            mAppController.DeleteSelectedModel();
        }

    }
}
