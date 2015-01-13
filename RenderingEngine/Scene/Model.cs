using Mogre;

namespace RenderingEngine.Scene
{
    public class Model
    {
        public Entity Entity { get; private set; }
        public SceneNode SceneNode { get; private set; }

        private readonly Engine.Engine mEngine = Engine.Engine.Instance;

        public Model(string name, string filePath)
        {
            Entity = mEngine.SceneManager.CreateEntity(name, filePath);
            SceneNode = mEngine.SceneManager.RootSceneNode.CreateChildSceneNode(name + "Node");
            SceneNode.AttachObject(Entity);
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
            SceneNode.Translate(new Vector3(t.x,1,t.z));
        }

    }
}
