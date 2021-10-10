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
            //How to change mario's position
            game.GetMario.Position = new Vector2(game.GetMario.Position.X - 10, game.GetMario.Position.Y);
            //game.GetMario.WalkingTransition();
            Debug.WriteLine("LeftCommand, facing {0}\n AState {1}\n", game.GetMario.Facing, game.GetMario.marioActionState);
        }

        public void Unexecute()
        {
            
        }
    }
}