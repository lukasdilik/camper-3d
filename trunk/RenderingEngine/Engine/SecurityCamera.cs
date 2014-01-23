using Mogre;

namespace RenderingEngine.Engine
{
    public class SecurityCamera
    {
        public const string MeshName = "cctvCamera.mesh";

        private bool mSelected = false;

        public bool Selected {
            get { return mSelected; }
            set
            {
                if (SceneNode != null) { SceneNode.ShowBoundingBox = value; }
                mSelected = value;
            }

        }

        public string Name { get; private set; }
        public Entity Entity { get; private set; }
        public SceneNode SceneNode { get; private set; }

        public SecurityCamera(Vector3 position)
        {
            CreateCamera(position);
        }

        private void CreateCamera(Vector3 position)
        {
            int index = Engine.Instance.SecurityCameras.Count;
            Name = "SecurityCamera" + index;

            Entity = Engine.Instance.SceneManager.CreateEntity(Name, MeshName);
            SceneNode = Engine.Instance.SceneManager.RootSceneNode.CreateChildSceneNode(Name + "Node");
            SceneNode.AttachObject(Entity);
            SceneNode.Position = position;
            Selected = true;
        }
    }
}
