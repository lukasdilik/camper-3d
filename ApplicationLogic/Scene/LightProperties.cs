using Mogre;

namespace ApplicationLogic.Scene
{
    public class LightProperties
    {
        public enum LightType
        {
            Directional,Point
        }

        public LightType Type;
        public Vector3 Direction;
        public Vector3 Position;
        public Vector3 Color;
    }
}
