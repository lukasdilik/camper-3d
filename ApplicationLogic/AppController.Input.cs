using System;
using System.Windows.Forms;
using RenderingEngine.Engine;

namespace ApplicationLogic
{
    public partial class AppController : IDisposable
    {
        protected virtual void HandleKeyPress(char keyChar)
        {
            
        }

        protected virtual void HandleKeyDown(Keys key)
        {
            switch (key)
            {
                case Keys.W:
                    Engine.Instance.CameraMan.GoingForward = true;
                    break;
                case Keys.S:
                    Engine.Instance.CameraMan.GoingBack = true;
                    break;
                case Keys.A:
                    Engine.Instance.CameraMan.GoingLeft = true;
                    break;
                case Keys.D:
                    Engine.Instance.CameraMan.GoingRight = true;
                    break;
                case Keys.Q:
                    Engine.Instance.CameraMan.GoingUp = true;
                    break;
                case Keys.E:
                    Engine.Instance.CameraMan.GoingDown = true;
                    break;
                case Keys.RShiftKey:
                    Engine.Instance.CameraMan.FastMove = true;
                    break;
                case Keys.F1:
                    Engine.Instance.CyclePolygonMode();
                    break;
                case Keys.F2:
                    Engine.Instance.CycleTextureFilteringMode();
                    break;
                case Keys.F3:
                    Engine.Instance.ReloadAllTextures();
                    break;
                case Keys.F5:
                    Engine.Instance.ChangeTerrainClamping();
                    break;
                case Keys.X:
                    Engine.Instance.SetSideView();
                    break;
                case Keys.Y:
                    Engine.Instance.SetTopView();
                    break;
                case Keys.Z:
                    Engine.Instance.SetFrontView();
                    break;
                case Keys.Escape:
                    Engine.Instance.ResetToNormalView();
                    break;
                case Keys.Enter:
                    if (ActiveMode == Mode.CAMERA_MODE)
                    {
                        SwitchToSelectedCamera();
                    }
                    break;
                case Keys.Up:
                    if (ActiveMode == Mode.MODEL_MODE)
                    {
                        if (SelectedModel != null)
                        {
                            SelectedModel.MoveForward();
                            mApplicationUi.ModelSelected(SelectedModel.ModelProperties);
                        }
                    }
                    break;
                case Keys.Down:
                    if (ActiveMode == Mode.MODEL_MODE)
                    {
                        if (SelectedModel != null)
                        {
                            SelectedModel.MoveBackward();
                            mApplicationUi.ModelSelected(SelectedModel.ModelProperties);
                        }
                    }
                    break;
                case Keys.Left:
                    if (ActiveMode == Mode.MODEL_MODE)
                    {
                        if (SelectedModel != null)
                        {
                            SelectedModel.MoveLeft();
                            mApplicationUi.ModelSelected(SelectedModel.ModelProperties);
                        }
                    }
                    break;
                case Keys.Right:
                    if (ActiveMode == Mode.MODEL_MODE)
                    {
                        if (SelectedModel != null)
                        {
                            SelectedModel.MoveRight();
                            mApplicationUi.ModelSelected(SelectedModel.ModelProperties);
                        }
                    }
                    break;
            }
        }

        protected virtual void HandleKeyUp(Keys key)
        {
            switch (key)
            {
                case Keys.W:
                    Engine.Instance.CameraMan.GoingForward = false;
                    break;

                case Keys.S:
                    Engine.Instance.CameraMan.GoingBack = false;
                    break;

                case Keys.A:
                    Engine.Instance.CameraMan.GoingLeft = false;
                    break;

                case Keys.D:
                    Engine.Instance.CameraMan.GoingRight = false;
                    break;

                case Keys.Q:
                    Engine.Instance.CameraMan.GoingUp = false;
                    break;

                case Keys.E:
                    Engine.Instance.CameraMan.GoingDown = false;
                    break;
                case Keys.RShiftKey:
                    Engine.Instance.CameraMan.FastMove = false;
                    break;
            }
        }

        public void StopMovement()
        {
            if (Engine.Instance.CameraMan == null) return;

            Engine.Instance.CameraMan.GoingForward = false;
            Engine.Instance.CameraMan.GoingBack = false;
            Engine.Instance.CameraMan.GoingLeft = false;
            Engine.Instance.CameraMan.GoingRight = false;
            Engine.Instance.CameraMan.GoingUp = false;
            Engine.Instance.CameraMan.GoingDown = false;
            Engine.Instance.CameraMan.FastMove = false;
        }

        protected virtual void HandleMouseUp(MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
                ;
        }

        protected virtual void HandleMouseDown(MouseEventArgs e)
        {
            Engine.Instance.CameraMan.Click(e.X, e.Y);
        }

        protected virtual void HandleMouseMove(MouseEventArgs e)
        {
            Engine.Instance.CameraMan.MouseMovement(e.X, e.Y);
        }

        protected virtual void HandleMouseDoubleClick(MouseEventArgs e)
        {

        }

        protected virtual void HandleMouseWheel(MouseEventArgs e)
        {
            var factor = e.Delta;
            if (factor > 0)
            {
                Engine.Instance.ZoomIn();
            }
            else
            {
                Engine.Instance.ZoomOut();
            }
        }

        public void Dispose()
        {
         
        }
    }
}
