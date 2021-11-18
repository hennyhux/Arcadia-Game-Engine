using Microsoft.Xna.Framework;


namespace GameSpace.Interfaces
{
    public interface IMarioActionStates
    {

        //Kinematics PreviousKinematics { get; set; }
        public IMarioActionStates previousActionState { get; set; }

        void Enter(IMarioActionStates previousActionState);
        void Exit();

        void StandingTransition();
        void CrouchingTransition();
        void WalkingTransition();
        void RunningTransition();//Longer you hold running you increase velocity and speed of animation
        void JumpingTransition();
        void FallingTransition();
        void LeapTransition();

        void FaceLeftTransition();
        void FaceRightTransition();

        public abstract void UpTransition();
        public abstract void DownTransition();

        void SmallPowerUp();
        void BigPowerUp();
        void FirePowerUp();
        void DeadPowerUp();

        void CrouchingDiscontinueTransition();//when you exit crouch, release down key
        void FaceLeftDiscontinueTransition();//generic entering walk and run, face left then start walking, then start running
        void FaceRightDiscontinueTransition();
        void WalkingDiscontinueTransition();//decelerata and go to standing
        void RunningDiscontinueTransition();//decelerate and go to walking dis
        void JumpingDiscontinueTransition();//abort jump or force jump to disc bc you reached apex of jump

        void Update(GameTime gametime);
        //void Update(GameTime gametime, GraphicsDeviceManager graphics);

        //Vector2 ClampVelocity(Vector2 velocity); // max velocity speed, clamp for each state speed
    }
}
