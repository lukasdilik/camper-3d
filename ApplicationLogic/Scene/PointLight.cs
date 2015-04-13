using System.Windows.Forms;
using Mogre;
using RenderingEngine;

namespace ApplicationLogic.Scene
{
    public class PointLight : Light
    {
        public PointLight(SceneNode parentNode, LightProperties properties) : base(parentNode, ApplicationLogicResources.PointLightModelMesh, properties)
        {
            CreateOgreLight();
            OgreLight.Position = Properties.Position;
        }

        public override sealed void CreateOgreLight()
        {
            OgreLight = LightManager.Instance.CreatePointLight(Properties.Name, Properties.Position, Properties.Color);
        }

        public override void UpdateLightProperties(LightProperties newlightProperties)
        {
            Properties = newlightProperties;
            Properties.Position = newlightProperties.Position;
            SceneNode.Position = newlightProperties.Position;
            OgreLight.Position = newlightProperties.Position;

            Properties.Color = newlightProperties.Color;
            OgreLight.DiffuseColour = newlightProperties.Color;
            OgreLight.SpecularColour = newlightProperties.Color;

            string materialName = ColorMaterialManager.Instance.GetSolidColorMaterialName(Properties.Color);
            SetNewMaterial(materialName);
        }

        public override void MouseClick(MouseEventArgs e)
        {
        }

        public override void MouseMove(MouseEventArgs e)
        {
        }

        public override void Rotate(Vector2 dir)
        {
        }
    }
}
