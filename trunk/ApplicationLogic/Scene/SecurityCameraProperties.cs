using Mogre;

namespace ApplicationLogic.Scene
{
    public class SecurityCameraProperties
    {
        public string Name = "";
        public Vector3 Position = new Vector3();
        public Vector3 Direction = new Vector3();
        public float AspectRatio = 1.44f;
        public Degree FOVy = new Degree(110);
        public float FocalDistance;
        public Vector2 Resolution = new Vector2();
        public float Rotation;
    }
}
