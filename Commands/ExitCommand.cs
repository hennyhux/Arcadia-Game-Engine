
using System;

namespace GameSpace
{
    public class ExitCommand : ICommand
    {
        private protected GameRoot game;
        public ExitCommand(GameRoot game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Exit();
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }


}
