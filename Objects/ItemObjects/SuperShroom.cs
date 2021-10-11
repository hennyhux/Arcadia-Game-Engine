using System;
using System.Collections.Generic;
using System.Text;
using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States.ItemStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.GameObjects.ItemObjects
{
    public class SuperShroom : IGameObjects
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

        public SuperShroom(Vector2 initialPosition)
        {
            this.ObjectID = (int)ItemID.SUPERSHROOM;
            this.Sprite = SpriteItemFactory.GetInstance().CreateSuperShroom();
            this.Position = initialPosition;
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            drawBox = false;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            Sprite.Draw(spritebatch, Position);
            if (drawBox) Sprite.DrawBoundary(spritebatch, CollisionBox);
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
            switch(entity.ObjectID)
            {
                case (int) AvatarID.MARIO:
                    this.Trigger();
                    break;
            }
        }

        public void SetPosition(Vector2 location)
        {
            throw new NotImplementedException();
        }

        public void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

    }
}
