using GameSpace.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GameSpace
{
    public class ChangeBlockCommand : ICommand
    {
        private protected Game1 game;

        public ChangeBlockCommand(Game1 game)
        {
            this.game = game;

        }
        public void Execute()
        {
            game.Objects.ElementAt<IGameObjects>(0).Trigger();
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}