using GameSpace.EntityManaging;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.GameObjects.EnemyObjects;
using Microsoft.Xna.Framework;
using System;

namespace GameSpace.Commands
{
    public class SpawnGoombaCommand : ICommand
    {

        private protected GameRoot game;
        private Mario mario;

        public SpawnGoombaCommand(GameRoot game)
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
            Goomba goomba = (Goomba)ObjectFactory.GetInstance().CreateGoombaObject(loc);
            TheaterHandler.GetInstance().AddItemToStage(goomba);

        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}