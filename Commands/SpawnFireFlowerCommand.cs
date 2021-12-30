using GameSpace.EntityManaging;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.GameObjects.ItemObjects;
using Microsoft.Xna.Framework;
using System;

namespace GameSpace.Commands
{
    public class SpawnFireFlowerCommand : ICommand
    {

        private protected GameRoot game;
        private Mario mario;


        public SpawnFireFlowerCommand(GameRoot game)
        {
            this.game = game;
        }

        public void Execute()
        {
            mario = FinderHandler.GetInstance().FindMario();
            Vector2 loc = new Vector2(mario.Position.X + 4, mario.Position.Y - 65);
            FireFlower flower = (FireFlower)ObjectFactory.GetInstance().CreateFireFlowerObject(loc);//some location
            TheaterHandler.GetInstance().AddItemToStage(flower);

        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}