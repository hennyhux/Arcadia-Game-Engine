using Microsoft.Xna.Framework;

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
            game.GetMarioSprite.UpdateLocation(-20, 0);
        }

        public void Unexecute()
        {
            
        }
    }
}