using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.GameObjects.ItemObjects;
using System;





namespace GameSpace.Commands
{
    public class ThrowFireBallCommand : ICommand
    {
        private protected GameRoot game;
        private Mario mario;
        private Fireball fireball;

        public ThrowFireBallCommand(GameRoot game)
        {
            this.game = game;
        }

        public void Execute()
        {
            mario = (Mario)EntityManager.FindItem((int)AvatarID.MARIO);
            if (mario.numFireballs < 2)
            {
                if (EntityManager.IsCurrentlyFireMario())
                {
                    mario = (Mario)EntityManager.FindItem((int)AvatarID.MARIO);
                    fireball = (Fireball)ObjectFactory.GetInstance().CreateFireBallObject(mario);
                    EntityManager.AddEntity(fireball);
                }
            }
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }


}
