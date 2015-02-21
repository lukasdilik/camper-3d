﻿using Mogre;

namespace RenderingEngine.Scene
{
    public class Line
    {
        public Vector3 LineColor = new Vector3(0,0,1f);

        public string Name { get; private set; }
        public Vector3 P0 { get; private set; }
        public Vector3 P1 { get; private set; }

        private readonly SceneNode mParentNode;
        private SceneNode mSceneNode;

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
            mParentNode.RemoveAndDestroyChild(mSceneNode.Name);
            Engine.Engine.Instance.SceneManager.DestroyManualObject(Name);
        }

        private void CreateLineManualObject()
        {
            ManualObject lineManualObject = Engine.Engine.Instance.SceneManager.CreateManualObject(Name);

            lineManualObject.Begin(ColorMaterialManager.Instance.GetSolidColorMaterialName(LineColor), RenderOperation.OperationTypes.OT_LINE_LIST);
                lineManualObject.Position(P0);
                lineManualObject.Position(P1);
            lineManualObject.End();

            mSceneNode = mParentNode.CreateChildSceneNode(Name + "_node");
            mSceneNode.AttachObject(lineManualObject);
        }
    }
}
