using System;
using System.Collections.Generic;
using Mogre;
using Math = System.Math;

namespace RenderingEngine.Engine
{
    public class Engine : BaseEngine
    {
        public const String WindowName = "MOGRE Window";

        private static Engine mInstance;
        private string mModelName;
        private string mModelFilePath;

        public Dictionary<string, Entity> SceneEntities { get; private set; }
        public Dictionary<string, SecurityCamera> SecurityCameras { get; private set; }
        public RaySceneQuery RaySceneQuery { get; private set; }

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

        protected override void CreateScene()
        {
            SecurityCameras = new Dictionary<string, SecurityCamera>();
            SceneEntities = new Dictionary<string, Entity>();
            
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
                }
            }
        }

        private void GetIntersectionWithTerrain(int screenX, int screenY)
        {
            var coords = GetNormalizedCoords(screenX, screenY);
            Ray mouseRay = Camera.GetCameraToViewportRay(coords.x, coords.y);
            Engine.Instance.RaySceneQuery.Ray = mouseRay;

            RaySceneQueryResult result = Engine.Instance.RaySceneQuery.Execute();
            RaySceneQueryResult.Enumerator iter = (RaySceneQueryResult.Enumerator)(result.GetEnumerator());
        }

        private void DeselectAllSecurityCameras()
        {
            foreach (var camera in SecurityCameras)
            {
                camera.Value.Selected = false;
            }
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
            PolygonRayCast rayCast = new PolygonRayCast();
            Vector3 result = new Vector3(), normal = new Vector3();
            var isHit = rayCast.RaycastFromPoint(startPosition, rayDirection, ref result, ref normal);

            if (isHit)
            {
                var securityCamera = new SecurityCamera(result);
                SecurityCameras.Add(securityCamera.Name, securityCamera);    
            }
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
