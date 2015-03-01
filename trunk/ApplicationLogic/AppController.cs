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
using PixelFormat = Mogre.PixelFormat;
using Rectangle = System.Drawing.Rectangle;


namespace ApplicationLogic
{
    public partial class AppController : IKeyboardInput, IMouseInput,IApplication
    {
        public static string DefaultMaterialGroupName = ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME;

        private const string StoredModelsPath = "./Resources/models/scene";

        private int mModelCounter;
        private bool mIsStarted;
        private bool mIsMainCameraActivated = true;
        private readonly List<string> mAvailableModels;
        private readonly IApplicationUI mApplicationUi;
        private Size mCameraViewDimension;

        public Dictionary<string, Model> Models { get; private set; }
        public Model SelectedModel { get; private set; }

        public AppController(IApplicationUI appUi)
        {
            Models = new Dictionary<string, Model>();
            mAvailableModels = new List<string>();
            Engine.Instance.SetApplicationInstance(this);
            mApplicationUi = appUi;
            GetAvailableModels();
            mCameraViewDimension = mApplicationUi.GetCameraPreviewDimension();
        }

        private void GetAvailableModels()
        {
            var models = Directory.GetFiles(@StoredModelsPath, "*.mesh");
            foreach (var model in models)
            {
                var modelName = Path.GetFileName(model);
                mAvailableModels.Add(modelName);
            }
            mApplicationUi.ShowAvailableModels(mAvailableModels);
        }

        public void SetUpRenderingWindow(IntPtr handle, int width, int height)
        {
            Engine.Instance.SetUpRenderWindow(handle, width, height);
        }

        private Model LoadModel(string fileName)
        {
            if (!File.Exists(Path.Combine(StoredModelsPath,fileName)))
            {
                var e = new FileNotFoundException("Model file name not found on path: "+fileName);
                mApplicationUi.LogMessage(e.ToString());
                throw e;
            }

            var modelName = fileName.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
            modelName += mModelCounter; 
            var newModel = new Model(modelName,fileName);
            Models.Add(modelName, newModel);

            mModelCounter++;

            mApplicationUi.SendMessage("Model "+modelName+" successfully loaded");
            return newModel;
        }

        public void SelectModel(int screenX, int screenY)
        {
            DeselectAllModels();
            var movableObject = Engine.Instance.SelectObject(screenX, screenY);
            
            if (movableObject == null) return;
            
            var name = movableObject.Name;
            
            foreach (var model in Models)
            {
                if (name == model.Value.Name)
                {
                    model.Value.Selected = true;
                    SelectedModel = model.Value;
                }
                foreach (var camera in model.Value.SecurityCameras.Where(camera => name == camera.Value.Camera.Name))
                {
                    SelectedModel.SelectSecurityCamera(name);
                    mApplicationUi.CameraSelected(camera.Value.Properties);
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
                        Engine.Instance.SetCameraViewport(selectedCamera.Camera.MogreCamera);
                        mIsMainCameraActivated = false;
                    }
                    else
                    {
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
            
            int selectedIndex = mApplicationUi.GetSelectedModelIndex();
            if (selectedIndex > 0)
            {
                string modelFileName = mAvailableModels[selectedIndex];
                var newModel = LoadModel(modelFileName);
                newModel.Selected = true;
                SelectedModel = newModel;

                newModel.Translate(intersection);
            }
        }

        public void Start() 
        {
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
            foreach (var model in Models)
            {
                model.Value.Selected = false;
                model.Value.DeselectAllSecurityCameras();
            }
        }

        public void CreateSecurityCamera(int screenX, int screenY)
        {
            if (SelectedModel != null)
            {
                var success = SelectedModel.CreateCamera(screenX, screenY);
                if (success)
                {
                    if (SelectedModel.SelectedSecurityCamera != null)
                    {

                        InitRTTOnSelectedCamera(SelectedModel.SelectedSecurityCamera);
                        mApplicationUi.AddCamera(SelectedModel.SelectedSecurityCamera.Properties);
                    }
                }
            }
        }

        private void InitRTTOnSelectedCamera(SecurityCamera selectedSecurityCamera)
        {
            selectedSecurityCamera.InitRTT(CreateTexturePtr(SelectedModel.SelectedSecurityCamera.InternalName));
            selectedSecurityCamera.RenderTexture.PreRenderTargetUpdate += RenderTexture_PreRenderTargetUpdate;
            selectedSecurityCamera.RenderTexture.PostRenderTargetUpdate += RenderTextureOnPostRenderTargetUpdate;
        }

        private void RenderTexture_PreRenderTargetUpdate(RenderTargetEvent_NativePtr evt)
        {
        }

        private void RenderTextureOnPostRenderTargetUpdate(RenderTargetEvent_NativePtr evt)
        {
            if (SelectedModel.SelectedSecurityCamera != null)
            {
                var bmp = MogreTexturePtrToBitmap(SelectedModel.SelectedSecurityCamera.RenderTexturePtr);
                mApplicationUi.UpdateCameraView(SelectedModel.SelectedSecurityCamera.Properties.Name, bmp);    
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

        public void SelectCamera(string key)
        {
            foreach (var model in Models)
            {
                if (model.Value.SecurityCameras.ContainsKey(key))
                {
                    model.Value.SelectSecurityCamera(key);
                    mApplicationUi.CameraSelected(model.Value.SecurityCameras[key].Properties);
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

        public void CameraMouseClick(MouseEventArgs e)
        {
            if (SelectedModel != null)
            {
                SelectedModel.CameraMouseClick(e);
            }
        }

        public void CameraMouseMove(MouseEventArgs e)
        {
            if (SelectedModel != null)
            {
                SelectedModel.CameraMouseMove(e);
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

        private void RefreshSelectedCameraProperties()
        {
            if (SelectedModel != null && SelectedModel.SelectedSecurityCamera != null)
            {
                mApplicationUi.UpdateCameraProperties(SelectedModel.SelectedSecurityCamera.Properties);
            }
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

            if (e.Button == MouseButtons.Left)
            {
                SelectModel(e.X, e.Y);
            }

            if (e.Button == MouseButtons.Right)
            {
                CreateSecurityCamera(e.X, e.Y);
            }

            if (IsSecurityCameraSelected())
            {
                CameraMouseClick(e);
            }
            else
            {
                HandleMouseDown(e);        
            }

            UpdateStatusBar();
        }

        public void MouseMove(MouseEventArgs e)
        {
            if (!mIsStarted) return;
            
            if (IsSecurityCameraSelected())
            {
                CameraMouseMove(e);
            }
            else
            {
                HandleMouseMove(e);    
            }
            
            UpdateStatusBar();
        }

        public void MouseDoubleClick(MouseEventArgs e)
        {
            if (!mIsStarted) return;

            if (e.Button == MouseButtons.Left)
            {
                AddModel(e.X,e.Y);
            }

            HandleMouseDoubleClick(e);
        }


        #endregion

        public void DeleteSelectedCamera()
        {
            if (SelectedModel != null)
            {
                var name = SelectedModel.SelectedSecurityCamera.InternalName;
                SelectedModel.DeleteSelectedCamera();
                mApplicationUi.RemoveCamera(name);
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



        public void UpdateStatusBar()
        {
            RefreshSelectedCameraProperties();
            var pos = Engine.Instance.GetMainCameraPosition();
            var dir = Engine.Instance.GetMainCameraDirection();
            var selectedModelName = (SelectedModel != null) ? SelectedModel.Name : "N/A";
            string info = string.Format("Camera Pos:[{0} ; {1}; {2}] | Dir:[{3} ; {4} ; {5}] | Selected Model: {6}",pos.x,pos.y,pos.z,dir.x,dir.y,dir.z,selectedModelName);
            mApplicationUi.UpdateStatusBarInfo(info);
        }

        public void LogMessage(string msg)
        {
            mApplicationUi.LogMessage(msg);
        }
    }
}
