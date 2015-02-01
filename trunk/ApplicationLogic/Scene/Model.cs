using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mogre;
using RenderingEngine.Helpers;

namespace ApplicationLogic.Scene
{
    public class Model
    {
        private readonly RenderingEngine.Engine.Engine mEngine = RenderingEngine.Engine.Engine.Instance;

        private bool mSelected;

        public string Name { get; private set; }
        public string FilePath { get; private set; }
        public RenderingEngine.Scene.Model RenderModel { get; private set; }
        public Dictionary<string, SecurityCamera> SecurityCameras { get; private set; }
        public SecurityCamera SelectedSecurityCamera { get; private set; }

        public bool Selected
        {
            get { return mSelected; }
            set
            {
                if (value)
                {
                    RenderModel.ShowBoundingBox();
                }
                else
                {
                    RenderModel.HideBoundingBox();
                }
                mSelected = true;
            }

        }

        public Model(string name, string filePath)
        {
            Name = name;
            FilePath = filePath;
            SecurityCameras = new Dictionary<string, SecurityCamera>();
            SelectedSecurityCamera = null;

            RenderModel = new RenderingEngine.Scene.Model(name,filePath);
        }

        public void Translate(Vector3 t)
        {
           RenderModel.Translate(new Vector3(t.x,1,t.z));
        }

        public void SelectSecurityCamera(string name)
        {
            if (SecurityCameras.ContainsKey(name))
            {
                DeselectAllSecurityCameras();
                SelectedSecurityCamera = SecurityCameras[name];
                SelectedSecurityCamera.Selected = true;
            }
        }

        public void DeselectAllSecurityCameras()
        {
            foreach (var camera in SecurityCameras)
            {
                camera.Value.Selected = false;
                Selected = true;
            }
            SelectedSecurityCamera = null;
        }

        public string CreateCamera(int screenX, int screenY)
        {
            try
            {
                DeselectAllSecurityCameras();

                var normCoords = mEngine.GetNormalizedCoords(screenX, screenY);
                Ray mouseRay = mEngine.MainCamera.GetCameraToViewportRay(normCoords.x, normCoords.y);

                Vector3 startPosition = mouseRay.Origin;
                Vector3 rayDirection = mouseRay.Direction;
                Vector3 contactPoint = new Vector3(), normal = new Vector3();
                var rayCast = new PolygonRayCast();
                var isHit = rayCast.RaycastFromPoint(startPosition, rayDirection, ref contactPoint, ref normal);
                //mesh normal is directed into polygon
                normal *= -1;

                if (isHit)
                {
                    
                    var securityCamera = new SecurityCamera(SecurityCameras.Count,contactPoint, normal);

                    SecurityCameras.Add(securityCamera.Name, securityCamera);

                    securityCamera.Selected = true;
                    SelectedSecurityCamera = securityCamera;
                    
                    return securityCamera.Name;
                }
                return "";
            }            
            catch (System.Runtime.InteropServices.SEHException e)
            {
                if(OgreException.IsThrown)
                    LogManager.Singleton.LogMessage(OgreException.LastException.ToString());
                return "";
            }
            catch (Exception e)
            {
                LogManager.Singleton.LogMessage(e.ToString());
                return "";
            }
    
        }

        public void DeleteSelectedCamera()
        {
            if (SelectedSecurityCamera != null)
            {
                var toDeleteKey = SelectedSecurityCamera.Name;
                if (SecurityCameras.ContainsKey(toDeleteKey))
                {
                    SecurityCameras[toDeleteKey].Delete();
                    SecurityCameras.Remove(toDeleteKey);
                    SelectedSecurityCamera = null;
                }
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
