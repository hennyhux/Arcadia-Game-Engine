using GameSpace.Interfaces;

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