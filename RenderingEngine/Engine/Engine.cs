using System;
using Mogre;

namespace RenderingEngine.Engine
{
    public class Engine : BaseEngine
    {
        private const String WindowName = "MOGRE Window";

        private const String ModelName = "policeStation";
        private const String ModelFile = "policeStation.mesh";

        private Entity mModelEntity;
        private SceneNode mModelSceneNode;

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
            SceneManager.AmbientLight = new ColourValue(0.25f, 0.25f, 0.25f);

            Light pointLight = SceneManager.CreateLight("pointLight");
            pointLight.Type = Light.LightTypes.LT_POINT;
            pointLight.Position = new Vector3(250, 150, 250);
            pointLight.DiffuseColour = ColourValue.White;
            pointLight.SpecularColour = ColourValue.White;
            

            mModelEntity = SceneManager.CreateEntity(ModelName, ModelFile);
            mModelSceneNode = SceneManager.RootSceneNode.CreateChildSceneNode(ModelName+"Node");

            mModelSceneNode.AttachObject(mModelEntity);
        }
    }
}
