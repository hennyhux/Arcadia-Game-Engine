using Microsoft.Xna.Framework.Media;
using System;


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
            MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}