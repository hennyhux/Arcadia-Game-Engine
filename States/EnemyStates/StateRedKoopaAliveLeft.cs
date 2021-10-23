using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameSpace.States.StateMachines
{
    public class StateRedKoopaAliveLeft: IEnemyState
    {
        public ISprite StateSprite { get; set; }
        public Boolean CollidedWithMario { get; set; }
        private int countDown;

        public StateRedKoopaAliveLeft()
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateRedKoopaSprite();
            CollidedWithMario = false;
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            StateSprite.Draw(spritebatch, location);
            if (CollidedWithMario) countDown++;
        }

        public void Update(GameTime gametime)
        {
            StateSprite.Update(gametime);

            if (countDown == 225) StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaShellAndLegsSprite();

            if (countDown == 550)
            {
                StateSprite = SpriteEnemyFactory.GetInstance().CreateRedKoopaSprite();
                countDown = 0;
                CollidedWithMario = false;
            }
        }

        public void Trigger()
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateRedKoopaShellSprite();
            CollidedWithMario = true;
        }

        public void DrawBoundaries(SpriteBatch spritebatch, Rectangle destination)
        {
            StateSprite.DrawBoundary(spritebatch, destination);
        }
    }
}
