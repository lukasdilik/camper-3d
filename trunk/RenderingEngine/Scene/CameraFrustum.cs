using Mogre;

namespace RenderingEngine.Scene
{
    public class CameraFrustum
    {
        public const float FarDistance = 50f;
        public const float NearDistance = 1f;
        private readonly Camera mParentCamera;
        public string Name { private set; get; }
        public ManualObject FrustumManualObject { private set; get; }
        public SceneNode FrustumSceneNode { private set; get; }
        public Vector3 Position { private set; get; }
        public Vector3 FarTopLeft { private set; get; }
        public Vector3 FarTopRight { private set; get; }
        public Vector3 FarBottomLeft { private set; get; }
        public Vector3 FarBottomRight { private set; get; }

        public CameraFrustum(Camera parentCamera)
        {
            mParentCamera = parentCamera;
            Position = mParentCamera.MogreCamera.Position;

            Name = parentCamera.Name + "Frustum";

            CreateMaterial();
            CalculateFarPoints();
            CreateManualObject();
            
            FrustumSceneNode = Engine.Engine.Instance.SceneManager.RootSceneNode.CreateChildSceneNode(Name + "_node");
            FrustumSceneNode.AttachObject(FrustumManualObject);
        }

        private void CalculateFarPoints()
        {
            Vector3 camUp = mParentCamera.MogreCamera.Up;
            Vector3 camRight = mParentCamera.MogreCamera.Right;
            Vector3 farCenter = Position - mParentCamera.MogreCamera.Direction * FarDistance;

            float farHeight = (float) (2 * System.Math.Tan(mParentCamera.MogreCamera.FOVy.ValueRadians / 2) * FarDistance);
            float farWidth = farHeight * mParentCamera.MogreCamera.AspectRatio;

            FarTopLeft = farCenter + camUp * (farHeight * 0.5f) - camRight * (farWidth * 0.5f);
            FarTopRight = farCenter + camUp * (farHeight * 0.5f) + camRight * (farWidth * 0.5f);
            FarBottomLeft = farCenter - camUp * (farHeight * 0.5f) - camRight * (farWidth * 0.5f);
            FarBottomRight = farCenter - camUp * (farHeight * 0.5f) + camRight * (farWidth * 0.5f);
        }

        private void CreateManualObject()
        {
            FrustumManualObject = new ManualObject(Name)
            {
                CastShadows = false,
                RenderQueueGroup = (byte) RenderQueueGroupID.RENDER_QUEUE_OVERLAY - 1
            };

            FrustumManualObject.Begin("frustum_material", RenderOperation.OperationTypes.OT_LINE_LIST);
            FrustumManualObject.Position(Position);
            FrustumManualObject.Position(FarTopRight);
            //FrustumManualObject.Position(FarLeftTop);
            FrustumManualObject.End();

            FrustumManualObject.Begin("frustum_material", RenderOperation.OperationTypes.OT_LINE_LIST);
            FrustumManualObject.Position(Position);
            FrustumManualObject.Position(FarBottomLeft);
           // FrustumManualObject.Position(FarRightBottom);
            FrustumManualObject.End();

            FrustumManualObject.Begin("frustum_material", RenderOperation.OperationTypes.OT_LINE_LIST);
            FrustumManualObject.Position(Position);
            FrustumManualObject.Position(FarBottomRight);
           // FrustumManualObject.Position(FarRightTop);
            FrustumManualObject.End();

            FrustumManualObject.Begin("frustum_material", RenderOperation.OperationTypes.OT_LINE_LIST);
            FrustumManualObject.Position(Position);
            FrustumManualObject.Position(FarTopLeft);
           // FrustumManualObject.Position(FarLeftBottom);
            FrustumManualObject.End();
        }



        private void CreateMaterial()
        {
            const string resourceGroupName = "default";
            if (!ResourceGroupManager.Singleton.ResourceGroupExists(resourceGroupName))
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
    }
}
