using System;
using Mogre;

namespace ApplicationLogic.Scene
{
    public class LightProperties
    {
        public enum LightType
        {
            Spot,Point
        }

        public String Name;
        public LightType Type = LightType.Spot;
        public Vector3 Direction;
        public Vector3 Position;
        public ColourValue Color = ColourValue.White;
        public Degree InnerAngle = new Degree(45);
        public Degree OuterAngle = new Degree(45);
    }
}
