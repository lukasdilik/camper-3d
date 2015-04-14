using System;
using Mogre;
using Math = System.Math;

namespace ApplicationLogic.Scene
{
    public class SecurityCameraProperties
    {
        public string Name;
        public Vector3 Position = new Vector3();
        public Vector3 Direction = new Vector3();
        public float AspectRatio
        {
            get { return (float) Math.Round((Decimal)Resolution.x / (decimal) Resolution.y, 2, MidpointRounding.AwayFromZero); }
        }

        public Degree FOVy = new Degree(45);
        public Vector2 Resolution = new Vector2(1280,720);
        public Degree Rotation = new Degree(0);
        public int PitchDeg;
        public int YawDeg;
    }
}
