using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States.ItemStates
{
    public class StarState : IItemStates
    {
        private ISprite sprite;
        private bool triggered;
        public ISprite StateSprite { get; set; }

        public StarState(GameRoot game)
        {
            this.sprite = SpriteItemFactory.GetInstance().CreateStar();
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
