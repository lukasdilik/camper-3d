using Mogre;

namespace RenderingEngine.Helpers
{
    public class CameraMan
    {
        private readonly Camera mCamera;
        private bool mGoingForward;
        private bool mGoingBack;
        private bool mGoingRight;
        private bool mGoingLeft;
        private bool mGoingUp;
        private bool mGoingDown;
        private bool mFastMove;
        private int mOldX, mOldY;

        public CameraMan(Camera camera)
        {
            mCamera = camera;
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

            var move = Vector3.ZERO;
            if (mGoingForward)
                move += mCamera.Direction;
            if (mGoingBack)
                move -= mCamera.Direction;
            if (mGoingRight)
                move += mCamera.Right;
            if (mGoingLeft)
                move -= mCamera.Right;
            if (mGoingUp)
                move += mCamera.Up;
            if (mGoingDown)
                move -= mCamera.Up;

            move.Normalise();
            move *= 150; // Natural speed is 150 units/sec.
            if (mFastMove)
                move *= 3; // With shift button pressed, move twice as fast.

            if (move != Vector3.ZERO)
                mCamera.Move(move * timeFragment);
        }

        public void Click(int x, int y)
        {
            mOldX = x;
            mOldY = y;
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
