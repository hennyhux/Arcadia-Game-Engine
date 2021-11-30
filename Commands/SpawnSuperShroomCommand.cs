using System;
using GameSpace.Factories;
using GameSpace.EntityManaging;
using GameSpace.GameObjects.ItemObjects;
using GameSpace.GameObjects.BlockObjects;
using Microsoft.Xna.Framework;

namespace GameSpace.Commands
{
    public class SpawnSuperShroomCommand : ICommand
    {

        private protected GameRoot game;
        private Mario mario;


        public SpawnSuperShroomCommand(GameRoot game)
        {
            this.game = game;
        }

        public void Execute()
        {
            mario = FinderHandler.GetInstance().FindMario();
            Vector2 loc = new Vector2(mario.Position.X + 4, mario.Position.Y - 65);
            SuperShroom shroom = (SuperShroom)ObjectFactory.GetInstance().CreateSuperShroomObject(loc);//some location

            TheaterHandler.GetInstance().AddItemToStage(shroom);

        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}