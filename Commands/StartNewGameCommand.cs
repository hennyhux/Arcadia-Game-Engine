using GameSpace.EntityManaging;

namespace GameSpace.Commands
{
    public class StartNewGameCommand : ICommand
    {
        public StartNewGameCommand()
        {

        }

        public void Execute()
        {
            TheaterHandler.GetInstance().ChangeStageToPlaying();
        }

        public void Unexecute()
        {

        }

    }
}
