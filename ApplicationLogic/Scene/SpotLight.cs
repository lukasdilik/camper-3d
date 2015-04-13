using System.Windows.Forms;
using Mogre;
using RenderingEngine;

namespace ApplicationLogic.Scene
{
    public class SpotLight : Light
    {
        public SpotLight(SceneNode parentNode, LightProperties properties): base(parentNode, ApplicationLogicResources.SpotLightMesh, properties)
        {
            CreateOgreLight();
        }

        public override sealed void CreateOgreLight()
        {
            OgreLight = LightManager.Instance.CreateSpotLight(Properties.Name, Properties.Position, Properties.Direction,
                Properties.Color, Properties.InnerAngle, Properties.OuterAngle);
            RotateToDirection(Properties.Position+10*Properties.Direction);
            TranslateCameraOnPolygonFace();
            OgreLight.Position = Properties.Position;
            OgreLight.Direction = Properties.Direction;
        }

        public override void UpdateLightProperties(LightProperties lightProperties)
        {
            Properties.Position = lightProperties.Position;
            SceneNode.Position = lightProperties.Position;
            OgreLight.Position = lightProperties.Position;

            Properties.Direction = lightProperties.Direction;

            
            Properties.Color = lightProperties.Color;
            OgreLight.DiffuseColour = lightProperties.Color;
            OgreLight.SpecularColour = lightProperties.Color;

            string materialName = ColorMaterialManager.Instance.GetSolidColorMaterialName(Properties.Color);
            SetNewMaterial(materialName);

            Properties.InnerAngle = lightProperties.InnerAngle;
            Properties.OuterAngle = lightProperties.OuterAngle;
            OgreLight.SpotlightInnerAngle = lightProperties.InnerAngle;
            OgreLight.SpotlightOuterAngle = lightProperties.OuterAngle;
        }

        public override void MouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mOldX = e.X;
                mOldY = e.Y;
            }
        }

        public override void MouseMove(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var dx = e.X - mOldX;
            var dy = e.Y - mOldY;
            var dir = new Vector2(Math.Sign(dx), Math.Sign(dy));

            Rotate(dir);
            Properties.Position = SceneNode.Position;
            mOldX = e.X;
            mOldY = e.Y;
        }

        public override void Rotate(Vector2 dir)
        {
            SceneNode.Pitch(new Degree(-dir.y));
            SceneNode.Yaw(new Degree(dir.x));
        }
    }
}
