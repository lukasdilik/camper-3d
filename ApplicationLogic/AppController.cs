using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ApplicationLogic.Interfaces;
using ApplicationLogic.Scene;
using ApplicationLogic.Scene.Seriaziable;
using Mogre;
using RenderingEngine.Engine;
using RenderingEngine.Interfaces;
using Rectangle = System.Drawing.Rectangle;


namespace ApplicationLogic
{
    public partial class AppController : IKeyboardInput, IMouseInput,IApplication
    {
        public static string DefaultMaterialGroupName = ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME;

        private bool mFullPreview;
        public bool FullPreview
        {
            get { return mFullPreview; }
            set {
                if (IsSecurityCameraSelected())
                {
                    SelectedModel.SelectedSecurityCamera.IsNativeRendering = value;
                }
                mFullPreview = value;
            }
        }

        public enum Mode { CAMERA_MODE, LIGHT_MODE, MODEL_MODE };

        public Mode ActiveMode = Mode.CAMERA_MODE;
        public LightProperties.LightType ActiveLightType = LightProperties.LightType.Spot;
        private int mModelCounter;
        private int mCameraCounter;
        private int mLightCounter;
        public bool IsFrustumVisible = true;
        private bool mIsStarted;
        private bool mIsMainCameraActivated = true;
        private readonly IApplicationUI mApplicationUi;
        public static Size CameraViewDimension;
        private String mSelectedSceneNode;

        public Dictionary<string, Model> LoadedModels { get; private set; }
        public Model SelectedModel { get; private set; }
        public ModelLibrary ModelLibrary { get; private set; }
        public AppController(IApplicationUI appUi)
        {
            LoadedModels = new Dictionary<string, Model>();
            Engine.Instance.SetApplicationInstance(this);
            mApplicationUi = appUi;
            CameraViewDimension = mApplicationUi.GetCameraPreviewDimension();

            if (File.Exists(@ApplicationLogicResources.LibraryFilename))
            {
                DeserializeLibrary(ApplicationLogicResources.LibraryFilename);
            }
            else
            {
                ModelLibrary = new ModelLibrary();
            }
            GetAvailableModels();
        }

        public void SetUpRenderingWindow(IntPtr handle, int width, int height)
        {
            Engine.Instance.SetUpRenderWindow(handle, width, height);
        }

        public string GetMovableObjectName(int screenX, int screenY)
        {
            var movableObject = Engine.Instance.SelectObject(screenX, screenY);
            if (movableObject != null)
            {
                return movableObject.Name;
            }
            return null;
        }

        #region Model Controls

        public void AddModel(int screenX, int screenY)
        {
            Vector3 intersection;

            var isIntersection = Engine.Instance.IsIntersectionWithTerrain(screenX, screenY, out intersection);

            if (!isIntersection) return;

            DeselectAllModels();

            string selectedModelName = mApplicationUi.GetSelectedModelName();
            if (String.IsNullOrEmpty(selectedModelName)) return;

            var modelData = ModelLibrary.GetModelMesh(selectedModelName);
            var newModel = LoadModel(modelData.Name);


            intersection.y += 1;
            newModel.Translate(intersection);
            mApplicationUi.ModelAdded(newModel.ModelProperties);
        }

        private Model CreateModel(ModelEntity entity)
        {
            string meshName = entity.MeshName;
            if (String.IsNullOrEmpty(meshName)) return null;

            var newModel = new Model(entity.Name, entity.MeshName);
       
            newModel.SetTransformationMatrix(entity.GetTransformationMatrix());

            foreach (var cameraEntity in entity.Cameras)
            {
                var newCamera = newModel.AddCamera(cameraEntity.GetCameraProperties());
                InitRTTOnSelectedCamera(newCamera);
                mApplicationUi.CameraAdded(newCamera.Properties);
            }

            foreach (var lightEntity in entity.Lights)
            {
                var newLight = newModel.AddLight(lightEntity.GetLightProperties());
                mApplicationUi.LightAdded(newLight.Properties);
            }

            
            return newModel;
        }

        public Vector2 GetActiveResolution()
        {
            if (SelectedModel != null)
            {
                if (SelectedModel.SelectedSecurityCamera != null)
                {
                    return SelectedModel.SelectedSecurityCamera.Properties.Resolution;
                }
            }
            return new Vector2();
        }

        public void GetAvailableModels()
        {
            mApplicationUi.ShowAvailableModels(ModelLibrary.GetAvailableModelsName());
        }

        private Model LoadModel(string meshName)
        {
            string modelInstanceName = Path.GetFileNameWithoutExtension(meshName) + mModelCounter;
            var newModel = new Model(modelInstanceName, meshName);
            LoadedModels.Add(modelInstanceName, newModel);

            mModelCounter++;

            mApplicationUi.SendMessage("Model " + modelInstanceName + " successfully loaded");
            return newModel;
        }

        public void SelectModel(string name)
        {
            if (name == null) return;
            if(LoadedModels.ContainsKey(name))
            {
                DeselectAllModels();
                var selectedModel = LoadedModels[name];
                selectedModel.Selected = true;
                SelectedModel = selectedModel;
                mApplicationUi.ModelSelected(selectedModel.ModelProperties);
            }
        }

        private void DeselectAllModels()
        {
            foreach (var model in LoadedModels)
            {
                model.Value.Selected = false;
            }
        }


        public void SetNewPositionToSelectedModel(Vector3 newPosition)
        {
            if (SelectedModel != null)
            {
                var oldPosition = SelectedModel.RenderModel.SceneNode.Position;
                SelectedModel.Translate(newPosition - oldPosition);
                mApplicationUi.ModelSelected(SelectedModel.ModelProperties);
            }
        }

        public void ScaleSelectedModel(float factor)
        {
            if (SelectedModel != null)
            {
                SelectedModel.Scale(factor);
                mApplicationUi.ModelSelected(SelectedModel.ModelProperties);
            }
        }

        public void RotateSelectedModel(Degree degree)
        {
            if (SelectedModel != null)
            {
                SelectedModel.RotateY(degree);
                mApplicationUi.ModelSelected(SelectedModel.ModelProperties);
            }
        }

        public void DeleteSelectedModel()
        {
            if (SelectedModel != null)
            {
                var name = SelectedModel.ModelProperties.Name;
                DeleteModel(name);
            }
        }

        public void DeleteModel(string name)
        {
            if (LoadedModels.ContainsKey(name))
            {
                var toDelete = LoadedModels[name];
                toDelete.Delete();
                SelectedModel = null;
                LoadedModels.Remove(name);
                mApplicationUi.ModelRemoved(name);
            }
        }

        #endregion

        #region Camera Controls

        public void CreateSecurityCamera(int screenX, int screenY)
        {
            if (SelectedModel != null)
            {

                var success = SelectedModel.CreateCamera(mCameraCounter, screenX, screenY);

                if (success)
                {
                    mCameraCounter++;
                    if (SelectedModel.SelectedSecurityCamera != null)
                    {

                        InitRTTOnSelectedCamera(SelectedModel.SelectedSecurityCamera);
                        mApplicationUi.CameraAdded(SelectedModel.SelectedSecurityCamera.Properties);
                    }
                }
            }
        }

        public void SelectCamera(string key)
        {
            foreach (var model in LoadedModels)
            {
                if (model.Value.SecurityCameras.ContainsKey(key))
                {
                    model.Value.SelectSecurityCamera(key);
                    SelectModel(model.Value.ModelProperties.Name);
                    if (IsSecurityCameraSelected())
                    {
                        LogMessage("Seletected Camera: " + SelectedModel.SelectedSecurityCamera.Properties.Name);
                        SetCameraMode();
                        mApplicationUi.CameraSelected(model.Value.SecurityCameras[key].Properties);
                    }

                }
            }
        }

        public bool IsSecurityCameraSelected()
        {
            if (SelectedModel != null)
            {
                return SelectedModel.IsSecurityCameraSelected();
            }
            return false;
        }

        public void SwitchToSelectedCamera()
        {
            if (SelectedModel != null)
            {
                var selectedCamera = SelectedModel.SelectedSecurityCamera;
                if (selectedCamera != null)
                {
                    if (mIsMainCameraActivated)
                    {
                        selectedCamera.Camera.HideFrustum();
                        Engine.Instance.SetCameraViewport(selectedCamera.Camera.MogreCamera);
                        mIsMainCameraActivated = false;
                    }
                    else
                    {
                        selectedCamera.Camera.ShowFrustum();
                        Engine.Instance.ResetViewportToMainCamera();
                        mIsMainCameraActivated = true;
                    }
                }
            }
        }

        private void DeselectAllCameras()
        {
            foreach (var model in LoadedModels)
            {
                model.Value.DeselectAllSecurityCameras();
            }
            mApplicationUi.ClearPreview();
        }

        public void CameraMouseClick(MouseEventArgs e)
        {
            //if (SelectedModel != null)
            //{
            //    SelectedModel.CameraMouseClick(e);
            //}
        }



        public void CameraMouseMove(MouseEventArgs e)
        {
            //if (SelectedModel != null)
            //{
            //    SelectedModel.CameraMouseMove(e);
            //}
        }
        public void CameraYaw(int deg)
        {
            if (IsSecurityCameraSelected())
            {
                SelectedModel.SelectedSecurityCamera.CameraYaw(deg);
                mApplicationUi.CameraSelected(SelectedModel.SelectedSecurityCamera.Properties);
            }

        }

        public void CameraPitch(int deg)
        {
            if (IsSecurityCameraSelected())
            {
                SelectedModel.SelectedSecurityCamera.CameraPitch(deg);
                mApplicationUi.CameraSelected(SelectedModel.SelectedSecurityCamera.Properties);
            }
        }

        public void UpdateCameraProperties(SecurityCameraProperties properties)
        {
            if (mIsStarted)
            {
                if (SelectedModel != null)
                {
                    SelectedModel.UpdateSelectedCameraProperties(properties);
                    mApplicationUi.UpdateCameraProperties(properties);
                }
            }
        }

        public void DeleteSelectedCamera()
        {

            foreach (var loadedModel in LoadedModels)
            {
                if (loadedModel.Value.SelectedSecurityCamera != null)
                {
                    var name = loadedModel.Value.SelectedSecurityCamera.Properties.Name;
                    SelectedModel.DeleteSelectedCamera();
                    mApplicationUi.CameraRemoved(name);
                }
            }
        }

        #endregion
        
        #region Light Controls

        private void DeselectAllLights()
        {
            foreach (var model in LoadedModels)
            {
                model.Value.DeselectAllLights();
            }
        } 

        public void CreateLight(int screenX, int screenY)
        {
            if (SelectedModel != null)
            {
                var success = SelectedModel.CreateLight(mLightCounter, screenX, screenY, ActiveLightType);
                if (success)
                {
                    mLightCounter++;
                    if (SelectedModel.SelectedLight != null)
                    {
                        mApplicationUi.LightAdded(SelectedModel.SelectedLight.Properties);
                    }
                }
            }
        }

        public void SelectLight(string key)
        {
            foreach (var model in LoadedModels)
            {
                if (model.Value.Lights.ContainsKey(key))
                {
                    model.Value.SelectLight(key);
                    mApplicationUi.LightSelected(model.Value.Lights[key].Properties);
                }
            }
        }

        public bool IsLightSelected()
        {
            if (SelectedModel != null)
            {
                return SelectedModel.IsLightSelected();
            }
            return false;
        }

        public void UpdateLightProperties(LightProperties properties)
        {
            if (mIsStarted)
            {
                if (SelectedModel != null)
                {
                    SelectedModel.UpdateSelectedLightProperties(properties);
                    mApplicationUi.UpdateLightProperties(properties);
                }
            }
        }

        public void LightMouseMove(MouseEventArgs e)
        {
            if (SelectedModel != null)
            {
                SelectedModel.LightMouseMove(e);
            }
        }

        public void LightMouseClick(MouseEventArgs e)
        {
            if (SelectedModel != null)
            {
                SelectedModel.LightMouseClick(e);
            }
        }

        public void DeleteSelectedLight()
        {
            if (SelectedModel != null && SelectedModel.SelectedLight != null)
            {
                var lightName = SelectedModel.SelectedLight.Properties.Name;
                SelectedModel.DeleteSelectedLight();
                mApplicationUi.LightRemoved(lightName);
            }
        }

        #endregion

        #region RenderToTexture

        private void InitRTTOnSelectedCamera(SecurityCamera camera)
        {
            camera.RenderTexture.PreRenderTargetUpdate += RenderTexture_PreRenderTargetUpdate;
            camera.RenderTexture.PostRenderTargetUpdate += RenderTextureOnPostRenderTargetUpdate;
            camera.RenderTextureNative.PreRenderTargetUpdate += RenderTextureNative_PreRenderTargetUpdate;
            camera.RenderTextureNative.PostRenderTargetUpdate += RenderTextureNative_PostRenderTargetUpdate;
        }

        void RenderTextureNative_PostRenderTargetUpdate(RenderTargetEvent_NativePtr evt)
        {
            if (SelectedModel != null && SelectedModel.SelectedSecurityCamera != null)
            {
                var bmp = MogreTexturePtrToBitmap(SelectedModel.SelectedSecurityCamera.RenderTextureNativePtr);
                mApplicationUi.UpdateCameraViewNative(SelectedModel.SelectedSecurityCamera.Properties, bmp);
                if(IsFrustumVisible)
                    SelectedModel.SelectedSecurityCamera.Camera.ShowFrustum();
            }
        }

        void RenderTextureNative_PreRenderTargetUpdate(RenderTargetEvent_NativePtr evt)
        {
            if (SelectedModel != null && SelectedModel.SelectedSecurityCamera != null)
            {
                SelectedModel.SelectedSecurityCamera.Camera.HideFrustum();
            }
        }

        private void RenderTexture_PreRenderTargetUpdate(RenderTargetEvent_NativePtr evt)
        {
            if (SelectedModel != null && SelectedModel.SelectedSecurityCamera != null)
            {
                SelectedModel.SelectedSecurityCamera.Camera.HideFrustum();
            }
        }

        private void RenderTextureOnPostRenderTargetUpdate(RenderTargetEvent_NativePtr evt)
        {
            if (SelectedModel != null && SelectedModel.SelectedSecurityCamera != null)
            {
                var bmp = MogreTexturePtrToBitmap(SelectedModel.SelectedSecurityCamera.RenderTexturePtr);
                mApplicationUi.UpdateCameraView(SelectedModel.SelectedSecurityCamera.Properties, bmp);
                if (IsFrustumVisible)
                    SelectedModel.SelectedSecurityCamera.Camera.ShowFrustum();
            }
        }

        public unsafe static Bitmap MogreTexturePtrToBitmap(TexturePtr texturePtr)
        {
            var bitmap = new Bitmap((int) texturePtr.Width, (int) texturePtr.Height);
            var rgbValues = new byte[texturePtr.Width * 4 * texturePtr.Height];
            fixed (byte* ptr = &rgbValues[0])
            {
                var pixelBox = new PixelBox(texturePtr.Width, texturePtr.Height, 1, Mogre.PixelFormat.PF_A8R8G8B8,
                                     (IntPtr)ptr);

                texturePtr.GetBuffer().BlitToMemory(pixelBox);
            }
            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, (int)texturePtr.Width, (int)texturePtr.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
            Marshal.Copy(rgbValues, 0, bitmapData.Scan0, rgbValues.Length);
            bitmap.UnlockBits(bitmapData);

            return bitmap;
        }

        private Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        {
            return new Bitmap(bmp, new Size(width, height));
        }
  

        #endregion

        #region Keyboard Input

        public void KeyPress(char key)
        {
            if (!mIsStarted) return;
            HandleKeyPress(key);
        }

        public void KeyDown(Keys key)
        {
            if (!mIsStarted) return;

            if (SelectedModel != null)
            {
                SelectedModel.CameraControl(key);
            }

            HandleKeyDown(key);
            UpdateStatusBar();
        }

        public void KeyUp(Keys key)
        {
            if (!mIsStarted) return;

            HandleKeyUp(key);
        }

        #endregion

        #region Mouse Input

        public void MouseUp(MouseEventArgs e)
        {
            if (!mIsStarted) return;

            HandleMouseUp(e);
        }

        public void MouseDown(MouseEventArgs e)
        {
            if (!mIsStarted) return;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    MouseLeftClick(e);
                    break;
                case MouseButtons.Right:
                    MouseRightClick(e);
                    break;
            }
            HandleMouseDown(e);
            UpdateStatusBar();
        }

        private void MouseLeftClick(MouseEventArgs e)
        {
            mSelectedSceneNode = GetMovableObjectName(e.X, e.Y);

            if (mSelectedSceneNode == null) return;
            
            if (mSelectedSceneNode.Contains("SecurityCamera"))
            {
                DeselectAllCameras();
                SelectCamera(mSelectedSceneNode);
            }
            else if (mSelectedSceneNode.Contains("Light"))
            {
                DeselectAllLights();
                SelectLight(mSelectedSceneNode);
            }
            else
            {
                DeselectAllModels();
                SelectModel(mSelectedSceneNode);
                SetModelMode();
            }
            mApplicationUi.ActiveModeChanged(ActiveMode);
            switch (ActiveMode)
            {
                case Mode.CAMERA_MODE:
                    if (IsSecurityCameraSelected())
                    {
                        CameraMouseClick(e);
                    }
                    break;
                case Mode.LIGHT_MODE:
                    if (IsLightSelected())
                    {
                        LightMouseClick(e);
                    }
                    break;
            }
            UpdateStatusBar();
        }

        private void MouseRightClick(MouseEventArgs e)
        {
            switch (ActiveMode)
            {
                case Mode.CAMERA_MODE:
                    CreateSecurityCamera(e.X, e.Y);
                    break;
                case Mode.LIGHT_MODE:
                    CreateLight(e.X,e.Y);
                    break;
                case Mode.MODEL_MODE:
                    AddModel(e.X, e.Y);
                    break;
            }
        }

        public void MouseMove(MouseEventArgs e)
        {
            if (!mIsStarted) return;

            if (IsSecurityCameraSelected() && ActiveMode == Mode.CAMERA_MODE)
            {
                CameraMouseMove(e);
                return;
            }
            if (IsLightSelected() && ActiveMode == Mode.LIGHT_MODE)
            {
                LightMouseMove(e);
                return;
            }
            
            HandleMouseMove(e);

            UpdateStatusBar();
        }

        public void MouseDoubleClick(MouseEventArgs e)
        {
            if (!mIsStarted) return;
            HandleMouseDoubleClick(e);
        }

        public void MouseWheel(MouseEventArgs e)
        {
            if (!mIsStarted) return;
            LogMessage("wheel");
            HandleMouseWheel(e);
        }


        #endregion

        #region ModeSetter

        public void SetCameraMode()
        {
            ActiveMode = Mode.CAMERA_MODE;
            UpdateStatusBar();
        }

        public void SetLightMode()
        {
            ActiveMode = Mode.LIGHT_MODE;
            UpdateStatusBar();
        }

        public void SetModelMode()
        {
            ActiveMode = Mode.MODEL_MODE;
            UpdateStatusBar();
        }
        #endregion


        public void Start()
        {
            if (mIsStarted) return;
            mIsStarted = true;
            Engine.Instance.Start();
        }

        public void Resize(int width, int height)
        {
            Engine.Instance.Resize(width, height);
        }
        
        #region Saving Scene
        public void LoadScene(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    MessageBox.Show("File " + fileName + " does not exist");
                    return;
                }

                if (Path.GetExtension(fileName) != ApplicationLogicResources.SceneFileExt)
                {
                    MessageBox.Show("Wrong file name extension, correct is: " + ApplicationLogicResources.SceneFileExt);
                    return;
                }

                var reader = new System.Xml.Serialization.XmlSerializer(typeof(SceneEntity));
                var file = new StreamReader(@fileName);
                SceneEntity loadedScene = (SceneEntity)reader.Deserialize(file);
                file.Close();
                ClearScene();

                mModelCounter = loadedScene.ModelCounterValue;
                mCameraCounter = loadedScene.CameraCounterValue;
                mLightCounter = loadedScene.LightCounterValue;

                foreach (var loadedModel in loadedScene.Models)
                {
                    var newModel = CreateModel(loadedModel);
                    LoadedModels.Add(newModel.ModelProperties.Name, newModel);
                    mApplicationUi.ModelAdded(newModel.ModelProperties);
                }

                LogMessage("Model library loaded from file: " + fileName);
                MessageBox.Show("Scene successfully loaded from file: " + fileName);
            }
            catch (Exception e)
            {
                LogMessage("Loading scene failed:"+e);
                MessageBox.Show("Loading scene failed:" + e.Message);
            }
            
        }

        public void SaveScene(string fileName)
        {
            try
            {
                var savedScene = new SceneEntity
                {
                    CameraCounterValue = mCameraCounter,
                    LightCounterValue = mLightCounter,
                    ModelCounterValue = mModelCounter
                };
                foreach (var loadedModel in LoadedModels)
                {
                    var model = new ModelEntity(loadedModel.Value.ModelProperties);
                    var m = loadedModel.Value.GetTransformationMatrix();
                    model.SetTransformationMatrix(m);
                    foreach (var camera in loadedModel.Value.SecurityCameras)
                    {
                        model.AddCameraProperties(camera.Value.Properties);
                    }
                    foreach (var light in loadedModel.Value.Lights)
                    {
                        model.AddLightProperties(light.Value.Properties);
                    }
                    savedScene.AddModel(model);
                }

                var writer = new System.Xml.Serialization.XmlSerializer(typeof(Scene.Seriaziable.SceneEntity));
                var file = new StreamWriter(@fileName);
                writer.Serialize(file, savedScene);
                file.Close();
                MessageBox.Show("Scene successfully saved to file: " + fileName);
            }
            catch (Exception e)
            {
                LogMessage("Scene saving failed: " + e);
                MessageBox.Show("Scene saving failed: " + e.Message);
            }
        }

        public void ClearScene()
        {
            foreach (var loadedModel in LoadedModels)
            {
                var toDelete = LoadedModels[loadedModel.Key];
                toDelete.Delete();
                SelectedModel = null;
            }
            LoadedModels.Clear();
            mApplicationUi.ClearScene();
        }

        #endregion

        public void Shutdown()
        {
            Engine.Instance.Dispose();
        }

        public void UpdateStatusBar()
        {
            if (!mIsStarted) return;
            var pos = Engine.Instance.GetMainCameraPosition();
            var dir = Engine.Instance.GetMainCameraDirection();
            var selectedModelName = (SelectedModel != null) ? SelectedModel.ModelProperties.Name : "N/A";
            string info = string.Format("Camera Pos:[{0} ; {1}; {2}] | Dir:[{3} ; {4} ; {5}] | Selected Model: {6} | ActiveMode: {7} | SelectedNode: {8}",pos.x,pos.y,pos.z,dir.x,dir.y,dir.z,selectedModelName,ActiveMode,mSelectedSceneNode);
            mApplicationUi.UpdateStatusBarInfo(info);
        }

        private void SerializeLibrary(string fileName)
        {
            var writer = new System.Xml.Serialization.XmlSerializer(typeof(ModelLibrary));
            var file = new StreamWriter(@fileName);
            writer.Serialize(file, ModelLibrary);
            file.Close();
        }

        private void DeserializeLibrary(string fileName)
        {
            if (!File.Exists(fileName)) return;
            var reader = new System.Xml.Serialization.XmlSerializer(typeof(ModelLibrary));
            var file = new StreamReader(@fileName);
            ModelLibrary = (ModelLibrary)reader.Deserialize(file);
            file.Close();
            LogMessage("Model library loaded from file: " + fileName);
        }

        public void ShowFrustum()
        {
            foreach (var loadedModel in LoadedModels)
            {
                foreach (var securityCamera in loadedModel.Value.SecurityCameras)
                {
                    securityCamera.Value.Camera.ShowFrustum();
                }
                loadedModel.Value.SetOriginalMaterials();
            }
            IsFrustumVisible = true;
        }

        public void HideFrustum()
        {
            foreach (var loadedModel in LoadedModels)
            {
                foreach (var securityCamera in loadedModel.Value.SecurityCameras)
                {
                    securityCamera.Value.Camera.HideFrustum();
                }
                loadedModel.Value.SetNoTextureMaterial();
            }
            IsFrustumVisible = false;
        }

        public void LogMessage(string msg)
        {
            mApplicationUi.LogMessage(msg);
        }

        public void Exit()
        {
            try {
                SerializeLibrary(@ApplicationLogicResources.LibraryFilename);
                if (mIsStarted)
                    Engine.Instance.Shutdown();
            }
            catch (Exception e) {}
        }
    }
}
