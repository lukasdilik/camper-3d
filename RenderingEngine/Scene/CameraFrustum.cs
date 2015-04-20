using System.Collections.Generic;
using Mogre;

namespace RenderingEngine.Scene
{
    public class CameraFrustum
    {
        public const float FarDistance = 50f;
        public const float NearDistance = 1f;
        private readonly Camera mParentCamera;
        private string mMaterialName;
        private string mLineMaterialName;
        private string mRotatedMaterialName;
        public string Name { private set; get; }
        public ManualObject FrustumManualObject { private set; get; }
        public ColourValue Color { private set; get; }
        public SceneNode FrustumSceneNode { private set; get; }
        public Vector3 Position { private set; get; }
        public Vector3 FarCenter { private set; get; }
        //top left, top right, bottom right, bottom left
        public List<Vector3> FarPlanePoints { private set; get; }
        public List<Vector3> FarPlanePointsRotated { private set; get; }

        public CameraFrustum(Camera parentCamera)
        {
            FarPlanePoints = new List<Vector3>();
            FarPlanePointsRotated = new List<Vector3>();

            ColourValue outColor;
            mMaterialName = ColorMaterialManager.Instance.GetNextFrustumMaterialName(out outColor);
            Color = outColor;

            mLineMaterialName = ColorMaterialManager.Instance.GetSolidColorMaterialName(0, 0, 0, 1);
            mRotatedMaterialName = ColorMaterialManager.Instance.GetSemiTransparentMaterial(new ColourValue(1, 1, 0));
            
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
            Position = mParentCamera.GetCameraCenterWorld();

            Vector3 camUp = mParentCamera.MogreCamera.Up;
            Vector3 camRight = mParentCamera.MogreCamera.Right;
            FarCenter = Position + mParentCamera.MogreCamera.Direction*FarDistance;

            float farHeight = (float) (2*System.Math.Tan(mParentCamera.MogreCamera.FOVy.ValueRadians/2)*FarDistance);
            float farWidth = farHeight*mParentCamera.MogreCamera.AspectRatio;

            FarPlanePoints.Add(FarCenter + camUp * (farHeight * 0.5f) - camRight * (farWidth * 0.5f));
            FarPlanePoints.Add(FarCenter + camUp * (farHeight * 0.5f) + camRight * (farWidth * 0.5f));
            FarPlanePoints.Add(FarCenter - camUp * (farHeight * 0.5f) + camRight * (farWidth * 0.5f));
            FarPlanePoints.Add(FarCenter - camUp * (farHeight * 0.5f) - camRight * (farWidth * 0.5f));

            CalculateRotatedFrustum();
        }

        private void CalculateRotatedFrustum()
        {
            var quat = new Quaternion(new Degree(-30).ValueRadians, Vector3.UNIT_Y);
            var R = quat.ToRotationMatrix();
            var topLeft = FarPlanePoints[0];
            var bottomLeft= FarPlanePoints[3];
            topLeft *= R;
            bottomLeft *= R;
            FarPlanePointsRotated.Add(topLeft);
            FarPlanePointsRotated.Add(bottomLeft);
        }


        public void RecalculatePoints()
        {
            mParentCamera.SceneNode.RemoveAndDestroyChild(Name + "_node");
            
            FarPlanePoints.Clear();
            FarPlanePointsRotated.Clear();

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

            CreateSolidFrustum();
            CreateFrustumLines();
            //CreateSolidFrustumRotatedRight();
        }

        private void CreateSolidFrustum()
        {
            for (int i = 0; i < FarPlanePoints.Count; i++)
            {
                FrustumManualObject.Begin(mMaterialName, RenderOperation.OperationTypes.OT_TRIANGLE_LIST);
                FrustumManualObject.Position(Position);
                FrustumManualObject.Position(FarPlanePoints[i]);
                FrustumManualObject.Position(FarPlanePoints[(i + 1) % (FarPlanePoints.Count)]);
                FrustumManualObject.Triangle(2, 1, 0);
                FrustumManualObject.End();
            }

            FrustumManualObject.Begin(mMaterialName, RenderOperation.OperationTypes.OT_TRIANGLE_LIST);
            FrustumManualObject.Position(FarPlanePoints[0]);
            FrustumManualObject.Position(FarPlanePoints[1]);
            FrustumManualObject.Position(FarPlanePoints[2]);
            FrustumManualObject.Triangle(2, 1, 0);
            FrustumManualObject.End();

            FrustumManualObject.Begin(mMaterialName, RenderOperation.OperationTypes.OT_TRIANGLE_LIST);
            FrustumManualObject.Position(FarPlanePoints[2]);
            FrustumManualObject.Position(FarPlanePoints[3]);
            FrustumManualObject.Position(FarPlanePoints[0]);
            FrustumManualObject.Triangle(2, 1, 0);
            FrustumManualObject.End();
        }

        private void CreateFrustumLines()
        {
            for (int i = 0; i < FarPlanePoints.Count; i++)
            {
                FrustumManualObject.Begin(mLineMaterialName, RenderOperation.OperationTypes.OT_LINE_LIST);
                FrustumManualObject.Position(Position);
                FrustumManualObject.Position(FarPlanePoints[i]);
                FrustumManualObject.End();

                FrustumManualObject.Begin(mLineMaterialName, RenderOperation.OperationTypes.OT_LINE_LIST);
                FrustumManualObject.Position(FarPlanePoints[i]);
                FrustumManualObject.Position(FarPlanePoints[(i + 1) % (FarPlanePoints.Count)]);
                FrustumManualObject.End();
            }
        }

        private void CreateSolidFrustumRotatedRight()
        {
            FrustumManualObject.Begin(mRotatedMaterialName, RenderOperation.OperationTypes.OT_TRIANGLE_LIST);
            FrustumManualObject.Position(Position);
            FrustumManualObject.Position(FarPlanePointsRotated[0]);
            FrustumManualObject.Position(FarPlanePointsRotated[1]);
            FrustumManualObject.Triangle(2, 1, 0);
            FrustumManualObject.End();
        }

        private void TranformPointToLocalSpace()
        {
            Position = mParentCamera.SceneNode.ConvertWorldToLocalPosition(Position);
            FarCenter = mParentCamera.SceneNode.ConvertWorldToLocalPosition(FarCenter);
            for (int i = 0; i < FarPlanePoints.Count; i++)
            {
                FarPlanePoints[i] = mParentCamera.SceneNode.ConvertWorldToLocalPosition(FarPlanePoints[i]);     
            }
            for (int i = 0; i < FarPlanePointsRotated.Count; i++)
            {
                FarPlanePointsRotated[i] = mParentCamera.SceneNode.ConvertWorldToLocalPosition(FarPlanePointsRotated[i]);
            }
        }
    }
}
