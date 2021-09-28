using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Commands
{
    public class StateDeadMarioCommand : ICommand
    {
        private protected Game1 game;

        public StateDeadMarioCommand(Game1 game)
        {
            this.game = game;
        }
        public void Execute()
        {
            game.GetMario.Dead();
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}
