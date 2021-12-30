using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using GameSpace.Machines;
using Microsoft.Xna.Framework;


namespace GameSpace.States.MarioStates
{
    internal class BigMarioJumpingState : MarioActionStates
    {
        public BigMarioJumpingState(Mario mario)
            : base(mario)
        {

        }

        public override void Enter(IMarioActionStates previousActionState)
        {
            Mario.MarioActionState = this;
            this.previousActionState = previousActionState;
            Mario.Velocity = new Vector2(Mario.Velocity.X, -400);
            Mario.sprite = MarioFactory.GetInstance().CreateSprite(MarioFactory.MarioSpriteType(this, Mario.MarioPowerUpState));
            MusicHandler.GetInstance().PlaySoundEffect(1);
        }
        public override void Exit()
        {
            //Mario.Acceleration = new Vector2(0, 0);
        }
        public override void StandingTransition()
        {

            Mario.MarioActionState = new BigMarioStandingState(Mario);

            Mario.MarioActionState.Enter(this);

        }
        public override void CrouchingTransition()
        {
            Exit();
            Mario.MarioActionState = new BigMarioCrouchingState(Mario);

            Mario.MarioActionState.Enter(this);
        }
        public override void WalkingTransition()
        {

        }
        public override void RunningTransition()
        {
            Exit();
            Mario.MarioActionState = new BigMarioRunningState(Mario);
            Mario.MarioActionState.Enter(this);
        }
        public override void JumpingTransition()
        {

        }
        public override void FallingTransition()
        {
            Exit();
            Mario.MarioActionState = new BigMarioFallingState(Mario);
            Mario.MarioActionState.Enter(this);
        }

        public override void FaceLeftTransition()
        {
            if (Mario.Facing == MarioDirection.LEFT)
            {
                RunningTransition();
            }

            else
            {
                Mario.Facing = MarioDirection.LEFT;
            }
        }
        public override void FaceRightTransition()
        {

            if (Mario.Facing == MarioDirection.RIGHT)
            {
                RunningTransition();
            }

            else
            {
                Mario.Facing = MarioDirection.RIGHT;
            }
        }

        public override void UpTransition()
        {

        }
        public override void DownTransition()
        {
            StandingTransition();
        }

        public override void SmallPowerUp()
        {
            Exit();
            Mario.MarioActionState = new SmallMarioJumpingState(Mario);
            Mario.MarioActionState.Enter(this);
        }
        public override void BigPowerUp() { }
        public override void FirePowerUp()
        {
            Exit();
            Mario.MarioActionState = new FireMarioJumpingState(Mario);
            Mario.MarioActionState.Enter(this);
        }
        public override void DeadPowerUp()
        {
        }
        public override void CrouchingDiscontinueTransition() { }//when you exit crouch, release down key
        public override void FaceLeftDiscontinueTransition() { }//generic entering walk and run, face left then start walking, then start running
        public override void FaceRightDiscontinueTransition() { }
        public override void WalkingDiscontinueTransition() { }//decelerata and go to standing
        public override void RunningDiscontinueTransition() { }//decelerate and go to walking dis
        public override void JumpingDiscontinueTransition() { }//abort jump or force jump to disc bc you reached apex of jump

        public override void Update(GameTime gametime)
        {
            Mario.Velocity += Mario.Acceleration * (float)gametime.ElapsedGameTime.TotalSeconds;
            Mario.Velocity = ClampVelocity(Mario.Velocity);
        }

        private Vector2 ClampVelocity(Vector2 velocity)
        {
            return new Vector2(Mario.Velocity.X, Mario.Velocity.Y);//return the actualy velocity
        }

    }
}
