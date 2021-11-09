using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using GameSpace.States.MarioStates;
using Microsoft.Xna.Framework;
using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Machines;
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

        public abstract void DamageTransition();
        public abstract void DeadTransition();

        public abstract void Update(GameTime gametime);

    }

    public class SmallMarioState : MarioPowerUpStates
    {
        public SmallMarioState(Mario mario)
            : base(mario)
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

            Mario.Position = new Vector2((int)Mario.Position.X, (int)Mario.Position.Y - 32);
            //Mario.marioPowerUpState.Enter(this);
            // if(Mario.Velocity.Y )
            Mario.marioActionState.BigPowerUp();

        }

        public override void fireMarioTransformation()
        {
            Mario.marioPowerUpState = new FireMarioState(Mario);
            Mario.Position = new Vector2((int)Mario.Position.X, (int)Mario.Position.Y - 32);
            Mario.marioActionState.FirePowerUp();
        }

        public override void DamageTransition()
        {
            Mario.marioPowerUpState = new DeadMarioState(Mario);
            Mario.marioActionState.DeadPowerUp();
            Mario.marioPowerUpState.Enter(this);

        }
        public override void DeadTransition()
        {
            Mario.marioPowerUpState = new DeadMarioState(Mario);
            Mario.marioActionState.DeadPowerUp();
            Mario.marioPowerUpState.Enter(this);
        }

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
            Mario.Position = new Vector2((int)Mario.Position.X, (int)Mario.Position.Y + 32);
            Mario.marioActionState.SmallPowerUp();
        }

        public override void bigMarioTransformation() { }

        public override void fireMarioTransformation()
        {
            Mario.marioPowerUpState = new FireMarioState(Mario);
            Mario.Position = new Vector2((int)Mario.Position.X, (int)Mario.Position.Y);
            Mario.marioActionState.FirePowerUp();
        }

        public override void DeadTransition()
        {
            //Temp
            Mario.marioPowerUpState = new SmallMarioState(Mario);
            Mario.Position = new Vector2((int)Mario.Position.X, (int)Mario.Position.Y + 32);
            Mario.marioActionState.SmallPowerUp();

            Mario.marioPowerUpState = new DeadMarioState(Mario);
            Mario.marioActionState.DeadPowerUp();
            Mario.marioPowerUpState.Enter(this);
            //Mario.marioPowerUpState = new SmallMarioState(Mario);
            //Mario.Position = new Vector2((int)Mario.Position.X, (int)Mario.Position.Y + 32);
            //Mario.marioActionState.SmallPowerUp();

        }
        public override void DamageTransition()
        {
            //Mario.marioPowerUpState = new DeadMarioState(Mario);
            //Mario.marioActionState.DeadPowerUp();

            Mario.marioPowerUpState = new SmallMarioState(Mario);
            Mario.Position = new Vector2((int)Mario.Position.X, (int)Mario.Position.Y + 32);
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
            Mario.Position = new Vector2((int)Mario.Position.X, (int)Mario.Position.Y + 32);
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
            //Temp
            Mario.marioPowerUpState = new SmallMarioState(Mario);
            Mario.Position = new Vector2((int)Mario.Position.X, (int)Mario.Position.Y + 32);
            Mario.marioActionState.SmallPowerUp();

            Mario.marioPowerUpState = new DeadMarioState(Mario);
            Mario.marioActionState.DeadPowerUp();
            Mario.marioPowerUpState.Enter(this);
            //Goes to Big
            //Mario.marioPowerUpState = new BigMarioState(Mario);
            //Mario.marioActionState.BigPowerUp();

            //Goes to Small
            // Mario.marioPowerUpState = new SmallMarioState(Mario);
            //Mario.Position = new Vector2((int)Mario.Position.X, (int)Mario.Position.Y + 32);
            //Mario.marioActionState.SmallPowerUp();
        }

        public override void DamageTransition()
        {
            //Mario.marioPowerUpState = new DeadMarioState(Mario);
            //Mario.marioActionState.DeadPowerUp();
            Debug.Print("DamageT");
            Mario.marioPowerUpState = new SmallMarioState(Mario);
            Mario.Position = new Vector2((int)Mario.Position.X, (int)Mario.Position.Y + 32);
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
        public override void Enter(IMarioPowerUpStates previousPowerUpState)
        {
            Mario.Velocity = new Vector2(0, 0);
            MarioHandler.GetInstance().DecrementMarioLives();
            Debug.Print("DeadMarioState: Lives: {0}", MarioHandler.marioLives);
        }
        public override void Exit() { }

        public override void smallMarioTransformation()
        {
            Mario.marioPowerUpState = new SmallMarioState(Mario);
            Mario.marioActionState = new BigMarioStandingState(Mario); //Make it big mario State so I can call SmallPowerUp to get it small.
            Mario.marioActionState.SmallPowerUp();

        }

        public override void bigMarioTransformation()
        {
            Mario.marioPowerUpState = new BigMarioState(Mario);
            Mario.marioActionState = new SmallMarioStandingState(Mario); //Make it small mario State so I can call BigPowerUp to get it big.
            Mario.marioActionState.BigPowerUp();
            Mario.Position = new Vector2((int)Mario.Position.X, (int)Mario.Position.Y - 32);
        }

        public override void fireMarioTransformation()
        {
            Mario.marioPowerUpState = new FireMarioState(Mario);
            Mario.marioActionState = new SmallMarioStandingState(Mario); //Make it small mario State so I can call FirePowerUp to get it fire.
            Mario.marioActionState.FirePowerUp();
            Mario.Position = new Vector2((int)Mario.Position.X, (int)Mario.Position.Y - 32);
        }

        public override void DeadTransition() { }

        public override void DamageTransition() { }

        public override void Update(GameTime gametime) { }

    }


}
