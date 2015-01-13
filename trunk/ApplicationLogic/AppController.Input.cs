using System.Windows.Forms;

namespace ApplicationLogic
{
    public partial class AppController 
    {
        protected virtual void HandleKeyPress(char keyChar)
        {
            
        }

        protected virtual void HandleKeyDown(Keys key)
        {
            switch (key)
            {
                case Keys.W:
                    mEngine.CameraMan.GoingForward = true;
                    break;
                case Keys.S:
                    mEngine.CameraMan.GoingBack = true;
                    break;
                case Keys.A:
                    mEngine.CameraMan.GoingLeft = true;
                    break;
                case Keys.D:
                    mEngine.CameraMan.GoingRight = true;
                    break;
                case Keys.Q:
                    mEngine.CameraMan.GoingUp = true;
                    break;
                case Keys.E:
                    mEngine.CameraMan.GoingDown = true;
                    break;
                case Keys.RShiftKey:
                    mEngine.CameraMan.FastMove = true;
                    break;
                case Keys.F1:
                    mEngine.CyclePolygonMode();
                    break;
                case Keys.F2:
                    mEngine.CycleTextureFilteringMode();
                    break;
                case Keys.F3:
                    mEngine.ReloadAllTextures();
                    break;
                case Keys.F5:
                    mEngine.ChangeTerrainClamping();
                    break;
                case Keys.Delete:
                    DeleteSelectedCamera();
                    break;
                case Keys.Enter:
                    SwitchToSelectedCamera();
                    break;
                case Keys.Escape:
                    mEngine.Shutdown();
                    break;
            }
        }

        protected virtual void HandleKeyUp(Keys key)
        {
            switch (key)
            {
                case Keys.W:
                    mEngine.CameraMan.GoingForward = false;
                    break;

                case Keys.S:
                    mEngine.CameraMan.GoingBack = false;
                    break;

                case Keys.A:
                    mEngine.CameraMan.GoingLeft = false;
                    break;

                case Keys.D:
                    mEngine.CameraMan.GoingRight = false;
                    break;

                case Keys.Q:
                    mEngine.CameraMan.GoingUp = false;
                    break;

                case Keys.E:
                    mEngine.CameraMan.GoingDown = false;
                    break;

                case Keys.RShiftKey:
                    mEngine.CameraMan.FastMove = false;
                    break;
            }
        }

        protected virtual void HandleMouseUp(MouseEventArgs e)
        {
        }

        protected virtual void HandleMouseDown(MouseEventArgs e)
        {
            mEngine.CameraMan.Click(e.X,e.Y);
        }

        protected virtual void HandleMouseMove(MouseEventArgs e)
        {
            mEngine.CameraMan.MouseMovement(e.X, e.Y);
        }

        protected virtual void HandleMouseDoubleClick(MouseEventArgs e)
        {

        }
    }
}
