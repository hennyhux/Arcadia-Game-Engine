using GameSpace.EntityManaging;
using GameSpace.States.GameStates;
using System;
using System.Collections.Generic;
using System.Text;

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
