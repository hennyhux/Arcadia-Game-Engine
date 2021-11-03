using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using GameSpace.Level;

namespace GameSpace
{
    public class RestartCommand : ICommand
    {
        private protected GameRoot MyGame;
        public LevelRestart levelRestart;

        public RestartCommand(GameRoot game)
        {
            MyGame = game;
            levelRestart = new LevelRestart(game, 0);
        }

        public void Execute()
        {
            levelRestart.Restart(true);
        }

        public void Unexecute()
        {

        }
    }
}