using GameSpace.Abstracts;
using GameSpace.Interfaces;
using GameSpace.Sprites.ExtraItems;
using GameSpace.States.MarioStates;
using Microsoft.Xna.Framework;

namespace GameSpace.Machines
{
    public class MarioHandler : AbstractHandler
    {
        private static readonly MarioHandler instance = new MarioHandler();
        private static readonly int warpRooomNum = 0;
        public static MarioHandler GetInstance()
        {
            return instance;
        }

        private MarioHandler()
        {

        }

        public bool IsCurrentlyBigMario()
        {
            return (mario.marioActionState is BigMarioFallingState ||
                   mario.marioActionState is BigMarioJumpingState ||
                   mario.marioActionState is BigMarioRunningState ||
                   mario.marioActionState is BigMarioStandingState ||
                   mario.marioActionState is FireMarioFallingState ||
                   mario.marioActionState is FireMarioJumpingState ||
                   mario.marioActionState is FireMarioRunningState ||
                   mario.marioActionState is FireMarioStandingState);
        }

        public void SetMarioStateToWarp()
        {
            MusicHandler.GetInstance().PlaySoundEffect(10);
            if (currentWarpLocation < listOfWarpPipes.Count - 2)
            {
                currentWarpLocation++;
            }

            AbstractBlock nextPipeBlock = (AbstractBlock)(listOfWarpPipes.ToArray()[currentWarpLocation]);

            if (!(nextPipeBlock.state is StateWarpPipeDeactiveated))
            {
                mario.Position = new Vector2(nextPipeBlock.Position.X, nextPipeBlock.Position.Y - 20);
            }
        }

        public void WarpMario(Vector2 location)
        {
            MusicHandler.GetInstance().PlaySoundEffect(10);
            mario.Position = location;
        }

        public void BounceMario()
        {
            mario.Position = new Vector2(mario.Position.X, mario.Position.Y - 3);
        }

        public void WarpMarioToHiddenRoom()
        {
            IGameObjects warpPipe = listOfWarpRoomPipes.ToArray()[warpRooomNum + 1];
            Vector2 warpLocation = new Vector2(warpPipe.Position.X, warpPipe.Position.Y - warpPipe.Sprite.Texture.Height);
            WarpMario(warpLocation);
        }

        public void WarpMarioBackToStart()
        {
            IGameObjects warpPipe = listOfWarpRoomPipes.ToArray()[warpRooomNum];
            Vector2 warpLocation = new Vector2(warpPipe.Position.X, warpPipe.Position.Y - warpPipe.Sprite.Texture.Height);
            WarpMario(warpLocation);
        }

        internal void EnterVictoryPanel()
        {
            gameRootCopy.ChangeToVictoryState();
        }

        public void IncrementMarioPoints(int points)
        {
            mario.score += points;
        }

        public void IncrementMarioLives()
        {
            ++marioLives;
            //mario.marioLives -= 1;
        }

        public void DecrementMarioLives()
        {
            marioLives -= 1;
        }

        public void ResetMarioLives()
        {
            marioLives = 3;
        }


        public int CalculateScore()
        {
            return 1;
        }

        public Vector2 GetPosition()
        {
            return mario.Position;
        }

        public void CalculateFinalScore(int marioY, int poleY)
        {
            int multiplier = poleY - marioY;
            mario.score = (int)(mario.score * (float)(multiplier * .042));
        }
    }
}
