using GameSpace.States.MarioStates;
using GameSpace.States;
namespace GameSpace.Commands
{
    public class StateDeadMarioCommand : ICommand
    {
        private protected GameRoot game;

        public StateDeadMarioCommand(GameRoot game)
        {
            this.game = game;
        }
        public void Execute()
        {
            //game.GetMario.Dead();
            //game.GetMario.DeadTransition();
            //if (!(game.GetMario.marioActionState.previousActionState is GameSpace.States.MarioStates.DeadMarioState && game.GetMario.marioActionState is GameSpace.States.MarioStates.SmallMarioStandingState))
            //{
            game.GetMario.DamageTransition();
            //}


        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}
