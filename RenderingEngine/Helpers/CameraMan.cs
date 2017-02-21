using Mogre;
using RenderingEngine.Scene;
using Camera = Mogre.Camera;

namespace RenderingEngine.Helpers
{
    public class CameraMan
    {
        private readonly SceneManager mSceneManager = Engine.Engine.Instance.SceneManager;
        private readonly CollisionTools mCollisionTools = CollisionTools.Instance;
        private readonly Camera mCamera;
        private bool mGoingForward;
        private bool mGoingBack;
        private bool mGoingRight;
        private bool mGoingLeft;
        private bool mGoingUp;
        private bool mGoingDown;
        private bool mFastMove;
        private int mOldX, mOldY;
        private Light spotlight;
        private Vector3 previousPosition = new Vector3();

        public CameraMan(Camera camera)
        {
            mCamera = camera;
            previousPosition = camera.Position;
            spotlight = LightManager.Instance.CreateSpotLight("cameraMan_light",camera.Position, camera.Direction, ColourValue.White, new Degree(180), new Degree(180));
        }

        public bool Pressed { get; set; }


        public bool GoingForward
        {
            set { mGoingForward = value; }
            get { return mGoingForward; }
        }

        public bool GoingBack
        {
            set { mGoingBack = value; }
            get { return mGoingBack; }
        }

        public bool GoingLeft
        {
            set { mGoingLeft = value; }
            get { return mGoingLeft; }
        }

        public bool GoingRight
        {
            set { mGoingRight = value; }
            get { return mGoingRight; }
        }

        public bool GoingUp
        {
            set { mGoingUp = value; }
            get { return mGoingUp; }
        }

        public bool GoingDown
        {
            set { mGoingDown = value; }
            get { return mGoingDown; }
        }

        public bool FastMove
        {
            set { mFastMove = value; }
            get { return mFastMove; }
        }

        public void UpdateCamera(float timeFragment)
        {
            float dist = 20;
            previousPosition = mCamera.Position;
 

            var move = Vector3.ZERO;
            if (mGoingForward)
            {
                bool isHit = isHitByModel(mCamera.Position + mCamera.Direction * dist);
                if (isHit)
                {
                    Engine.Engine.Instance.ApplicationLogic.LogMessage("HIT"+mCamera.Position.ToString());
                    return;
                }

                move += mCamera.Direction;        
            }

            if (mGoingBack)
            {
                bool isHit = isHitByModel(mCamera.Position - mCamera.Direction * dist);
                if (isHit)
                {
                    Engine.Engine.Instance.ApplicationLogic.LogMessage("HIT" + mCamera.Position.ToString());
                    return;
                }
                move -= mCamera.Direction;
            }

            if (mGoingRight)
            {
                bool isHit = isHitByModel(mCamera.Position + mCamera.Right * dist);
                if (isHit)
                {
                    Engine.Engine.Instance.ApplicationLogic.LogMessage("HIT" + mCamera.Position.ToString());
                    return;
                }
                move += mCamera.Right;
            }

            if (mGoingLeft)
            {
                bool isHit = isHitByModel(mCamera.Position - mCamera.Right * dist);
                if (isHit)
                {
                    Engine.Engine.Instance.ApplicationLogic.LogMessage("HIT" + mCamera.Position.ToString());
                    return;
                }
                move -= mCamera.Right;
            }

            if (mGoingUp)
            {
                bool isHit = isHitByModel(mCamera.Position + mCamera.Up * dist);
                if (isHit)
                {
                    Engine.Engine.Instance.ApplicationLogic.LogMessage("HIT" + mCamera.Position.ToString());
                    return;
                }
                move += mCamera.Up;
            }

            if (mGoingDown)
            {
                bool isHit = isHitByModel(mCamera.Position - mCamera.Up * dist);
                if (isHit)
                {
                    Engine.Engine.Instance.ApplicationLogic.LogMessage("HIT" + mCamera.Position.ToString());
                    return;
                }
                move -= mCamera.Up;
            }
                

            move.Normalise();
            move *= 150; // Natural speed is 150 units/sec.
            if (mFastMove)
                move *= 3; // With shift button pressed, move twice as fast.

            if (move != Vector3.ZERO)
            {
                mCamera.Move(move * timeFragment);
                updateLight();
            }

        }

        private bool isHitByModel(Vector3 to)
        {
            Vector3 from = mCamera.Position;
            bool isHit = CollisionTools.Instance.CollidesWithEntity(from, to, 1, 0,
                RenderModel.QueryMask);
            return isHit;

        }

        public void Click(int x, int y)
        {
            mOldX = x;
            mOldY = y;
        }

        private void updateLight(){
            spotlight.SetDirection(mCamera.Direction.x, mCamera.Direction.y, mCamera.Direction.z);
            spotlight.SetPosition(mCamera.Position.x, mCamera.Position.y, mCamera.Position.z);
        }

        public void MouseMovement(int x, int y)
        {

            int dX = x - mOldX;
            int dY = y - mOldY;

            mCamera.Yaw(new Degree(-dX * 0.15f));
            mCamera.Pitch(new Degree(-dY * 0.15f));

            mOldX = x;
            mOldY = y;
        }
    }
}
