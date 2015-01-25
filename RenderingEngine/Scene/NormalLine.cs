using Mogre;

namespace RenderingEngine.Scene
{
    public class NormalLine
    {
        public const float NormalLength = 5f;
        public const string ResourceGroupName = "drawing";
        public const string LineMaterialName = "line_material";
        
        public readonly Vector3 LineColor = new Vector3(0,0,1f);

        public string Name { get; private set; }
        public Vector3 Normal = new Vector3();

        private readonly SceneNode mParentNode;
        private SceneNode mSceneNode;

        public NormalLine(string name, Vector3 start, Vector3 normal, SceneNode parent)
        {
            Name = name;
            Normal = normal;
            mParentNode = parent;
            CreateResourceGroup();
            CreateMaterial();
            CreateLineManualObject();
        }

        private void CreateResourceGroup()
        {
            if (!ResourceGroupManager.Singleton.ResourceGroupExists(ResourceGroupName))
            {
                ResourceGroupManager.Singleton.CreateResourceGroup(ResourceGroupName);
            }
        }

        private void CreateMaterial()
        {
            if(MaterialManager.Singleton.GetByName(LineMaterialName) != null) return;

            MaterialPtr lineMaterial = MaterialManager.Singleton.Create(LineMaterialName, ResourceGroupName);
            lineMaterial.ReceiveShadows = false;
            lineMaterial.GetTechnique(0).SetLightingEnabled(true);
            lineMaterial.GetTechnique(0).GetPass(0).SetDiffuse(LineColor.x,LineColor.y,LineColor.z,0);
            lineMaterial.GetTechnique(0).GetPass(0).SetAmbient(LineColor.x, LineColor.y, LineColor.z);
            lineMaterial.GetTechnique(0).GetPass(0).SetSelfIllumination(LineColor.x, LineColor.y, LineColor.z);
            lineMaterial.Dispose();  
        }

        private void CreateLineManualObject()
        {
            ManualObject lineManualObject = Engine.Engine.Instance.SceneManager.CreateManualObject(Name);

            lineManualObject.Begin(LineMaterialName, RenderOperation.OperationTypes.OT_LINE_LIST);
                lineManualObject.Position(new Vector3(0,0,0));
                lineManualObject.Position(new Vector3(0, 0, NormalLength));
            lineManualObject.End();

            mSceneNode = mParentNode.CreateChildSceneNode(Name + "_node");
            mSceneNode.AttachObject(lineManualObject);
        }
    }
}
