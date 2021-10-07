using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace GameSpace
{
    public class MoveLeftCommand : ICommand
    {
        private protected GameRoot game;
        public static int temp = 0;

        public MoveLeftCommand(GameRoot game)
        {
            this.game = game;
        }

        public void Execute()
        {
            
            game.GetMario.FaceLeftTransition();
            //game.GetMario.WalkingTransition();
            Debug.WriteLine("LeftCommand, facing {0}\n AState {1}\n", game.GetMario.Facing, game.GetMario.marioActionState);
        }

        public void Unexecute()
        {
            
        }
    }
}