using GameSpace.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameSpace.Commands
{
    public class ShowHiddenBlockCommand : ICommand
    {
        private protected Game1 game;

        public ShowHiddenBlockCommand(Game1 game)
        {
            this.game = game;

        }
        public void Execute()
        {
            game.Objects.ElementAt<IGameObjects>(5).Trigger();
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}

