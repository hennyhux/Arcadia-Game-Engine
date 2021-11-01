using GameSpace.Factories;
using GameSpace.GameObjects.ItemObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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
            Star = star;
            Star.Velocity = new Vector2(-40, 0);
            Star.Acceleration = new Vector2(0, 400);
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            if (bounce) ++countdown;
        }

        public void Trigger()
        {
            bounce = true;
        }

        public void Update(GameTime gametime)
        {
            if (countdown == 15)
            {
                Star.Acceleration = new Vector2(0, 400);
                bounce = false;
                countdown = 0;
            }
        }
    }
}
