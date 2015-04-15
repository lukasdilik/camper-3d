using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ApplicationLogic.Interfaces;
using ApplicationLogic.Scene;
using Mogre;
using RenderingEngine.Engine;
using RenderingEngine.Interfaces;
using Math = System.Math;
using PixelFormat = Mogre.PixelFormat;
using Rectangle = System.Drawing.Rectangle;


namespace ApplicationLogic
{
    public partial class AppController : IKeyboardInput, IMouseInput,IApplication
    {
        public static string DefaultMaterialGroupName = ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME;

        public enum Mode { CAMERA_MODE, LIGHT_MODE, MODEL_MODE };

        public Mode ActiveMode = Mode.CAMERA_MODE;
        public LightProperties.LightType ActiveLightType = LightProperties.LightType.Spot;
        private int previousYaw, previousPitch;
        private int mModelCounter;
        private int mCameraCounter;
        private int mLightCounter;

        private bool mIsStarted;
        private bool mIsMainCameraActivated = true;
        private readonly IApplicationUI mApplicationUi;
        private Size mCameraViewDimension;

        private String SelectedSceneNode;

        private bool isLeftMouseButtonDown;

        public Dictionary<string, Model> LoadedModels { get; private set; }
        public Model SelectedModel { get; private set; }
        public ModelLibrary ModelLibrary { get; private set; }
        public AppController(IApplicationUI appUi)
        {
            LoadedModels = new Dictionary<string, Model>();
            Engine.Instance.SetApplicationInstance(this);
            mApplicationUi = appUi;
            mCameraViewDimension = mApplicationUi.GetCameraPreviewDimension();

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

        public void GetAvailableModels()
        {
            mApplicationUi.ShowAvailableModels(ModelLibrary.GetAvailableModelsName());
        }

        public void SetUpRenderingWindow(IntPtr handle, int width, int height)
        {
            Engine.Instance.SetUpRenderWindow(handle, width, height);
        }

        private Model LoadModel(string modelName)
        {
            string modelInstanceName = Path.GetFileNameWithoutExtension(modelName) + mModelCounter;
            var newModel = new Model(modelInstanceName, modelName);
            LoadedModels.Add(modelInstanceName, newModel);

            mModelCounter++;

            mApplicationUi.SendMessage("Model " + modelInstanceName + " successfully loaded");
            return newModel;
        }

        public void SelectModel(int screenX, int screenY)
        {
            DeselectAllModels();
            var movableObject = Engine.Instance.SelectObject(screenX, screenY);
            
            if (movableObject == null) return;
            
            SelectedSceneNode = movableObject.Name;
            
            foreach (var model in LoadedModels)
            {
                if (SelectedSceneNode == model.Value.ModelProperties.Name)
                {
                    SelectModel(model.Value.ModelProperties.Name);    
                }
            }
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

        public void SelectSecurityCamera(int screenX, int screenY)
        {
            DeselectAllCameras();
            var movableObject = Engine.Instance.SelectObject(screenX, screenY);

            if (movableObject == null) return;

            SelectedSceneNode =  movableObject.Name;

            foreach (var model in LoadedModels)
            {
                foreach (var camera in model.Value.SecurityCameras)
                {
                    bool found = SelectedSceneNode.Contains(camera.Value.InternalName);
                    if (found)
                    {
                        model.Value.SelectSecurityCamera(camera.Value.InternalName);    
                        SelectModel(model.Key);
                        mApplicationUi.CameraSelected(camera.Value.Properties);
                    }
                }
            }
        }

        public void SelectLight(int screenX, int screenY)
        {
            DeselectAllLights();
            var movableObject = Engine.Instance.SelectObject(screenX, screenY);

            if (movableObject == null) return;

            SelectedSceneNode = movableObject.Name;

            foreach (var model in LoadedModels)
            {
                foreach (var light in model.Value.Lights)
                {
                    bool found = SelectedSceneNode.Contains(light.Value.Properties.Name);
                    if (found)
                    {
                        SelectedModel.SelectLight(light.Value.Properties.Name);
                        SelectModel(model.Key);
                        mApplicationUi.LightSelected(light.Value.Properties);
                    }
                }
            }
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

        public void AddModel(int screenX, int screenY)
        {
            Vector3 intersection;

            var isIntersection = Engine.Instance.IsIntersectionWithTerrain(screenX, screenY, out intersection);
            
            if (!isIntersection) return;

            DeselectAllModels();
            
            string selectedModelName = mApplicationUi.GetSelectedModelName();
            if (String.IsNullOrEmpty(selectedModelName)) return;
            
            var modelData = ModelLibrary.GetModel(selectedModelName);
            var newModel = LoadModel(modelData.Name);
           

            intersection.y += 1;
            newModel.Translate(intersection);
            mApplicationUi.ModelAdded(newModel.ModelProperties);
        }

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

        public void Shutdown()
        {
            Engine.Instance.Dispose();
        }

        private void DeselectAllModels()
        {
            foreach (var model in LoadedModels)
            {
                model.Value.Selected = false;
            }
        }

        private void DeselectAllCameras()
        {
            foreach (var model in LoadedModels)
            {
                model.Value.DeselectAllSecurityCameras();
            }
        }

        private void DeselectAllLights()
        {
            foreach (var model in LoadedModels)
            {
                model.Value.DeselectAllLights();
            }
        }

        public void CreateSecurityCamera(int screenX, int screenY)
        {
            if (SelectedModel != null)
            {
                var success = SelectedModel.CreateCamera(mCameraCounter,screenX, screenY);
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

        #region RenderToTexture

        private void InitRTTOnSelectedCamera(SecurityCamera selectedSecurityCamera)
        {
            selectedSecurityCamera.InitRTT(CreateTexturePtr(SelectedModel.SelectedSecurityCamera.InternalName));
            selectedSecurityCamera.RenderTexture.PreRenderTargetUpdate += RenderTexture_PreRenderTargetUpdate;
            selectedSecurityCamera.RenderTexture.PostRenderTargetUpdate += RenderTextureOnPostRenderTargetUpdate;
        }

        private void RenderTexture_PreRenderTargetUpdate(RenderTargetEvent_NativePtr evt)
        {
            if (SelectedModel.SelectedSecurityCamera != null )
            {
                SelectedModel.SelectedSecurityCamera.Camera.HideFrustum();
            }
        }

        private void RenderTextureOnPostRenderTargetUpdate(RenderTargetEvent_NativePtr evt)
        {
            if (SelectedModel.SelectedSecurityCamera != null)
            {
                var bmp = MogreTexturePtrToBitmap(SelectedModel.SelectedSecurityCamera.RenderTexturePtr);
                mApplicationUi.UpdateCameraView(SelectedModel.SelectedSecurityCamera.Properties.Name, bmp);
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

        private TexturePtr CreateTexturePtr(string cameraName)
        {
            var textureName = "Texture" + cameraName;
            var width = (uint)mCameraViewDimension.Width;
            var height = (uint)mCameraViewDimension.Height;
            return TextureManager.Singleton.CreateManual(textureName, AppController.DefaultMaterialGroupName,
                TextureType.TEX_TYPE_2D, width, height, 0, PixelFormat.PF_B8G8R8, (int)TextureUsage.TU_RENDERTARGET);
        }

        #endregion

        public void SelectCamera(string key)
        {
            foreach (var model in LoadedModels)
            {
                if (model.Value.SecurityCameras.ContainsKey(key))
                {
                    model.Value.SelectSecurityCamera(key);
                    mApplicationUi.CameraSelected(model.Value.SecurityCameras[key].Properties);
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

        public bool IsSecurityCameraSelected()
        {
            if (SelectedModel != null)
            {
                return SelectedModel.IsSecurityCameraSelected();
            }
            return false;
        }

        public bool IsLightSelected()
        {
            if (SelectedModel != null)
            {
                return SelectedModel.IsLightSelected();
            }
            return false;
        }

        public void CameraMouseClick(MouseEventArgs e)
        {
            //if (SelectedModel != null)
            //{
            //    SelectedModel.CameraMouseClick(e);
            //}
        }

        public void LightMouseClick(MouseEventArgs e)
        {
            if (SelectedModel != null)
            {
                SelectedModel.LightMouseClick(e);
            }
        }

        public void CameraMouseMove(MouseEventArgs e)
        {
            //if (SelectedModel != null)
            //{
            //    SelectedModel.CameraMouseMove(e);
            //}
        }

        public void LightMouseMove(MouseEventArgs e)
        {
            if (SelectedModel != null)
            {
                SelectedModel.LightMouseMove(e);
            }
        }

        public void CameraYaw(int deg)
        {
            if (SelectedModel != null && SelectedModel.SelectedSecurityCamera != null)
            {
                SelectedModel.SelectedSecurityCamera.CameraYaw(deg);
            }
            
        }

        public void CameraPitch(int deg)
        {
            if (SelectedModel != null && SelectedModel.SelectedSecurityCamera != null)
            {
                SelectedModel.SelectedSecurityCamera.CameraPitch(deg);
            }
        }

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
            isLeftMouseButtonDown = true;
            switch (ActiveMode)
            {
                case Mode.CAMERA_MODE:
                    DeselectAllCameras();
                    SelectSecurityCamera(e.X, e.Y);
                    if (IsSecurityCameraSelected())
                    {
                        CameraMouseClick(e);
                    }
                    break;
                case Mode.LIGHT_MODE:
                    DeselectAllLights();
                    SelectLight(e.X,e.Y);
                    if (IsLightSelected())
                    {
                        LightMouseClick(e);
                    }
                    break;
                case Mode.MODEL_MODE:
                    DeselectAllModels();
                    SelectModel(e.X, e.Y);
                    break;
            }
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

        public void DeleteSelectedCamera()
        {
            if (SelectedModel != null && SelectedModel.SelectedSecurityCamera != null)
            {
                var name = SelectedModel.SelectedSecurityCamera.InternalName;
                SelectedModel.DeleteSelectedCamera();
                mApplicationUi.CameraRemoved(name);
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

        public void DeleteSelectedModel()
        {
            if (SelectedModel != null)
            {
                var name = SelectedModel.ModelProperties.Name;
                LoadedModels.Remove(name);
                SelectedModel.Delete();
                SelectedModel = null;
                mApplicationUi.ModelRemoved(name);
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

        public void UpdateStatusBar()
        {
            if (!mIsStarted) return;
            var pos = Engine.Instance.GetMainCameraPosition();
            var dir = Engine.Instance.GetMainCameraDirection();
            var selectedModelName = (SelectedModel != null) ? SelectedModel.ModelProperties.Name : "N/A";
            string info = string.Format("Camera Pos:[{0} ; {1}; {2}] | Dir:[{3} ; {4} ; {5}] | Selected Model: {6} | ActiveMode: {7} | SelectedNode: {8}",pos.x,pos.y,pos.z,dir.x,dir.y,dir.z,selectedModelName,ActiveMode,SelectedSceneNode);
            mApplicationUi.UpdateStatusBarInfo(info);
        }

        private void SerializeLibrary(string fileName)
        {
            var writer = new System.Xml.Serialization.XmlSerializer(typeof(ModelLibrary));
            var file = new StreamWriter(@fileName);
            writer.Serialize(file, ModelLibrary);
            //LogMessage("Model library serialized to file: " + fileName);
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

        public void LogMessage(string msg)
        {
            mApplicationUi.LogMessage(msg);
        }

        public void Exit()
        {
            SerializeLibrary(@ApplicationLogicResources.LibraryFilename);
            if(mIsStarted)
                Engine.Instance.Shutdown();
        }
    }
}
