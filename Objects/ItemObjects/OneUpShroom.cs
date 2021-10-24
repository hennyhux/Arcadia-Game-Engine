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
    public class OneUpShroom : IGameObjects
    {
        private IItemStates state;
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }
        public int ObjectID { get; set; }

        private Boolean hasCollided;
        private Boolean drawBox;

        public OneUpShroom(Vector2 initialPosition)
        {
            this.ObjectID = (int)ItemID.ONEUPSHROOM;
            this.Sprite = SpriteItemFactory.GetInstance().CreateOneUpShroom();
            this.Position = initialPosition;
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            drawBox = false;
            this.hasCollided = false;

            //Change based on Mario's position
            this.state = new StateOneUpShroomLeft(this);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            Sprite.Draw(spritebatch, Position);
            if (drawBox) Sprite.DrawBoundary(spritebatch, CollisionBox);
        }

        public void Update(GameTime gametime)
        {
            UpdatePosition(Position,gametime);
            Sprite.Update(gametime);
        }

        public void Trigger()
        {
            if (!hasCollided)
            {
                this.Sprite.SetVisible();
                this.CollisionBox = new Rectangle();
            }
            this.hasCollided = true;

            //Increase Mario's lives
        }

        public void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    this.Trigger();
                    break;

                case (int)BlockID.USEDBLOCK:
                case (int)BlockID.QUESTIONBLOCK:
                case (int)BlockID.FLOORBLOCK:
                case (int)BlockID.STAIRBLOCK:
                case (int)BlockID.COINBRICKBLOCK:
                case (int)BlockID.BRICKBLOCK:
                    CollisionWithBlock(entity);
                    break;
            }
        }

        public void UpdatePosition(Vector2 location, GameTime gameTime)
        {
            this.Position = new Vector2(location.X + this.Velocity.X, location.Y);
            UpdateCollisionBox(this.Position);
        }

        public void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

        public bool IsCurrentlyColliding()
        {
            throw new NotImplementedException();
        }

        public void CollisionWithBlock(IGameObjects entity)
        {
            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.RIGHT)
            {
                this.Velocity = new Vector2(0, 0);
                this.state = new StateOneUpShroomRight(this);
            }

            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.LEFT)
            {
                this.Velocity = new Vector2(0, 0);
                this.state = new StateOneUpShroomLeft(this);
            }
        }

        private void UpdateCollisionBox(Vector2 location)
        {
            this.CollisionBox = new Rectangle((int)location.X + state.StateSprite.Texture.Width / 2 - 10, (int)Position.Y,
                state.StateSprite.Texture.Width * 2, state.StateSprite.Texture.Height * 2);
        }
    }
}
