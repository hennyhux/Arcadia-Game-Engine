using GameSpace.EntitiesManager;
using GameSpace.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GameSpace
{
    public class ChangeBlockCommand : ICommand
    {
        private static IGameObjects reciever;

        public ChangeBlockCommand(IGameObjects block)
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