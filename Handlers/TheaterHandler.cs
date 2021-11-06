using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using GameSpace.States.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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

        public void LoadData(List<IGameObjects> objectList, GameRoot copy)
        {
            gameEntityList = objectList;

            foreach (IGameObjects entity in gameEntityList)
            {
                if (entity.ObjectID == (int)ItemID.WARPPIPEHEAD)
                {
                    listOfWarpPipes.Add(entity);
                }
            }
            gameRootCopy = copy;
            mario = (Mario)FinderHandler.GetInstance().FindItem((int)AvatarID.MARIO);
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

        public void ChangeGameState(State state)
        {
            gameRootCopy.ChangeState(state);
        }

    }
}
