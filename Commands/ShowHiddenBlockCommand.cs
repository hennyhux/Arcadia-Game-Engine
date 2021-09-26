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
        private List<IBlockObjects> blocks;

        public ShowHiddenBlockCommand(Game1 game)
        {
            this.game = game;
            this.blocks = game.Blocks;

        }
        public void Execute()
        {
            game.Blocks.ElementAt<IBlockObjects>(5).Trigger();
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}

