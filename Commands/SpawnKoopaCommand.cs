using GameSpace.EntityManaging;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.Objects.EnemyObjects;
using Microsoft.Xna.Framework;
using System;

namespace GameSpace.Commands
{
    public class SpawnKoopaCommand : ICommand
    {

        private protected GameRoot game;
        private Mario mario;


        public SpawnKoopaCommand(GameRoot game)
        {
            this.game = game;
        }

        public void Execute()
        {
            Vector2 loc;
            mario = FinderHandler.GetInstance().FindMario();
            if (mario.Facing == Enums.MarioDirection.RIGHT)
            {
                loc = new Vector2(mario.Position.X + 75, mario.Position.Y - 15);
            }
            else
            {
                loc = new Vector2(mario.Position.X - 75, mario.Position.Y - 15);
            }
            Koopa koopa = (Koopa)ObjectFactory.GetInstance().CreateGreenKoopaObject(loc);
            TheaterHandler.GetInstance().AddItemToStage(koopa);
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}