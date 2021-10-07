﻿using System;
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
    class BigMarioStandingState : MarioActionStates//MarioPowerUpStates
    {
        public BigMarioStandingState(Mario mario)
            : base(mario)
        {

        }

        public override void Enter(IMarioActionStates previousActionState)
        {
            Mario.marioActionState = this;
            this.previousActionState = previousActionState;
            //Mario.marioPowerUpState = new BigMarioState(Mario);
            Debug.WriteLine("BigMarioStandState(25) Enter, {0}", Mario.marioActionState);
            Debug.WriteLine("BigMarioStandState(25) facing:, {0}", Mario.marioPowerUpState);

            //AABB aabb = Mario.AABB;
            //eFacing Facing = MarioStandingState.Facing;
            eFacing Facing = Mario.Facing;
            Mario.Facing = Facing;
            //Mario.Sprite = MarioStandingState.SpriteFactory.CreateSprite(MarioSpriteFactory.MarioSpriteType(this, currentPowerUpState));
            Mario.sprite = MarioFactory.GetInstance().CreateSprite(MarioFactory.MarioSpriteType(this, Mario.marioPowerUpState));

            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(1);

        }

        public override void Exit() { }

        public override void StandingTransition()
        {//going to crouch for now(going to superstand
         //currentActionState.Exit();
            /// Debug.WriteLine("Big Standtrans");

            /*Mario.marioActionState = new BigMarioStandingState(Mario);
            Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(2);
            Mario.marioActionState.Enter(this); // Changing states*/

        }
        public override void CrouchingTransition() 
        {
            Exit();
            Mario.marioActionState = new BigMarioCrouchingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(3);
            Mario.marioActionState.Enter(this); // Changing states
        }//nothing
        public override void WalkingTransition()//Not Used Now, Used after Sprint2
        {

        }
        public override void RunningTransition()
        {
            Exit();
            Mario.marioActionState = new BigMarioRunningState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(4);
            Mario.marioActionState.Enter(this); // Changing states
        } //Longer you hold running you increase velocity and speed of animation
        public override void JumpingTransition()
        {
            Exit();
            Mario.marioActionState = new BigMarioJumpingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(5);
            Mario.marioActionState.Enter(this); // Changing states
        }
        public override void FallingTransition()
        {
            Exit();
            Mario.marioActionState = new BigMarioFallingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(6);
            Mario.marioActionState.Enter(this); // Changing states
        }

        public override void FaceLeftTransition()
        {
            if (Mario.Facing == eFacing.Left)
                RunningTransition();
            // WalkingTransition(); bc no walking
            else
                Mario.Facing = eFacing.Left;
        }
        public override void FaceRightTransition()
        {

            if (Mario.Facing == eFacing.Right)
                RunningTransition();
            // WalkingTransition();
            else
                Mario.Facing = eFacing.Right;
        }
        public override void SmallPowerUp()
        {
            Exit();
            Mario.marioActionState = new SmallMarioStandingState(Mario);
            Mario.marioActionState.Enter(this);
        }
        public override void BigPowerUp() { }
        public override void FirePowerUp()
        {
            Exit();
            Mario.marioActionState = new FireMarioStandingState(Mario);
            Mario.marioActionState.Enter(this);
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

        }
        //void Update(GameTime gametime, GraphicsDeviceManager graphics);

        Vector2 ClampVelocity(Vector2 velocity)
        {
            return Vector2.Zero;
        }
        // max velocity speed, clamp for each state speed
    }
}