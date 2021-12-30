using GameSpace.EntityManaging;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.GameObjects.ItemObjects;
using Microsoft.Xna.Framework;
using System;

namespace GameSpace.Commands
{
    public class SpawnCoinCommand : ICommand
    {

        private protected GameRoot game;
        private Mario mario;


        public SpawnCoinCommand(GameRoot game)
        {
            this.game = game;
        }

        public void Execute()
        {
            mario = FinderHandler.GetInstance().FindMario();
            Vector2 loc = new Vector2(mario.Position.X + 4, mario.Position.Y - 35);
            Coin coin = (Coin)ObjectFactory.GetInstance().CreateCoinObject(loc);//some location

            TheaterHandler.GetInstance().AddItemToStage(coin);
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}