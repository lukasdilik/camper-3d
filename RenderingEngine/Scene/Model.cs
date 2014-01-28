using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mogre;
using RenderingEngine.Helpers;

namespace RenderingEngine.Scene
{
    public class Model
    {
        public Dictionary<string, SecurityCamera> SecurityCameras { get; private set; }
        public SecurityCamera SelectedSecurityCamera { get; private set; }

        public string Name { get; private set; }
        public Entity Entity { get; private set; }
        public SceneNode SceneNode { get; private set; }

        private readonly Engine.Engine mEngine = Engine.Engine.Instance;
        private readonly PolygonRayCast mRayCast;
        private bool mSelected;

        public bool Selected
        {
            get { return mSelected; }
            set
            {
                if (SceneNode != null) { SceneNode.ShowBoundingBox = value; }
                mSelected = value;
            }

        }

        public Model(string name, string filePath)
        {
            mRayCast = new PolygonRayCast();

            int index = mEngine.Models.Count;
            Name = name+index;
            SecurityCameras = new Dictionary<string, SecurityCamera>();
            SelectedSecurityCamera = null;

            Entity = mEngine.SceneManager.CreateEntity(Name, filePath);
            SceneNode = mEngine.SceneManager.RootSceneNode.CreateChildSceneNode(Name + "Node");
            SceneNode.AttachObject(Entity);

            SceneNode.Translate(750, 1, 750);
        }

        public void SelectSecurityCamera(int screenX, int screenY)
        {
            DeselectAllSecurityCameras();
            var coords = Engine.Engine.Instance.GetNormalizedCoords(screenX, screenY);

            Ray mouseRay = mEngine.Camera.GetCameraToViewportRay(coords.x, coords.y);
            RaySceneQuery query = mEngine.SceneManager.CreateRayQuery(mouseRay);
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

        public void DeselectAllSecurityCameras()
        {
            foreach (var camera in SecurityCameras)
            {
                camera.Value.Selected = false;
            }
            SelectedSecurityCamera = null;
        }

        public void CreateCamera(int screenX, int screenY)
        {
            try
            {
                DeselectAllSecurityCameras();

                var normCoords = mEngine.GetNormalizedCoords(screenX, screenY);
                Ray mouseRay = mEngine.Camera.GetCameraToViewportRay(normCoords.x, normCoords.y);

                Vector3 startPosition = mouseRay.Origin;
                Vector3 rayDirection = mouseRay.Direction;
                Vector3 contactPoint = new Vector3(), normal = new Vector3();

                var isHit = mRayCast.RaycastFromPoint(startPosition, rayDirection, ref contactPoint, ref normal);
                //mesh normal is directed into polygon
                normal *= -1;

                if (isHit)
                {
                    var securityCamera = new SecurityCamera(contactPoint, normal, this);

                    SecurityCameras.Add(securityCamera.Name, securityCamera);
                    SelectedSecurityCamera = securityCamera;
                }
            }            
            catch (System.Runtime.InteropServices.SEHException e)
            {
                if(OgreException.IsThrown)
                    LogManager.Singleton.LogMessage(OgreException.LastException.ToString());
            }
            catch (Exception e)
            {
                LogManager.Singleton.LogMessage(e.ToString());
            }
    
        }

        public void DeleteSelectedCamera()
        {
            if (SelectedSecurityCamera != null)
            {
                SelectedSecurityCamera.SceneNode.DetachAllObjects();
                SecurityCameras.Remove(SelectedSecurityCamera.Name);
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
    }
}
