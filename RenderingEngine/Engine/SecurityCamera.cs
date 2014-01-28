using System.Windows.Forms;
using Mogre;
using RenderingEngine.Drawing;

namespace RenderingEngine.Engine
{
    public class SecurityCamera
    {
        public const float NormalLength = 10f;
        public const float TranslationRate = 0.2f;
        public const string MeshName = "cctv1.mesh";
        public static readonly Vector3 Scale = new Vector3(8,8,8);

        private bool mSelected = false;
        private int mOldX, mOldY;
        private SceneNode mNormalNode;

        public bool Selected {
            get { return mSelected; }
            set
            {
                if (SceneNode != null) { SceneNode.ShowBoundingBox = value; }
                mSelected = value;
            }

        }

        public string Name { get; private set; }
        public Vector3 Normal { get; private set; }
        public Entity Entity { get; private set; }
        public SceneNode SceneNode { get; private set; }

        public SecurityCamera(Vector3 position, Vector3 normal)
        {
            Normal = normal;
            Normal = position + (Normal * NormalLength);

            CreateCamera(position);
        }

        private void CreateCamera(Vector3 position)
        {
            int index = Engine.Instance.SecurityCameras.Count;
            Name = "SecurityCamera" + index;

            Entity = Engine.Instance.SceneManager.CreateEntity(Name, MeshName);
            SceneNode = Engine.Instance.SceneManager.RootSceneNode.CreateChildSceneNode(Name + "Node");
            SceneNode.AttachObject(Entity);
            SceneNode.Position = position;

            SceneNode.Scale(Scale);
            
            RotateToDirection(Normal);
            
            DrawNormal();

            TranslateCameraOnPolygonFace();

            Selected = true;
        }

        private void TranslateCameraOnPolygonFace()
        {
            var aabb = Entity.BoundingBox;
            aabb.Scale(Scale);
            var center = aabb.Center;
            var dist = 2 * new Vector3(center.z, center.z, center.z);
            var dir = Engine.Instance.GetCameraDirection();
            var t = dist * dir;
            t = (dir.z < 0) ? -t : t;
            t = (dir.y > 0) ? -t : t;
            t = (dir.x < 0) ? -t : t;
            Translate(t);
        }

        public void RotateToDirection(Vector3 destination)
        {
            Vector3 direction = destination - SceneNode.Position; // B-A = A->B (see vector questions above)
            Vector3 src = SceneNode.Position * Vector3.UNIT_Z; //facing direction of this mesh is +Z
            direction.Normalise();
            Quaternion quat = src.GetRotationTo(direction);  // Get a quaternion rotation operation 
            
            SceneNode.Rotate(quat); 
        }

        private void DrawNormal()
        {
            Draw.Instance.Color = new Vector3(0, 0, 1f);
            mNormalNode = Draw.Instance.DrawLine(SceneNode.Position, Normal);
        }

        private void Translate(Vector3 t)
        {
            if (Engine.Instance.Camera != null)
            {
                t = t*Engine.Instance.Camera.Direction;
            }
            SceneNode.Translate(t);
            if (mNormalNode != null)
            {
                mNormalNode.Translate(t);
            }

        }

        public void HandleKey(Keys key)
        {
            switch (key)
            {
                case Keys.Up: 
                    Translate(new Vector3(0,-TranslationRate,0));
                    break;
                case Keys.Left: 
                    Translate(new Vector3(-TranslationRate,0,0)); 
                    break;
                case Keys.Down:
                    Translate(new Vector3(0,TranslationRate, 0)); 
                    break;
                case Keys.Right:
                    Translate(new Vector3(TranslationRate,0, 0));
                    break;
                case Keys.Add:
                    Translate(new Vector3(0, 0, -TranslationRate));
                    break;
                case Keys.Subtract:
                    Translate(new Vector3(0,0, TranslationRate));
                    break;
            }
        }

        public void MouseClick(MouseEventArgs e)
        {
           if (e.Button == MouseButtons.Left)
           {
               mOldX = e.X;
               mOldY = e.Y;   
           }
        }

        public void Delete()
        {
            SceneNode.RemoveAndDestroyAllChildren();
        }

        public void MouseMove(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            
            int dx = e.X - mOldX;
            int dy = e.Y - mOldY;
            Vector2 dir = new Vector2(Math.Sign(dx), Math.Sign(dy));
   
            SceneNode.Pitch(new Degree(dir.y));
            SceneNode.Yaw(new Degree(dir.x));

            mOldX = e.X;
            mOldY = e.Y;
        }
    }
}
