using Mogre;
using Math = System.Math;

namespace RenderingEngine.Scene
{
    public class Camera
    {
        private readonly Vector3 mDirection;
        public static readonly Vector3 DefaultScaleVector = new Vector3(8, 8, 8);
        private const float TranslationRate = 0.1f;
        public string Name { get; private set; }
        public Entity Mesh { get; private set; }
        public SceneNode SceneNode { get; private set; }
        public Mogre.Camera MogreCamera { get; private set; }
        public CameraFrustum Frustum { get; private set; }
        public Line NormalLine { get; private set; }

        public Camera(string name, Vector3 position, Vector3 direction, string meshName)
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

            CreateMogreCameraObject();
            
            Frustum = new CameraFrustum(this);

            NormalLine = new Line(name + "_line",new Vector3(), Frustum.FarCenter, SceneNode);
        }

        private void CreateMogreCameraObject()
        {
            MogreCamera = Engine.Engine.Instance.SceneManager.CreateCamera(Name+"_camera");
            MogreCamera.Position = SceneNode.Position;
            MogreCamera.LookAt(SceneNode.Position + mDirection*100);

            MogreCamera.NearClipDistance = 8;
        }

        public void UpdateProperties(Vector3 position, Vector3 direction, Degree FOVy, float aspectRatio)
        {
            direction.Normalise();
            
            MogreCamera.Position = position;
            MogreCamera.Direction = direction;
            MogreCamera.LookAt(position + direction * 100);
            
            RotateToDirection(position + direction * 10);
            
            SceneNode.Position = position;
            
            if (Math.Abs(MogreCamera.AspectRatio - aspectRatio) > 0.001 || MogreCamera.FOVy != FOVy.ValueRadians)
            {
                MogreCamera.AspectRatio = aspectRatio;
                MogreCamera.FOVy = FOVy.ValueRadians;
                Frustum.RecalculatePoints();
            }

            NormalLine.Destroy();
            NormalLine = new Line(Name + "_line", new Vector3(), Frustum.FarCenter, SceneNode);
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

        public void MoveRight()
        {
            var dir = Engine.Engine.Instance.MainCamera.Direction;
            if (dir.z != 0)
            {
                Translate(dir.z < 0 ? new Vector3(TranslationRate, 0, 0) : new Vector3(-TranslationRate, 0, 0));
            }
            else if (dir.x != 0)
            {
                Translate(dir.z < 0 ? new Vector3(0, TranslationRate, 0) : new Vector3(0, TranslationRate,0 ));
            }
            
        }

        public void MoveLeft()
        {
            var dir = Engine.Engine.Instance.MainCamera.Direction;
            if (dir.z != 0)
            {
                Translate(dir.z > 0 ? new Vector3(TranslationRate, 0, 0) : new Vector3(-TranslationRate, 0, 0));
            }
            else if (dir.x != 0)
            {
                Translate(dir.z > 0 ? new Vector3(0, 0, TranslationRate) : new Vector3(0,0, TranslationRate));
            }
        }

        public void MoveTop()
        {
            Translate(new Vector3(0, TranslationRate, 0));
        }

        public void MoveDown()
        {
            Translate(new Vector3(0, -TranslationRate, 0));
        }

        public void MoveForward()
        {
            Translate(new Vector3(0, 0 ,-TranslationRate));
        }

        public void MoveBackward()
        {
            Translate(new Vector3(0, 0, TranslationRate));
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
        }

        public AxisAlignedBox GetBoundingBox()
        {
            return Mesh.BoundingBox;
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
            Frustum.Destroy();
            NormalLine.Destroy();
            SceneNode.ShowBoundingBox = false;
            SceneNode.RemoveAndDestroyAllChildren();
            Engine.Engine.Instance.SceneManager.DestroyEntity(Mesh);
            Engine.Engine.Instance.SceneManager.DestroySceneNode(SceneNode);
        }

        public void Pitch(Radian angleInRad)
        {
            MogreCamera.Pitch(-angleInRad);
            SceneNode.Pitch(angleInRad);

        }
        public void Yaw(Radian angleInRad)
        {
            MogreCamera.Yaw(-angleInRad);
            SceneNode.Yaw(-angleInRad);
        }
    }
}
