using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;

namespace GameSpace.States.MarioStates
{
    internal class SmallMarioWalkingState : MarioActionStates//MarioPowerUpStates
    {
        public SmallMarioWalkingState(Mario mario)
            : base(mario)
        {

        }

        public override void Enter(IMarioActionStates previousActionState)
        {
            Mario.MarioActionState = this;
            this.previousActionState = previousActionState;
            //Mario.marioPowerUpState = new ();
            // Debug.WriteLine("MarioWalkingState(25) currentAState, {0}", Mario.marioActionState);
            // Debug.WriteLine("MarioWalkingState(25) facing:, {0}", Mario.Facing);

            //AABB aabb = 
            //eFacing Facing = MarioStandingState.Facing;
            MarioDirection Facing = Mario.Facing;
            Mario.Facing = Facing;
            Mario.sprite = MarioFactory.GetInstance().CreateSprite(MarioFactory.MarioSpriteType(this, Mario.MarioPowerUpState));

            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(3);

        }

        public override void Exit()
        {
            //Velocity == 0;
        }

        /*public override void smallMarioTransformation(){ }
         public override void bigMarioTransformation() {

         }
         public override void fireMarioTransformation()
         {
             //Mario.marioPowerUpState = new FireMarioStandingState(Mario);//CREATE FIRE MARIO STATE
         }
         public override void DeadTransition()
         {
             //Mario.marioPowerUpState = new DeadMarioState(Mario);//CREATE DEADMARIOSTATE
         }*/

        public override void StandingTransition()
        {//going to crouch for now(going to superstand
         //currentActionState.Exit();
            /// Debug.WriteLine("Small Standtrans");
            Exit();
            Mario.MarioActionState = new SmallMarioStandingState(Mario);
            // Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(2);
            Mario.MarioActionState.Enter(this); // Changing states

        }
        public override void CrouchingTransition() { }//nothing
        public override void WalkingTransition()
        {
            //Exit();
            //Mario.marioActionState = new SmallMarioWalkingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(3);
            //Mario.marioActionState.Enter(this); // Changing states
            //Mario.marioActionState.SmallMarioWalkingState.Enter(this);
        }
        public override void RunningTransition()
        {
            //Exit();
            Mario.MarioActionState = new SmallMarioRunningState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            Mario.sprite = MarioFactory.GetInstance().CreateSprite(4);
            Mario.MarioActionState.Enter(this); // Changing states
        } //Longer you hold running you increase velocity and speed of animation
        public override void JumpingTransition()
        {
            Exit();
            Mario.MarioActionState = new SmallMarioJumpingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            Mario.sprite = MarioFactory.GetInstance().CreateSprite(5);
            Mario.MarioActionState.Enter(this); // Changing states
        }
        public override void FallingTransition()
        {
            Exit();
            Mario.MarioActionState = new SmallMarioFallingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            Mario.sprite = MarioFactory.GetInstance().CreateSprite(6);
            Mario.MarioActionState.Enter(this); // Changing states
        }

        public override void FaceLeftTransition()
        {
            if (Mario.Facing == MarioDirection.LEFT)//we are walking, Want to go left, if we face left run left
            {
                RunningTransition();//then run?
            }
            else
            {
                //Mario.Facing = eFacing.Left;//we walking, want to go left, if we face right we idle
                StandingTransition();
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
                //Mario.Facing = eFacing.Right; 
                StandingTransition();
            }

        }

        public override void UpTransition()
        {

        }
        public override void DownTransition()
        {

        }

        public override void SmallPowerUp() { }
        public override void BigPowerUp()
        {
            Exit();
            Mario.MarioActionState = new BigMarioRunningState(Mario);
            Mario.MarioActionState.Enter(this);
        }
        public override void FirePowerUp()
        {
            Exit();
            Mario.MarioActionState = new FireMarioRunningState(Mario);
            Mario.MarioActionState.Enter(this);
        }
        public override void DeadPowerUp()
        {
            Exit();
            Mario.MarioActionState = new DeadMarioState(Mario);
            Mario.MarioActionState.Enter(this);
        }
        public override void CrouchingDiscontinueTransition() { }//when you exit crouch, release down key
        public override void FaceLeftDiscontinueTransition() { }//generic entering walk and run, face left then start walking, then start running
        public override void FaceRightDiscontinueTransition() { }
        public override void WalkingDiscontinueTransition()
        {
            Exit();
            Mario.MarioActionState = new SmallMarioStandingState(Mario);
            // Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(2);
            Mario.MarioActionState.Enter(this); // Changing states
        }//decelerata and go to standing
        public override void RunningDiscontinueTransition()
        {
            //Exit();
            // Mario.marioActionState = new SmallMarioWalkingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(3);
            //Mario.marioActionState.Enter(this); // Changing states
            //Mario.marioActionState.SmallMarioWalkingState.Enter(this);

        }//decelerate and go to walking dis
        public override void JumpingDiscontinueTransition() { }//abort jump or force jump to disc bc you reached apex of jump

        public override void Update(GameTime gametime)
        {
            //something with velocity
        }

        //void Update(GameTime gametime, GraphicsDeviceManager graphics);

        private Vector2 ClampVelocity(Vector2 velocity)
        {
            return Vector2.Zero;//return the actualy velocity
        }
        // max velocity speed, clamp for each state speed
    }


}
