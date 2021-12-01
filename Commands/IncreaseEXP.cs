using GameSpace.Machines;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Commands
{
    public class IncreaseEXP : ICommand
    {
        public IncreaseEXP()
        {

        }

        public void Execute()
        {
            HUDHandler.GetInstance().UpdateExp(100);
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
