using GameSpace.Factories;
using GameSpace.GameObjects.ItemObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameSpace.States.ItemStates
{
    public class StateOneUpShroomRight : IItemStates
    {
        public ISprite StateSprite { get; set; }
        public Boolean CollidedWithMario { get; set; }
        public OneUpShroom OneUpShroom;

        public StateOneUpShroomRight(OneUpShroom oneUpShroom)
        {
            StateSprite = SpriteItemFactory.GetInstance().CreateOneUpShroom();
            CollidedWithMario = false;
            OneUpShroom = oneUpShroom;
            OneUpShroom.Velocity = new Vector2(+1, 0);
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            // StateSprite.Draw(spritebatch, location);
        }

        public void Trigger()
        {
            //throw new NotImplementedException();
        }

        public void Update(GameTime gametime)
        {
            // StateSprite.Update(gametime);
        }
    }
}
