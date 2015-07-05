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
    }
}
