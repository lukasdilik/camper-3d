using Mogre;

namespace RenderingEngine.Scene
{
    public class RenderModel
    {
        public Entity Entity { get; private set; }
        public SceneNode SceneNode { get; private set; }

        private readonly Engine.Engine mEngine = Engine.Engine.Instance;

        public RenderModel(string name, string meshName)
        {
            Entity = mEngine.SceneManager.CreateEntity(name, meshName);
            Entity.SetMaterial(MaterialManager.Singleton.GetByName("Ogre/Compositor/BlackAndWhite"));
            
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
            SceneNode.Translate(t);
        }

    }
}
