namespace GameSpace
{
    public class ChangeBlockCommand : ICommand
    {
        private protected Game1 game;

        public ChangeBlockCommand(Game1 game)
        {
            this.game = game;
        }
        public void Execute()
        {
            game.NewBricks.Trigger();
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}