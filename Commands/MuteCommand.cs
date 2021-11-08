using GameSpace.Machines;



namespace GameSpace
{
    //Future command 
    public class MuteCommand : ICommand
    {
        private protected GameRoot game;
        public MuteCommand(GameRoot game)
        {
            this.game = game;
        }
        public void Execute()
        {
            MusicHandler.GetInstance().MuteSong();
            MusicHandler.GetInstance().MuteSoundEffects();
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}