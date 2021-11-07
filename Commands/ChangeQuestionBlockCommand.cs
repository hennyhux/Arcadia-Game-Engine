using GameSpace.Interfaces;
using GameSpace.Machines;

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
            MarioHandler.GetInstance().WarpMarioToHiddenRoom();
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}