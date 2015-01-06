﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ApplicationLogic.Interfaces;
using RenderingEngine.Engine;

namespace ApplicationLogic
{
    public partial class AppController : IDisposable, IKeyboardInput, IMouseInput
    {
        private const string StoredModelsPath = "./Resources/models/scene";

        private bool mIsStarted;
        private bool mIsModelLoaded;
        private readonly List<string> mAvailableModels;
        private readonly Engine mEngine;
        private readonly IApplicationUI mApplicationUi;

        public AppController(IApplicationUI appUi)
        {
            mEngine = Engine.Instance;
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
            mApplicationUi.SendMessage("Model successfully loaded");
        }

        public void Start() 
        {
            if(!mIsModelLoaded) mApplicationUi.ExceptionOccured(new Exception("Model are not loaded"));

            mIsStarted = true;
            mEngine.Start();
        }

        public void Resize(int width, int height)
        {
            mEngine.Resize(width, height);
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

            HandleKeyPress(key);
        }

        public void KeyDown(Keys key)
        {
            if (!mIsStarted) return;

            mEngine.CameraControl(key);
            HandleKeyDown(key);
            UpdateUI();
        }

        public void KeyUp(Keys key)
        {
            if (!mIsStarted) return;

            HandleKeyUp(key);
        }

        #endregion

        #region Mouse Input

        public void MouseUp(MouseEventArgs e)
        {
            if (!mIsStarted) return;

            HandleMouseUp(e);
        }

        public void MouseDown(MouseEventArgs e)
        {
            if (!mIsStarted) return;

            if (e.Button == MouseButtons.Left)
            {
                mEngine.SelectObject(e.X,e.Y);
            }

            if (e.Button == MouseButtons.Right)
            {
                mEngine.CreateSecurityCamera(e.X, e.Y);
            }

            if (mEngine.IsSecurityCameraSelected())
            {
                mEngine.CameraMouseClick(e);
            }
            else
            {
                HandleMouseDown(e);        
            }

            UpdateUI();
        }

        public void MouseMove(MouseEventArgs e)
        {
            if (!mIsStarted) return;
            
            if (mEngine.IsSecurityCameraSelected())
            {
                mEngine.CameraMouseMove(e);
            }
            else
            {
                HandleMouseMove(e);    
            }
            
            UpdateUI();
        }

        public void MouseDoubleClick(MouseEventArgs e)
        {
            if (!mIsStarted) return;

            if (e.Button == MouseButtons.Left)
            {
                mEngine.AddModel(e.X,e.Y);
            }

            HandleMouseDoubleClick(e);
        }


        #endregion

        public void UpdateUI()
        {
            var pos = mEngine.GetCameraPosition();
            var dir = mEngine.GetCameraDirection();
            string info = string.Format("Camera Pos:[{0} ; {1}; {2}] | Dir:[{3} ; {4} ; {5}]",pos.x,pos.y,pos.z,dir.x,dir.y,dir.z);
            mApplicationUi.UpdateCameraInformation(info);
        }
    }
}