using GameSpace.EntityManaging;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.GameObjects.ItemObjects;
using Microsoft.Xna.Framework;
using System;


namespace GameSpace.Commands
{
    public class SpawnOneUpShroomCommand : ICommand
    {

        private protected GameRoot game;
        private Mario mario;


        public SpawnOneUpShroomCommand(GameRoot game)
        {
            this.game = game;
        }

        public void Execute()
        {
            mario = FinderHandler.GetInstance().FindMario();
            Vector2 loc = new Vector2(mario.Position.X + 4, mario.Position.Y - 65);
            OneUpShroom shroom = (OneUpShroom)ObjectFactory.GetInstance().CreateOneUpShroomObject(loc);//some location
            TheaterHandler.GetInstance().AddItemToStage(shroom);

        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
