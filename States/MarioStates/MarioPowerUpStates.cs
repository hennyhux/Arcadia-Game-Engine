using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using GameSpace.Machines;
using GameSpace.States.MarioStates;
using Microsoft.Xna.Framework;
using System.Diagnostics;
namespace GameSpace.States.BlockStates
{
    public abstract class MarioPowerUpStates : IMarioPowerUpStates
    {
        public Mario Mario { get; }
        public IMarioPowerUpStates previousPowerUpState { get; set; }
        protected MarioPowerUpStates(Mario mario)
        {
            Mario = mario;
        }

        public abstract void Enter(IMarioPowerUpStates previousPowerUpState);
        public abstract void Exit();
        public abstract void SmallMarioTransformation();
        public abstract void BigMarioTransformation();
        public abstract void FireMarioTransformation();
        public abstract void DamageTransition();
        public abstract void DeadTransition();
        public abstract void Update(GameTime gametime);
        internal virtual void UpdateBigCollisionBox()
        {
            Mario.Position = new Vector2((int)Mario.Position.X, (int)Mario.Position.Y - 32);
        }
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

        public override void SmallMarioTransformation() { }

        public override void BigMarioTransformation()
        {
            Mario.MarioPowerUpState = new BigMarioState(Mario);
            UpdateBigCollisionBox();
            Mario.MarioActionState.BigPowerUp();
        }

        public override void FireMarioTransformation()
        {
            Mario.MarioPowerUpState = new FireMarioState(Mario);
            UpdateBigCollisionBox();
            Mario.MarioActionState.FirePowerUp();
        }

        public override void DamageTransition()
        {
            HUDHandler.GetInstance().UpdateHealth();
            Mario.MarioActionState.SmallPowerUp();
        }
        public override void DeadTransition()
        {
            Mario.MarioPowerUpState = new DeadMarioState(Mario);
            Mario.MarioActionState.DeadPowerUp();
            Mario.MarioPowerUpState.Enter(this);
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

        public override void SmallMarioTransformation()
        {
            Mario.MarioPowerUpState = new SmallMarioState(Mario);
            Mario.Position = new Vector2((int)Mario.Position.X, (int)Mario.Position.Y + 32);
            Mario.MarioActionState.SmallPowerUp();
        }

        public override void BigMarioTransformation() { }

        public override void FireMarioTransformation()
        {
            Mario.MarioPowerUpState = new FireMarioState(Mario);
            Mario.Position = new Vector2((int)Mario.Position.X, (int)Mario.Position.Y);
            Mario.MarioActionState.FirePowerUp();
        }

        public override void DeadTransition()
        {

            Mario.MarioPowerUpState = new SmallMarioState(Mario);
            Mario.Position = new Vector2((int)Mario.Position.X, (int)Mario.Position.Y + 32);
            Mario.MarioActionState.SmallPowerUp();

            Mario.MarioPowerUpState = new DeadMarioState(Mario);
            Mario.MarioActionState.DeadPowerUp();
            Mario.MarioPowerUpState.Enter(this);
        }
        public override void DamageTransition()
        {

            Mario.MarioPowerUpState = new SmallMarioState(Mario);
            Mario.Position = new Vector2((int)Mario.Position.X, (int)Mario.Position.Y + 32);
            Mario.MarioActionState.SmallPowerUp();

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

        public override void SmallMarioTransformation()
        {
            Mario.MarioPowerUpState = new SmallMarioState(Mario);
            Mario.Position = new Vector2((int)Mario.Position.X, (int)Mario.Position.Y + 32);
            Mario.MarioActionState.SmallPowerUp();
        }

        public override void BigMarioTransformation()
        {

            Mario.MarioPowerUpState = new BigMarioState(Mario);
            Mario.MarioActionState.BigPowerUp();
        }

        public override void FireMarioTransformation() { }

        public override void DeadTransition()
        {
            //Temp
            Mario.MarioPowerUpState = new SmallMarioState(Mario);
            Mario.Position = new Vector2((int)Mario.Position.X, (int)Mario.Position.Y + 32);
            Mario.MarioActionState.SmallPowerUp();

            Mario.MarioPowerUpState = new DeadMarioState(Mario);
            Mario.MarioActionState.DeadPowerUp();
            Mario.MarioPowerUpState.Enter(this);

        }

        public override void DamageTransition()
        {

            Debug.Print("DamageT");
            Mario.MarioPowerUpState = new SmallMarioState(Mario);
            Mario.Position = new Vector2((int)Mario.Position.X, (int)Mario.Position.Y + 32);
            Mario.MarioActionState.SmallPowerUp();

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
        }

        public override void Exit() { }

        public override void SmallMarioTransformation()
        {
            Mario.MarioPowerUpState = new SmallMarioState(Mario);
            Mario.MarioActionState = new BigMarioStandingState(Mario);
            Mario.MarioActionState.SmallPowerUp();
        }

        public override void BigMarioTransformation()
        {
            Mario.MarioPowerUpState = new BigMarioState(Mario);
            Mario.MarioActionState = new SmallMarioStandingState(Mario);
            Mario.MarioActionState.BigPowerUp();
            Mario.Position = new Vector2((int)Mario.Position.X, (int)Mario.Position.Y - 32);
        }

        public override void FireMarioTransformation()
        {
            Mario.MarioPowerUpState = new FireMarioState(Mario);
            Mario.MarioActionState = new SmallMarioStandingState(Mario);
            Mario.MarioActionState.FirePowerUp();
            Mario.Position = new Vector2((int)Mario.Position.X, (int)Mario.Position.Y - 32);
        }

        public override void DeadTransition() { }

        public override void DamageTransition() { }

        public override void Update(GameTime gametime) { }

    }


}
