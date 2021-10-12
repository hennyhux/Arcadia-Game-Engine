using System;
using System.Collections.Generic;
using System.Text;
using GameSpace.Interfaces;
using GameSpace.States.BlockStates;
using GameSpace.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameSpace.States.MarioStates;
using GameSpace.GameObjects.BlockObjects;
using System.Diagnostics;
using GameSpace.Enums;

namespace GameSpace.States.MarioStates
{
    class SmallMarioJumpingState : MarioActionStates//MarioPowerUpStates
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
            Mario.Velocity = new Vector2((float)0, (float)-50);


            //AABB aabb = Mario.AABB;
            //eFacing Facing = MarioStandingState.Facing;
            eFacing Facing = Mario.Facing;
            Mario.Facing = Facing;
            //Mario.Sprite = MarioStandingState.SpriteFactory.CreateSprite(MarioSpriteFactory.MarioSpriteType(this, currentPowerUpState));
            Mario.sprite = MarioFactory.GetInstance().CreateSprite(MarioFactory.MarioSpriteType(this, Mario.marioPowerUpState));
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(5);

        }

        public override void Exit()
        {
            //Velocity == 0;
            Mario.Velocity = new Vector2(Mario.Velocity.X, (float)0);
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
                WalkingTransition();
            else
                Mario.Facing = eFacing.LEFT;
        }
        public override void FaceRightTransition()
        {
            if (Mario.Facing == eFacing.RIGHT)
                WalkingTransition();
            else
                Mario.Facing = eFacing.RIGHT;
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
            //something with velocity
            /*if (Mario.marioPowerUpState != new SmallMarioState(Mario))
            {
                //if (Mario.marioActionState == new BigMarioState(Mario))
                Debug.WriteLine("SMJump(158) currentAState, {0}, currentPowerup {1}", Mario.marioActionState, Mario.marioPowerUpState);
                Debug.WriteLine("SMJump(158) currentPowerup, {0}, checkState {1}", Mario.marioPowerUpState, new BigMarioState(Mario));
                Debug.WriteLine("SMJump(158) isEqual: {0}", Mario.marioPowerUpState is BigMarioState);
                if (Mario.marioPowerUpState is BigMarioState)
                {
                    //Exit();
                    Mario.marioActionState = new BigMarioJumpingState(Mario);
                    Mario.marioActionState.Enter(this); // Changing states
                }
                else if (Mario.marioPowerUpState is FireMarioState)
                {
                    //Exit();
                    Mario.marioActionState = new FireMarioJumpingState(Mario);
                    Mario.marioActionState.Enter(this); // Changing states
                }
            }*/
        }
        //void Update(GameTime gametime, GraphicsDeviceManager graphics);

        Vector2 ClampVelocity(Vector2 velocity)
        {
            return Vector2.Zero;//return the actualy velocity
        }
        // max velocity speed, clamp for each state speed
    }
}
