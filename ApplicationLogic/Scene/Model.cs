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
        private readonly Engine mEngine = Engine.Instance;
        
        private const float MoveStep = 5f;

        private bool mSelected;

        public ModelProperties ModelProperties; 
        public RenderingEngine.Scene.RenderModel RenderModel { get; private set; }
        public Dictionary<string, SecurityCamera> SecurityCameras { get; private set; }
        public SecurityCamera SelectedSecurityCamera { get; private set; }

        public Dictionary<string, Light> Lights { get; private set; }
        public Light SelectedLight { get; private set; }

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

        public Model(string name, string meshName)
        {
            ModelProperties = new ModelProperties {Name = name, MeshName = meshName};

            SecurityCameras = new Dictionary<string, SecurityCamera>();
            Lights = new Dictionary<string, Light>();
            SelectedSecurityCamera = null;
            SelectedLight = null;

            RenderModel = new RenderingEngine.Scene.RenderModel(name,meshName);
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

        public Matrix4 GetTransformationMatrix()
        {
            return RenderModel.SceneNode._getFullTransform();
        }

        public void SetTransformationMatrix(Matrix4 m)
        {
            var quat = m.ExtractQuaternion();
            var trans = m.GetTrans();
            RenderModel.SceneNode.Scale(m.m00,m.m11,m.m22);
            RenderModel.SceneNode.SetOrientation(quat.w, quat.x, quat.y, quat.z);
            Translate(trans);
            
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
                SelectedSecurityCamera = SecurityCameras[name];
                SelectedSecurityCamera.Selected = true;
            }
        }

        public void SelectLight(string name)
        {
            if (Lights.ContainsKey(name))
            {
                SelectedLight = Lights[name];
                SelectedLight.Selected = true;
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
            }
            SelectedSecurityCamera = null;
        }

        public void DeselectAllLights()
        {
            foreach (var light in Lights)
            {
                light.Value.Selected = false;
            }
            SelectedLight = null;
        }

        public Boolean CreateCamera(int index, int screenX, int screenY)
        {
            try
            {
                DeselectAllSecurityCameras();

                Vector3 normal;
                bool isHit;
                var contactPoint = CalculateIntersection(screenX, screenY, out normal, out isHit);

                if (isHit)
                {
                    var internalName = "SecurityCamera" + index;
                    var securityCamera = new SecurityCamera(internalName,contactPoint, normal);
                    SecurityCameras.Add(internalName, securityCamera);

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

        public SecurityCamera AddCamera(SecurityCameraProperties properties)
        {
            var securityCamera = new SecurityCamera(properties.Name,properties.Position, properties.Direction);
            SecurityCameras.Add(properties.Name, securityCamera);
            securityCamera.Selected = true;
            SelectedSecurityCamera = securityCamera;
            UpdateSelectedCameraProperties(properties);
            return securityCamera;
        }

        private Vector3 CalculateIntersection(int screenX, int screenY, out Vector3 normal, out bool isHit)
        {
            var normCoords = mEngine.GetNormalizedCoords(screenX, screenY);
            Ray mouseRay = mEngine.MainCamera.GetCameraToViewportRay(normCoords.x, normCoords.y);

            Vector3 startPosition = mouseRay.Origin;
            Vector3 rayDirection = mouseRay.Direction;
            normal = new Vector3();
            Vector3 contactPoint = new Vector3();
            var rayCast = new PolygonRayCast();
            isHit = rayCast.RaycastFromPoint(startPosition, rayDirection, ref contactPoint, ref normal);
            //mesh normal is directed into polygon
            normal *= -1;
            return contactPoint;
        }

        public Boolean CreateLight(int index, int screenX, int screenY, LightProperties.LightType ligthType)
        {
            try
            {
                DeselectAllLights();
              
                Vector3 normal;
                bool isHit;
                var contactPoint = CalculateIntersection(screenX, screenY, out normal, out isHit);

                if (isHit)
                {
                    var ligthProperties = new LightProperties {Position = contactPoint};

                    Light light = null;
                    if (ligthType == LightProperties.LightType.Spot)
                    {
                        ligthProperties.Type = LightProperties.LightType.Spot;
                        ligthProperties.Name = "SpotLight" + index;
                        ligthProperties.Direction = normal;
                        light = new SpotLight(RenderModel.SceneNode, ligthProperties);
                    }
                    else
                    {
                        ligthProperties.Type = LightProperties.LightType.Point;
                        ligthProperties.Name = "PointLight" + index;
                        light = new PointLight(RenderModel.SceneNode, ligthProperties);
                    }
                    Lights.Add(light.Properties.Name, light);

                    light.Selected = true;
                    SelectedLight = light;

                    return true;
                }
            }
            catch (System.Runtime.InteropServices.SEHException e)
            {
                if (OgreException.IsThrown)
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

        public Light AddLight(LightProperties properties)
        {
            var ligthProperties = new LightProperties { Position = properties.Position };

            Light light = null;
            if (properties.Type == LightProperties.LightType.Spot)
            {
                ligthProperties.Type = LightProperties.LightType.Spot;
                ligthProperties.Name = properties.Name;
                ligthProperties.Direction = properties.Direction;
                light = new SpotLight(RenderModel.SceneNode, ligthProperties);
            }
            else
            {
                ligthProperties.Type = LightProperties.LightType.Point;
                ligthProperties.Name = properties.Name;
                light = new PointLight(RenderModel.SceneNode, ligthProperties);
            }
            Lights.Add(light.Properties.Name, light);

            light.Selected = true;
            SelectedLight = light;

            return light;
        }

        public void UpdateSelectedCameraProperties(SecurityCameraProperties properties)
        {
            if (IsSecurityCameraSelected())
            {
                SelectedSecurityCamera.UpdateCameraProperties(properties);
            }
        }

        public void UpdateSelectedLightProperties(LightProperties properties)
        {
            if (IsLightSelected())
            {
                SelectedLight.UpdateLightProperties(properties);
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
        public void DeleteSelectedLight()
        {
            {
                var toDeleteKey = SelectedLight.Properties.Name;
                if (Lights.ContainsKey(toDeleteKey))
                {
                    Lights[toDeleteKey].Delete();
                    Lights.Remove(toDeleteKey);
                    SelectedLight = null;
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

            foreach (var light in Lights)
            {
                light.Value.Delete();
            }
            Lights.Clear();

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

        public bool IsLightSelected()
        {
            return SelectedLight != null;
        }

        public void CameraMouseClick(MouseEventArgs e)
        {
            if (SelectedSecurityCamera != null)
            {
                SelectedSecurityCamera.MouseClick(e);
            }

        }

        public void LightMouseClick(MouseEventArgs e)
        {
            if (SelectedLight != null)
            {
                SelectedLight.MouseClick(e);
            }
        }

        public void CameraMouseMove(MouseEventArgs e)
        {
            if (SelectedSecurityCamera != null)
            {
                SelectedSecurityCamera.MouseMove(e);
            }
        }

        public void LightMouseMove(MouseEventArgs e)
        {
            if (SelectedLight != null)
            {
                SelectedLight.MouseMove(e);
            }
        }
    }
}
