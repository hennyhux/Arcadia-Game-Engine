using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameSpace.EntityManaging
{
    public class TheaterMachine : AbstractMachine
    {
        private static readonly TheaterMachine instance = new TheaterMachine();
        public static TheaterMachine GetInstance()
        {
            return instance;
        }

        private TheaterMachine()
        {

        }

        public void LoadData(List<IGameObjects> objectList)
        {
            gameEntityList = objectList;

            foreach (IGameObjects entity in gameEntityList)
            {
                if (entity.ObjectID == (int)ItemID.BIGPIPE)
                {
                    listOfWarpPipes.Add(entity);
                }
            }

            mario = (Mario)FinderMachine.GetInstance().FindItem((int)AvatarID.MARIO);
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

            //ColliderMachine.GetInstance().UpdateCollision();
        }

        public void Update(GameTime gametime)
        {

            foreach (IGameObjects entity in gameEntityList)
            {
                entity.Update(gametime);
            }

            foreach (IObjectAnimation animation in animationList)
            {
                animation.Update(gametime);
            }

            ColliderMachine.GetInstance().UpdateCollision();
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

    }
}
