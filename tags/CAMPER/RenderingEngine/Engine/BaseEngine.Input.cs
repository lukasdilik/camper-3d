using System.Windows.Forms;

namespace RenderingEngine.Engine
{
    public abstract partial class BaseEngine 
    {
        public virtual void KeyPress(KeyPressEventArgs e)
        {
            
        }

        public virtual void KeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                    CameraMan.GoingForward = true;
                    break;

                case Keys.S:
                case Keys.Down:
                    CameraMan.GoingBack = true;
                    break;

                case Keys.A:
                case Keys.Left:
                    CameraMan.GoingLeft = true;
                    break;

                case Keys.D:
                case Keys.Right:
                    CameraMan.GoingRight = true;
                    break;

                case Keys.Q:
                case Keys.PageUp:
                    CameraMan.GoingUp = true;
                    break;

                case Keys.E:
                case Keys.PageDown:
                    CameraMan.GoingDown = true;
                    break;

                case Keys.RShiftKey:
                    CameraMan.FastMove = true;
                    break;

                case Keys.T:
                    CycleTextureFilteringMode();
                    break;

                case Keys.R:
                    CyclePolygonMode();
                    break;

                case Keys.F5:
                    ReloadAllTextures();
                    break;

                case Keys.Escape:
                    Shutdown();
                    break;
            }
        }

        public virtual void KeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                    CameraMan.GoingForward = false;
                    break;

                case Keys.S:
                case Keys.Down:
                    CameraMan.GoingBack = false;
                    break;

                case Keys.A:
                case Keys.Left:
                    CameraMan.GoingLeft = false;
                    break;

                case Keys.D:
                case Keys.Right:
                    CameraMan.GoingRight = false;
                    break;

                case Keys.Q:
                case Keys.PageUp:
                    CameraMan.GoingUp = false;
                    break;

                case Keys.E:
                case Keys.PageDown:
                    CameraMan.GoingDown = false;
                    break;

                case Keys.RShiftKey:
                    CameraMan.FastMove = false;
                    break;
            }
        }

        public virtual void MouseUp(MouseEventArgs e)
        {
        }

        public virtual void MouseDown(MouseEventArgs e)
        {
            CameraMan.Click(e.X,e.Y);
        }

        public virtual void MouseMove(MouseEventArgs e)
        {
            CameraMan.MouseMovement(e.X, e.Y);
        }

        public virtual void MouseDoubleClick(MouseEventArgs e)
        {

        }
    }
}
