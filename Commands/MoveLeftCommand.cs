using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace GameSpace
{
    public class MoveLeftCommand : ICommand
    {
        private protected Game1 game;

        public MoveLeftCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            Debug.WriteLine("IN LEFT COMMAND: ");
            game.GetMario.FaceLeft();
            game.GetMario.Run();
            //game.GetMarioSprite.UpdateLocation(-20, 0);
        }

        public void Unexecute()
        {
            
        }
    }
}