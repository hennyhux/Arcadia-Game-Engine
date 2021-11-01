using GameSpace.Interfaces;

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

