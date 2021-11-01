using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace GameSpace.EntityManaging
{
    public class TheaterMachine
    {
        private static List<IGameObjects> gameEntityList = new List<IGameObjects>();
        private static readonly List<IGameObjects> prunedList = new List<IGameObjects>();
        private static List<IGameObjects> copyPrunedList = new List<IGameObjects>();
        private static readonly List<IObjectAnimation> animationList = new List<IObjectAnimation>();
        private static readonly List<IGameObjects> listOfWarpPipes = new List<IGameObjects>();
        private static IGameObjects mario;
        private static Vector2 marioCurrentLocation;

        private static readonly TheaterMachine instance = new TheaterMachine();
        public static TheaterMachine GetInstance()
        {
            return instance;
        }

        private TheaterMachine()
        {

        }

        public void LoadList(List<IGameObjects> objectList)
        {
            gameEntityList = objectList;

            foreach (IGameObjects entity in gameEntityList)
            {
                if (entity.ObjectID == (int)ItemID.BIGPIPE)
                {
                    listOfWarpPipes.Add(entity);
                }
            }
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

            SweepAndPrune();
        }
        private void SweepAndPrune()
        {
            mario = EntityManager.FindItem((int)AvatarID.MARIO);
            marioCurrentLocation = mario.Position;
            //Debug.WriteLine("MARIO POSITION " + mario.Position.X + "   "+ mario.Position.Y);
            foreach (IGameObjects entity in gameEntityList)
            {
                if (marioCurrentLocation.X + 800 >= entity.Position.X && entity.Position.X - 800 < marioCurrentLocation.X)
                {
                    prunedList.Add(entity);
                }
            }

            for (int i = 0; i < prunedList.Count; i++)
                for (int j = i + 1; j < prunedList.Count; j++)
                {
                    if (ColliderMachine.GetInstance().IntersectAABB(prunedList[i], prunedList[j]))
                    {
                        prunedList[i].HandleCollision(prunedList[j]);
                        prunedList[j].HandleCollision(prunedList[i]);
                    }
                }
            // Debug.WriteLine("SIZE OF PRUNED LIST " + prunedList.Count);
            //Debug.WriteLine("SIZE OF OG LIST " + gameEntities.Count);
            copyPrunedList = prunedList.ToList();
            prunedList.Clear();
            //Debug.WriteLine("SIZE OF PRUNED COPY LIST " + copyPrunedList.Count);
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
        }

        public void ToggleCollisionBox()
        {
            foreach (IGameObjects entity in gameEntityList)
            {
                entity.ToggleCollisionBoxes();
            }
        }

        public List<IGameObjects> ExportGameList()
        {
            return gameEntityList;
        }

    }
}
