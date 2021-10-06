using GameSpace.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameSpace.Commands
{
    public class ShowHiddenBlockCommand : ICommand
    {
        private static IGameObjects reciever;

        public ShowHiddenBlockCommand(IGameObjects block)
        {
            reciever = block;

        }
        public void Execute()
        {
            reciever.Trigger();
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}

