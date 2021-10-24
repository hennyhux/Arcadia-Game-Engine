using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States.EnemyStates
{
    public class StateGoombaDead : IEnemyState
    {
        public ISprite StateSprite { get; set; }
        public StateGoombaDead()
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateDeadGoombaSprite();
        }

        public bool CollidedWithMario { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            StateSprite.Draw(spritebatch, location);
        }

        public void Update(GameTime gametime)
        {
            StateSprite.Update(gametime);
        }

        public void DrawBoundaries(SpriteBatch spritebatch, Rectangle destination)
        {
            StateSprite.DrawBoundary(spritebatch, destination);
        }

        public void Trigger()
        {
            throw new NotImplementedException();
        }

    }
}
