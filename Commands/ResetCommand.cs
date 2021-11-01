namespace GameSpace
{
    public class ResetCommand : ICommand
    {
        private protected GameRoot MyGame;

        public ResetCommand(GameRoot game)
        {
            MyGame = game;
        }

        public void Execute()
        {
            MyGame.Reset();
        }

        public void Unexecute()
        {

        }
    }
}