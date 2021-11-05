using GameSpace.Abstracts;
using GameSpace.Camera2D;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace GameSpace.EntityManaging
{
    public class FinderHandler : AbstractHandler
    {
        private static readonly FinderHandler instance = new FinderHandler();
        public static FinderHandler GetInstance()
        {
            return instance;
        }

        private FinderHandler()
        {

        }

        public IGameObjects FindItem(int ItemID)
        {
            foreach (IGameObjects entity in gameEntityList)
            {
                if (entity.ObjectID == ItemID)
                {
                    return entity;
                }
            }
            return null; //lets try not to return null
        }

        public IGameObjects[] FindWarpPipes()
        {
            return listOfWarpPipes.ToArray();
        }

        public Camera FindCameraCopy()
        {
            return cameraCopy;
        }

        public List<SoundEffect> FindListOFSoundEffects()
        {
            return musicList;
        }

        public Mario FindMario()
        {
            return mario;
        }

        public Vector2 FindMarioPosition()
        {
            return mario.Position;
        }

    }
}
