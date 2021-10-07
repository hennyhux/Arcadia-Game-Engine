using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Commands
{
    public class StateDeadMarioCommand : ICommand
    {
        private protected GameRoot game;

        public StateDeadMarioCommand(GameRoot game)
        {
            this.game = game;
        }
        public void Execute()
        {
            //game.GetMario.Dead();
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}
