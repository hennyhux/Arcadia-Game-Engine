using GameSpace.Level;
using Microsoft.Xna.Framework;

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
            MyGame.Restart();
        }

        public void Unexecute()
        {

        }
    }
}