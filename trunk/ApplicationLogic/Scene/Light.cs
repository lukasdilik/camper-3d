using System;
using Mogre;
using RenderingEngine.Engine;

namespace ApplicationLogic.Scene
{
    abstract class Light
    {
        public Entity Mesh;
        public SceneNode SceneNode;
        public LightProperties Properties;
        public String Name;
        private bool mSelected;
        public bool Selected
        {
            get { return Selected; }
            set
            {
                SceneNode.ShowBoundingBox = value;
                Selected = value;
            }
        }

        protected Light(string lightName, string meshName, LightProperties properties)
        {
            Properties = properties;
            Name = lightName;
            Mesh = Engine.Instance.SceneManager.CreateEntity(lightName, meshName);
            SceneNode = Engine.Instance.SceneManager.RootSceneNode.CreateChildSceneNode(lightName + "_node");
            SceneNode.AttachObject(Mesh);
            SceneNode.Position = properties.Position;
        }

        public abstract void AddLight();
    }
}
