using Mogre;
using RenderingEngine.Drawing;

namespace RenderingEngine.Scene
{
    public class Camera
    {

        private readonly Vector3 mDirection;
        private SceneNode mNormalNode;

        public string Name { get; private set; }
        public Entity Mesh { get; private set; }
        public SceneNode SceneNode { get; private set; }
        public Mogre.Camera MogreCamera { get; private set; }
        public CameraFrustum Frustum { get; private set; }


        public Camera(string name, Vector3 position, Vector3 direction, string meshName)
        {
            Name = name;
            mDirection = direction;
            Mesh = Engine.Engine.Instance.SceneManager.CreateEntity(name, meshName);
            SceneNode = Engine.Engine.Instance.SceneManager.RootSceneNode.CreateChildSceneNode(name + "_node");
            SceneNode.AttachObject(Mesh);
            SceneNode.Position = position;

            CreateMogreCameraObject();

            Frustum = new CameraFrustum(this);
        }

        private void CreateMogreCameraObject()
        {
            MogreCamera = Engine.Engine.Instance.SceneManager.CreateCamera(Name+"_camera");
            MogreCamera.Position = SceneNode.Position;
            MogreCamera.LookAt(mDirection);
        }

        public void SetClipDistance(int near, int far)
        {
            MogreCamera.NearClipDistance = near;
            MogreCamera.FarClipDistance = far;
        }

        public void DrawNormal(Vector3 p0, Vector3 p1, Vector3 color)
        {
            Draw.Instance.Color = color;
            mNormalNode = Draw.Instance.DrawLine(p0, p1);
        }

        public void ShowBoundingBox()
        {
            SceneNode.ShowBoundingBox = true;
        }

        public void HideBoundingBox()
        {
            SceneNode.ShowBoundingBox = false;
        }

        public void Translate(Vector3 t)
        {
            if (Engine.Engine.Instance.MainCamera != null)
            {
                t = t*Engine.Engine.Instance.MainCamera.Direction;
            }

            SceneNode.Translate(t);
            if (mNormalNode != null)
            {
                mNormalNode.Translate(t);
            }

            MogreCamera.Position = SceneNode.Position;
            Frustum.Translate(t);
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
            Frustum.Rotate(quat);
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
