using GameSpace.Camera2D;
using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States.EnemyStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GameSpace.GameObjects.EnemyObjects
{
    public class Goomba : IGameObjects
    {
        private IEnemyState state;

        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }

        public int ObjectID { get; set; }
        private Boolean hasCollidedOnTop;
        private Boolean drawBox;
        private int countDown;
        private int direction;
        public Rectangle ExpandedCollisionBox { get; set; }

        public Goomba(Vector2 initalPosition)
        {
            ObjectID = (int)EnemyID.GOOMBA;
            direction = (int)eFacing.LEFT;
            Velocity = new Vector2(0, 0);
            Acceleration = new Vector2(0, 0);

            this.Sprite = SpriteEnemyFactory.GetInstance().CreateGoombaSprite();
            this.Position = initalPosition;
            this.CollisionBox = new Rectangle((int)(Position.X + Sprite.Texture.Width / 32), (int)Position.Y, Sprite.Texture.Width, Sprite.Texture.Height * 2);
            ExpandedCollisionBox = new Rectangle((int)(Position.X + Sprite.Texture.Width / 32), (int)Position.Y, Sprite.Texture.Width, Sprite.Texture.Height * 3);

            drawBox = false;
            state = new StateGoombaAlive();
        }

        public void Draw(SpriteBatch spritebatch)
        {
            state.Draw(spritebatch, Position);
            if (drawBox && !hasCollidedOnTop)
            {
                Sprite.DrawBoundary(spritebatch, CollisionBox);
                Sprite.DrawBoundary(spritebatch, ExpandedCollisionBox);
            }
            if (hasCollidedOnTop)countDown++;
        }

        public void Update(GameTime gametime)
        {
            state.Update(gametime);
            if (!hasCollidedOnTop && IsInview()) UpdatePosition(Position, gametime);
            if (countDown == 90) state.StateSprite.SetVisible();
        }

        private bool IsInview()
        {
            Camera copyCam = EntityManager.Camera;
            Debug.WriteLine(copyCam.Position.X);
            return (Position.X > copyCam.Position.X && Position.X < copyCam.Position.X + 800);
        }
        public void Trigger()
        {
            state = new StateGoombaDead();
            countDown = 0;
        }

        public void UpdatePosition(Vector2 location, GameTime gameTime)
        {

            if (EntityManager.IsGoingToFall(this))
            {
                //Velocity = new Vector2(0, Velocity.Y);
                Acceleration = new Vector2(0, 400);
            }

            else if (!EntityManager.IsGoingToFall(this))
            {
                Acceleration = new Vector2(0, 0);
                if (direction == (int)eFacing.RIGHT)Velocity = new Vector2(85, 0);
                if (direction == (int)eFacing.LEFT) Velocity = new Vector2(-85, 0);
            }

            Velocity += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            UpdateCollisionBox(Position);
        }

        public void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    CollisionWithMario(entity);
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
        private void CollisionWithBlock(IGameObjects block)
        {
            if (EntityManager.DetectCollisionDirection(this, block) == (int)CollisionDirection.LEFT)
            {
                direction = (int)eFacing.LEFT;
            }

            else if (EntityManager.DetectCollisionDirection(this, block) == (int)CollisionDirection.RIGHT)
            {
                direction = (int)eFacing.RIGHT;
            }

            else if (EntityManager.DetectCollisionDirection(this, block) == (int)CollisionDirection.UP)
            {
                PreformBounce();
                HaltAllMotion();
            }
        }


        private void CollisionWithMario(IGameObjects mario)
        {
            if (EntityManager.DetectCollisionDirection(this, mario) == (int)CollisionDirection.UP)
            {
                this.Trigger();
                this.CollisionBox = new Rectangle(1, 1, 0, 0);
                if (!hasCollidedOnTop) hasCollidedOnTop = true;
            }
        }

        public void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

        public bool IsCurrentlyColliding()
        {
            throw new NotImplementedException();
        }

        private void UpdateCollisionBox(Vector2 location)
        {
            this.CollisionBox = new Rectangle((int)location.X + state.StateSprite.Texture.Width / 4 - 6 , (int)Position.Y,
                state.StateSprite.Texture.Width, state.StateSprite.Texture.Height * 2);

            this.ExpandedCollisionBox = new Rectangle((int)location.X + state.StateSprite.Texture.Width / 4 - 6, (int)Position.Y,
                state.StateSprite.Texture.Width, (state.StateSprite.Texture.Height * 2) + 3);
        }

        private void HaltAllMotion()
        {
            Velocity = new Vector2(0, 0);
            Acceleration = new Vector2(0, 0);
        }

        private void PreformBounce()
        {
            Position = new Vector2(Position.X, Position.Y - 4);
        }
    }
}

