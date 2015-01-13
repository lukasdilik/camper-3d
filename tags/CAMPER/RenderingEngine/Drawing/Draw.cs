using System.Collections.Generic;
using Mogre;

namespace RenderingEngine.Drawing
{
    public class Draw
    {
        public const string ResourceGroupName = "drawing";
        public const string LineMaterialName = "line_material";

        private Vector3 mColor = new Vector3(0,0,0);

        private static Draw mInstance;

        public  Dictionary<string, ManualObject> Lines { get; private set; }

        public static Draw Instance
        {
            get { return mInstance ?? (mInstance = new Draw()); }
        }

        public Vector3 Color
        {
            get { return mColor; }
            set
            {
                CreateMaterial(value);
                mColor = value;
            }
        }

        private Draw()
        {
            Lines = new Dictionary<string, ManualObject>();
            CreateResourceGroup();
            CreateMaterial(mColor);
        }

        private void CreateResourceGroup()
        {
            if (!ResourceGroupManager.Singleton.ResourceGroupExists(ResourceGroupName))
            {
                ResourceGroupManager.Singleton.CreateResourceGroup(ResourceGroupName);
            }
        }

        private void CreateMaterial(Vector3 color)
        {
            if(color == mColor) return;

            MaterialPtr lineMaterial = MaterialManager.Singleton.Create(LineMaterialName, ResourceGroupName);
            lineMaterial.ReceiveShadows = false;
            lineMaterial.GetTechnique(0).SetLightingEnabled(true);
            lineMaterial.GetTechnique(0).GetPass(0).SetDiffuse(color.x,color.y,color.z,0);
            lineMaterial.GetTechnique(0).GetPass(0).SetAmbient(color.x, color.y, color.z);
            lineMaterial.GetTechnique(0).GetPass(0).SetSelfIllumination(color.x, color.y, color.z);
            lineMaterial.Dispose();  
        }

        public SceneNode DrawLine(Vector3 start, Vector3 end)
        {
            string name = "line" + Lines.Count;

            ManualObject manObj = Engine.Engine.Instance.SceneManager.CreateManualObject(name);
            manObj.Begin(LineMaterialName, RenderOperation.OperationTypes.OT_LINE_LIST);
                manObj.Position(start);
                manObj.Position(end);
            manObj.End();

            SceneNode manObjNode = Engine.Engine.Instance.SceneManager.RootSceneNode.CreateChildSceneNode(name+"_node");
            //manObjNode.SetPosition(start.x, start.y, start.z);
            
            manObjNode.AttachObject(manObj);
            Lines.Add(name,manObj);
            return manObjNode;
        }

        public void ShowLine(string name)
        {
            if (Lines.ContainsKey(name))
            {
                Engine.Engine.Instance.SceneManager.GetSceneNode(name + "_node").SetVisible(true);
            }
        }

        public void HideLine(string name)
        {
            if (Lines.ContainsKey(name))
            {
                Engine.Engine.Instance.SceneManager.GetSceneNode(name + "_node").SetVisible(false);
            }
        }

        public void RemoveLine(string name)
        {
            if (Lines.ContainsKey(name))
            {
                Engine.Engine.Instance.SceneManager.DestroyManualObject(name);
                Engine.Engine.Instance.SceneManager.DestroySceneNode(name+"_node");
                Lines.Remove(name);
            }
        }

    }
}
