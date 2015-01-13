using System.Collections.Generic;
using System.Dynamic;
using Mogre;

namespace RenderingEngine.Scene
{
    public class SecurityCameraFrustum
    {
        private readonly uint[] mIndices = { 
                                               1, 2, 5, 
                                               2, 5, 6,
                                               5, 6, 7,
                                               6, 7, 2,
                                               7, 2, 3,
                                               2, 3, 1,
                                               3, 1, 0,
                                               1, 0, 5,
                                               0, 5, 4,
                                               5, 4, 7,
                                               4, 7, 0,
                                               7, 0, 3
                                             };

        private List<Vector3> mCorners = new List<Vector3>();
        private readonly SecurityCamera mParentCamera;
        public string Name { private set; get; }
        public ManualObject ManualObject { private set; get; }
        public SceneNode SceneNode{ private set; get; }

        public SecurityCameraFrustum(SecurityCamera parentCamera)
        {
            mParentCamera = parentCamera;
            Name = parentCamera.Name + "Frustum";

            ManualObject = Engine.Engine.Instance.SceneManager.CreateManualObject(Name);
            SceneNode = Engine.Engine.Instance.SceneManager.RootSceneNode.CreateChildSceneNode(Name + "_node");
            
            CreateMaterial();
            DrawFrustum();

            SceneNode.AttachObject(ManualObject);
        }

        private unsafe void DrawFrustum()
        {
            mCorners = GetWorldSpaceCorners(mParentCamera.Camera.WorldSpaceCorners);
            ManualObject.Begin("frustum_material", RenderOperation.OperationTypes.OT_TRIANGLE_STRIP);
                ManualObject.Colour(0,0,1,0.5f);

                ManualObject.Position(mCorners[0]);
                ManualObject.Position(mCorners[1]);
                ManualObject.Position(mCorners[2]);
                ManualObject.Position(mCorners[3]);

                ManualObject.Position(mCorners[4]);
                ManualObject.Position(mCorners[5]);
                ManualObject.Position(mCorners[6]);
                ManualObject.Position(mCorners[7]);

                foreach (var index in mIndices){ ManualObject.Index(index); }

            ManualObject.End();
        }

        private unsafe List<Vector3> GetWorldSpaceCorners(Vector3* corners)
        {
            var worldSpaceCorners = new List<Vector3>();
            for (int i = 0; i < 8; i++)
            {
                worldSpaceCorners.Add(corners[i]);
            }
            return worldSpaceCorners;
        }

        private void CreateMaterial()
        {
            const string resourceGroupName = "default";
            if (ResourceGroupManager.Singleton.ResourceGroupExists(resourceGroupName) == false)
            {
                ResourceGroupManager.Singleton.CreateResourceGroup(resourceGroupName);
            }
            MaterialPtr moMaterial = MaterialManager.Singleton.Create("frustum_material", resourceGroupName);
            moMaterial.ReceiveShadows = false;
            moMaterial.GetTechnique(0).SetSceneBlending(SceneBlendType.SBT_TRANSPARENT_ALPHA);
            moMaterial.GetTechnique(0).SetLightingEnabled(true);
            moMaterial.GetTechnique(0).GetPass(0).SetDiffuse(0, 0, 1, 0.5f);
            moMaterial.GetTechnique(0).GetPass(0).SetAmbient(0, 0, 1);
            moMaterial.GetTechnique(0).GetPass(0).SetSelfIllumination(0, 0, 1);
            moMaterial.Dispose(); 
        }

        public void Delete()
        {
            SceneNode.DetachAllObjects();
            SceneNode.RemoveAndDestroyAllChildren();
            Engine.Engine.Instance.SceneManager.DestroyManualObject(ManualObject);
            Engine.Engine.Instance.SceneManager.DestroySceneNode(SceneNode);
        }
    }
}
