using GameSpace.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GameSpace
{
    public class ChangeQuestionBlockCommand : ICommand
    {
        private static IGameObjects reciever;

        public ChangeQuestionBlockCommand(IGameObjects block)
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