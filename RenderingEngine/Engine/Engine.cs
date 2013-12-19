using System;
using Mogre;

namespace RenderingEngine.Engine
{
    public class Engine : BaseEngine
    {
        private const String WindowName = "MOGRE Window";

        private string mModelName;
        private string mModelFilePath;

        public void LoadModel(string modelFileName, string modelFilePath)
        {
            var temp = modelFileName.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            mModelName = temp[0];
            mModelFilePath = modelFilePath;
        }

        public override void SetUpRenderWindow(IntPtr handle, int width, int height)
        {
            WindowParams.Name = WindowName;
            WindowParams.Handle = handle;
            WindowParams.Width = (uint)width;
            WindowParams.Height = (uint)height;
            WindowParams.ColorDepth = 32;
        }

        protected override void CreateScene()
        {
            SetupLights();
            LoadModel();

            SceneManager.SetSkyBox(true, "Examples/CloudyNoonSkyBox");
        }

        private void SetupLights()
        {
            SceneManager.AmbientLight = new ColourValue(0.25f, 0.25f, 0.25f);

            Light pointLight = SceneManager.CreateLight("pointLight");
            pointLight.Type = Light.LightTypes.LT_POINT;
            pointLight.Position = new Vector3(250, 150, 250);
            pointLight.DiffuseColour = ColourValue.White;
            pointLight.SpecularColour = ColourValue.White;
        }

        private void LoadModel()
        {
            var entity = SceneManager.CreateEntity(mModelName, mModelFilePath);
            SceneManager.RootSceneNode.CreateChildSceneNode(mModelName + "_node").AttachObject(entity);
        }
    }
}
