using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Commands
{
    public class MoveUpCommand : ICommand
    {
        private protected Game1 game;

        public MoveUpCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.GetMario.Jump();
        }

        public void Unexecute()
        {

        }
    }
}
