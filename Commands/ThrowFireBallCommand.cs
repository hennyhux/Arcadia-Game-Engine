using GameSpace.EntityManaging;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using GameSpace.States.MarioStates;
using System;





namespace GameSpace.Commands
{
    public class ThrowFireBallCommand : ICommand
    {
        private protected GameRoot game;
        private Mario mario;
        private IGameObjects fireball;

        public ThrowFireBallCommand(GameRoot game)
        {
            this.game = game;
        }

        public void Execute()
        {
            mario = FinderHandler.GetInstance().FindMario();
            if (mario.numFireballs != 0)
            {
                if (mario.marioActionState is FireMarioFallingState ||
                    mario.marioActionState is FireMarioJumpingState ||
                    mario.marioActionState is FireMarioRunningState ||
                    mario.marioActionState is FireMarioStandingState)
                {
                    fireball = ObjectFactory.GetInstance().CreateFireBallObject(mario);
                    TheaterHandler.GetInstance().AddItemToStage(fireball);
                    mario.numFireballs--;
                }
            }
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }


}
