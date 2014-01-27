using System.Windows.Forms;
using Mogre;
using RenderingEngine.Drawing;

namespace RenderingEngine.Engine
{
    public class SecurityCamera
    {
        public const float NormalLength = 10f;
        public const float TranslationRate = 0.1f;
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
        public AxisAlignedBox BoundingBox { get; private set; }

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
            
            Selected = true;
        }

        public void RotateToDirection(Vector3 destination)
        {
            Vector3 direction = destination - SceneNode.Position;    // B-A = A->B (see vector questions above)
            Vector3 src = SceneNode.Position * Vector3.UNIT_Z;  //facing direction of this mesh is +Z
            direction.Normalise();
            Quaternion quat = src.GetRotationTo(direction);             // Get a quaternion rotation operation 
            
            SceneNode.Rotate(quat); 
        }

        private void DrawNormal()
        {
            Draw.Instance.Color = new Vector3(0, 0, 1f);
            mNormalNode = Draw.Instance.DrawLine(SceneNode.Position, Normal);
        }

        private void Translate(Vector3 t)
        {
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
                case Keys.W: 
                    Translate(new Vector3(0,TranslationRate,0));
                    break;
                case Keys.A: 
                    Translate(new Vector3(-TranslationRate,0,0)); 
                    break;
                case Keys.S:
                    Translate(new Vector3(0,-TranslationRate, 0)); 
                    break;
                case Keys.D:
                    Translate(new Vector3(TranslationRate,0, 0));
                    break;
                case Keys.Q:
                    Translate(new Vector3(0, 0, -TranslationRate));
                    break;
                case Keys.E:
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

        public void MouseMove(MouseEventArgs e)
        {
           if (e.Button == MouseButtons.Left)
           {
               int dx = e.X - mOldX;
               int dy = e.Y - mOldY;
               SceneNode.Yaw(new Degree(dx*0.15f));
               SceneNode.Roll(new Degree(dy*0.15f));  
           }
        }
    }
}
