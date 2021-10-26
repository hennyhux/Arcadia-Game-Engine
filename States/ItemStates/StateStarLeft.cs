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
    public class StateStarLeft : IItemStates
    {
        public ISprite StateSprite { get; set; }
        public Boolean CollidedWithMario { get; set; }
        public Star Star;
        private Boolean bounce;
        private int countdown;

        public StateStarLeft(Star star)
        {
            StateSprite = SpriteItemFactory.GetInstance().CreateStar();
            CollidedWithMario = false;
            this.Star = star;
            this.Star.Velocity = new Vector2((float)-40, (float)0);
            this.Star.Acceleration = new Vector2(0, 400);
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            if (bounce) ++this.countdown;
        }

        public void Trigger()
        {
            bounce = true;
        }

        public void Update(GameTime gametime)
        {
            if (this.countdown == 15)
            {
                this.Star.Acceleration = new Vector2(0, 400);
                bounce = false;
                this.countdown = 0;
            }
        }
    }
}
