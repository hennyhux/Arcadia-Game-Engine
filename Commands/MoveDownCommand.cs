using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Commands
{
    public class MoveDownCommand : ICommand
    {
        private Game1 game;
        public MoveDownCommand(Game1 game)
        {
            this.game = game;
        }
        public void Execute()
        {
            game.GetMario.Idle();
            game.GetMario.Crouch();
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
