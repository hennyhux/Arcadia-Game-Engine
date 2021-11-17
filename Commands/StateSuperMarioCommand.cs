using System.Diagnostics;

namespace GameSpace.Commands
{
    public class StateSuperMarioCommand : ICommand
    {
        //private IGameObjects reciever;
        private protected GameRoot game;
        public static int temp = 0;

        public StateSuperMarioCommand(GameRoot game)
        {
            this.game = game;
        }

        public void Execute()
        {
            //EntityManager.MoveBlock(0, 1);
            game.GetMario.BigMarioTransformation();
            Debug.WriteLine("BigMarioTransformation, powerUp {0}\n AState {1}\n", game.GetMario.MarioPowerUpState, game.GetMario.MarioActionState);
        }

        public void Unexecute()
        {
            //EntityManager.MoveBlock(0, -1);
        }
    }
}
