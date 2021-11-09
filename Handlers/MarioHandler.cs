﻿using GameSpace.Abstracts;
using GameSpace.Interfaces;
using GameSpace.Sprites.ExtraItems;
using GameSpace.States.MarioStates;
using Microsoft.Xna.Framework;
using System.Diagnostics;


namespace GameSpace.Machines
{
    public class MarioHandler : AbstractHandler
    {
        private static readonly MarioHandler instance = new MarioHandler();
        private static readonly int warpRooomNum = 0;
        public static int marioLives = 3;
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
            int flagPoints = 0;//Calculates the points from the flag
            double pixelConversionMult = 2;//To make different heights of poles more accerate for points
            if(poleY >= marioY + mario.sprite.Height/2){//If mario on top of flag pole 
                //increment lives
                flagPoints = 8000;
                MarioHandler.GetInstance().IncrementMarioLives();
            }
            else if ((marioY - poleY < 25 * pixelConversionMult)){//If 
                flagPoints = 4000;
            }
            else if ((marioY - poleY < 71 * pixelConversionMult))
            {//If 
                flagPoints = 2000;
            }
            else if ((marioY - poleY < 95 * pixelConversionMult))
            {//If 
                flagPoints = 800;
            }
            else if ((marioY - poleY < 135 * pixelConversionMult))
            {//If 
                flagPoints = 400;
            }
            else
            {//If bottom of flag 
                flagPoints = 100;
            }
            Debug.Print("FlagPoints : {0}", flagPoints);
            
            //int multiplier = poleY - marioY ;
            double multiplier = 200;
            mario.score += (int)(multiplier * HUDHandler.seconds);//Get points based off time
            mario.score += flagPoints;// points based off flag
        }
    }
}
