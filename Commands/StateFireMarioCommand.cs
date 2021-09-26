using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Commands
{
    public class StateFireMarioCommand : ICommand
    {
        private protected Game1 game;

        public StateFireMarioCommand(Game1 game)
        {
            this.game = game;
        }
        public void Execute()
        {
            
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}
