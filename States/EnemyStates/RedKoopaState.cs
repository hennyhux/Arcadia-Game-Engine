using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States.EnemyStates
{
    public class RedKoopaState : IEnemyStates
    {
        private ISprite sprite;
        private bool triggered;

        public RedKoopaState()
        {
            sprite = EnemySpriteFactory.GetInstance().ReturnRedKoopa();
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            sprite.Draw(spritebatch, location);
        }

        public void Trigger()
        {
            
        }

        public void Update(GameTime gametime)
        {
            sprite.Update(gametime);
        }
    }
}
