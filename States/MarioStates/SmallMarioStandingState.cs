using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;

namespace GameSpace.States.MarioStates
{
    internal class SmallMarioStandingState : MarioActionStates
    {
        public SmallMarioStandingState(Mario mario)
            : base(mario)
        {

        }

        public override void Enter(IMarioActionStates previousActionState)
        {
            Mario.MarioActionState = this;
            this.previousActionState = previousActionState;
            Mario.MarioPowerUpState = new SmallMarioState(Mario);
            Mario.sprite = MarioFactory.GetInstance().CreateSprite(MarioFactory.MarioSpriteType(this, Mario.MarioPowerUpState));
        }

        public override void Exit() { }

        public override void StandingTransition()
        {
        }
        public override void CrouchingTransition() { }//nothsing
        public override void WalkingTransition()//Not Used Now, Used after Sprint2
        {
            Exit();
            Mario.MarioActionState = new SmallMarioWalkingState(Mario);
            Mario.MarioActionState.Enter(this); // Changing states
        }
        public override void RunningTransition()
        {
            Exit();
            Mario.MarioActionState = new SmallMarioRunningState(Mario);
            Mario.MarioActionState.Enter(this); 
        }
        public override void JumpingTransition()
        {
            Exit();
            Mario.MarioActionState = new SmallMarioJumpingState(Mario);

            Mario.MarioActionState.Enter(this);
        }
        public override void FallingTransition()
        {
            Exit();
            Mario.MarioActionState = new SmallMarioFallingState(Mario);
            Mario.MarioActionState.Enter(this);
        }

        public override void UpTransition()
        {
            if (Mario.Velocity.Y > 0 && false)
            {
                Mario.Velocity = new Vector2(0, 0);
            }
            else
            {
                JumpingTransition();
            }
        }
        public override void DownTransition()
        {
            FallingTransition();
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
        public override void SmallPowerUp() { }
        public override void BigPowerUp()
        {
            Exit();
            Mario.MarioActionState = new BigMarioStandingState(Mario);
            Mario.MarioActionState.Enter(this);
        }
        public override void FirePowerUp()
        {
            Exit();
            Mario.MarioActionState = new FireMarioStandingState(Mario);
            Mario.MarioActionState.Enter(this);
        }
        public override void DeadPowerUp()
        {
            Exit();
            Mario.MarioActionState = new DeadMarioState(Mario);
            Mario.MarioActionState.Enter(this);
        }

        public override void CrouchingDiscontinueTransition() { }
        public override void FaceLeftDiscontinueTransition() { }
        public override void FaceRightDiscontinueTransition() { }
        public override void WalkingDiscontinueTransition() { }
        public override void RunningDiscontinueTransition() { }
        public override void JumpingDiscontinueTransition() { }

        public override void Update(GameTime gametime)
        {
            Mario.Velocity = ClampVelocity(Mario.Velocity);
        }

        private Vector2 ClampVelocity(Vector2 velocity)
        {
            return Vector2.Zero;
        }

    }
}
