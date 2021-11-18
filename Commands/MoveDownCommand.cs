namespace GameSpace.Commands
{
    public class MoveDownCommand : ICommand
    {
        private protected GameRoot game;
        public static int temp = 0;

        public MoveDownCommand(GameRoot game)
        {
            this.game = game;
        }

        public void Execute()
        {

            game.GetMario.DownTransition();

        }

        public void Unexecute()
        {
            
        }
    }
}
