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

namespace GameSpace.States.BlockStates
{
    public abstract class MarioPowerUpStates : IMarioPowerUpStates
    {
        //protected MarioActionStateMachine marioActionStateMachine; dont use
        //protected IMarioActionState currentActionState;
        //protected IMarioActionState previousActionState;
        //protected Kinematics previousKinematics;

        public Mario Mario { get; }

        //public IMarioActionStates CurrentActionState { get { return currentActionState; } }
        public IMarioPowerUpStates previousPowerUpState { get; set; }

        /*public Kinematics PreviousKinematics
        {a
            get { return previousKinematics; }
            set { previousKinematics = value; }
        }*/
        protected MarioPowerUpStates(Mario mario)
        {

            Mario = mario;
        }
        // protected IMarioPowerUpState CurrentPowerUpState {  get { return Mario.CurrentPowerUpState; } }

        public abstract void Enter(IMarioPowerUpStates previousPowerUpState);
        public abstract void Exit();

        public abstract void smallMarioTransformation();

        public abstract void bigMarioTransformation();

        public abstract void fireMarioTransformation();

        public abstract void DeadTransition();

       public abstract void Update(GameTime gametime);

    }

    public class SmallMarioState : MarioPowerUpStates
    {
        public SmallMarioState(Mario mario)
            :base(mario)
        {

        }
            public override void Enter(IMarioPowerUpStates previousPowerUpState) 
            { 
                
            }
            public override void Exit() { }

            public override void smallMarioTransformation() { }

            public override void bigMarioTransformation() 
            {
               // Exit();
                Mario.marioPowerUpState = new BigMarioState(Mario);
                //Mario.marioPowerUpState.Enter(this);
                Mario.marioActionState.BigPowerUp();
            
            }

            public override void fireMarioTransformation() 
            { 
                Mario.marioPowerUpState = new FireMarioState(Mario);
                Mario.marioActionState.FirePowerUp();
            }

            public override void DeadTransition() { Mario.marioPowerUpState = new DeadMarioState(Mario); }

            public override void Update(GameTime gametime) { } 
        
    }

    public class BigMarioState : MarioPowerUpStates
    {
        public BigMarioState(Mario mario)
            : base(mario)
        {

        }
        public override void Enter(IMarioPowerUpStates previousPowerUpState) 
        { 

        }
        public override void Exit() { }

        public override void smallMarioTransformation() 
        { 
            Mario.marioPowerUpState = new SmallMarioState(Mario);
            Mario.marioActionState.SmallPowerUp();
        }

        public override void bigMarioTransformation() {}

        public override void fireMarioTransformation() 
        { 
            Mario.marioPowerUpState = new FireMarioState(Mario);
            Mario.marioActionState.FirePowerUp();
        }

        public override void DeadTransition() 
        { 
            Mario.marioPowerUpState = new SmallMarioState(Mario);
            Mario.marioActionState.SmallPowerUp();
        
        }

        public override void Update(GameTime gametime) { }

    }

    public class FireMarioState : MarioPowerUpStates
    {
        public FireMarioState(Mario mario)
            : base(mario)
        {

        }
        public override void Enter(IMarioPowerUpStates previousPowerUpState) { }
        public override void Exit() { }

        public override void smallMarioTransformation() 
        { 
            Mario.marioPowerUpState = new SmallMarioState(Mario);
            Mario.marioActionState.SmallPowerUp();
        }

        public override void bigMarioTransformation() 
        { 
            Mario.marioPowerUpState = new BigMarioState(Mario);
            Mario.marioActionState.BigPowerUp();
        }

        public override void fireMarioTransformation() { }

        public override void DeadTransition() 
        {
            Mario.marioPowerUpState = new SmallMarioState(Mario);
            Mario.marioActionState.SmallPowerUp();
        }

        public override void Update(GameTime gametime) { }

    }

    public class DeadMarioState : MarioPowerUpStates
    {
        public DeadMarioState(Mario mario)
            : base(mario)
        {

        }
        public override void Enter(IMarioPowerUpStates previousPowerUpState) { }
        public override void Exit() { }

        public override void smallMarioTransformation() { Mario.marioPowerUpState = new DeadMarioState(Mario); }

        public override void bigMarioTransformation() { Mario.marioPowerUpState = new DeadMarioState(Mario); }

        public override void fireMarioTransformation() { Mario.marioPowerUpState = new DeadMarioState(Mario); }

        public override void DeadTransition() {  }

        public override void Update(GameTime gametime) { }

    }


}
