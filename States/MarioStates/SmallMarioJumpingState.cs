using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using GameSpace.Machines;

namespace GameSpace.States.MarioStates
{
    internal class SmallMarioJumpingState : MarioActionStates//MarioPowerUpStates
    {
        public SmallMarioJumpingState(Mario mario)
            : base(mario)
        {

        }

        public override void Enter(IMarioActionStates previousActionState)
        {
            Mario.marioActionState = this;
            this.previousActionState = previousActionState;
            Debug.WriteLine("MarioJumpingtate(25) Enter, {0}", Mario.marioActionState);
            Debug.WriteLine("MarioWalkingState(25) facing:, {0}", Mario.Facing);

            //Set Proper velocity upon entering state
            Mario.Velocity = new Vector2(Mario.Velocity.X, -200);//NEW


            //AABB aabb = Mario.AABB;
            //eFacing Facing = MarioStandingState.Facing;
            eFacing Facing = Mario.Facing;
            Mario.Facing = Facing;
            //Mario.Sprite = MarioStandingState.SpriteFactory.CreateSprite(MarioSpriteFactory.MarioSpriteType(this, currentPowerUpState));
            Mario.sprite = MarioFactory.GetInstance().CreateSprite(MarioFactory.MarioSpriteType(this, Mario.marioPowerUpState));
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(5);

            //play standard jumping sound effect
            MusicMachine.GetInstance().PlaySoundEffect(0);
        }

        public override void Exit()
        {
            Mario.Velocity = new Vector2(Mario.Velocity.X, -15);
        }

        public override void StandingTransition()
        {//going to crouch for now(going to superstand
         //currentActionState.Exit();
            /// Debug.WriteLine("Small Standtrans");
            Exit();
            Mario.marioActionState = new SmallMarioStandingState(Mario);
            Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(2);
            Mario.marioActionState.Enter(this); // Changing states

        }
        public override void CrouchingTransition() { }//nothing
        public override void WalkingTransition()
        {

        }
        public override void RunningTransition()
        {
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
            Exit();
            Mario.marioActionState = new SmallMarioFallingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(6);
            Mario.marioActionState.Enter(this); // Changing states
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

        }
        public override void DownTransition()
        {
            StandingTransition();
        }

        public override void SmallPowerUp() { }
        public override void BigPowerUp()
        {
            Exit();
            Mario.marioActionState = new BigMarioJumpingState(Mario);
            Mario.marioActionState.Enter(this);
        }
        public override void FirePowerUp()
        {
            Exit();
            Mario.marioActionState = new FireMarioJumpingState(Mario);
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
        public override void WalkingDiscontinueTransition()
        {
            //Exit();
            //Mario.marioActionState = new SmallMarioStandingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(2);
            //Mario.marioActionState.Enter(this); // Changing states
        }//decelerata and go to standing
        public override void RunningDiscontinueTransition()
        {
            //Exit();
            // Mario.marioActionState = new SmallMarioWalkingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(3);
            //Mario.marioActionState.Enter(this); // Changing states


        }//decelerate and go to walking dis
        public override void JumpingDiscontinueTransition()
        {
            Exit();
            Mario.marioActionState = new SmallMarioFallingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(3);
            Mario.marioActionState.Enter(this); // Changing states

        }
        //abort jump or force jump to disc bc you reached apex of jump

        public override void Update(GameTime gametime)
        {
            Mario.Velocity += Mario.Acceleration * (float)gametime.ElapsedGameTime.TotalSeconds;
            Mario.Velocity = ClampVelocity(Mario.Velocity);
        }

        //void Update(GameTime gametime, GraphicsDeviceManager graphics);

        private Vector2 ClampVelocity(Vector2 velocity)
        {
            return new Vector2(Mario.Velocity.X, Mario.Velocity.Y);//return the actualy velocity
        }
        // max velocity speed, clamp for each state speed
    }
}
