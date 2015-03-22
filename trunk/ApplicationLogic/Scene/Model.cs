using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mogre;
using RenderingEngine.Engine;
using RenderingEngine.Helpers;

namespace ApplicationLogic.Scene
{
    public class Model
    {
        private readonly Engine mEngine = RenderingEngine.Engine.Engine.Instance;
        
        private const float MoveStep = 5f;

        private bool mSelected;
        private int mCameraCounter;
        public ModelProperties ModelProperties; 
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

        public Model(string name, string modelName)
        {
            ModelProperties = new ModelProperties {Name = name, FileName = modelName};

            SecurityCameras = new Dictionary<string, SecurityCamera>();
            SelectedSecurityCamera = null;

            RenderModel = new RenderingEngine.Scene.Model(name,modelName);
        }

        public void Translate(Vector3 t)
        {
            RenderModel.Translate(t);
            foreach (var securityCamera in SecurityCameras)
            {
                securityCamera.Value.Camera.Translate(t);
            }
            ModelProperties.Position = RenderModel.SceneNode.Position;
        }

        public void RotateY(Degree deg)
        {
            RenderModel.SceneNode.Rotate(new Vector3(0,1,0),deg.ValueRadians);
        }


        public void Scale(float factor)
        {
            RenderModel.SceneNode.Scale(factor,factor,factor);
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

        public void MoveForward()
        {
            var oldPos = RenderModel.SceneNode.Position;
            var newPos = oldPos;
            oldPos.z += MoveStep;
            Translate(newPos- oldPos);
        }

        public void MoveBackward()
        {
            var oldPos = RenderModel.SceneNode.Position;
            var newPos = oldPos;
            oldPos.z -= MoveStep;
            Translate(newPos - oldPos);
        }

        public void MoveLeft()
        {
            var oldPos = RenderModel.SceneNode.Position;
            var newPos = oldPos;
            oldPos.x += MoveStep;
            Translate(newPos - oldPos);
        }

        public void MoveRight()
        {
            var oldPos = RenderModel.SceneNode.Position;
            var newPos = oldPos;
            oldPos.x -= MoveStep;
            Translate(newPos - oldPos);
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

        public Boolean CreateCamera(int screenX, int screenY)
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
                    var internalName = "SecurityCamera" + mCameraCounter;
                    var securityCamera = new SecurityCamera(internalName,contactPoint, normal);
                    mCameraCounter++;
                    SecurityCameras.Add(securityCamera.InternalName, securityCamera);

                    securityCamera.Selected = true;
                    SelectedSecurityCamera = securityCamera;
                    
                    return true;
                }
            }            
            catch (System.Runtime.InteropServices.SEHException e)
            {
                if(OgreException.IsThrown)
                    LogManager.Singleton.LogMessage(OgreException.LastException.ToString());
                return false;
            }
            catch (Exception e)
            {
                LogManager.Singleton.LogMessage(e.ToString());
                return false;
            }
            return false;
        }

        public void UpdateSelectedCameraProperties(SecurityCameraProperties properties)
        {
            if (IsSecurityCameraSelected())
            {
                SelectedSecurityCamera.UpdateCameraProperties(properties);
            }
        }

        public void DeleteSelectedCamera()
        {
            if (SelectedSecurityCamera != null)
            {
                var toDeleteKey = SelectedSecurityCamera.Properties.Name;
                if (SecurityCameras.ContainsKey(toDeleteKey))
                {
                    SecurityCameras[toDeleteKey].Delete();
                    SecurityCameras.Remove(toDeleteKey);
                    SelectedSecurityCamera = null;
                }
            }

        }

        public void Delete()
        {
            RenderModel.HideBoundingBox();
            foreach (var securityCamera in SecurityCameras)
            {
                securityCamera.Value.Delete();
            }
            SecurityCameras.Clear();
            RenderModel.SceneNode.RemoveAndDestroyAllChildren();
            Engine.Instance.SceneManager.DestroyEntity(RenderModel.Entity);
            Engine.Instance.SceneManager.DestroySceneNode(RenderModel.SceneNode);
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
