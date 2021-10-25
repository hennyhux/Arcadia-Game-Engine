using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using GameSpace.GameObjects.ItemObjects;
using System.Diagnostics;
using GameSpace.Enums;

namespace GameSpace.States.ItemStates
{
    public class StateOneUpShroomHidden : IItemStates
    {
        public ISprite StateSprite { get; set; }
        public Boolean CollidedWithMario { get; set; }
        public OneUpShroom OneUpShroom;

        public StateOneUpShroomHidden(OneUpShroom oneUpShroom)
        {
            StateSprite = SpriteItemFactory.GetInstance().CreateOneUpShroom();
            CollidedWithMario = false;
            this.OneUpShroom = oneUpShroom;
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            //StateSprite.Draw(spritebatch, location);
        }

        public void Trigger()
        {
            // throw new NotImplementedException();
        }

        public void Update(GameTime gametime)
        {
            // StateSprite.Update(gametime);
        }
    }
}
