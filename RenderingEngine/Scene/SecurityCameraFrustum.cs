using System;
using System.Collections.Generic;
using Mogre;

namespace RenderingEngine.Scene
{
    public class SecurityCameraFrustum
    {
        //A,B,C,D,E,F,G,H
        //0,1,2,3,4,5,6,7
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

        private readonly List<Vector3> mCorners = new List<Vector3>();
        public string Name { private set; get; }
        public ManualObject FrustumManualObject { private set; get; }
        public SceneNode FrustumSceneNode{ private set; get; }
        public SecurityCameraFrustum(String name, List<Vector3> corners)
        {
            mCorners = corners;
            Name = name;

            FrustumManualObject = Engine.Engine.Instance.SceneManager.CreateManualObject(name);

            DrawFrustum();

            FrustumSceneNode = Engine.Engine.Instance.SceneManager.RootSceneNode.CreateChildSceneNode();
            FrustumSceneNode.AttachObject(FrustumManualObject);
        }

        private void DrawFrustum()
        {
            FrustumManualObject.Begin("Frustum", RenderOperation.OperationTypes.OT_TRIANGLE_STRIP);
                FrustumManualObject.Colour(0,0,1,0.5f);

                FrustumManualObject.Position(mCorners[0]);
                FrustumManualObject.Position(mCorners[1]);
                FrustumManualObject.Position(mCorners[2]);
                FrustumManualObject.Position(mCorners[3]);

                FrustumManualObject.Position(mCorners[4]);
                FrustumManualObject.Position(mCorners[5]);
                FrustumManualObject.Position(mCorners[6]);
                FrustumManualObject.Position(mCorners[7]);

                foreach (var index in mIndices){ FrustumManualObject.Index(index); }

            FrustumManualObject.End();
        }
    }
}
