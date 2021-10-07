
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSpace
{
    public class ExitCommand: ICommand
    {
        private protected GameRoot game;
        public ExitCommand(GameRoot game)
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
