using GameSpace.Abstracts;

namespace GameSpace.Commands
{
    public class ExitCommand : RootCommand
    {
        public ExitCommand(GameRoot reciever) : base(reciever)
        {

        }

        public override void Execute()
        {
            reciever.Exit();
        }

        public override void Unexecute()
        {

        }
    }
}
