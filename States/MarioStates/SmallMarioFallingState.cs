using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace GameSpace.States.MarioStates
{
    internal class SmallMarioFallingState : MarioActionStates//MarioPowerUpStates
    {
        public SmallMarioFallingState(Mario mario)
            : base(mario)
        {

        }

        public override void Enter(IMarioActionStates previousActionState)
        {
            Mario.marioActionState = this;
            this.previousActionState = previousActionState;
           // Debug.WriteLine("MarioFallingState(25) Enter, {0}", Mario.marioActionState);
            //Debug.WriteLine("MarioWalkingState(25) facing:, {0}", Mario.Facing);

            //Set Proper velocity upon entering state
            Mario.Velocity = new Vector2(Mario.Velocity.X, 100);

            //AABB aabb = Mario.AABB;
            //eFacing Facing = MarioStandingState.Facing;
            eFacing Facing = Mario.Facing;
            Mario.Facing = Facing;
            //Mario.Sprite = MarioStandingState.SpriteFactory.CreateSprite(MarioSpriteFactory.MarioSpriteType(this, currentPowerUpState));

            // Mario.sprite = MarioFactory.GetInstance().CreateSprite(6);
            Mario.sprite = MarioFactory.GetInstance().CreateSprite(MarioFactory.MarioSpriteType(this, Mario.marioPowerUpState));
        }

        public override void Exit()
        {
            //Velocity == 0;
        }

        public override void StandingTransition()
        {//going to crouch for now(going to superstand
            // Debug.WriteLine("Small Standtrans");
            //if(OnGround)
            Exit();
            Mario.marioActionState = new SmallMarioStandingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(2);
            Mario.marioActionState.Enter(this); // Changing states

        }
        public override void CrouchingTransition() { }//nothing
        public override void WalkingTransition()
        {

        }
        public override void RunningTransition()
        {
            // if(OnGround)
            Exit();
            Mario.marioActionState = new SmallMarioRunningState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(4);
            Mario.marioActionState.Enter(this); // Changing states
        } //Longer you hold running you increase velocity and speed of animation
        public override void JumpingTransition()
        {

        }
        public override void FallingTransition()
        {

        }

        public override void FaceLeftTransition()
        {
            if (Mario.Facing == eFacing.LEFT)
            {
                RunningTransition();
            }
            else
            {
                Mario.Facing = eFacing.LEFT;
            }
        }
        public override void FaceRightTransition()
        {
            if (Mario.Facing == eFacing.RIGHT)
            {
                RunningTransition();
            }
            else
            {
                Mario.Facing = eFacing.RIGHT;
            }
        }

        public override void UpTransition()
        {
            StandingTransition();
        }
        public override void DownTransition()
        {
            if (previousActionState is SmallMarioJumpingState)
            {
                StandingTransition();
            }
            else
            {
                Mario.marioActionState = previousActionState;
                Mario.marioActionState.Enter(this);
            }


        }
        public override void SmallPowerUp() { }
        public override void BigPowerUp()
        {
            Exit();
            Mario.marioActionState = new BigMarioFallingState(Mario);
            Mario.marioActionState.Enter(this);
        }
        public override void FirePowerUp()
        {
            Exit();
            Mario.marioActionState = new FireMarioFallingState(Mario);
            Mario.marioActionState.Enter(this);
        }
        public override void DeadPowerUp()
        {
            Exit();
            Mario.marioActionState = new DeadMarioState(Mario);
            Mario.marioActionState.Enter(this);
        }

        public override void CrouchingDiscontinueTransition() { }//when you exit crouch, release down key
        public override void FaceLeftDiscontinueTransition() { }//generic entering walk and run, face left then start walking, then start running
        public override void FaceRightDiscontinueTransition() { }
        public override void WalkingDiscontinueTransition() { }
        public override void RunningDiscontinueTransition() { }
        public override void JumpingDiscontinueTransition() { }

        public override void Update(GameTime gametime)
        {
            Mario.Velocity += Mario.Acceleration * (float)gametime.ElapsedGameTime.TotalSeconds;
            Mario.Velocity = ClampVelocity(Mario.Velocity);
        }

        //void Update(GameTime gametime, GraphicsDeviceManager graphics);

        private Vector2 ClampVelocity(Vector2 velocity)
        {
            return new Vector2(Mario.Velocity.X, Mario.Velocity.Y);
        }
        // max velocity speed, clamp for each state speed
    }
}
