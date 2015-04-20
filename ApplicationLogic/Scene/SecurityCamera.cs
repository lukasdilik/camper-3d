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
        public bool mNativeRendering;
        public bool IsNativeRendering
        {
            get { return mNativeRendering; }
            set
            {
                mNativeRendering = value;
                StartRenderingToTexture();
            }
        }

        public bool Selected
        {
            get { return mSelected; }
            set
            {
                if (value)
                {
                    Camera.ShowBoundingBox();
                    StartRenderingToTexture();
                }
                else
                {
                    Camera.HideBoundingBox();
                    StopRenderingToTexture();
                }
                mSelected = value;
            }
        }

        public SecurityCameraProperties Properties { get; private set; }
        public Camera Camera { get; private set; }
        public RenderTexture RenderTexture { get; private set; }
        public RenderTexture RenderTextureNative { get; private set; }
        public TexturePtr RenderTexturePtr { get; private set; }
        public TexturePtr RenderTextureNativePtr { get; private set; }
        public SecurityCamera(string name, Vector3 position, Vector3 normal)
        {
            normal.Normalise();
            Properties = new SecurityCameraProperties {Name = name, Position = position, Direction = normal};

            CreateCameraInScene();
            Properties.Position = Camera.SceneNode.Position;
            InitRTT();
        }

        private void InitRTT()
        {
            CreateTexturePtr();
            CreateTexturePtrNative();
            RenderTexture = RenderTexturePtr.GetBuffer().GetRenderTarget();
            RenderTexture.AddViewport(Camera.MogreCamera);
            RenderTexture.GetViewport(0).SetClearEveryFrame(true);
            RenderTexture.GetViewport(0).BackgroundColour = ColourValue.Black;
            RenderTexture.GetViewport(0).OverlaysEnabled = false;
            RenderTexture.IsAutoUpdated = true;

            RenderTextureNative = RenderTextureNativePtr.GetBuffer().GetRenderTarget();
            RenderTextureNative.AddViewport(Camera.MogreCamera);
            RenderTextureNative.GetViewport(0).SetClearEveryFrame(true);
            RenderTextureNative.GetViewport(0).BackgroundColour = ColourValue.Black;
            RenderTextureNative.GetViewport(0).OverlaysEnabled = false;
            RenderTextureNative.IsAutoUpdated = false;
        }

        public void UpdateCameraProperties(SecurityCameraProperties newProperties)
        {
            Properties.Position = newProperties.Position;
            Properties.Direction = newProperties.Direction;
            Properties.FOVy = newProperties.FOVy;
            Properties.Resolution = newProperties.Resolution;
            Properties.Rotation = newProperties.Rotation;
            Camera.UpdateProperties(newProperties.Position,newProperties.Direction, newProperties.FOVy, newProperties.AspectRatio, Properties.Rotation);
        }

        private void CreateCameraInScene()
        {
            Camera = new Camera(Properties.Name, Properties.Position, Properties.Direction,Properties.FOVy, Properties.AspectRatio, MeshName);
        }

        private void CreateTexturePtr()
        {
            var textureName = "Texture" + Properties.Name;
            var width = (uint)AppController.CameraViewDimension.Width;
            var height = (uint)AppController.CameraViewDimension.Height;
            RenderTexturePtr = TextureManager.Singleton.CreateManual(textureName, AppController.DefaultMaterialGroupName,
                TextureType.TEX_TYPE_2D, width, height, 0, PixelFormat.PF_B8G8R8, (int)TextureUsage.TU_RENDERTARGET);
        }

        private void CreateTexturePtrNative()
        {
            var textureName = "TextureNative" + Properties.Name;
            var width = (uint)Properties.Resolution.x;
            var height = (uint)Properties.Resolution.y;
            RenderTextureNativePtr = TextureManager.Singleton.CreateManual(textureName, AppController.DefaultMaterialGroupName,
                TextureType.TEX_TYPE_2D, width, height, 0, PixelFormat.PF_B8G8R8, (int)TextureUsage.TU_RENDERTARGET);
        }

        public void StartRenderingToTexture()
        {
            if (mNativeRendering)
            {
                RenderTextureNative.IsAutoUpdated = true;
                RenderTexture.IsAutoUpdated = false;
            }
            else
            {
                RenderTextureNative.IsAutoUpdated = false;
                RenderTexture.IsAutoUpdated = true;
            }
        }

        public void StopRenderingToTexture()
        {
            RenderTextureNative.IsAutoUpdated = false;
            RenderTexture.IsAutoUpdated = false;
        }

        public void HandleKey(Keys key)
        {
            switch (key)
            {
                case Keys.Up:
                    Camera.MoveForward();
                    Properties.Position = Camera.SceneNode.Position;
                    break;
                case Keys.Left:
                    Camera.MoveLeft();
                    Properties.Position = Camera.SceneNode.Position;
                    break;
                case Keys.Down:
                    Camera.MoveBackward();
                    Properties.Position = Camera.SceneNode.Position;
                    break;
                case Keys.Right:
                    Camera.MoveRight();
                    Properties.Position = Camera.SceneNode.Position;
                    break;
                case Keys.PageUp:
                    Camera.MoveTop();
                    Properties.Position = Camera.SceneNode.Position;
                    break;
                case Keys.PageDown:
                    Camera.MoveDown();
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
            StopRenderingToTexture();
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

        public void CameraPitch(int deg)
        {
            var diff = Properties.PitchDeg - deg;
            Camera.Pitch(new Degree(diff).ValueRadians);
            Properties.Direction = Camera.MogreCamera.Direction;
            Properties.PitchDeg = deg;
        }

        public void CameraYaw(int deg)
        {
            var diff = Properties.YawDeg - deg;
            Camera.Yaw(new Degree(diff).ValueRadians);
            Properties.Direction = Camera.MogreCamera.Direction;
            Properties.YawDeg = deg;
        }

        private void CameraRotation(Vector2 dir)
        {
            Camera.Pitch(new Degree(-dir.y));
            Camera.Yaw(new Degree(dir.x));
        }
    }
}
