using System.Windows.Forms;
using Mogre;
using RenderingEngine.Engine;
using Camera = RenderingEngine.Scene.Camera;

namespace ApplicationLogic.Scene
{
    public class SecurityCamera
    {
       
        
        public const string MeshName = "cctv1.mesh";
        public static readonly Vector3 DefaultScaleVector = new Vector3(8, 8, 8);

        private bool mSelected;
        private int mOldX, mOldY;

        public bool Selected
        {
            get { return mSelected; }
            set
            {
                if (value)
                {
                    Camera.ShowBoundingBox();
                }
                else
                {
                    Camera.HideBoundingBox();
                }
                mSelected = value;
            }
        }

        public string Name { get; private set; }
        public SecurityCameraProperties Properties { get; private set; }
        public RenderingEngine.Scene.Camera Camera { get; private set; }

        public SecurityCamera(string name, Vector3 position, Vector3 normal)
        {
            Name = name;
            
            Properties = new SecurityCameraProperties {Position = position, Normal = normal};

            CreateCamera();
        }

        private void CreateCamera()
        {
            Camera = new Camera(Name, Properties.Position, Properties.Normal, MeshName);

            Camera.Scale(DefaultScaleVector);
            Camera.RotateToDirection(Properties.Position + Properties.Normal*10);

            TranslateCameraOnPolygonFace();

            Selected = true;
        }

        private void TranslateCameraOnPolygonFace()
        {
            var aabb = Camera.GetBoundingBox();
            aabb.Scale(DefaultScaleVector);
            var center = aabb.Center;
            
            var dist = 2*new Vector3(center.z, center.z, center.z);
            
            var mainCameradir = Engine.Instance.GetMainCameraDirection();
            
            var t = dist*mainCameradir;
            t = (mainCameradir.z < 0) ? -t : t;
            t = (mainCameradir.y > 0) ? -t : t;
            t = (mainCameradir.x < 0) ? -t : t;

            if (Engine.Instance.MainCamera != null)
            {
                t = t * Engine.Instance.MainCamera.Direction;
            }

            Camera.SceneNode.Translate(t);
        }

        public void HandleKey(Keys key)
        {
            switch (key)
            {
                case Keys.Up:
                    Camera.MoveTop();
                    break;
                case Keys.Left:
                    Camera.MoveLeft();
                    break;
                case Keys.Down:
                    Camera.MoveDown();
                    break;
                case Keys.Right:
                    Camera.MoveRight();
                    break;
                case Keys.Add:
                    Camera.MoveForward();
                    break;
                case Keys.Subtract:
                    Camera.MoveBackward();
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
            Camera.Delete();
        }

        public void MouseMove(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var dx = e.X - mOldX;
            var dy = e.Y - mOldY;
            var dir = new Vector2(Math.Sign(dx), Math.Sign(dy));

            CameraRotation(dir);

            mOldX = e.X;
            mOldY = e.Y;
        }

        private void CameraRotation(Vector2 dir)
        {
            Camera.Pitch(new Degree(-dir.y));
            Camera.Yaw(new Degree(dir.x));
        }
    }
}
