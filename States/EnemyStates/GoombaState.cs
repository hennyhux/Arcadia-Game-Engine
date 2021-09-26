using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States.EnemyStates
{
    public class GoombaState : IEnemyStates
    {
        private ISprite sprite;
        private EnemySpriteFactory blockFactory;
        private bool triggered;

        public GoombaState(Game1 game)
        {
            blockFactory = game.EnemySpriteFactory;
            this.sprite = blockFactory.ReturnGoomba();
            triggered = false;
        }


        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            sprite.Draw(spritebatch, location);
        }

        public void Trigger()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gametime)
        {
            sprite.Update(gametime);
        }
    }
}
