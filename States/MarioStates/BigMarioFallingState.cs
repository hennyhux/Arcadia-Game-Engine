using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;

namespace GameSpace.States.MarioStates
{
    internal class BigMarioFallingState : MarioActionStates//MarioPowerUpStates
    {
        public BigMarioFallingState(Mario mario)
            : base(mario)
        {

        }

        public override void Enter(IMarioActionStates previousActionState)
        {
            Mario.MarioActionState = this;
            this.previousActionState = previousActionState;
            Mario.Velocity = new Vector2(Mario.Velocity.X, 100);
            Mario.sprite = MarioFactory.GetInstance().CreateSprite(MarioFactory.MarioSpriteType(this, Mario.MarioPowerUpState));
        }

        public override void Exit() { }



        public override void StandingTransition()
        {
            Exit();
            Mario.MarioActionState = new BigMarioStandingState(Mario);
            Mario.MarioActionState.Enter(this);

        }
        public override void CrouchingTransition()
        {

        }
        public override void WalkingTransition()//Not Used Now, Used after Sprint2
        {

        }
        public override void RunningTransition()
        {
            Exit();
            Mario.MarioActionState = new BigMarioRunningState(Mario);

            Mario.MarioActionState.Enter(this); // Changing states
        }
        public override void JumpingTransition()
        {

        }
        public override void FallingTransition()
        {

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
            if (previousActionState is BigMarioJumpingState)
            {
                StandingTransition();
            }
            else
            {
                Mario.MarioActionState = previousActionState;
                Mario.MarioActionState.Enter(this);
            }
        }
        public override void SmallPowerUp()
        {
            Exit();
            Mario.MarioActionState = new SmallMarioFallingState(Mario);
            Mario.MarioActionState.Enter(this);
        }
        public override void BigPowerUp() { }
        public override void FirePowerUp()
        {
            Exit();
            Mario.MarioActionState = new FireMarioFallingState(Mario);
            Mario.MarioActionState.Enter(this);
        }
        public override void DeadPowerUp()
        {

        }
        public override void CrouchingDiscontinueTransition() { }
        public override void FaceLeftDiscontinueTransition() { }
        public override void FaceRightDiscontinueTransition() { }
        public override void WalkingDiscontinueTransition() { }
        public override void RunningDiscontinueTransition() { }
        public override void JumpingDiscontinueTransition() { }

        public override void Update(GameTime gametime)
        {
            Mario.Velocity += Mario.Acceleration * (float)gametime.ElapsedGameTime.TotalSeconds;
            Mario.Velocity = ClampVelocity(Mario.Velocity);
        }
        private Vector2 ClampVelocity(Vector2 velocity)
        {
            return new Vector2(Mario.Velocity.X, Mario.Velocity.Y);
        }
    }
}
