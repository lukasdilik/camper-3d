using Mogre;
using ColourValue = Mogre.ColourValue;

namespace RenderingEngine.Scene
{
    public class Line
    {
        public ColourValue LineColor = ColourValue.Blue;

        public string Name { get; private set; }
        public Vector3 P0 { get; private set; }
        public Vector3 P1 { get; private set; }

        private readonly SceneNode mParentNode;
        public SceneNode SceneNode;

        public Line(string name,Vector3 p0, Vector3 p1, SceneNode parentNode)
        {
            Name = name;
            P0 = p0;
            P1 = p1;
            mParentNode = parentNode;
            CreateLineManualObject();
        }

        public void Destroy()
        {
            mParentNode.RemoveAndDestroyChild(SceneNode.Name);
            Engine.Engine.Instance.SceneManager.DestroyManualObject(Name);
        }

        private void CreateLineManualObject()
        {
            ManualObject lineManualObject = Engine.Engine.Instance.SceneManager.CreateManualObject(Name);

            lineManualObject.Begin(ColorMaterialManager.Instance.GetSolidColorMaterialName(LineColor), RenderOperation.OperationTypes.OT_LINE_LIST);
                lineManualObject.Position(P0);
                lineManualObject.Position(P1);
            lineManualObject.End();

            SceneNode = mParentNode.CreateChildSceneNode(Name + "_node");
            SceneNode.AttachObject(lineManualObject);
        }
    }
}
