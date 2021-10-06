using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Commands
{
    public class StateFireMarioCommand : ICommand
    {
        private protected GameRoot game;

        public StateFireMarioCommand(GameRoot game)
        {
            this.game = game;
        }
        public void Execute()
        {
            game.GetMario.Fire();
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}
