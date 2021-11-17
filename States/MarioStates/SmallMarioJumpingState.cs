using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using GameSpace.Machines;
using Microsoft.Xna.Framework;

namespace GameSpace.States.MarioStates
{
    internal class SmallMarioJumpingState : MarioActionStates
    {
        public SmallMarioJumpingState(Mario mario)
            : base(mario)
        {

        }

        public override void Enter(IMarioActionStates previousActionState)
        {
            Mario.MarioActionState = this;
            this.previousActionState = previousActionState;
            Mario.Velocity = new Vector2(Mario.Velocity.X, -400);
            Mario.Acceleration = new Vector2(0, 600);

            MarioDirection Facing = Mario.Facing;
            Mario.Facing = Facing;
    
            Mario.sprite = MarioFactory.GetInstance().CreateSprite(MarioFactory.MarioSpriteType(this, Mario.MarioPowerUpState));

            MusicHandler.GetInstance().PlaySoundEffect(0);
        }

        public override void Exit()
        {
            Mario.Velocity = new Vector2(Mario.Velocity.X, -15);
        }

        public override void StandingTransition()
        {
            Exit();
            Mario.MarioActionState = new SmallMarioStandingState(Mario);
       
            Mario.MarioActionState.Enter(this); 

        }
        public override void CrouchingTransition() { }
        public override void WalkingTransition()
        {

        }
        public override void RunningTransition()
        {
            Exit();
            Mario.MarioActionState = new SmallMarioRunningState(Mario);
            
            Mario.MarioActionState.Enter(this); // Changing states
        } 
        public override void JumpingTransition()
        {

        }
        public override void FallingTransition()
        {
            Exit();
            Mario.MarioActionState = new SmallMarioFallingState(Mario);
            
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

        public override void SmallPowerUp() { }
        public override void BigPowerUp()
        {
            Exit();
            Mario.MarioActionState = new BigMarioJumpingState(Mario);
            Mario.MarioActionState.Enter(this);
        }
        public override void FirePowerUp()
        {
            Exit();
            Mario.MarioActionState = new FireMarioJumpingState(Mario);
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
            Mario.MarioActionState = new SmallMarioFallingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(3);
            Mario.MarioActionState.Enter(this); // Changing states

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
