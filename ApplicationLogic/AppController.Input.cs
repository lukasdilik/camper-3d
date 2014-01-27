﻿using System.Windows.Forms;

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
                case Keys.Up:
                    mEngine.CameraMan.GoingForward = true;
                    break;
                case Keys.Down:
                    mEngine.CameraMan.GoingBack = true;
                    break;
                case Keys.Left:
                    mEngine.CameraMan.GoingLeft = true;
                    break;
                case Keys.Right:
                    mEngine.CameraMan.GoingRight = true;
                    break;
                case Keys.PageUp:
                    mEngine.CameraMan.GoingUp = true;
                    break;
                case Keys.PageDown:
                    mEngine.CameraMan.GoingDown = true;
                    break;
                case Keys.RShiftKey:
                    mEngine.CameraMan.FastMove = true;
                    break;
                case Keys.T:
                    mEngine.CycleTextureFilteringMode();
                    break;
                case Keys.R:
                    mEngine.CyclePolygonMode();
                    break;
                case Keys.F5:
                    mEngine.ReloadAllTextures();
                    break;
                case Keys.NumPad1:
                    mEngine.LookFromTop();
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
                case Keys.Up:
                    mEngine.CameraMan.GoingForward = false;
                    break;

                case Keys.Down:
                    mEngine.CameraMan.GoingBack = false;
                    break;

                case Keys.Left:
                    mEngine.CameraMan.GoingLeft = false;
                    break;

                case Keys.Right:
                    mEngine.CameraMan.GoingRight = false;
                    break;

                case Keys.PageUp:
                    mEngine.CameraMan.GoingUp = false;
                    break;

                case Keys.PageDown:
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
