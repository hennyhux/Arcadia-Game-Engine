using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using GameSpace.States.MarioStates;
using GameSpace.GameObjects.BlockObjects;
using System.Diagnostics;

namespace GameSpace.States.MarioStates
{
    public abstract class MarioActionStates : IMarioActionStates
    {
        //protected MarioActionStateMachine marioActionStateMachine; dont use
        //protected IMarioActionState currentActionState;
       // protected IMarioActionState previousActionState;
        //protected Kinematics previousKinematics;

        public Mario Mario { get; }

        //public IMarioActionStates CurrentActionState { get { return currentActionState; } }
        public IMarioActionStates previousActionState { get; set; }

        /*public Kinematics PreviousKinematics
        {a
            get { return previousKinematics; }
            set { previousKinematics = value; }
        }*/
        protected MarioActionStates(Mario mario)
        {

            Mario = mario;
        }
        // protected IMarioPowerUpState CurrentPowerUpState {  get { return Mario.CurrentPowerUpState; } }

        public abstract void Enter(IMarioActionStates previousActionState);
        public abstract void Exit();

        public abstract void SmallPowerUp();
        public abstract void BigPowerUp();
        public abstract void FirePowerUp();
        public abstract void DeadPowerUp();

        public abstract void StandingTransition();
        public abstract void CrouchingTransition();
        public abstract void WalkingTransition();
        public abstract void RunningTransition();//Longer you hold running you increase velocity and speed of animation
        public abstract void JumpingTransition();
        public abstract void FallingTransition();

        public abstract void FaceLeftTransition();
        public abstract void FaceRightTransition();

        public abstract void UpTransition();
        public abstract void DownTransition();

        public abstract void CrouchingDiscontinueTransition();//when you exit crouch, release down key
        public abstract void FaceLeftDiscontinueTransition();//generic entering walk and run, face left then start walking, then start running
        public abstract void FaceRightDiscontinueTransition();
        public abstract void WalkingDiscontinueTransition();//decelerata and go to standing
        public abstract void RunningDiscontinueTransition();//decelerate and go to walking dis
        public abstract void JumpingDiscontinueTransition();//abort jump or force jump to disc bc you reached apex of jump

        public abstract void Update(GameTime gametime);

    }
}
