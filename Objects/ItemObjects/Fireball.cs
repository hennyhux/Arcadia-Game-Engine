using System;
using System.Collections.Generic;
using System.Text;
using GameSpace.Interfaces;
using GameSpace.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameSpace.Enums;
using GameSpace.GameObjects.BlockObjects;

namespace GameSpace.Objects.ItemObjects
{
    public class Fireball : IGameObjects
    {

        private IObjectState state;
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }
        public int ObjectID { get; set; }

        private Boolean hasCollided;
        private Boolean drawBox;
        public Mario Mario;

        public Fireball(Mario mario)
        {
            this.ObjectID = (int)ItemID.FIREBALL;
            this.Sprite = SpriteItemFactory.GetInstance().CreateFireBall();
            this.Mario = mario;
            this.Position = this.Mario.Position;
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2 / 4, Sprite.Texture.Height * 2);
            this.Velocity = new Vector2(2, 0);
            hasCollided = false;
            drawBox = false;

        }

        public void Draw(SpriteBatch spritebatch)
        {
            Sprite.Draw(spritebatch, Position);
            if (drawBox && Sprite.GetVisibleStatus()) Sprite.DrawBoundary(spritebatch, CollisionBox);
        }

        public void Update(GameTime gametime)
        {
            Sprite.Update(gametime);
        }

        public void Trigger()
        {
            this.Sprite.SetVisible();
            this.CollisionBox = new Rectangle(1, 1, 0, 0);
        }

        public void HandleCollision(IGameObjects entity)
        {
            this.Trigger();
        }

        public void UpdatePosition(Vector2 location, GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

        public bool IsCurrentlyColliding()
        {
            throw new NotImplementedException();
        }
    }
}
}
