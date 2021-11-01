using GameSpace.Factories;
using GameSpace.GameObjects.ItemObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameSpace.States.ItemStates
{
    public class StateFireballRight : IItemStates
    {
        public ISprite StateSprite { get; set; }
        public Boolean CollidedWithMario { get; set; }
        public Fireball Fireball;
        private Boolean bounce;
        public int countdown;
        private int timeAlive;

        public StateFireballRight(Fireball fireball)
        {
            StateSprite = SpriteItemFactory.GetInstance().CreateFireBall();
            CollidedWithMario = false;
            Fireball = fireball;
            Fireball.Position = new Vector2(Fireball.Position.X + 35, Fireball.Position.Y + 25);

            Fireball.Velocity = new Vector2(80, 0);
            Fireball.Acceleration = new Vector2(0, 400);
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            if (bounce) ++countdown;
            ++timeAlive;
        }

        public void Trigger()
        {
            bounce = true;
        }

        public void Update(GameTime gametime)
        {
            if (countdown == 4)
            {
                Fireball.Acceleration = new Vector2(0, 400);
                bounce = false;
                countdown = 0;
            }

            if (timeAlive == 75) Fireball.Trigger();
        }
    }
}
