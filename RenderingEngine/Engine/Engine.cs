using System;
using System.Collections.Generic;
using Mogre;
using RenderingEngine.Interfaces;
using Math = System.Math;

namespace RenderingEngine.Engine
{
    public class Engine : BaseEngine
    {
        public const String WindowName = "MOGRE Window";
        public const float MaxHeight = 10f;

        private static Engine mInstance;

        private bool mIsClampedToTerrain;
        public RaySceneQuery RaySceneQuery { get; private set; }
    
        
        public void ChangeTerrainClamping()
        {
            mIsClampedToTerrain = !mIsClampedToTerrain;
        }

        public static Engine Instance
        {
            get { return mInstance ?? (mInstance = new Engine()); }
        }

        public void SetApplicationInstance(IApplication application)
        {
            ApplicationLogic = application;
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
            SetupLights();
            
            SetSkyBox();
            
            SceneManager.SetWorldGeometry("./Resources/terrain.cfg");

            RaySceneQuery = SceneManager.CreateRayQuery(new Ray());
        }

        private void SetSkyBox()
        {
            SceneManager.SetSkyBox(true, "Examples/CloudyNoonSkyBox");
        }

        private void SetupLights()
        {
            LightManager.Instance.CreateWorldLight();
        }

        #region Model controls



        public bool IsIntersectionWithTerrain(int screenX, int screenY, out Vector3 outIntersection)
        {
            var coords = GetNormalizedCoords(screenX, screenY);

            var results = GetRaySceneQueryResult(coords);
            var intersection = new Vector3();
            bool isIntersection = false;
            foreach (RaySceneQueryResultEntry entry in results)
            {
               if(entry == null || entry.worldFragment == null) continue;
               intersection = entry.worldFragment.singleIntersection;
               isIntersection = true;
            }
            outIntersection = intersection;
            return isIntersection;
        }

        public MovableObject SelectObject(int screenX, int screenY)
        {
            var coords = GetNormalizedCoords(screenX, screenY);

            var results = GetRaySceneQueryResult(coords);

            MovableObject intersectedNode = null;
            foreach (RaySceneQueryResultEntry entry in results)
            {
                intersectedNode = entry.movable;
            }

            return intersectedNode;

        }

        private IEnumerable<RaySceneQueryResultEntry> GetRaySceneQueryResult(Vector2 coords)
        {
            Ray mouseRay = MainCamera.GetCameraToViewportRay(coords.x, coords.y);
            RaySceneQuery query = SceneManager.CreateRayQuery(mouseRay);
            RaySceneQueryResult results = query.Execute();
            return results;
        }

        #endregion

        #region Helpers

        public Vector2 GetNormalizedCoords(int X, int Y)
        {
            float normX = Math.Abs(X / (float) WindowParams.Width);
            float normY = Math.Abs(Y / (float) WindowParams.Height);
            return new Vector2(normX, normY);
        }



        public Vector3 GetMainCameraDirection()
        {
            Vector3 res = new Vector3(MainCamera.Direction.x,MainCamera.Direction.y,MainCamera.Direction.z);
            res.x = (float) Math.Round(res.x);
            res.y = (float) Math.Round(res.y);
            res.z = (float) Math.Round(res.z);
            return res;
        }

        public Vector3 GetMainCameraPosition()
        {
            Vector3 res = new Vector3(MainCamera.Position.x, MainCamera.Position.y, MainCamera.Position.z);
            res.x = (float)Math.Round(res.x);
            res.y = (float)Math.Round(res.y);
            res.z = (float)Math.Round(res.z);
            return res;
        }

        #endregion

        public void SetCameraViewport(Camera camera)
        {
            Viewport.Camera = camera;
        }

        public void ResetViewportToMainCamera()
        {
            SetCameraViewport(MainCamera);
        }

        protected override void UpdateScene(FrameEvent evt)
        {
            try
            {

                var camPos = MainCamera.Position;
                var cameraRay = new Ray(new Vector3(camPos.x, 5000.0f, camPos.z), Vector3.NEGATIVE_UNIT_Y);
                RaySceneQuery.Ray = cameraRay;

                // Perform the scene query;
                var result = RaySceneQuery.Execute();
                var itr = (RaySceneQueryResult.Enumerator)(result.GetEnumerator());

                // Get the results, set the camera height
                if (!itr.MoveNext()) return;

                var terrainHeight = itr.Current.worldFragment.singleIntersection.y;

                if (mIsClampedToTerrain)
                {
                    MainCamera.SetPosition(camPos.x, terrainHeight + 10.0f, camPos.z);
                }
                else
                {
                    if ((terrainHeight + MaxHeight) > camPos.y)
                        MainCamera.SetPosition(camPos.x, terrainHeight + 10.0f, camPos.z);
                }
            }
            catch (Exception e)
            {
            }
        }

         protected override void DestroyScene()
        {
            base.DestroyScene();
            RaySceneQuery.Dispose();
        }

        public void SetUpRenderingWindow(IntPtr handle, int width, int height)
        {
            mInstance.SetUpRenderingWindow(handle, width, height);
        }
    }
}
