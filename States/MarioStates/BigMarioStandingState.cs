using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;

namespace GameSpace.States.MarioStates
{
    internal class BigMarioStandingState : MarioActionStates//MarioPowerUpStates
    {
        public BigMarioStandingState(Mario mario)
            : base(mario)
        {

        }

        public override void Enter(IMarioActionStates previousActionState)
        {
            Mario.MarioActionState = this;
            this.previousActionState = previousActionState;

            // Debug.WriteLine("BigMarioStandState(25) Enter, {0}", Mario.marioActionState);
            // Debug.WriteLine("BigMarioStandState(25) facing:, {0}", Mario.marioPowerUpState);

            //AABB aabb = Mario.AABB;
            //eFacing Facing = MarioStandingState.Facing;
            MarioDirection Facing = Mario.Facing;
            Mario.Facing = Facing;
            //Mario.Sprite = MarioStandingState.SpriteFactory.CreateSprite(MarioSpriteFactory.MarioSpriteType(this, currentPowerUpState));
            Mario.sprite = MarioFactory.GetInstance().CreateSprite(MarioFactory.MarioSpriteType(this, Mario.MarioPowerUpState));

            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(1);

        }

        public override void Exit() { }

        public override void StandingTransition()
        {//going to crouch for now(going to superstand




        }
        public override void CrouchingTransition()
        {


            Exit();
            Mario.MarioActionState = new BigMarioCrouchingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(3);
            Mario.MarioActionState.Enter(this); // Changing states
        }//nothing
        public override void WalkingTransition()//Not Used Now, Used after Sprint2
        {

        }
        public override void RunningTransition()
        {
            Exit();
            Mario.MarioActionState = new BigMarioRunningState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(4);
            Mario.MarioActionState.Enter(this); // Changing states
        } //Longer you hold running you increase velocity and speed of animation
        public override void JumpingTransition()
        {
            if (Mario.Velocity.Y > 0)//MOVE THIS TO UpTransition()
            {
                Mario.Velocity = new Vector2(0, 0);
            }
            else
            {
                Exit();
                Mario.MarioActionState = new BigMarioJumpingState(Mario);
                //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
                //Mario.sprite = MarioFactory.GetInstance().CreateSprite(5);
                Mario.MarioActionState.Enter(this); // Changing states
            }
        }
        public override void FallingTransition()
        {
            Exit();
            Mario.MarioActionState = new BigMarioFallingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(6);
            Mario.MarioActionState.Enter(this); // Changing states
        }

        public override void FaceLeftTransition()
        {
            if (Mario.Facing == MarioDirection.LEFT)
            {
                RunningTransition();
            }
            // WalkingTransition(); bc no walking
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
            // WalkingTransition();
            else
            {
                Mario.Facing = MarioDirection.RIGHT;
            }
        }

        public override void UpTransition()
        {
            JumpingTransition();
        }
        public override void DownTransition()
        {
            CrouchingTransition();
        }
        public override void SmallPowerUp()
        {
            Exit();
            Mario.MarioActionState = new SmallMarioStandingState(Mario);
            Mario.MarioActionState.Enter(this);
        }
        public override void BigPowerUp() { }
        public override void FirePowerUp()
        {
            Exit();
            Mario.MarioActionState = new FireMarioStandingState(Mario);
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
            Mario.Velocity = ClampVelocity(Mario.Velocity);
        }

        //void Update(GameTime gametime, GraphicsDeviceManager graphics);

        private Vector2 ClampVelocity(Vector2 velocity)
        {
            return Vector2.Zero;
        }
        // max velocity speed, clamp for each state speed
    }
}
