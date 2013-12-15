using System;
using System.Windows.Forms;
using ApplicationLogic.Interfaces;
using RenderingEngine.Engine;

namespace ApplicationLogic
{
    public class AppController : IDisposable, IKeyboardInput, IMouseInput
    {
        private readonly Engine mEngine;
        private IApplicationUI mApplicationUi;

        public AppController(IApplicationUI appUI)
        {
            mEngine = new Engine();
            mApplicationUi = appUI;
        }

        public void SetUpRenderingWindow(IntPtr handle, int width, int height)
        {
            mEngine.SetUpRenderWindow(handle, width, height);
        }


        public void Start() 
        {
            mEngine.Start();
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            
        }

        public void Dispose()
        {
            if(mEngine != null)
                mEngine.Dispose();
        }

        public void KeyPress(KeyPressEventArgs e)
        {
            mEngine.KeyPress(e);
        }

        public void KeyDown(KeyEventArgs e)
        {
            mEngine.KeyDown(e);
        }

        public void KeyUp(KeyEventArgs e)
        {
            mEngine.KeyUp(e);
        }

        public void MouseUp(MouseEventArgs e)
        {
            mEngine.MouseUp(e);
        }

        public void MouseDown(MouseEventArgs e)
        {
            mEngine.MouseDown(e);
        }

        public void MouseMove(MouseEventArgs e)
        {
            mEngine.MouseMove(e);
        }

        public void MouseDoubleClick(MouseEventArgs e)
        {
            mEngine.MouseDoubleClick(e);
        }
    }
}
