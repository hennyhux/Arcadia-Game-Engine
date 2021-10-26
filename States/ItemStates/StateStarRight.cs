﻿using GameSpace.Factories;
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
    public class StateStarRight : IItemStates
    {
        public ISprite StateSprite { get; set; }
        public Star Star;
        private int countdown;
        private Boolean bounce;

        public StateStarRight(Star star)
        {
            StateSprite = SpriteItemFactory.GetInstance().CreateStar();
            this.Star = star;
            this.Star.Velocity = new Vector2((float)45, (float)0);
            this.Star.Acceleration = new Vector2(0, 400);
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            //StateSprite.Draw(spritebatch, location);
            if(bounce) ++this.countdown;
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