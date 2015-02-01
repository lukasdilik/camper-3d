using System.Windows.Forms;
using Mogre;
using Camera = RenderingEngine.Scene.Camera;

namespace ApplicationLogic.Scene
{
    public class SecurityCamera
    {
        public const string MeshName = "cctv1.mesh";
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
        public int Index { get; private set; }
        public SecurityCameraProperties Properties { get; private set; }
        public Camera Camera { get; private set; }

        public SecurityCamera(int index, Vector3 position, Vector3 normal)
        {
            Index = index;
            Name = "SecurityCamera" + index;
            
            Properties = new SecurityCameraProperties {Position = position, Normal = normal};

            CreateCameraInScene();
        }

        private void CreateCameraInScene()
        {
            Camera = new Camera(Name, Properties.Position, Properties.Normal, MeshName);
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
