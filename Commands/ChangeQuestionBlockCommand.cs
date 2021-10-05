using GameSpace.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GameSpace
{
    public class ChangeQuestionBlockCommand : ICommand
    {
        private protected Game1 game;

        public ChangeQuestionBlockCommand(Game1 game)
        {
            this.game = game;

        }
        public void Execute()
        {
            game.Objects.ElementAt<IGameObjects>(3).Trigger();
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}