using System;
using System.Collections.Generic;
using System.Text;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States.ItemStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameSpace.EntitiesManager;
using GameSpace.GameObjects.BlockObjects;



namespace GameSpace.GameObjects.ItemObjects
{
    public class Star : IGameObjects
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
        public Rectangle ExpandedCollisionBox { get; set; }


        public Star(Vector2 initialPosition)
        {
            this.ObjectID = (int)ItemID.STAR;
            this.Sprite = SpriteItemFactory.GetInstance().CreateStar();
            this.Position = initialPosition;
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2 / 4, Sprite.Texture.Height * 2);
            //ExpandedCollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2 / 4, Sprite.Texture.Height * 3);
            drawBox = false;
            this.hasCollided = false;
            this.state = new StateStarHidden(this);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            Sprite.Draw(spritebatch, Position);
            this.state.Draw(spritebatch, Position);
            if (drawBox)
            {
                Sprite.DrawBoundary(spritebatch, CollisionBox);
                Sprite.DrawBoundary(spritebatch, ExpandedCollisionBox);
            }
        }

        public void Update(GameTime gametime)
        {
            if (this.state is StateStarHidden) findMario();
            this.state.Update(gametime);
            UpdatePosition(Position, gametime);
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
            //Acceleration = new Vector2(0, 3000);

            //if (this.state is StateStarRight) Velocity = new Vector2(85, 0);
            //if (this.state is StateStarLeft) Velocity = new Vector2(-85, 0);

            Velocity += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            UpdateCollisionBox(Position);
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
                //this.Velocity = new Vector2(0, 0);
                this.state = new StateStarRight(this);
            }

            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.LEFT)
            {
                //this.Velocity = new Vector2(0, 0);
                this.state = new StateStarLeft(this);
            }
            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.DOWN)
            {
                this.Position = new Vector2(this.Position.X, this.Position.Y - 10);
                if(this.state is StateStarRight) this.Velocity = new Vector2(45, 0);
                if (this.state is StateStarLeft) this.Velocity = new Vector2(-45, 0);
                this.Acceleration = new Vector2(0, -400);
                this.state.Trigger();
            }
        }
        private void UpdateCollisionBox(Vector2 location)
        {
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2 / 4, Sprite.Texture.Height * 2);
            //ExpandedCollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2 / 4, (Sprite.Texture.Height * 2) + 3);
        }

        private void findMario()
        {
            if (EntityManager.FindItem((int)AvatarID.MARIO).Position.X <= Position.X)
            {
                this.state = new StateStarRight(this);

            }
            else
            {
                this.state = new StateStarLeft(this);

            }
        }
    }
}
