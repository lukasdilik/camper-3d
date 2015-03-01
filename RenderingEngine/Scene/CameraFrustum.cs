using Mogre;

namespace RenderingEngine.Scene
{
    public class CameraFrustum
    {
        public const float FarDistance = 50f;
        public const float NearDistance = 1f;
        private readonly Camera mParentCamera;
        private string mMaterialName;
        public string Name { private set; get; }
        public ManualObject FrustumManualObject { private set; get; }
        public SceneNode FrustumSceneNode { private set; get; }
        public Vector3 Position { private set; get; }
        public Vector3 FarCenter { private set; get; }
        public Vector3 FarTopLeft { private set; get; }
        public Vector3 FarTopRight { private set; get; }
        public Vector3 FarBottomLeft { private set; get; }
        public Vector3 FarBottomRight { private set; get; }

        public CameraFrustum(Camera parentCamera)
        {
            mMaterialName = ColorMaterialManager.Instance.GetNextSolidColorMaterialName();
            mParentCamera = parentCamera;
            Position = mParentCamera.SceneNode.Position;

            Name = parentCamera.Name + "Frustum";

            CalculateFarPointsWorld();
            CreateManualObject();

            FrustumSceneNode = parentCamera.SceneNode.CreateChildSceneNode(Name + "_node");
            FrustumSceneNode.AttachObject(FrustumManualObject);
        }

        private void CalculateFarPointsWorld()
        {
            Position = mParentCamera.SceneNode.Position;

            Vector3 camUp = mParentCamera.MogreCamera.Up;
            Vector3 camRight = mParentCamera.MogreCamera.Right;
            FarCenter = Position + mParentCamera.MogreCamera.Direction*FarDistance;

            float farHeight = (float) (2*System.Math.Tan(mParentCamera.MogreCamera.FOVy.ValueRadians/2)*FarDistance);
            float farWidth = farHeight*mParentCamera.MogreCamera.AspectRatio;

            FarTopLeft = FarCenter + camUp*(farHeight*0.5f) - camRight*(farWidth*0.5f);
            FarTopRight = FarCenter + camUp*(farHeight*0.5f) + camRight*(farWidth*0.5f);
            FarBottomLeft = FarCenter - camUp*(farHeight*0.5f) - camRight*(farWidth*0.5f);
            FarBottomRight = FarCenter - camUp*(farHeight*0.5f) + camRight*(farWidth*0.5f);
        }


        public void RecalculatePoints()
        {
            mParentCamera.SceneNode.RemoveAndDestroyChild(Name + "_node");
            
            CalculateFarPointsWorld();
            CreateManualObject();

            FrustumSceneNode = mParentCamera.SceneNode.CreateChildSceneNode(Name + "_node");
            FrustumSceneNode.AttachObject(FrustumManualObject);

        }

        public void Destroy()
        {
            Engine.Engine.Instance.SceneManager.DestroyManualObject(FrustumManualObject);
        }

        private void CreateManualObject()
        {
            FrustumManualObject = new ManualObject(Name)
            {
                CastShadows = false,
                RenderQueueGroup = (byte) RenderQueueGroupID.RENDER_QUEUE_OVERLAY - 1
            };
           
            TranformPointToLocalSpace();

            FrustumManualObject.Begin(mMaterialName, RenderOperation.OperationTypes.OT_TRIANGLE_LIST);
            FrustumManualObject.Position(Position);
            FrustumManualObject.Position(FarTopRight);
            FrustumManualObject.Position(FarTopLeft);
            FrustumManualObject.Triangle(2, 1, 0);
            FrustumManualObject.End();

            FrustumManualObject.Begin(mMaterialName, RenderOperation.OperationTypes.OT_TRIANGLE_LIST);
            FrustumManualObject.Position(Position);
            FrustumManualObject.Position(FarBottomLeft);
            FrustumManualObject.Position(FarBottomRight);
            FrustumManualObject.Triangle(2, 1, 0);
            FrustumManualObject.End();

            FrustumManualObject.Begin(mMaterialName, RenderOperation.OperationTypes.OT_TRIANGLE_LIST);
            FrustumManualObject.Position(Position);
            FrustumManualObject.Position(FarBottomRight);
            FrustumManualObject.Position(FarTopRight);
            FrustumManualObject.Triangle(2, 1, 0);
            FrustumManualObject.End();

            FrustumManualObject.Begin(mMaterialName, RenderOperation.OperationTypes.OT_TRIANGLE_LIST);
            FrustumManualObject.Position(Position);
            FrustumManualObject.Position(FarTopLeft);
            FrustumManualObject.Position(FarBottomLeft);
            FrustumManualObject.Triangle(2, 1, 0);
            FrustumManualObject.End();

        }

        private void TranformPointToLocalSpace()
        {
            Position = mParentCamera.SceneNode.ConvertWorldToLocalPosition(Position);
            FarCenter = mParentCamera.SceneNode.ConvertWorldToLocalPosition(FarCenter); 
            FarTopLeft = mParentCamera.SceneNode.ConvertWorldToLocalPosition(FarTopLeft); 
            FarTopRight = mParentCamera.SceneNode.ConvertWorldToLocalPosition(FarTopRight); 
            FarBottomLeft = mParentCamera.SceneNode.ConvertWorldToLocalPosition(FarBottomLeft); 
            FarBottomRight = mParentCamera.SceneNode.ConvertWorldToLocalPosition(FarBottomRight); 
        }
    }
}
