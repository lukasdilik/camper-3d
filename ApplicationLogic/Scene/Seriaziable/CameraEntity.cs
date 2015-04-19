using System;
using Mogre;

namespace ApplicationLogic.Scene.Seriaziable
{
    [Serializable]
    public class CameraEntity : Entity
    {
        public float FOVy;
        public float[] Resolution;
        public int RotationDeg;
        public int PitchDeg;
        public int YawDeg;

        public CameraEntity()
        {
            Resolution = new float[2];
        }

        public CameraEntity(string name, Vector3 position, Vector3 direction, Degree fovy, Vector2 resolution, Degree rotationDeg, int pitchDeg, int yawDeg) 
                : base(name, position, direction)
        {
            Resolution = new float[2];
            FOVy = fovy.ValueDegrees;
            Resolution[0] = resolution.x;
            Resolution[1] = resolution.y;
            RotationDeg = (int) rotationDeg.ValueDegrees;
            PitchDeg = pitchDeg;
            YawDeg = yawDeg;
        }

        private Degree GetFovy()
        {
            return new Degree(FOVy);
        }

        private Degree GetRotation()
        {
            return new Degree(RotationDeg);
        }

        private Vector2 GetResolution()
        {
            return new Vector2(Resolution[0], Resolution[1]);
        }

        public SecurityCameraProperties GetCameraProperties()
        {
            var properties = new SecurityCameraProperties
            {
                Name = Name,
                Position = GetPosition(),
                Direction = GetDirection(),
                FOVy = GetFovy(),
                Rotation = GetRotation(),
                Resolution = GetResolution(),
                YawDeg = YawDeg,
                PitchDeg = PitchDeg
            };
            return properties;
        }
    }
}
