using Mogre;

namespace RenderingEngine.Scene
{
    public class RenderModel
    {
        public const uint QueryMask = 1 << 1;
        public Entity Entity { get; private set; }
        public SceneNode SceneNode { get; private set; }

        private readonly Engine.Engine mEngine = Engine.Engine.Instance;

        public RenderModel(string name, string meshName)
        {
            Entity = mEngine.SceneManager.CreateEntity(name, meshName);
            Entity.AddQueryFlags(QueryMask);
            SceneNode = mEngine.SceneManager.RootSceneNode.CreateChildSceneNode(name + "Node");
            SceneNode.AttachObject(Entity);
            SceneNode.Scale(10,10,10);
        }

        public void ShowBoundingBox()
        {
            SceneNode.ShowBoundingBox = true;
        }

        public void HideBoundingBox()
        {
            SceneNode.ShowBoundingBox = false;
        }

        public void Translate(Vector3 t)
        {
            SceneNode.Translate(t);
        }

    }
}
