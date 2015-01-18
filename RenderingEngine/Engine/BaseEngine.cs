using System;
using Mogre;
using RenderingEngine.Helpers;
using RenderingEngine.Interfaces;

namespace RenderingEngine.Engine
{
    class ShutdownException : Exception { }

    public abstract class BaseEngine : IDisposable
    {
        public Camera MainCamera;
        public CameraMan CameraMan;
        
        protected IApplication ApplicationLogic;
        
        protected Root Root;
        
        public SceneManager SceneManager;
        
        protected WindowParams WindowParams;
        protected RenderWindow RenderWindow;
        protected RenderSystem RenderSystem;
        protected Viewport Viewport;
        protected bool ShutDown = false;
        protected int TextureMode = 0;
        protected int RenderMode = 0;
        protected DebugOverlay DebugOverlay;

        public void Dispose()
        {
            if (Root == null) return;
            Root.Shutdown();
            Root = null;
        }

        public void Start()
        {
            try
            {
                if (!Setup())
                    return;

                Root.StartRendering();

                DestroyScene();
            }
            catch (System.Runtime.InteropServices.SEHException e)
            {
                if (OgreException.IsThrown)
                {
                    LogManager.Singleton.LogMessage(OgreException.LastException.ToString());
                    if (ApplicationLogic != null)
                    {
                        ApplicationLogic.LogMessage(OgreException.LastException.ToString());    
                    }
                    
                }
                    
            }
            catch (Exception e)
            {
                LogManager.Singleton.LogMessage(e.ToString());
                if (ApplicationLogic != null)
                {
                    ApplicationLogic.LogMessage(e.ToString());    
                }
            }
        }

        protected virtual bool Setup()
        {
            CreateRoot();

            if (!Configure())
                return false;

            CreateSceneManager();
            
            CreateCamera();
            
            CreateViewports();

            TextureManager.Singleton.DefaultNumMipmaps = 5;

            CreateResourceListener();
            
            LoadResources();

            CreateScene();

            CreateFrameListeners();

            DebugOverlay = new DebugOverlay(RenderWindow) {AdditionalInfo = "Bilinear"};

            return true;
        }

        private void CreateRoot()
        {
            Root = new Root();
        }

        protected virtual bool Configure()
        {
            SetUpRenderSystem();  
   
            CreateRenderWindow();

            return true;
        }

        protected virtual void SetUpRenderSystem()
        {
            RenderSystem = Root.GetRenderSystemByName("OpenGL Rendering Subsystem");
            RenderSystem.SetConfigOption("Full Screen", "No");
            RenderSystem.SetConfigOption("Video Mode", String.Format("{0} x {1} @ {2}-bit colour", WindowParams.Width, WindowParams.Height, WindowParams.ColorDepth));
            Root.RenderSystem = RenderSystem;
        }

        protected virtual void CreateRenderWindow()
        {
            Root.Initialise(false, WindowParams.Name);

            NameValuePairList miscParams = new NameValuePairList();
            miscParams["externalWindowHandle"] = WindowParams.Handle.ToString();
            miscParams["vsync"] = "true";
            RenderWindow = Root.CreateRenderWindow(WindowParams.Name, WindowParams.Width, WindowParams.Height, false, miscParams);
        }

        public void Resize(int width, int height)
        {
            RenderWindow.Resize((uint)width,(uint)height);
        }

        protected virtual void CreateSceneManager()
        {
            SceneManager = Root.CreateSceneManager(SceneType.ST_EXTERIOR_CLOSE);
        }

        protected virtual void CreateCamera()
        {
            MainCamera = SceneManager.CreateCamera("MainCamera");

            MainCamera.Position = new Vector3(0, 0, 0);

            MainCamera.LookAt(new Vector3(0, 0, -1));
            MainCamera.NearClipDistance = 5;

            CameraMan = new CameraMan(MainCamera);
        }

        protected virtual void CreateViewports()
        {
            Viewport = RenderWindow.AddViewport(MainCamera);
            Viewport.BackgroundColour = ColourValue.Black;

            MainCamera.AspectRatio = ((float)Viewport.ActualWidth / (float)Viewport.ActualHeight);
        }

        protected virtual void CreateResourceListener()
        {
        }

        protected virtual void LoadResources()
        {
            var configFile = new ConfigFile();
            configFile.Load("resources.cfg", "\t:=", true);

            var section = configFile.GetSectionIterator();
            while (section.MoveNext())
            {
                foreach (var pair in section.Current)
                {
                    ResourceGroupManager.Singleton.AddResourceLocation(pair.Value, pair.Key, section.CurrentKey);
                }
            }

            ResourceGroupManager.Singleton.InitialiseAllResourceGroups();
        }

        public void ReloadAllTextures()
        {
            TextureManager.Singleton.ReloadAll();
        }

        public void CycleTextureFilteringMode()
        {
            TextureMode = (TextureMode + 1) % 4;
            switch (TextureMode)
            {
                case 0:
                    MaterialManager.Singleton.SetDefaultTextureFiltering(TextureFilterOptions.TFO_BILINEAR);
                    DebugOverlay.AdditionalInfo = "BiLinear";
                    break;

                case 1:
                    MaterialManager.Singleton.SetDefaultTextureFiltering(TextureFilterOptions.TFO_TRILINEAR);
                    DebugOverlay.AdditionalInfo = "TriLinear";
                    break;

                case 2:
                    MaterialManager.Singleton.SetDefaultTextureFiltering(TextureFilterOptions.TFO_ANISOTROPIC);
                    MaterialManager.Singleton.DefaultAnisotropy = 8;
                    DebugOverlay.AdditionalInfo = "Anisotropic";
                    break;

                case 3:
                    MaterialManager.Singleton.SetDefaultTextureFiltering(TextureFilterOptions.TFO_NONE);
                    MaterialManager.Singleton.DefaultAnisotropy = 1;
                    DebugOverlay.AdditionalInfo = "None";
                    break;
            }
        }

        public void CyclePolygonMode()
        {
            RenderMode = (RenderMode + 1) % 3;
            switch (RenderMode)
            {
                case 0:
                    MainCamera.PolygonMode = PolygonMode.PM_SOLID;
                    break;

                case 1:
                    MainCamera.PolygonMode = PolygonMode.PM_WIREFRAME;
                    break;

                case 2:
                    MainCamera.PolygonMode = PolygonMode.PM_POINTS;
                    break;
            }
        }

        protected virtual void CreateFrameListeners()
        {
            Root.FrameRenderingQueued += OnFrameRenderingQueued;
        }

        protected virtual bool OnFrameRenderingQueued(FrameEvent evt)
        {
            if (RenderWindow.IsClosed)
                return false;

            if (ShutDown)
                return false;

            try
            {
                UpdateScene(evt);

                CameraMan.UpdateCamera(evt.timeSinceLastFrame);

                DebugOverlay.Update(evt.timeSinceLastFrame);

                return true;
            }
            catch (ShutdownException)
            {
                ShutDown = true;
                return false;
            }
        }

        public void Shutdown()
        {
            Root.Shutdown();
        }

        protected virtual void CreateScene()
        {
        }

        protected virtual void UpdateScene(FrameEvent evt)
        {
        }

        protected virtual void DestroyScene()
        {
        }

        public abstract void SetUpRenderWindow(IntPtr handle, int width, int height);
    }
}
