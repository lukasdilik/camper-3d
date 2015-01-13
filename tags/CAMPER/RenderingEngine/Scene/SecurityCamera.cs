using System.Collections.Generic;
using System.Windows.Forms;
using Mogre;
using RenderingEngine.Drawing;

namespace RenderingEngine.Scene
{
    public class SecurityCamera
    {
        public const float NormalLength = 10f;
        public const float TranslationRate = 0.2f;
        public const string MeshName = "cctv1.mesh";
        public static readonly Vector3 Scale = new Vector3(8, 8, 8);

        private bool mSelected;
        private int mOldX, mOldY;
        private SceneNode mNormalNode;

        public bool Selected
        {
            get { return mSelected; }
            set
            {
                if (SceneNode != null)
                {
                    SceneNode.ShowBoundingBox = value;
                }
                mSelected = value;
            }

        }

        public string Name { get; private set; }
        public Vector3 Normal { get; private set; }
        public Entity Mesh { get; private set; }
        public SceneNode SceneNode { get; private set; }
        public Model Parent { get; private set; }
        public Camera Camera { get; private set; }
        public SecurityCameraFrustum Frustum { get; private set; }


        public SecurityCamera(Vector3 position, Vector3 normal, Model parent)
        {
            Parent = parent;
            Normal = normal;
            Normal = position + (Normal*NormalLength);

            CreateCamera(position);
        }

        private void CreateCamera(Vector3 position)
        {
            int index = Parent.SecurityCameras.Count;

            AddCamera(position, index);

            SceneNode.Scale(Scale);

            RotateToDirection(Normal);

            DrawNormal();

            TranslateCameraOnPolygonFace();

            Selected = true;
        }

        private void AddCamera(Vector3 position, int index)
        {
            Name = "SecurityCamera" + index;

            Mesh = Engine.Engine.Instance.SceneManager.CreateEntity(Name, MeshName);
            SceneNode = Engine.Engine.Instance.SceneManager.RootSceneNode.CreateChildSceneNode(Name + "_node");
            SceneNode.AttachObject(Mesh);

            SceneNode.Position = position;
            
            Camera = Engine.Engine.Instance.SceneManager.CreateCamera(Name+"_camera");
            
            Camera.Position = SceneNode.Position;
            Camera.LookAt(Normal);
            Camera.NearClipDistance = 5;
            Camera.FarClipDistance = 50;

            CreateCameraFrustum();
        }

        private void CreateCameraFrustum()
        {
            Frustum = new SecurityCameraFrustum(this);    
        }

        public void FlipVisibility()
        {
            Frustum.SceneNode.FlipVisibility();
        }

        private void TranslateCameraOnPolygonFace()
        {
            var aabb = Mesh.BoundingBox;
            aabb.Scale(Scale);
            var center = aabb.Center;
            var dist = 2*new Vector3(center.z, center.z, center.z);
            var dir = Engine.Engine.Instance.GetCameraDirection();
            var t = dist*dir;
            t = (dir.z < 0) ? -t : t;
            t = (dir.y > 0) ? -t : t;
            t = (dir.x < 0) ? -t : t;
            Translate(t);
        }

        public void RotateToDirection(Vector3 destination)
        {
            Vector3 direction = destination - SceneNode.Position; // B-A = A->B (see vector questions above)
            Vector3 src = SceneNode.Position*Vector3.UNIT_Z; //facing direction of this mesh is +Z
            direction.Normalise();
            Quaternion quat = src.GetRotationTo(direction); // Get a quaternion rotation operation 

            SceneNode.Rotate(quat);
        }

        private void DrawNormal()
        {
            Draw.Instance.Color = new Vector3(0, 0, 1f);
            mNormalNode = Draw.Instance.DrawLine(SceneNode.Position, Normal);
        }

        private void Translate(Vector3 t)
        {
            if (Engine.Engine.Instance.MainCamera != null)
            {
                t = t*Engine.Engine.Instance.MainCamera.Direction;
            }

            SceneNode.Translate(t);
            Frustum.SceneNode.Translate(t);
            if (mNormalNode != null)
            {
                mNormalNode.Translate(t);
            }

            Camera.Position = SceneNode.Position;
        }

        public void HandleKey(Keys key)
        {
            switch (key)
            {
                case Keys.Up:
                    Translate(new Vector3(0, -TranslationRate, 0));
                    break;
                case Keys.Left:
                    Translate(new Vector3(-TranslationRate, 0, 0));
                    break;
                case Keys.Down:
                    Translate(new Vector3(0, TranslationRate, 0));
                    break;
                case Keys.Right:
                    Translate(new Vector3(TranslationRate, 0, 0));
                    break;
                case Keys.Add:
                    Translate(new Vector3(0, 0, -TranslationRate));
                    break;
                case Keys.Subtract:
                    Translate(new Vector3(0, 0, TranslationRate));
                    break;
            }
        }

        public void MouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mOldX = e.X;
                mOldY = e.Y;
            }
        }

        public void Delete()
        {
            SceneNode.ShowBoundingBox = false;
            SceneNode.RemoveAndDestroyAllChildren();
        }

        public void MouseMove(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            int dx = e.X - mOldX;
            int dy = e.Y - mOldY;
            Vector2 dir = new Vector2(Math.Sign(dx), Math.Sign(dy));

            CameraRotation(dir);

            mOldX = e.X;
            mOldY = e.Y;
        }

        private void CameraRotation(Vector2 dir)
        {
            SceneNode.Pitch(new Degree(dir.y));
            Camera.Pitch(new Degree(-dir.y));
            SceneNode.Yaw(new Degree(dir.x));
            Camera.Yaw(new Degree(dir.x));
            Frustum.Delete();
            CreateCameraFrustum();
        }
    }
}
