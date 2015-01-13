using System.Windows.Forms;
using Mogre;

namespace ApplicationLogic.Scene
{
    public class SecurityCamera
    {
       
        public const float TranslationRate = 0.2f;
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
        public NormalLine NormalLine { get; private set; }
        public RenderingEngine.Scene.Camera Camera { get; private set; }

        public SecurityCamera(string name, Vector3 position, Vector3 direction)
        {
            Name = name;
            
            Properties = new SecurityCameraProperties {Position = position, Direction = direction};

            NormalLine = new NormalLine {Start = position, End = position + (direction*NormalLine.NormalLength)};

            CreateCamera();
        }

        private void CreateCamera()
        {
            Camera = new RenderingEngine.Scene.Camera(Name, Properties.Position, Properties.Direction, MeshName)
            {
                MogreCamera =
                {
                    NearClipDistance = Properties.NearClipDistance,
                    FarClipDistance = Properties.FarClipDistance
                }
            };

            Camera.Scale(DefaultScaleVector);
            Camera.RotateToDirection(NormalLine.End);
            Camera.DrawNormal(NormalLine.Start,NormalLine.End,NormalLine.LineColor);

            TranslateCameraOnPolygonFace();

            Selected = true;
        }

        private void TranslateCameraOnPolygonFace()
        {
            var aabb = Camera.GetBoundingBox();
            
            var center = aabb.Center;
            
            var dist = 2*new Vector3(center.z, center.z, center.z);
            
            var mainCameradir = RenderingEngine.Engine.Engine.Instance.GetMainCameraDirection();
            
            var t = dist*mainCameradir;
            t = (mainCameradir.z < 0) ? -t : t;
            t = (mainCameradir.y > 0) ? -t : t;
            t = (mainCameradir.x < 0) ? -t : t;
            
            Camera.Translate(t);
        }

        public void HandleKey(Keys key)
        {
            switch (key)
            {
                case Keys.Up:
                    Camera.Translate(new Vector3(0, -TranslationRate, 0));
                    break;
                case Keys.Left:
                    Camera.Translate(new Vector3(-TranslationRate, 0, 0));
                    break;
                case Keys.Down:
                    Camera.Translate(new Vector3(0, TranslationRate, 0));
                    break;
                case Keys.Right:
                    Camera.Translate(new Vector3(TranslationRate, 0, 0));
                    break;
                case Keys.Add:
                    Camera.Translate(new Vector3(0, 0, -TranslationRate));
                    break;
                case Keys.Subtract:
                    Camera.Translate(new Vector3(0, 0, TranslationRate));
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
