using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;

namespace GameSpace.States.MarioStates
{
    internal class FireMarioRunningState : MarioActionStates//MarioPowerUpStates
    {
        public FireMarioRunningState(Mario mario)
            : base(mario)
        {

        }

        public override void Enter(IMarioActionStates previousActionState)
        {
            Mario.MarioActionState = this;
            this.previousActionState = previousActionState;
            //Mario.marioPowerUpState = new FireMarioState(Mario);
            //Debug.WriteLine("MarioStandState(25) Enter, {0}", Mario.marioActionState);
            //Debug.WriteLine("MarioWalkingState(25) facing:, {0}", Mario.Facing);
            if (Mario.Facing == MarioDirection.LEFT)//Set Proper velocity upon entering state
            {
                Mario.Velocity = new Vector2(-140, 0);
            }
            else if (Mario.Facing == MarioDirection.RIGHT)
            {
                Mario.Velocity = new Vector2(140, 0);
            }
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
         //currentActionState.Exit();
            /// Debug.WriteLine("Fire Standtrans");

            Mario.MarioActionState = new FireMarioStandingState(Mario);
            // Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(2);
            Mario.MarioActionState.Enter(this); // Changing states

        }
        public override void CrouchingTransition()
        {
            Exit();
            Mario.MarioActionState = new FireMarioCrouchingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(3);
            Mario.MarioActionState.Enter(this); // Changing states
        }//nothing
        public override void WalkingTransition()//Not Used Now, Used after Sprint2
        {

        }
        public override void RunningTransition()
        { }

        public override void JumpingTransition()
        {
            Exit();
            Mario.MarioActionState = new FireMarioJumpingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(5);
            Mario.MarioActionState.Enter(this); // Changing states
        }
        public override void FallingTransition()
        {
            Exit();
            Mario.MarioActionState = new FireMarioFallingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(6);
            Mario.MarioActionState.Enter(this); // Changing states
        }

        public override void FaceLeftTransition()
        {
            if (Mario.Facing == MarioDirection.LEFT)//running, want left, if we face left, increase velocity
            {//Increase Velocity
             // Debug.WriteLine("SmallRunning(107) Run/Face Left, Increase(-) Velocity");
            }
            else
            {
                //WalkingTransition();//if face right, start walking(Or idle)
                StandingTransition();
            }
        }
        public override void FaceRightTransition()
        {

            if (Mario.Facing == MarioDirection.RIGHT)
            {//incease Velocity
                //Debug.WriteLine("SmallRunning(107) Run/Face Right, Increase(+) Velocity");
            }
            //WalkingTransition();

            else
            {
                //WalkingTransition();//if face left, start walking(Or idle)
                StandingTransition();
            }
        }

        public override void UpTransition()
        {
            JumpingTransition();
        }
        public override void DownTransition()
        {

        }

        public override void SmallPowerUp()
        {
            Exit();
            Mario.MarioActionState = new SmallMarioRunningState(Mario);
            Mario.MarioActionState.Enter(this);
        }
        public override void BigPowerUp()
        {
            Exit();
            Mario.MarioActionState = new BigMarioRunningState(Mario);
            Mario.MarioActionState.Enter(this);
        }
        public override void FirePowerUp()
        {

        }
        public override void DeadPowerUp()
        {

        }

        public override void CrouchingDiscontinueTransition() { }//when you exit crouch, release down key
        public override void FaceLeftDiscontinueTransition() { }//generic entering walk and run, face left then start walking, then start running
        public override void FaceRightDiscontinueTransition() { }
        public override void WalkingDiscontinueTransition() { }//decelerata and go to standing
        public override void RunningDiscontinueTransition()
        {
            Exit();
            Mario.MarioActionState = new FireMarioStandingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            // Mario.sprite = MarioFactory.GetInstance().CreateSprite(3);
            Mario.MarioActionState.Enter(this); // Changing states
                                                //Mario.marioActionState.SmallMarioWalkingState.Enter(this);
        }//decelerate and go to walking dis
        public override void JumpingDiscontinueTransition() { }//abort jump or force jump to disc bc you reached apex of jump

        public override void Update(GameTime gametime)
        {
            //something with velocity
            Mario.Velocity += Mario.Acceleration * (float)gametime.ElapsedGameTime.TotalSeconds;
            Mario.Velocity = ClampVelocity(Mario.Velocity);
        }

        //void Update(GameTime gametime, GraphicsDeviceManager graphics);

        private Vector2 ClampVelocity(Vector2 velocity)
        {
            return new Vector2(Mario.Velocity.X, 0);
        }
        // max velocity speed, clamp for each state speed
    }
}
