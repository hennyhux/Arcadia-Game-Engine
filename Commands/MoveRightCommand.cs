using Microsoft.Xna.Framework;

namespace GameSpace
{
    public class MoveRightCommand : ICommand
    {
        private protected Game1 game;

        public MoveRightCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.GetMarioSprite.UpdateLocation(20, 0);
        }

        public void Unexecute()
        {
            
        }
    }
}