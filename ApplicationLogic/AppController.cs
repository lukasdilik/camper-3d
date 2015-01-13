using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ApplicationLogic.Interfaces;
using ApplicationLogic.Scene;
using Mogre;
using RenderingEngine.Engine;


namespace ApplicationLogic
{
    public partial class AppController : IDisposable, IKeyboardInput, IMouseInput
    {
        private const string StoredModelsPath = "./Resources/models/scene";

        private int mModelCounter = 0;
        private bool mIsStarted;
        private bool mIsMainCameraActivated = true;
        private readonly List<string> mAvailableModels;
        private readonly Engine mEngine;
        private readonly IApplicationUI mApplicationUi;
        public Dictionary<string, Model> Models { get; private set; }
        public Model SelectedModel { get; private set; }

        public AppController(IApplicationUI appUi)
        {
            Models = new Dictionary<string, Model>();
            mEngine = Engine.Instance;
            mAvailableModels = new List<string>();
            mApplicationUi = appUi;
            GetAvailableModels();
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
            mEngine.SetUpRenderWindow(handle, width, height);
        }

        private Model LoadModel(string fileName)
        {
            if (!File.Exists(Path.Combine(StoredModelsPath,fileName)))
            {
                var e = new FileNotFoundException("Model file name not found on path: "+fileName);
                mApplicationUi.ExceptionOccured(e);
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
            MovableObject movableObject = mEngine.SelectObject(screenX, screenY);
            if (movableObject != null)
            {
                var name = movableObject.Name;
                foreach (var model in Models)
                {
                    if (name == model.Value.Name)
                    {
                        model.Value.Selected = true;
                        SelectedModel = model.Value;
                    }
                }
            }
        }

        public void SwitchToSelectedCamera()
        {
            if (SelectedModel != null)
            {
                var selectedCamera = SelectedModel.GetSelectedSecurityCamera();
                if (selectedCamera != null)
                {
                    if (mIsMainCameraActivated)
                    {
                        mEngine.SetCameraViewport(selectedCamera.Camera.MogreCamera);
                        selectedCamera.Camera.Frustum.SceneNode.FlipVisibility();
                        mIsMainCameraActivated = false;
                    }
                    else
                    {
                        mEngine.ResetViewportToMainCamera();
                        selectedCamera.Camera.Frustum.SceneNode.FlipVisibility();
                        mIsMainCameraActivated = true;
                    }
                }
            }

        }

        public void AddModel(int screenX, int screenY)
        {
            Vector3 intersection;

            var isIntersection = mEngine.IsIntersectionWithTerrain(screenX, screenY, out intersection);
            
            if (!isIntersection) return;
            DeselectAllModels();
            int selectedIndex = mApplicationUi.GetSelectedModelIndex();
            string modelFileName = mAvailableModels[selectedIndex];
            var newModel = LoadModel(modelFileName);
            newModel.Selected = true;
            newModel.Translate(intersection);
        }

        protected void AlignCamera()
        {
//            if (Models.Count > 0)
//            {
//                var nodePos = Models[mModelName + "0"].SceneNode.Position;
//                var pos = new Vector3(nodePos.x, nodePos.y + 50, nodePos.z + 200);
//                MainCamera.Position = pos;
//            }
        }

        public void Start() 
        {
            mIsStarted = true;
            mEngine.Start();
        }

        public void Resize(int width, int height)
        {
            mEngine.Resize(width, height);
        }

        public void Dispose()
        {
            if(mEngine != null)
                mEngine.Dispose();
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
                SelectedModel.CreateCamera(screenX, screenY);
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
                SelectedModel.DeleteSelectedCamera();
            }
        }

        public void UpdateStatusBar()
        {
            var pos = mEngine.GetMainCameraPosition();
            var dir = mEngine.GetMainCameraDirection();
            string info = string.Format("Camera Pos:[{0} ; {1}; {2}] | Dir:[{3} ; {4} ; {5}]",pos.x,pos.y,pos.z,dir.x,dir.y,dir.z);
            mApplicationUi.UpdateCameraInformation(info);
        }
    }
}
