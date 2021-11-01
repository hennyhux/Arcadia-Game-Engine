using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameSpace.States.ItemStates
{
    public class FireFlowerState : IItemStates
    {
        private readonly ISprite sprite;
        private readonly bool triggered;

        public ISprite StateSprite { get; set; }

        public FireFlowerState(GameRoot game)
        {
            sprite = SpriteItemFactory.GetInstance().CreateFireFlower();
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
