
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSpace
{
    public class ExitCommand: ICommand
    {
        private protected Game1 game;
        public ExitCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            this.game.Exit();
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
