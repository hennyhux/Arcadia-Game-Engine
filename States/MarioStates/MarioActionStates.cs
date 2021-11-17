using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;

namespace GameSpace.States.MarioStates
{
    public abstract class MarioActionStates : IMarioActionStates
    {
        public Mario Mario { get; }
        public IMarioActionStates previousActionState { get; set; }

        protected MarioActionStates(Mario mario)
        {

            Mario = mario;
        }

        public abstract void Enter(IMarioActionStates previousActionState);
        public abstract void Exit();

        public abstract void SmallPowerUp();
        public abstract void BigPowerUp();
        public abstract void FirePowerUp();
        public abstract void DeadPowerUp();

        public abstract void StandingTransition();
        public abstract void CrouchingTransition();
        public abstract void WalkingTransition();
        public abstract void RunningTransition();//Longer you hold running you increase velocity and speed of animation
        public abstract void JumpingTransition();
        public abstract void FallingTransition();

        public abstract void FaceLeftTransition();
        public abstract void FaceRightTransition();

        public abstract void UpTransition();
        public abstract void DownTransition();

        public abstract void CrouchingDiscontinueTransition();//when you exit crouch, release down key
        public abstract void FaceLeftDiscontinueTransition();//generic entering walk and run, face left then start walking, then start running
        public abstract void FaceRightDiscontinueTransition();
        public abstract void WalkingDiscontinueTransition();//decelerata and go to standing
        public abstract void RunningDiscontinueTransition();//decelerate and go to walking dis
        public abstract void JumpingDiscontinueTransition();//abort jump or force jump to disc bc you reached apex of jump

        public abstract void Update(GameTime gametime);

    }
}
