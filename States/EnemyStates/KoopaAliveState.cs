using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States.EnemyStates
{
    public class KoopaAliveState : IEnemyStates
    {
        public ISprite StateSprite { get; set; }

        public KoopaAliveState()
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaSprite();
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            StateSprite.Draw(spritebatch, location);
        }

        public void Update(GameTime gametime)
        {
            StateSprite.Update(gametime);
        }
    }
}
