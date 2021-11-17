namespace GameSpace.Commands
{
    public class MoveUpCommand : ICommand
    {
        private protected GameRoot game;
        public static int temp = 0;

        public MoveUpCommand(GameRoot game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.GetMario.UpTransition();
        }

        public void Unexecute()
        {

        }
    }
}
