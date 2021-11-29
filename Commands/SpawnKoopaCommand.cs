using System;
using GameSpace.Factories;
using GameSpace.EntityManaging;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.GameObjects.BlockObjects;
using Microsoft.Xna.Framework;

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
            GreenKoopa koopa = (GreenKoopa)ObjectFactory.GetInstance().CreateGreenKoopaObject(loc);
            TheaterHandler.GetInstance().AddItemToStage(koopa);
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}