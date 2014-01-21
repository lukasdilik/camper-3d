using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ApplicationLogic.Interfaces;
using RenderingEngine.Engine;

namespace ApplicationLogic
{
    public class AppController : IDisposable, IKeyboardInput, IMouseInput
    {
        private const string StoredModelsPath = "./Resources/models";

        private bool mIsStarted;
        private bool mIsModelLoaded;
        private List<string> mAvailableModels;
        private readonly Engine mEngine;
        private readonly IApplicationUI mApplicationUi;

        public AppController(IApplicationUI appUi)
        {
            mEngine = new Engine();
            mAvailableModels = new List<string>();
            mApplicationUi = appUi;
            GetAvailableModels();
        }

        private void GetAvailableModels()
        {
            var models = Directory.GetFiles(@StoredModelsPath, "*.mesh");
            foreach (var model in models)
            {
                var modelName = Path.GetFileName(model);
                mAvailableModels.Add(modelName);
            }
            mApplicationUi.ShowAvailableModels(mAvailableModels);
        }

        public void SetUpRenderingWindow(IntPtr handle, int width, int height)
        {
            mEngine.SetUpRenderWindow(handle, width, height);
        }

        public void LoadModel(string fileName)
        {
            if (!File.Exists(Path.Combine(StoredModelsPath,fileName)))
            {
                mIsModelLoaded = false;
                var e = new FileNotFoundException("Model file name not found on path: "+fileName);
                mApplicationUi.ExceptionOccured(e);
                return;
            }

            var temp = fileName.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            mEngine.LoadModel(temp[0],fileName);
            mIsModelLoaded = true;
            mApplicationUi.ModelSucessfullyLoaded(fileName);
        }

        public void Start() 
        {
            if(!mIsModelLoaded) mApplicationUi.ExceptionOccured(new Exception("Model are not loaded"));

            mIsStarted = true;
            mEngine.Start();
        }

        public void Dispose()
        {
            if(mEngine != null)
                mEngine.Dispose();
        }

        #region Keyboard Input

        public void KeyPress(char key)
        {
            if (!mIsStarted) return;

            mEngine.KeyPress(key);
        }

        public void KeyDown(Keys key)
        {
            if (!mIsStarted) return;

            mEngine.KeyDown(key);
        }

        public void KeyUp(Keys key)
        {
            if (!mIsStarted) return;

            mEngine.KeyUp(key);
        }

        #endregion

        #region Mouse Input
        public void MouseUp(MouseEventArgs e)
        {
            if (!mIsStarted) return;

            mEngine.MouseUp(e);
        }

        public void MouseDown(MouseEventArgs e)
        {
            if (!mIsStarted) return;

            mEngine.MouseDown(e);
        }

        public void MouseMove(MouseEventArgs e)
        {
            if (!mIsStarted) return;

            mEngine.MouseMove(e);
        }

        public void MouseDoubleClick(MouseEventArgs e)
        {
            if (!mIsStarted) return;

            mEngine.MouseDoubleClick(e);
        }

        #endregion
    }
}
