using System.Windows.Forms;
using Mogre;
using Camera = RenderingEngine.Scene.Camera;

namespace ApplicationLogic.Scene
{
    public class SecurityCamera
    {
        public string MeshName = ApplicationLogicResources.SecurityCameraMeshName;
        private bool mSelected;
        private int mOldX, mOldY;

        public bool Selected
        {
            get { return mSelected; }
            set
            {
                if (RenderTexture != null)
                {
                    RenderTexture.IsAutoUpdated = value;
                }
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

        public string InternalName { get; private set; }
        public SecurityCameraProperties Properties { get; private set; }
        public Camera Camera { get; private set; }
        public RenderTexture RenderTexture { get; private set; }
        public TexturePtr RenderTexturePtr;
        public SecurityCamera(string internalName, Vector3 position, Vector3 normal)
        {
       
            InternalName = internalName;
            Properties = new SecurityCameraProperties {Name = internalName, Position = position, Direction = normal};

            CreateCameraInScene();
            Properties.Position = Camera.SceneNode.Position;
        }

        public void InitRTT(TexturePtr texturePtr)
        {
            RenderTexturePtr = texturePtr;
            RenderTexture = texturePtr.GetBuffer().GetRenderTarget();

            RenderTexture.AddViewport(Camera.MogreCamera);
            RenderTexture.GetViewport(0).SetClearEveryFrame(true);
            RenderTexture.GetViewport(0).BackgroundColour = ColourValue.Black;
            RenderTexture.GetViewport(0).OverlaysEnabled = false;
            RenderTexture.IsAutoUpdated = true;
        }

        public void UpdateCameraProperties(SecurityCameraProperties newProperties)
        {
            Properties = newProperties;

            Camera.UpdateProperties(newProperties.Position,newProperties.Direction, newProperties.FOVy, newProperties.AspectRatio);
        }

        private void CreateCameraInScene()
        {
            Camera = new Camera(Properties.Name, Properties.Position, Properties.Direction, MeshName)
            {
                MogreCamera = {AspectRatio = Properties.AspectRatio, FOVy = Properties.FOVy}
            };
        }

        public void HandleKey(Keys key)
        {
            switch (key)
            {
                case Keys.Up:
                    Camera.MoveTop();
                    Properties.Position = Camera.SceneNode.Position;
                    break;
                case Keys.Left:
                    Camera.MoveLeft();
                    Properties.Position = Camera.SceneNode.Position;
                    break;
                case Keys.Down:
                    Camera.MoveDown();
                    Properties.Position = Camera.SceneNode.Position;
                    break;
                case Keys.Right:
                    Camera.MoveRight();
                    Properties.Position = Camera.SceneNode.Position;
                    break;
                case Keys.Add:
                    Camera.MoveForward();
                    Properties.Position = Camera.SceneNode.Position;
                    break;
                case Keys.Subtract:
                    Camera.MoveBackward();
                    Properties.Position = Camera.SceneNode.Position;
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
            Properties.Position = Camera.SceneNode.Position;
            Properties.Direction = Camera.MogreCamera.Direction;

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
