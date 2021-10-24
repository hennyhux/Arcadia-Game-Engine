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
    public class StateSuperShroomRight : IItemStates
    {
        public ISprite StateSprite { get; set; }
        public Boolean CollidedWithMario { get; set; }
        private SuperShroom SuperShroom;

        public StateSuperShroomRight(SuperShroom superShroom)
        {
            StateSprite = SpriteItemFactory.GetInstance().CreateSuperShroom();
            CollidedWithMario = false;
            this.SuperShroom = superShroom;
            this.SuperShroom.Velocity = new Vector2((float)+ 1, (float)0);
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
