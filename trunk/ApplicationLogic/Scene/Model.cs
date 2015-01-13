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
        public string SelectedSecurityCameraIndex { get; private set; }

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
            SelectedSecurityCameraIndex = null;

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
                SecurityCameras[name].Selected = true;
                SelectedSecurityCameraIndex = name;
            }
        }

        public void DeselectAllSecurityCameras()
        {
            foreach (var camera in SecurityCameras)
            {
                camera.Value.Selected = false;
                Selected = true;
            }
            SelectedSecurityCameraIndex = null;
        }

        public void CreateCamera(int screenX, int screenY)
        {
            try
            {
                DeselectAllSecurityCameras();

                var normCoords = mEngine.GetNormalizedCoords(screenX, screenY);
                Ray mouseRay = mEngine.MainCamera.GetCameraToViewportRay(normCoords.x, normCoords.y);

                Vector3 startPosition = mouseRay.Origin;
                Vector3 rayDirection = mouseRay.Direction;
                Vector3 contactPoint = new Vector3(), normal = new Vector3();
                PolygonRayCast rayCast = new PolygonRayCast();
                var isHit = rayCast.RaycastFromPoint(startPosition, rayDirection, ref contactPoint, ref normal);
                //mesh normal is directed into polygon
                normal *= -1;

                if (isHit)
                {
                    string cameraName = "SecurityCamera" + SecurityCameras.Count;
                    var securityCamera = new SecurityCamera(cameraName, contactPoint, normal);

                    SecurityCameras.Add(securityCamera.Name, securityCamera);
                    SelectedSecurityCameraIndex = cameraName;
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

        public SecurityCamera GetSelectedSecurityCamera()
        {
            if (SelectedSecurityCameraIndex != null && SecurityCameras.ContainsKey(SelectedSecurityCameraIndex))
            {
                return SecurityCameras[SelectedSecurityCameraIndex];
            }
            return null;
        }

        public void DeleteSelectedCamera()
        {
            if (SelectedSecurityCameraIndex != null)
            {
                var toDeleteKey = SelectedSecurityCameraIndex;
                if (SecurityCameras.ContainsKey(toDeleteKey))
                {
                    SecurityCameras[toDeleteKey].Delete();
                    SecurityCameras.Remove(toDeleteKey);
                    SelectedSecurityCameraIndex = null;
                }
            }

        }

        public void CameraControl(Keys key)
        {
            if (SelectedSecurityCameraIndex != null)
            {
                if (SecurityCameras.ContainsKey(SelectedSecurityCameraIndex))
                {
                    SecurityCameras[SelectedSecurityCameraIndex].HandleKey(key);
                }
            }
        }

        public bool IsSecurityCameraSelected()
        {
            return SelectedSecurityCameraIndex != null;
        }

        public void CameraMouseClick(MouseEventArgs e)
        {
            if (SelectedSecurityCameraIndex != null)
            {
                if (SecurityCameras.ContainsKey(SelectedSecurityCameraIndex))
                {
                    SecurityCameras[SelectedSecurityCameraIndex].MouseClick(e);    
                }
                
            }
        }

        public void CameraMouseMove(MouseEventArgs e)
        {
            if (SelectedSecurityCameraIndex != null)
            {
                if (SecurityCameras.ContainsKey(SelectedSecurityCameraIndex))
                {
                    SecurityCameras[SelectedSecurityCameraIndex].MouseMove(e);
                }

            }
        }
    }
}
