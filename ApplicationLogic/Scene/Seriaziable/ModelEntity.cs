using System;
using System.Collections.Generic;
using Mogre;

namespace ApplicationLogic.Scene.Seriaziable
{
    [Serializable]
    public class ModelEntity
    {
        public string Name;
        public string MeshName;
        public float[] TransformationMatrix;
        public List<CameraEntity> Cameras;
        public List<LightEntity> Lights;

        public ModelEntity()
        {
            TransformationMatrix = new float[4*4];
            Cameras = new List<CameraEntity>();
            Lights = new List<LightEntity>();
        }

        public ModelEntity(ModelProperties properties)
        {
            TransformationMatrix = new float[4 * 4];
            Cameras = new List<CameraEntity>();
            Lights = new List<LightEntity>();

            Name = properties.Name;
            MeshName = properties.MeshName;
        }

        public void AddLightProperties(LightProperties properties)
        {
            var lightEntity = new LightEntity(properties.Name, properties.Position, properties.Direction,
                properties.Type, properties.Color, properties.InnerAngle, properties.OuterAngle);
            Lights.Add(lightEntity);
        }

        public void AddCameraProperties(SecurityCameraProperties properties)
        {
            var cameraEntity = new CameraEntity(properties.Name, properties.Position, properties.Direction,
                properties.FOVy, properties.Resolution,properties.Rotation, properties.PitchDeg, properties.YawDeg);
            Cameras.Add(cameraEntity);
        }

        public void SetTransformationMatrix(Matrix4 m)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    TransformationMatrix[(i*4)+j] = m[i, j];
                }
            }
        }

        public Matrix4 GetTransformationMatrix()
        {
            var m = new Matrix4();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    m[i, j] = TransformationMatrix[j+(i*4)];
                }
            }
            return m;
        }
    }
}
