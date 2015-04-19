using System;
using Mogre;

namespace ApplicationLogic.Scene.Seriaziable
{
    [Serializable]
    public class Entity
    {
        public string Name;
        public float[] Position;
        public float[] Direction;

        protected Entity()
        {
            Name = "";
            Position = new float[3];
            Direction = new float[3];
        }

        protected Entity(string name, Vector3 position, Vector3 direction)
        {
            Name = name;
            Position = new float[3];
            Direction = new float[3];
            for (int i = 0; i < Position.Length; i++)
            {
                Position[i] = position[i];
                Direction[i] = direction[i];
            }
        }

        public Vector3 GetPosition()
        {
            return new Vector3(Position);
        }

        public Vector3 GetDirection()
        {
            return new Vector3(Direction);
        }
    }
}
