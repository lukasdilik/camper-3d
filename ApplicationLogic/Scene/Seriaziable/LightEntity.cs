using System;
using Mogre;

namespace ApplicationLogic.Scene.Seriaziable
{
    [Serializable]
    public class LightEntity : Entity
    {
        public int Type;
        public float[] Color;
        public float InnerAngleDeg;
        public float OuterAngleDeg;

        public LightEntity()
        {
            Color = new float[3];
        }

        public LightEntity(string name, Vector3 position, Vector3 direction,LightProperties.LightType type, ColourValue color, Degree innerAngleDeg, Degree outerAngleDeg) : base(name, position, direction)
        {
            Color = new float[3];
            Type = (type == LightProperties.LightType.Spot) ? 0: 1;
            Color[0] = color.r;
            Color[1] = color.g;
            Color[2] = color.b;
            InnerAngleDeg = innerAngleDeg.ValueDegrees;
            OuterAngleDeg = outerAngleDeg.ValueDegrees;
        }

        private LightProperties.LightType GetLightType()
        {
            return (Type == 0) ? LightProperties.LightType.Spot : LightProperties.LightType.Point;
        }

        private ColourValue GetColourValue()
        {
            return new ColourValue(Color[0], Color[1], Color[2]);
        }

        public LightProperties GetLightProperties()
        {
            var properties = new LightProperties();
            properties.Name = Name;
            properties.Position = GetPosition();
            properties.Direction = GetDirection();
            properties.Color = GetColourValue();
            properties.Type = GetLightType();
            properties.InnerAngle = InnerAngleDeg;
            properties.OuterAngle = OuterAngleDeg;
            return properties;
        }
    }
}
