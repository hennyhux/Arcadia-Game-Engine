using System;
using System.Collections.Generic;
using System.Text;
using GameSpace.Interfaces;
using GameSpace.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameSpace.Enums;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.States.ItemStates;
using GameSpace.EntitiesManager;



namespace GameSpace.GameObjects.ItemObjects
{
    public class Fireball : IGameObjects
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
        public Mario Mario;

        public Fireball(Mario mario)
        {
            this.ObjectID = (int)ItemID.FIREBALL;
            this.Sprite = SpriteItemFactory.GetInstance().CreateFireBall();
            this.Mario = mario;
            this.Position = Mario.Position;
            this.CollisionBox = new Rectangle((int)Position.X + 5, (int)Position.Y, (Sprite.Texture.Width * 2 / 4) -10, Sprite.Texture.Height * 2);
            hasCollided = false;
            drawBox = false;
            ++this.Mario.numFireballs;

            if(Mario.Facing == 0)
            {
                this.state = new StateFireballLeft(this);
            }
            else
            {
                this.state = new StateFireballRight(this);
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            Sprite.Draw(spritebatch, Position);
            this.state.Draw(spritebatch, Position);
            if (drawBox)
            {
                Sprite.DrawBoundary(spritebatch, CollisionBox);
            }
        }

        public void Update(GameTime gametime)
        {
            this.state.Update(gametime);
            UpdatePosition(Position, gametime);
            Sprite.Update(gametime);
        }

        public void Trigger()
        {
            if (!hasCollided)
            {
                this.Sprite.SetVisible();
                this.CollisionBox = new Rectangle(1, 1, 0, 0);
            }
            this.hasCollided = true;
            --this.Mario.numFireballs;
        }

        public void HandleCollision(IGameObjects entity)
        {
            //if block from sides 
            //this.Trigger();

            //if block from top bounce

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
            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.RIGHT ||
                EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.LEFT)
            {
                this.Trigger();
            }
            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.DOWN)
            {
                this.Position = new Vector2(this.Position.X, this.Position.Y - 10);
                if (this.state is StateStarRight) this.Velocity = new Vector2(45, 0);
                if (this.state is StateStarLeft) this.Velocity = new Vector2(-45, 0);
                this.Acceleration = new Vector2(0, -400);
                this.state.Trigger();
            }
        }

        private void UpdateCollisionBox(Vector2 location)
        {
            if (!hasCollided)this.CollisionBox = new Rectangle((int)Position.X + 5, (int)Position.Y, (Sprite.Texture.Width * 2 / 4) - 10, Sprite.Texture.Height * 2);
        }
    }
}

