using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.GameObjects.ItemObjects;
using GameSpace.Factories;





namespace GameSpace
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
            this.mario = (Mario)EntityManager.FindItem((int)AvatarID.MARIO);
            if (this.mario.numFireballs < 2)
            {
                if (EntityManager.IsCurrentlyFireMario())
                {
                    this.mario = (Mario)EntityManager.FindItem((int)AvatarID.MARIO);
                    fireball = (Fireball)ObjectFactory.GetInstance().CreateFireBallObject(this.mario);
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
