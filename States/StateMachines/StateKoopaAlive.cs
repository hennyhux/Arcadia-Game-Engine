using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States.StateMachines
{
    public class StateKoopaAlive : IEnemyState
    {
        public ISprite StateSprite { get; set; }
        public Boolean Collided { get; set; }
        private int countDown;

        public StateKoopaAlive()
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaSprite();
            Collided = false;
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            StateSprite.Draw(spritebatch, location);
            if (Collided) countDown++;
        }

        public void Update(GameTime gametime)
        {
            StateSprite.Update(gametime);

            if (countDown == 225) StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaShellAndLegsSprite();

            if (countDown == 550)
            {
                StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaSprite();
                countDown = 0;
                Collided = false;
            }
        }

        public void Trigger()
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaShellSprite();
            Collided = true;
        }

        public void DrawBoundaries(SpriteBatch spritebatch, Rectangle destination)
        {
            StateSprite.DrawBoundary(spritebatch, destination);
        }
    }
}
