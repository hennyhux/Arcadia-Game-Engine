using GameSpace.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GameSpace
{
    public class ChangeQuestionBlockCommand : ICommand
    {
        private protected Game1 game;
        private List<IBlockObjects> blocks;

        public ChangeQuestionBlockCommand(Game1 game)
        {
            this.game = game;
            this.blocks = game.Blocks;

        }
        public void Execute()
        {
            game.Blocks.ElementAt<IBlockObjects>(3).Trigger();
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}