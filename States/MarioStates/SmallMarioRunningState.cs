using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace GameSpace.States.MarioStates
{
    internal class SmallMarioRunningState : MarioActionStates
    {
        public SmallMarioRunningState(Mario mario)
            : base(mario)
        {

        }

        public override void Enter(IMarioActionStates previousActionState)
        {
            Mario.MarioActionState = this;
            this.previousActionState = previousActionState;

            if (previousActionState is SmallMarioJumpingState)
            {
                if (Mario.Facing == MarioDirection.LEFT)
                {
                    Mario.Velocity = new Vector2(-100, 0);
                }
                else if (Mario.Facing == MarioDirection.RIGHT)
                {
                    Mario.Velocity = new Vector2(100, 0);
                }
            }

            else if (!(previousActionState is SmallMarioRunningState))
            {
                if (Mario.Facing == MarioDirection.LEFT)
                {
                    Mario.Velocity = new Vector2(-140, 0);
                }
                else if (Mario.Facing == MarioDirection.RIGHT)
                {
                    Mario.Velocity = new Vector2(140, 0);
                }
            }

            Mario.sprite = MarioFactory.GetInstance().CreateSprite(MarioFactory.MarioSpriteType(this, Mario.MarioPowerUpState));
        }

        public override void Exit()
        {
            
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
            
            Mario.MarioActionState = new SmallMarioWalkingState(Mario);
           
            Mario.MarioActionState.Enter(this); 
        }
        public override void RunningTransition()
        {
            
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

        public override void FaceLeftTransition()
        {
            if (Mario.Facing == MarioDirection.LEFT)
            {
                Debug.WriteLine("SmallRunning(107) Run/Face Left, Increase(-) Velocity");
            }
            else
            {
  
                StandingTransition();
            }
        }
        public override void FaceRightTransition()
        {
            if (Mario.Facing == MarioDirection.RIGHT)
            {//incease Velocity
                Debug.WriteLine("SmallRunning(107) Run/Face Right, Increase(+) Velocity");
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

        public override void LeapTransition()
        {
            Exit();
            Mario.MarioActionState = new SmallMarioLeapingState(Mario);
            Mario.MarioActionState.Enter(this);
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

        }//decelerata and go to standing
        public override void RunningDiscontinueTransition()
        {
            Exit();
            Mario.MarioActionState = new SmallMarioWalkingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            Mario.sprite = MarioFactory.GetInstance().CreateSprite(3);
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

