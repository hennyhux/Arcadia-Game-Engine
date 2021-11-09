using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using GameSpace.Abstracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using GameSpace.Machines;
using GameSpace.Sprites.ExtraItems;

namespace GameSpace.EntityManaging
{
    public class TheaterHandler : AbstractHandler
    {
        private static readonly TheaterHandler instance = new TheaterHandler();

        private IGameObjects addItem;
        public static TheaterHandler GetInstance()
        {
            return instance;
        }

        private TheaterHandler()
        {
            addItem = null;
        }

        public void LoadData(List<IGameObjects> objectList)
        {
            //ResetStaticMembers();
            gameEntityList = objectList;

            foreach (IGameObjects entity in objectList)
            {
                if (entity is WarpPipeHead)
                {
                    listOfWarpPipes.Add(entity);
                }

                if (entity.ObjectID == (int)ItemID.WARPPIPEROOM)
                {
                    listOfWarpRoomPipes.Add(entity);
                }
            }

            mario = (Mario)FinderHandler.GetInstance().FindItem((int)AvatarID.MARIO);
        }

        public void ResetStaticMembers()
        {
            gameEntityList = new List<IGameObjects>();
            prunedList = new List<IGameObjects>();
            copyPrunedList = new List<IGameObjects>();
            animationList = new List<IObjectAnimation>();
            listOfWarpPipes = new List<IGameObjects>();
            listOfWarpRoomPipes = new List<IGameObjects>();
            musicList = new List<SoundEffect>();
            GameTime internalGametime = new GameTime();
            GameRoot gameRootCopy = new GameRoot();

            currentWarpLocation = 0;
            marioScores = 0;
            //marioLives = 3;
            MarioHandler.marioLives = 3;
        }

        public void RestartStaticMembers()
        {
            gameEntityList = new List<IGameObjects>();
            prunedList = new List<IGameObjects>();
            copyPrunedList = new List<IGameObjects>();
            animationList = new List<IObjectAnimation>();
            listOfWarpPipes = new List<IGameObjects>();
            listOfWarpRoomPipes = new List<IGameObjects>();
            musicList = new List<SoundEffect>();
            GameTime internalGametime = new GameTime();
            GameRoot gameRootCopy = new GameRoot();

            currentWarpLocation = 0;
            marioScores = 0;
            //marioLives = 3;
           // MarioHandler.marioLives = 3;
        }

        public void InitializeGameroot(GameRoot copy)
        {
            gameRootCopy = copy;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IGameObjects entity in gameEntityList)
            {
                entity.Draw(spriteBatch);
            }

            foreach (IObjectAnimation animation in animationList)
            {
                animation.Draw(spriteBatch);
            }

            //CollisionHandler.GetInstance().UpdateCollision();
        }

        public void Update(GameTime gametime)
        {

            CollisionHandler.GetInstance().UpdateCollision();
            internalGametime = gametime;

            if (addItem != null)
            {
                gameEntityList.Add(addItem);
                addItem = null;
            }

            foreach (IGameObjects entity in gameEntityList)
            {
                entity.Update(gametime);
            }

            foreach (IObjectAnimation animation in animationList)
            {
                animation.Update(gametime);
            }

            //CollisionHandler.GetInstance().UpdateCollision();
        }

        public void ToggleCollisionBox()
        {
            foreach (IGameObjects entity in gameEntityList)
            {
                entity.ToggleCollisionBoxes();
            }
        }

        public void AddItemToStage(IGameObjects item)
        {
            gameEntityList.Add(item);
        }

        public void QueueItemAddToStage(IGameObjects item)
        {
            addItem = item;
        }

        public void ChangeStageToPlaying()
        {
            gameRootCopy.ChangeToPlayState();
        }

    }
}
