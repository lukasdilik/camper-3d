using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mogre;
using RenderingEngine.Drawing;
using Math = System.Math;

namespace RenderingEngine.Engine
{
    public class Engine : BaseEngine
    {
        public const String WindowName = "MOGRE Window";

        private static Engine mInstance;
        private PolygonRayCast mRayCast;
        private string mModelName;
        private string mModelFilePath;

        public Dictionary<string, Entity> SceneEntities { get; private set; }
        public Dictionary<string, SecurityCamera> SecurityCameras { get; private set; }
        public RaySceneQuery RaySceneQuery { get; private set; }
        public SecurityCamera SelectedSecurityCamera { get; private set; }

        private Engine() {}

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
            SecurityCameras = new Dictionary<string, SecurityCamera>();
            SceneEntities = new Dictionary<string, Entity>();
            SelectedSecurityCamera = null;
            mRayCast = new PolygonRayCast();
        }

        protected override void CreateScene()
        {
            InitVariables();
            
            SetupLights();
            
            SetSkyBox();
            
            SceneManager.SetWorldGeometry("terrain.cfg");

            LoadModel();

            RaySceneQuery = mInstance.SceneManager.CreateRayQuery(new Ray());
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

        private void LoadModel()
        {
            var entity = SceneManager.CreateEntity(mModelName, mModelFilePath);
            var node = SceneManager.RootSceneNode.CreateChildSceneNode(mModelName + "Node");
            node.AttachObject(entity);
            node.Translate(750,1,750);

            SceneEntities.Add(entity.Name,entity);
            
            AlignCamera();
        }

        public void SelectObject(int screenX, int screenY)
        {
            DeselectAllSecurityCameras();
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
                if (SecurityCameras.ContainsKey(name))
                {
                    SecurityCameras[name].Selected = true;
                    SelectedSecurityCamera = SecurityCameras[name];
                }
            }
        }

        private void DeselectAllSecurityCameras()
        {
            foreach (var camera in SecurityCameras)
            {
                camera.Value.Selected = false;
            }
            SelectedSecurityCamera = null;
        }

        private Vector2 GetNormalizedCoords(int X, int Y)
        {
            float normX = Math.Abs(X / (float) WindowParams.Width);
            float normY = Math.Abs(Y / (float) WindowParams.Height);
            return new Vector2(normX, normY);
        }

        public void CreateCamera(int screenX, int screenY)
        {
            DeselectAllSecurityCameras();
            var normCoords = GetNormalizedCoords(screenX, screenY);
            Ray mouseRay = Camera.GetCameraToViewportRay(normCoords.x, normCoords.y);
            
            Vector3 startPosition = mouseRay.Origin;
            Vector3 rayDirection = mouseRay.Direction;
            Vector3 contactPoint = new Vector3(), normal = new Vector3();
            var isHit = mRayCast.RaycastFromPoint(startPosition, rayDirection, ref contactPoint, ref normal);
            //otocim normalu pretoze smeruje do vnutra meshu
            normal *= -1;
           
            if (isHit)
            {
                var securityCamera = new SecurityCamera(contactPoint, normal);
                
                SecurityCameras.Add(securityCamera.Name, securityCamera);
                SelectedSecurityCamera = securityCamera;
            }
        }

        public void CameraControl(Keys key)
        {
            if (SelectedSecurityCamera != null)
            {
                SelectedSecurityCamera.HandleKey(key);
            }
        }

        public bool IsSecurityCameraSelected()
        {
            return SelectedSecurityCamera != null;
        }

        public void CameraMouseClick(MouseEventArgs e)
        {
            if (SelectedSecurityCamera != null)
            {
                SelectedSecurityCamera.MouseClick(e);
            }
        }

        public void CameraMouseMove(MouseEventArgs e)
        {
            if (SelectedSecurityCamera != null)
            {
                SelectedSecurityCamera.MouseMove(e);
            }
        }

        public void LookFromTop()
        {
            
        }


        protected override void UpdateScene(FrameEvent evt)
        {
            Vector3 camPos = Camera.Position;
            Ray cameraRay = new Ray(new Vector3(camPos.x, 5000.0f, camPos.z),Vector3.NEGATIVE_UNIT_Y);
            RaySceneQuery.Ray = cameraRay;

            // Perform the scene query;
            RaySceneQueryResult result = RaySceneQuery.Execute();
            RaySceneQueryResult.Enumerator itr = (RaySceneQueryResult.Enumerator)(result.GetEnumerator());

            // Get the results, set the camera height
            if ((itr != null) && itr.MoveNext())
            {
                float terrainHeight = itr.Current.worldFragment.singleIntersection.y;

                if ((terrainHeight + 10.0f) > camPos.y)
                    Camera.SetPosition(camPos.x, terrainHeight + 10.0f, camPos.z);
            }
        }

        protected void AlignCamera()
        {
            if (SceneEntities.Count > 0)
            {
                var nodePos = SceneEntities[mModelName].ParentSceneNode.Position;
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
