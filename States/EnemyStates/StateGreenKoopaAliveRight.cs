using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States.StateMachines
{
    public class StateGreenKoopaAliveRight : IEnemyState
    {
        public ISprite StateSprite { get; set; }
        public Boolean CollidedWithMario { get; set; }
        private int countDown;

        public StateGreenKoopaAliveRight()
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaSprite();
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
                StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaSprite();
                countDown = 0;
                CollidedWithMario = false;
            }
        }

        public void Trigger()
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaShellSprite();
            CollidedWithMario = true;
        }

        public void DrawBoundaries(SpriteBatch spritebatch, Rectangle destination)
        {
            StateSprite.DrawBoundary(spritebatch, destination);
        }
    }
}
