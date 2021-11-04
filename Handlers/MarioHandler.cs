using GameSpace.Abstracts;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace GameSpace.Machines
{
    public class MarioHandler : AbstractHandler
    {
        private static readonly MarioHandler instance = new MarioHandler();
        public static MarioHandler GetInstance()
        {
            return instance;
        }

        private MarioHandler()
        {

        }

        public void SetMarioStateToWarp()
        {
            if (currentWarpLocation < listOfWarpPipes.Count - 3)
            {
                currentWarpLocation++;
            }

            Debug.WriteLine(listOfWarpPipes.Count);
            IGameObjects nextPipe = listOfWarpPipes.ToArray()[currentWarpLocation];
            mario.Position = new Vector2(nextPipe.Position.X, nextPipe.Position.Y - 20);
        }

        public void BounceMario()
        {
            mario.Position = new Vector2(mario.Position.X, mario.Position.Y - 3);
        }

        internal void EnterVictoryPanel(GameRoot game)
        {
            HUDHandler.GetInstance().EnterVictoryMode(game);
        }
    }
}
