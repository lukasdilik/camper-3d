using Mogre;
using Math = System.Math;

namespace RenderingEngine.Scene
{
    public class Camera
    {
        private Vector3 mDirection;
        public static readonly Vector3 DefaultScaleVector = new Vector3(4, 4, 4);
        private const float TranslationRate = 0.1f;
        public string Name { get; private set; }
        public Entity Mesh { get; private set; }
        public SceneNode SceneNode { get; private set; }
        public Mogre.Camera MogreCamera { get; private set; }
        public Light SpotLight { get; private set; }
        public CameraFrustum Frustum { get; private set; }
        public Line NormalLine { get; private set; }

        public Degree Rotation { get; set; }

        public Camera(string name, Vector3 position, Vector3 direction, Degree foVy, float aspectRatio,string meshName)
        {
            if (SceneNode != null)
            {
                Engine.Engine.Instance.SceneManager.RootSceneNode.RemoveChild(name + "_node");
            }
            Name = name;
            direction.Normalise();
            mDirection = direction;
           
            Mesh = Engine.Engine.Instance.SceneManager.CreateEntity(name, meshName);
            SceneNode = Engine.Engine.Instance.SceneManager.RootSceneNode.CreateChildSceneNode(name + "_node");
            SceneNode.AttachObject(Mesh);
            SceneNode.Position = position;

            Scale(DefaultScaleVector);

            TranslateCameraOnPolygonFace();
            
            RotateToDirection(position + direction * 10);

            CreateMogreCameraObject(foVy,aspectRatio);
            
            Frustum = new CameraFrustum(this);

            NormalLine = new Line(name + "_line",new Vector3(), Frustum.FarCenter, SceneNode);

            SpotLight = LightManager.Instance.CreateSpotLight(Name + "_light", SceneNode.Position, mDirection, Frustum.Color,
            MogreCamera.FOVy*aspectRatio, MogreCamera.FOVy*aspectRatio);

            UpdateSpotLight();
        }

        private void UpdateSpotLight()
        {
            SpotLight.Position = MogreCamera.Position;
            SpotLight.Direction = MogreCamera.Direction;
        }

        private void CreateMogreCameraObject(Degree foVy, float aspectRatio)
        {
            MogreCamera = Engine.Engine.Instance.SceneManager.CreateCamera(Name+"_camera");
            MogreCamera.FOVy = foVy;
            MogreCamera.AspectRatio = aspectRatio;
            MogreCamera.Position = GetCameraCenterWorld();
            MogreCamera.LookAt(SceneNode.Position + mDirection*100);
            MogreCamera.NearClipDistance = 4;
        }

        public void UpdateProperties(Vector3 position, Vector3 direction, Degree foVy, float aspectRatio, Degree rotation)
        {
            mDirection = direction;
            mDirection.Normalise();
            
            MogreCamera.Position = position;
            MogreCamera.Direction = mDirection;
            MogreCamera.LookAt(position + direction * 100);
            
            RotateToDirection(position + direction * 10);
            
            SceneNode.Position = position;

            if (Math.Abs(MogreCamera.AspectRatio - aspectRatio) > 0.001 || MogreCamera.FOVy != foVy.ValueRadians)
            {
                MogreCamera.AspectRatio = aspectRatio;
                MogreCamera.FOVy = foVy.ValueRadians;
                SpotLight.SpotlightInnerAngle = foVy.ValueRadians;
                SpotLight.SpotlightOuterAngle = foVy.ValueRadians;
            }
            Rotation = rotation;

            Frustum.RecalculatePoints();

            NormalLine.Destroy();
            NormalLine = new Line(Name + "_line", new Vector3(), Frustum.FarCenter, SceneNode);

            UpdateSpotLight();
        }

        public void HideFrustum()
        {
            if (Engine.Engine.Instance.ShutDown) return;

            Frustum.FrustumSceneNode.SetVisible(false);
            NormalLine.SceneNode.SetVisible(false);
        }

        public void ShowFrustum()
        {
            if (Engine.Engine.Instance.ShutDown) return;

            Frustum.FrustumSceneNode.SetVisible(true);
            NormalLine.SceneNode.SetVisible(true);
        }

        public void ShowBoundingBox()
        {
            SceneNode.ShowBoundingBox = true;
        }

        public void HideBoundingBox()
        {
            SceneNode.ShowBoundingBox = false;
        }

        public void UpdateCameraFrustum()
        {
            Frustum.RecalculatePoints();
        }

        public void MoveForward()
        {
            var t = NormaliseCameraDirection();
            t = RotateVector(t, 0);
            Translate(t);
        }

        public void MoveBackward()
        {
            var t = NormaliseCameraDirection();
            t = RotateVector(t, 180);
            Translate(t);
        }

        public void MoveRight()
        {
            var t = NormaliseCameraDirection();
            t = RotateVector(t, 270);
            Translate(t);
        }

        public void MoveLeft()
        {
            var t = NormaliseCameraDirection();
            t = RotateVector(t, 90);
            Translate(t);
        }

        private Vector3 RotateVector(Vector3 v, float degree)
        {
            return new Quaternion(new Degree(degree), Vector3.UNIT_Y) * v;
        }

        private static Vector3 NormaliseCameraDirection()
        {
            var camDir = Engine.Engine.Instance.MainCamera.Direction;
            camDir.y = 0;
            camDir.Normalise();
            camDir *= TranslationRate;
            return camDir;
        }

        public void MoveTop()
        {
            Translate(new Vector3(0, TranslationRate, 0));
        }

        public void MoveDown()
        {
            Translate(new Vector3(0, -TranslationRate, 0));
        }

        private void TranslateCameraOnPolygonFace()
        {
            var aabb = GetBoundingBox();
            aabb.Scale(DefaultScaleVector);
            var center = aabb.Center;

            var dist = 2 * new Vector3(center.z, center.z, center.z);

            var mainCameradir = Engine.Engine.Instance.GetMainCameraDirection();

            var t = dist * mainCameradir;
            t = (mainCameradir.z < 0) ? -t : t;
            t = (mainCameradir.y > 0) ? -t : t;
            t = (mainCameradir.x < 0) ? -t : t;

            if (Engine.Engine.Instance.MainCamera != null)
            {
                t = t * Engine.Engine.Instance.MainCamera.Direction;
            }

            SceneNode.Translate(t);
        }

        public void Translate(Vector3 t)
        {
            SceneNode.Translate(t, Node.TransformSpace.TS_WORLD);
            MogreCamera.Position = SceneNode.Position;
            UpdateSpotLight();
        }

        public AxisAlignedBox GetBoundingBox()
        {
            return Mesh.BoundingBox;
        }

        public Vector3 GetCameraCenterWorld()
        {
            return SceneNode.ConvertLocalToWorldPosition(GetBoundingBox().Center);
        }

        public void Scale(Vector3 scaleVector)
        {
            SceneNode.Scale(scaleVector);
            var aabb = GetBoundingBox();
            aabb.Scale(scaleVector);
        }

        private void RotateToDirection(Vector3 destination)
        {
            Vector3 direction = destination - SceneNode.Position; // B-A = A->B (see vector questions above)
            Vector3 src = SceneNode.Position * Vector3.UNIT_Z; //facing direction of this mesh is +Z
            direction.Normalise();
            Quaternion quat = src.GetRotationTo(direction); // Get a quaternion rotation operation 

            SceneNode.Rotate(quat);
        }

        public void Delete()
        {
            Engine.Engine.Instance.SceneManager.DestroyCamera(MogreCamera);
            Engine.Engine.Instance.SceneManager.DestroyLight(SpotLight);
            Frustum.Destroy();
            NormalLine.Destroy();
            SceneNode.ShowBoundingBox = false;
            SceneNode.RemoveAndDestroyAllChildren();
            Engine.Engine.Instance.SceneManager.DestroyEntity(Mesh);
            Engine.Engine.Instance.SceneManager.DestroySceneNode(SceneNode);
        }

        public void SetNewDirection(Vector3 dir)
        {
            MogreCamera.Direction = dir;
            SceneNode.Position = SceneNode.ConvertLocalToWorldPosition(SceneNode.InitialPosition);
            SceneNode.Orientation = SceneNode.InitialOrientation;
            UpdateSpotLight();
        }

        public void Pitch(Radian angleInRad)
        {
            MogreCamera.Pitch(angleInRad);
            SceneNode.Pitch(-angleInRad);
            UpdateSpotLight();
        }
        public void Yaw(Radian angleInRad)
        {
            MogreCamera.Yaw(-angleInRad);
            SceneNode.Yaw(-angleInRad);
            UpdateSpotLight();
        }
    }
}
