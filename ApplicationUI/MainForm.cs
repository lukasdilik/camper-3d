using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using ApplicationLogic;
using ApplicationLogic.Interfaces;
using ApplicationLogic.Scene;
using Mogre;
using Math = System.Math;

namespace ApplicationUI
{
    public partial class MainForm : IApplicationUI
    {
        private readonly AppController mAppController;
        private SecurityCameraProperties ActualCameraProperties;
        private LightProperties ActualLightProperties;
        private bool isMainWindowActive = true;
        private LibraryForm mLibraryForm;
        private FullPreviewForm mFullPreviewForm;

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
            mLibraryForm.VisibleChanged += mLibraryForm_VisibleChanged;

            mFullPreviewForm = new FullPreviewForm();
            mFullPreviewForm.Hide();
            mFullPreviewForm.VisibleChanged += mFullPreviewForm_VisibleChanged;
            
            mLibraryForm.FormClosed += mLibraryForm_FormClosed;

            CameraProperties_panel.Hide();
            AddedCameras_comboBox.Hide();
            cameraRotation_panel.Hide();

            LightType_combo.Items.Add("SPOT");
            LightType_combo.Items.Add("POINT");
            LightType_combo.SelectedIndex = 0;

            loadToolStripMenuItem.Enabled = false;
            saveToolStripMenuItem.Enabled = false;

            if (AvailableModels_combo.Items.Count > 0)
            {
                AvailableModels_combo.SelectedIndex = 0;    
            }
        }

        void mFullPreviewForm_VisibleChanged(object sender, EventArgs e)
        {
            mAppController.FullPreview = mFullPreviewForm.Visible;
        }

        void mLibraryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mAppController.FullPreview = false;
        }

        private void mLibraryForm_VisibleChanged(object sender, EventArgs e)
        {
            mAppController.GetAvailableModels();
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

        void MainForm_MouseWheel(object sender, MouseEventArgs e)
        {
            if (isMainWindowActive)
                mAppController.MouseWheel(e);
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
            AvailableModels_combo.Items.Clear();
            foreach (var model in models)
            {
                AvailableModels_combo.Items.Add(model);
            }
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

        public void LightAdded(LightProperties lightProperties)
        {
            Lights_listBox.Items.Add(lightProperties.Name);
            mAppController.SelectLight(lightProperties.Name);
        }

        public void LightSelected(LightProperties lightProperties)
        {
            ShowCorrectControls(lightProperties.Type);

            FillLightProperties(lightProperties);
        }

        private void FillLightProperties(LightProperties lightProperties)
        {
            ActualLightProperties = lightProperties;

            Lights_listBox.SelectedItem = lightProperties.Name;
            LightName_label.Text = lightProperties.Name;
            LightType_combo.SelectedIndex = (lightProperties.Type == LightProperties.LightType.Spot) ? 0 : 1;
            LightPosition_textBox.Text = String.Format("{0:f2};{1:f2};{2:f2}", lightProperties.Position.x,
                lightProperties.Position.y, lightProperties.Position.z);
            LightDirection_textBox.Text = String.Format("{0:f2};{1:f2};{2:f2}", lightProperties.Direction.x,
                lightProperties.Direction.y, lightProperties.Direction.z);
            Color color = Color.FromArgb((int) (lightProperties.Color.r*255), (int) (lightProperties.Color.g*255),
                (int) (lightProperties.Color.b*255));
            LightColor_dialog.Color = color;
            ColorLight_btn.BackColor = color;

            Angle_textBox.Text = string.Format("{0:f0};{1:f0}", lightProperties.InnerAngle.ValueDegrees,
                lightProperties.OuterAngle.ValueDegrees);
        }

        private void ShowCorrectControls(LightProperties.LightType lightType)
        {
            if (lightType == LightProperties.LightType.Point)
            {
                LightDirection_label.Hide();
                Angle_label.Hide();
                LightDirection_textBox.Hide();
                Angle_textBox.Hide();
            }
            else
            {
                LightDirection_label.Show();
                Angle_label.Show();
                LightDirection_textBox.Show();
                Angle_textBox.Show();
            }
        }

        public void LightRemoved(string lightName)
        {
            Lights_listBox.Items.Remove(lightName);
            if (Lights_listBox.Items.Count > 0)
            {
                var firstItem = (string)Lights_listBox.Items[0];
                mAppController.SelectLight(firstItem);
            }
        }

        public void CameraAdded(SecurityCameraProperties cameraProperties)
        {
            AddedCameras_comboBox.Items.Add(cameraProperties.Name);
            AddedCameras_comboBox.SelectedItem = cameraProperties.Name;
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
            ModelFileName_label.Text = modelProperties.MeshName;
        }

        public void CameraRemoved(string cameraName)
        {
            AddedCameras_comboBox.Items.Remove(cameraName);
            ClearCameraProperties();
        }

        public void CameraSelected(SecurityCameraProperties cameraProperties)
        {
            AddedCameras_comboBox.SelectedIndex = AddedCameras_comboBox.FindString(cameraProperties.Name);
            UpdateCameraProperties(cameraProperties);
        }

        public void UpdateCameraProperties(SecurityCameraProperties cameraProperties)
        {
            FillCameraProperties(cameraProperties);
        }

        public void UpdateLightProperties(LightProperties lightProperties)
        {
            FillLightProperties(lightProperties);
        }

        public Size GetCameraPreviewDimension()
        {
            return new Size(CameraView_pictureBox.Width,CameraView_pictureBox.Height);
        }

        public void UpdateCameraView(SecurityCameraProperties properties, Bitmap bmp)
        {
            preview_label.Text = "Preview:" + properties.Name;
            CameraView_pictureBox.Image = bmp;
            CameraView_pictureBox.Invalidate();
        }

        public void UpdateCameraViewNative(SecurityCameraProperties properties, Bitmap bmp)
        {
            mFullPreviewForm.Text = "Preview: " + properties.Name + string.Format("{0:f:0}x{1:f:0}", properties.Resolution.x, properties.Resolution.y);
            var pb = mFullPreviewForm.GetPictureBox();
            pb.Image = bmp;
            pb.Invalidate();
        }

        public void UpdateCameraOrientation(int yawDeg, int pitchDeg)
        {
            PitchAngle_label.Text = pitchDeg.ToString();
            YawAngle_label.Text = yawDeg.ToString();
        }

        public void ActiveModeChanged(AppController.Mode newMode)
        {
            if (newMode == AppController.Mode.CAMERA_MODE)
            {
                Mode_tabControl.SelectedIndex = 0;
            }else if (newMode == AppController.Mode.MODEL_MODE)
            {
                Mode_tabControl.SelectedIndex = 1;
            }
            else if (newMode == AppController.Mode.LIGHT_MODE)
            {
                Mode_tabControl.SelectedIndex = 2;
            }
        }

        public void ClearPreview()
        {
            CameraView_pictureBox.Image = null;
            CameraView_pictureBox.Invalidate();
            preview_label.Text = "Preview: N/A";
        }

        private void FillCameraProperties(SecurityCameraProperties properties)
        {
            ActualCameraProperties = properties;
            Position_textBox.Text = String.Format("{0:f2};{1:f2};{2:f2}", properties.Position.x, properties.Position.y, properties.Position.z);
            Direction_textBox.Text = String.Format("{0:f2};{1:f2};{2:f2}", properties.Direction.x, properties.Direction.y, properties.Direction.z);
            AspectRatio_textBox.Text = properties.AspectRatio.ToString(CultureInfo.InvariantCulture);
            FOVy_textBox.Text = properties.FOVy.ValueDegrees.ToString(CultureInfo.InvariantCulture);
            Resolution_textBox.Text = String.Format("{0:f0};{1:f0}", properties.Resolution.x, properties.Resolution.y);
            Rotation_textBox.Text = String.Format("{0:f0}", properties.Rotation.ValueDegrees);
        }

        private void ClearCameraProperties()
        {
            ActualCameraProperties = new SecurityCameraProperties();
            Position_textBox.Text = "";
            Direction_textBox.Text = "";
            AspectRatio_textBox.Text = "";
            FOVy_textBox.Text = "";
            Resolution_textBox.Text = "";
            Rotation_textBox.Text = "";
            if (AddedCameras_comboBox.Items.Count < 1)
            {
                CameraProperties_panel.Hide();
                cameraRotation_panel.Hide();
            }
            else
            {
                mAppController.SelectCamera((string)AddedCameras_comboBox.Items[0]);
            }
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
                Log_textBox.AppendText(Environment.NewLine);
            }
            catch (Exception e)
            {
                Log_textBox.AppendText(e.ToString());
                Log_textBox.AppendText(Environment.NewLine);
            }

        }

        private void Update_btn_Click(object sender, EventArgs e)
        {
            var newProperties = new SecurityCameraProperties();
            SetNewPosition(ref newProperties);
            SetNewDirection(ref newProperties);
            SetNewFOVy(ref newProperties);
            SetNewResolution(ref newProperties);
            SetNewRotation(ref newProperties);
            mAppController.UpdateCameraProperties(newProperties);
        }
        
        #region CameraProperties
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

        private void SetNewResolution(ref SecurityCameraProperties newProperties)
        {
            try
            {
                var resolutionStr = Resolution_textBox.Text.Replace(',', '.');
                var resParts = resolutionStr.Split(ApplicationUIResources.ValueDelimiter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var x = int.Parse(resParts[0], CultureInfo.InvariantCulture);
                var y = int.Parse(resParts[1], CultureInfo.InvariantCulture);
                if (x <= 0 || y <= 0) throw new Exception("Value must positive");
                newProperties.Resolution = new Vector2(x,y);
            }
            catch (Exception e)
            {
                newProperties.Resolution = ActualCameraProperties.Resolution;
                Log_textBox.AppendText("Resolution data invalid: " + e);
            }
        }

        private void SetNewRotation(ref SecurityCameraProperties newProperties)
        {
            try
            {
                var rotationStr = Rotation_textBox.Text.Replace(',', '.');
                var deg = int.Parse(rotationStr, CultureInfo.InvariantCulture);
                if (deg > 90) throw new Exception("Value must be max 90");
                newProperties.Rotation = new Degree(deg);
            }
            catch (Exception e)
            {
                newProperties.Rotation = ActualCameraProperties.Rotation;
                Log_textBox.AppendText("Rotation angle invalid: " + e);
            }
        }

        #endregion
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
            loadToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
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

        private void ColorLight_btn_Click(object sender, EventArgs e)
        {
            LightColor_dialog.ShowDialog();
        }

        private void UpdateLight_btn_Click(object sender, EventArgs e)
        {
            var newLightProperties = new LightProperties();
            newLightProperties.Name = ActualLightProperties.Name;
            SetNewDirection(ref newLightProperties);
            SetNewPosition(ref newLightProperties);
            SetNewColor(ref newLightProperties);
            SetNewInnerOuterAngle(ref newLightProperties);
            mAppController.UpdateLightProperties(newLightProperties);
        }

        #region LightProperties
        private void SetNewPosition(ref LightProperties newProperties)
        {
            try
            {
                newProperties.Position = VectorFromString(LightPosition_textBox.Text);
            }
            catch (Exception e)
            {
                newProperties.Position = ActualLightProperties.Position;
                Log_textBox.AppendText("Light position data invalid: " + e);
            }
        }

        private void SetNewDirection(ref LightProperties newProperties)
        {
            try
            {
                newProperties.Direction = VectorFromString(LightDirection_textBox.Text);
            }
            catch (Exception e)
            {
                newProperties.Direction = ActualLightProperties.Direction;
                Log_textBox.AppendText("Light direction data invalid: " + e);
            }
        }

        private void SetNewColor(ref LightProperties newProperties)
        {
            try
            {
                var color = LightColor_dialog.Color;
                var r = (float) (color.R / 255.0);
                var g = (float) (color.G / 255.0);
                var b = (float) (color.B / 255.0);
                newProperties.Color = new ColourValue(r, g, b);
            }
            catch (Exception e)
            {
                newProperties.Color = ActualLightProperties.Color;
                Log_textBox.AppendText(" Light color data invalid: " + e);
            }
        }

        private void SetNewInnerOuterAngle(ref LightProperties newProperties)
        {
            try
            {
                var innerOuterStr = Angle_textBox.Text;
                var angles = innerOuterStr.Split(ApplicationUIResources.ValueDelimiter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                var innerAngle = new Degree(float.Parse(angles[0], CultureInfo.InvariantCulture));
                var outerAngle = new Degree(float.Parse(angles[1], CultureInfo.InvariantCulture));
                if (innerAngle < 0 || innerAngle > 180) throw new Exception("Inner Angle Value must be in range 0:180 degree");
                if (outerAngle < 0 || outerAngle > 180) throw new Exception("Inner Angle Value must be in range 0:180 degree");
                newProperties.InnerAngle = innerAngle;
                newProperties.OuterAngle = outerAngle;
            }
            catch (Exception e)
            {
                newProperties.InnerAngle = ActualLightProperties.InnerAngle;
                newProperties.OuterAngle = ActualLightProperties.OuterAngle;
                Log_textBox.AppendText("Inner/outer angle data invalid: " + e);
            }
        }
        #endregion

        private void LightType_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            mAppController.ActiveLightType = LightType_combo.SelectedIndex == 0 ? LightProperties.LightType.Spot : LightProperties.LightType.Point;
        }

        private void Lights_listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var seletectedLight = (string)Lights_listBox.SelectedItem;
            mAppController.SelectLight(seletectedLight);
        }

        private void DeleteLight_btn_Click(object sender, EventArgs e)
        {
            mAppController.DeleteSelectedLight();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mAppController.Exit();
        }

        private void Yaw_hScrollBar_ValueChanged(object sender, EventArgs e)
        {
            YawAngle_label.Text = Yaw_hScrollBar.Value.ToString();
            var yaw = Yaw_hScrollBar.Value;
            mAppController.CameraYaw(yaw);
        }

        private void Pitch_vScrollBar_ValueChanged(object sender, EventArgs e)
        {
            PitchAngle_label.Text = Pitch_vScrollBar.Value.ToString();
            var pitch = Pitch_vScrollBar.Value;
            mAppController.CameraPitch(pitch);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = LoadScene_openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                mAppController.LoadScene(LoadScene_openFileDialog.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                mAppController.SaveScene(saveFileDialog1.FileName);
            }
        }

        private void CameraView_pictureBox_DoubleClick(object sender, EventArgs e)
        {
            if (mFullPreviewForm.Visible) return;

            mAppController.FullPreview = true;
            
            if (mAppController.FullPreview)
            {
                int width = (int) mAppController.GetActiveResolution().x;
                int height = (int) mAppController.GetActiveResolution().y;
                mFullPreviewForm.Size = new Size(width, height);
                mFullPreviewForm.Show();
            }
        }

        public void ClearScene()
        {
            AddedCameras_comboBox.Items.Clear();
            addedModels_listBox.Items.Clear();
            Lights_listBox.Items.Clear();
        }

        private void AddedCameras_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AddedCameras_comboBox.SelectedIndex > -1)
            {
                if (AddedCameras_comboBox.Items.Count > 0)
                {
                    CameraProperties_panel.Show();
                    AddedCameras_comboBox.Show();
                    cameraRotation_panel.Show();
                }
                else
                {
                    AddedCameras_comboBox.Hide();
                    cameraRotation_panel.Hide();
                    CameraProperties_panel.Hide();
                }
                mAppController.SelectCamera((string)AddedCameras_comboBox.Items[AddedCameras_comboBox.SelectedIndex]);
            }
        }

        private void ShowFrustum_button_Click(object sender, EventArgs e)
        {
            if (mAppController.IsFrustumVisible)
            {
                mAppController.HideFrustum();
                ShowFrustum_button.Text = "Show Frustums";
            }
            else
            {
                mAppController.ShowFrustum();
                ShowFrustum_button.Text = "Hide Frustums";
            }
            
        }

        private void Pitch_vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {

        }
    }
}
