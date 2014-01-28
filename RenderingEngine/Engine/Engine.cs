using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mogre;
using RenderingEngine.Scene;
using Math = System.Math;

namespace RenderingEngine.Engine
{
    public class Engine : BaseEngine
    {
        public const String WindowName = "MOGRE Window";
        public const float MaxHeight = 10f;

        private static Engine mInstance;

        private string mModelName;
        private string mModelFilePath;
        private bool mIsClampedToTerrain = false;

        public RaySceneQuery RaySceneQuery { get; private set; }
        public Dictionary<string, Model> Models { get; private set; }
        public Model SelectedModel { get; private set; }
        
        public void ChangeTerrainClamping()
        {
            mIsClampedToTerrain = !mIsClampedToTerrain;
        }

        public static Engine Instance
        {
            get { return mInstance ?? (mInstance = new Engine()); }
        }

        public void LoadModel(string modelFileName, string modelFilePath)
        {
            var temp = modelFileName.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            mModelName = temp[0];
            mModelFilePath = modelFilePath;
        }

        public override void SetUpRenderWindow(IntPtr handle, int width, int height)
        {
            WindowParams.Name = WindowName;
            WindowParams.Handle = handle;
            WindowParams.Width = (uint)width;
            WindowParams.Height = (uint)height;
            WindowParams.ColorDepth = 32;
        }

        private void InitVariables()
        {
            Models = new Dictionary<string, Model>();
            SelectedModel = null;
        }

        protected override void CreateScene()
        {
            InitVariables();
            
            SetupLights();
            
            SetSkyBox();
            
            SceneManager.SetWorldGeometry("terrain.cfg");

            LoadModel();

            RaySceneQuery = SceneManager.CreateRayQuery(new Ray());
        }

        private void SetSkyBox()
        {
            SceneManager.SetSkyBox(true, "Examples/CloudyNoonSkyBox");
        }

        private void SetupLights()
        {
            SceneManager.AmbientLight = new ColourValue(0.25f, 0.25f, 0.25f);

            Light pointLight = SceneManager.CreateLight("pointLight0");
            pointLight.Type = Light.LightTypes.LT_POINT;
            pointLight.Position = new Vector3(0, 150, 0);
            pointLight.DiffuseColour = ColourValue.White;
            pointLight.SpecularColour = ColourValue.White;


            pointLight = SceneManager.CreateLight("pointLight1");
            pointLight.Type = Light.LightTypes.LT_POINT;
            pointLight.Position = new Vector3(1500, 150, 1500);
            pointLight.DiffuseColour = ColourValue.White;
            pointLight.SpecularColour = ColourValue.White;
        }

        #region Model controls

        private void LoadModel()
        {
            Model model = new Model(mModelName,mModelFilePath);
            model.Selected = true;
            Models.Add(model.Name,model);

            AlignCamera();
        }

        public void SelectModel(int screenX, int screenY)
        {
            DeselectAllModels();
            var coords = GetNormalizedCoords(screenX, screenY);

            Ray mouseRay = Camera.GetCameraToViewportRay(coords.x, coords.y);
            RaySceneQuery query = SceneManager.CreateRayQuery(mouseRay);
            RaySceneQueryResult results = query.Execute();

            MovableObject intersectedNode = null;
            foreach (RaySceneQueryResultEntry entry in results)
            {
                intersectedNode = entry.movable;
            }
            if (intersectedNode != null)
            {
                var name = intersectedNode.Name;
                if (Models.ContainsKey(name))
                {
                    Models[name].Selected = true;
                    SelectedModel = Models[name];
                    SelectedModel.SelectSecurityCamera(screenX,screenY);
                }
            }
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

        public void CameraControl(Keys key)
        {
            if (SelectedModel != null)
            {
                SelectedModel.CameraControl(key);
            }
        }

        public void DeleteSelectedCamera()
        {
            if (SelectedModel != null)
            {
                SelectedModel.DeleteSelectedCamera();
            }
        }

        #endregion

        #region Helpers

        public Vector2 GetNormalizedCoords(int X, int Y)
        {
            float normX = Math.Abs(X / (float) WindowParams.Width);
            float normY = Math.Abs(Y / (float) WindowParams.Height);
            return new Vector2(normX, normY);
        }



        public Vector3 GetCameraDirection()
        {
            Vector3 res = new Vector3(Camera.Direction.x,Camera.Direction.y,Camera.Direction.z);
            res.x = (float) Math.Round(res.x);
            res.y = (float) Math.Round(res.y);
            res.z = (float) Math.Round(res.z);
            return res;
        }

        public Vector3 GetCameraPosition()
        {
            Vector3 res = new Vector3(Camera.Position.x, Camera.Position.y, Camera.Position.z);
            res.x = (float)Math.Round(res.x);
            res.y = (float)Math.Round(res.y);
            res.z = (float)Math.Round(res.z);
            return res;
        }

        #endregion

        protected override void UpdateScene(FrameEvent evt)
        {
            Vector3 camPos = Camera.Position;
            Ray cameraRay = new Ray(new Vector3(camPos.x, 5000.0f, camPos.z),Vector3.NEGATIVE_UNIT_Y);
            RaySceneQuery.Ray = cameraRay;

            // Perform the scene query;
            RaySceneQueryResult result = RaySceneQuery.Execute();
            RaySceneQueryResult.Enumerator itr = (RaySceneQueryResult.Enumerator)(result.GetEnumerator());

            // Get the results, set the camera height
            if (itr.MoveNext())
            {
                float terrainHeight = itr.Current.worldFragment.singleIntersection.y;
                if (mIsClampedToTerrain)
                {
                    Camera.SetPosition(camPos.x, terrainHeight + 10.0f, camPos.z);
                }
                else
                {
                    if ((terrainHeight + MaxHeight) > camPos.y)
                        Camera.SetPosition(camPos.x, terrainHeight + 10.0f, camPos.z);
                }
            }
        }

        protected void AlignCamera()
        {
            if (Models.Count > 0)
            {
                var nodePos = Models[mModelName+"0"].SceneNode.Position;
                var pos = new Vector3(nodePos.x, nodePos.y+50, nodePos.z+200);
                Camera.Position = pos;
            }
        }

        protected override void DestroyScene()
        {
            base.DestroyScene();
            RaySceneQuery.Dispose();
        }
    }
}
