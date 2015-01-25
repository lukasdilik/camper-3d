using Mogre;

namespace RenderingEngine.Scene
{
    public class Camera
    {
        private readonly Vector3 mDirection;
        private const float TranslationRate = 0.1f;
        public string Name { get; private set; }
        public Entity Mesh { get; private set; }
        public SceneNode SceneNode { get; private set; }
        public Mogre.Camera MogreCamera { get; private set; }
        public CameraFrustum Frustum { get; private set; }
        public NormalLine NormalLine { get; private set; }

        public Camera(string name, Vector3 position, Vector3 normal, string meshName)
        {
            Name = name;
            mDirection = normal;
           
            Mesh = Engine.Engine.Instance.SceneManager.CreateEntity(name, meshName);
            SceneNode = Engine.Engine.Instance.SceneManager.RootSceneNode.CreateChildSceneNode(name + "_node");
            SceneNode.AttachObject(Mesh);
            SceneNode.Position = position;

            CreateMogreCameraObject();
            
            NormalLine = new NormalLine(name+"_line",position,normal,SceneNode);
            
            Frustum = new CameraFrustum(this);
        }

        private void CreateMogreCameraObject()
        {
            MogreCamera = Engine.Engine.Instance.SceneManager.CreateCamera(Name+"_camera");
            MogreCamera.Position = SceneNode.Position;
            MogreCamera.LookAt(-mDirection);
            MogreCamera.NearClipDistance = 8;
        }

        public void ShowBoundingBox()
        {
            SceneNode.ShowBoundingBox = true;
        }

        public void HideBoundingBox()
        {
            SceneNode.ShowBoundingBox = false;
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

        public void Translate(Vector3 t)
        {
            var dir = Engine.Engine.Instance.MainCamera.Direction;
            dir = Engine.Engine.Instance.MainCamera.DerivedOrientation*dir;
            SceneNode.Translate(t, Node.TransformSpace.TS_WORLD);
            MogreCamera.Position = SceneNode.Position;
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

        public void RotateToDirection(Vector3 destination)
        {
            Vector3 direction = destination - SceneNode.Position; // B-A = A->B (see vector questions above)
            Vector3 src = SceneNode.Position * Vector3.UNIT_Z; //facing direction of this mesh is +Z
            direction.Normalise();
            Quaternion quat = src.GetRotationTo(direction); // Get a quaternion rotation operation 

            SceneNode.Rotate(quat);
        }

        public void Delete()
        {
            SceneNode.ShowBoundingBox = false;
            SceneNode.RemoveAndDestroyAllChildren();
        }

        public void Pitch(Radian angleInRad)
        {
            MogreCamera.Pitch(angleInRad);
            SceneNode.Pitch(angleInRad);

        }
        public void Yaw(Radian angleInRad)
        {
            MogreCamera.Yaw(angleInRad);
            SceneNode.Yaw(angleInRad);
        }
    }
}
