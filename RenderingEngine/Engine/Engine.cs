using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mogre;
using Math = System.Math;

namespace RenderingEngine.Engine
{
    public class Engine : BaseEngine
    {
        private const String WindowName = "MOGRE Window";

        private string mModelName;
        private string mModelFilePath;

        private RaySceneQuery mRaySceneQuery = null;      // The ray scene query pointer
        private int mCount;                           // The number of robots on the screen

        public Dictionary<string,Entity> SceneEntities;  

        private SceneNode mCurrentObject = null;          // The newly created object

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
            SceneEntities = new Dictionary<string, Entity>();
            mCount = 0;
            SetupLights();
            SetSkyBox();
            SceneManager.SetWorldGeometry("terrain.cfg");
            mRaySceneQuery = SceneManager.CreateRayQuery(new Ray());

            LoadModel();
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

        private Vector2 GetNormalizedCoords(int X, int Y)
        {
            float normX = Math.Abs(X / (float) WindowParams.Width);
            float normY = Math.Abs(Y / (float) WindowParams.Height);
            return new Vector2(normX, normY);
        }

        public void SelectObject(int mouseX, int mouseY)
        {
            var coords = GetNormalizedCoords(mouseX, mouseY);

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
                if (SceneEntities.ContainsKey(name))
                {
                    SelectSceneNode(intersectedNode.ParentSceneNode);
                }
            }
        }

        private void SelectSceneNode(SceneNode node)
        {
            if (mCurrentObject != null)
            {
                mCurrentObject.ShowBoundingBox = false;
            }
            mCurrentObject = node;
            mCurrentObject.ShowBoundingBox = true;
        }

        public void CreateCamera(int mouseX, int mouseY)
        {
            var coords = GetNormalizedCoords(mouseX, mouseY);
            Ray mouseRay = Camera.GetCameraToViewportRay(coords.x, coords.y);
            mRaySceneQuery.Ray = mouseRay;

            RaySceneQueryResult result = mRaySceneQuery.Execute();
            RaySceneQueryResult.Enumerator itr = (RaySceneQueryResult.Enumerator)(result.GetEnumerator());

            if (itr.MoveNext())
            {
                var cameraEntity = SceneManager.CreateEntity("Camera" + mCount, "cctvCamera.mesh");
                SceneEntities.Add(cameraEntity.Name,cameraEntity);
                var node = SceneManager.RootSceneNode.CreateChildSceneNode("CameraNode" + mCount);
                node.AttachObject(cameraEntity);

                if (itr.Current.movable == null)
                {
                    node.Position = itr.Current.worldFragment.singleIntersection;
                }
                else
                {
                    node.Position = itr.Current.movable.ParentSceneNode.Position;
                }

                SelectSceneNode(node);
                mCount++;
            }
        }

        protected override void UpdateScene(FrameEvent evt)
        {
            Vector3 camPos = Camera.Position;
            Ray cameraRay = new Ray(new Vector3(camPos.x, 5000.0f, camPos.z),Vector3.NEGATIVE_UNIT_Y);
            mRaySceneQuery.Ray = cameraRay;

            // Perform the scene query;
            RaySceneQueryResult result = mRaySceneQuery.Execute();
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
            mRaySceneQuery.Dispose();
        }
    }
}
