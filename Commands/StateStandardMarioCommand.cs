using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Commands
{
    public class StateStandardMarioCommand : ICommand
    {
        private protected GameRoot game;

        public StateStandardMarioCommand(GameRoot game)
        {
            this.game = game;
        }
        public void Execute()
        {
            game.GetMario.Small();
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}
