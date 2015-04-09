using System.Windows.Forms;
using Mogre;
using RenderingEngine;
using RenderingEngine.Engine;

namespace ApplicationLogic.Scene
{
    public abstract class Light
    {
        public Entity Mesh;
        public SceneNode ParentNode;
        public SceneNode SceneNode;
        public LightProperties Properties;
        
        protected Mogre.Light OgreLight;

        protected int mOldX, mOldY;

        private bool mSelected;

        public bool Selected
        {
            get { return mSelected; }
            set
            {
                SceneNode.ShowBoundingBox = value;
                mSelected = value;
            }
        }

        protected Light(SceneNode parentNode, string meshName, LightProperties properties)
        {
            ParentNode = parentNode;
            Properties = properties;
            Mesh = Engine.Instance.SceneManager.CreateEntity(properties.Name, meshName);

            var color = new Vector4(properties.Color.r, properties.Color.g, properties.Color.b, properties.Color.a);
            string materialName = ColorMaterialManager.Instance.GetSolidColorMaterialName(color);
            SetNewMaterial(materialName);
            
            SceneNode = Engine.Instance.SceneManager.RootSceneNode.CreateChildSceneNode(properties.Name + "_node");
            SceneNode.AttachObject(Mesh);

            SceneNode.Position = properties.Position;
        }

        protected void SetNewMaterial(string materialName)
        {
            MaterialPtr materialPtr = MaterialManager.Singleton.GetByName(materialName);
            if (materialPtr != null)
            {
                Mesh.SetMaterial(materialPtr);
            }
        }

        public abstract void CreateOgreLight();

        public abstract void UpdateLightProperties(LightProperties lightProperties);

        public void Translate(Vector3 t)
        {
            SceneNode.Translate(t, Node.TransformSpace.TS_WORLD);
            OgreLight.Position = SceneNode.Position;
        }



        public void Delete()
        {
            Engine.Instance.SceneManager.DestroyLight(OgreLight);
            SceneNode.ShowBoundingBox = false;
            SceneNode.RemoveAndDestroyAllChildren();
            Engine.Instance.SceneManager.DestroyEntity(Mesh);
            Engine.Instance.SceneManager.DestroySceneNode(SceneNode);
        }
        private AxisAlignedBox GetBoundingBox()
        {
            return Mesh.BoundingBox;
        }

        protected void TranslateCameraOnPolygonFace()
        {
            var aabb = GetBoundingBox();
            var center = aabb.Center;

            var dist = 2 * new Vector3(center.z, center.z, center.z);

            var mainCameradir = Engine.Instance.GetMainCameraDirection();

            var t = dist * mainCameradir;
            t = (mainCameradir.z < 0) ? -t : t;
            t = (mainCameradir.y > 0) ? -t : t;
            t = (mainCameradir.x < 0) ? -t : t;

            if (Engine.Instance.MainCamera != null)
            {
                t = t * Engine.Instance.MainCamera.Direction;
            }

            SceneNode.Translate(t);
            Properties.Position = SceneNode.Position;
            OgreLight.Position = SceneNode.Position;
        }

        protected void RotateToDirection(Vector3 destination)
        {
            Vector3 direction = destination - SceneNode.Position; // B-A = A->B (see vector questions above)
            Vector3 src = SceneNode.Position * Vector3.UNIT_Z; //facing direction of this mesh is +Z
            direction.Normalise();
            Quaternion quat = src.GetRotationTo(direction); // Get a quaternion rotation operation 

            SceneNode.Rotate(quat);
        }

        public abstract void MouseClick(MouseEventArgs e);

        public abstract void MouseMove(MouseEventArgs e);

        public abstract void Rotate(Vector2 dir);
    }
}
