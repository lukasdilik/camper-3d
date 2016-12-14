using System;
using Mogre;

namespace RenderingEngine.Helpers
{
    class MoveableWidget
    {
        private readonly SceneManager mSceneManager = Engine.Engine.Instance.SceneManager;
        public void CreateWidget()
        {

            try
            {
                //sceneNodes
                SceneNode node = mSceneManager.CreateSceneNode("move_widget");
                SceneNode node_x = node.CreateChildSceneNode("move_widget_x");
                node_x.ShowBoundingBox = true;
                SceneNode node_y = node.CreateChildSceneNode("move_widget_y");
                node_y.ShowBoundingBox = true;
                SceneNode node_z = node.CreateChildSceneNode("move_widget_z");
                node_z.ShowBoundingBox = true;

                node_x.Yaw(new Degree(90));
                node_y.Pitch(new Degree(-90));

                //Materials
                MaterialPtr blue = MaterialManager.Singleton.Create("blue_widget", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);
                blue.SetSelfIllumination(0, 0, 1);
                blue.SetAmbient(0, 0, 0);
                blue.SetSpecular(0, 0, 0, 1);
                blue.SetDiffuse(0.5f, 0.5f, 0.5f, 1);

                MaterialPtr red = MaterialManager.Singleton.Create("red_widget", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);
                red.SetSelfIllumination(1, 0, 0);
                red.SetAmbient(0, 0, 0);
                red.SetSpecular(0, 0, 0, 1);
                red.SetDiffuse(0.5f, 0.5f, 0.5f, 1);
                
                MaterialPtr green = MaterialManager.Singleton.Create("green_widget", Mogre.ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);
                green.SetSelfIllumination(0, 1, 0);
                green.SetAmbient(0, 0, 0);
                green.SetSpecular(0, 0, 0, 1);
                green.SetDiffuse(0.5f, 0.5f, 0.5f, 1);

                //Entities
                Entity entityZ = mSceneManager.CreateEntity("move_widget_z", "arrow.mesh");
                Entity entityX = mSceneManager.CreateEntity("move_widget_x", "arrow.mesh");
                Entity entityY = mSceneManager.CreateEntity("move_widget_y", "arrow.mesh");

                var mPlane = new Plane(Vector3.UNIT_Y, new Vector3());
                MeshManager.Singleton.CreatePlane("dummy_plane_x", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, mPlane, 800, 800,1, 1, true, 1, 1, 1, Vector3.UNIT_X);
                mSceneManager.CreateEntity("dummy_plane_x", "dummy_plane_x");
                mSceneManager.GetEntity("dummy_plane_x").Visible = false;

                mPlane = new Plane(Vector3.UNIT_X, new Vector3());
                MeshManager.Singleton.CreatePlane("dummy_plane_z", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, mPlane, 800, 800, 1, 1, true, 1, 1, 1, Vector3.UNIT_Y);
                mSceneManager.CreateEntity("dummy_plane_z", "dummy_plane_z");
                mSceneManager.GetEntity("dummy_plane_z").Visible = false;

                mPlane = new Plane(Vector3.UNIT_Z, new Vector3());
                MeshManager.Singleton.CreatePlane("dummy_plane_y", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, mPlane, 800, 800,1, 1, true, 1, 1, 1, Vector3.UNIT_Y);
                mSceneManager.CreateEntity("dummy_plane_y", "dummy_plane_y");
                mSceneManager.GetEntity("dummy_plane_y").Visible = false;

                //ZZ arrows
                entityZ.CastShadows = false;
                entityZ.SetMaterialName("blue_widget");
                node_z.AttachObject(entityZ);

                //XX arrows
                entityX.CastShadows = false;
                entityX.SetMaterialName("red_widget");
                node_x.AttachObject(entityX);

                //YY arrows
                entityY.CastShadows = false;
                entityY.SetMaterialName("green_widget");
                node_y.AttachObject(entityY);
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception has occured while creating widgets: " + e);
            }
        }

        public MovableObject GetNode(float mouseScreenX, float mouseScreenY)
        {
            Ray mouseRay = Engine.Engine.Instance.MainCamera.GetCameraToViewportRay(mouseScreenX,mouseScreenY);
            RaySceneQuery mRaySceneQuery = new DefaultRaySceneQuery(mSceneManager);
            mRaySceneQuery.Ray = mouseRay;
            mRaySceneQuery.SetSortByDistance(true);
            RaySceneQueryResult result = mRaySceneQuery.Execute();
 
             MovableObject closestObject = null;
             double closestDistance = 100000;
 
             RaySceneQueryResult.Iterator rayIterator;
 
             for(rayIterator = result.Begin(); rayIterator != result.End(); rayIterator++ ) 
             {
                 if ((rayIterator.Value.movable != null) && (closestDistance > rayIterator.Value.distance) && rayIterator.Value.movable.MovableType != "TerrainMipMap")
                 {
                     closestObject = rayIterator.Value.movable;
                     closestDistance = rayIterator.Value.distance;
                 }
             }
 
             mRaySceneQuery.ClearResults();
             return closestObject;
         }

        public void SelectObjectForEdit(string idObject, string type)
        {
            try
            {
                SceneNode widget = mSceneManager.GetSceneNode(type);
                SceneNode node = mSceneManager.GetSceneNode(idObject);

                Vector3 scale = node.GetScale();
                if (node.Parent.GetScale() !=  Vector3.UNIT_SCALE)
                {
                    scale = node.Parent.GetScale();
                }
 
                 //size of the editable object
                AxisAlignedBox ax = node.GetAttachedObject(idObject).BoundingBox;
                Vector3 min = ax.Minimum * scale;
                Vector3 max = ax.Maximum * scale;
                Vector3 size = new Vector3(System.Math.Abs(max.x-min.x), System.Math.Abs(max.y-min.y), System.Math.Abs(max.z-min.z));
                Vector3 center = new Vector3((max.x+min.x)/2.0f,(max.y+min.y)/2.0f,(max.z+min.z)/2.0f);
 
                 float big = (size.x>size.y)?size.x:size.y;
                 big = (size.z>big)?size.z:big;
                 if (big < 1)
                     big = 1;
 
                 //size of the widgets 60
                 float size_w = big/60.0f;
 
                 widget.SetScale(2*size_w, 2*size_w, 2*size_w);
 
                 mSceneManager.RootSceneNode.AddChild(widget);
                var point = node.Position + center;
                widget.SetPosition(point.x,point.y,point.z);
                widget.SetVisible(true);
             }
             catch(Exception e)
             {
                 //
             }
    }
    }
}
