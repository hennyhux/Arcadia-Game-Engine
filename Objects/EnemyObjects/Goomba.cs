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
        private IEnemyStates CurrentState;

        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }

        public Rectangle CollisionBox { get; set; }

        public int ObjectID { get; set; }
        private Boolean hasCollidedOnTop;
        private Boolean drawBox;
        private int countDown;

        public Goomba(Vector2 initalPosition)
        {
            //some initial state 
            ObjectID = (int)EnemyID.GOOMBA;
            this.Sprite = SpriteEnemyFactory.GetInstance().CreateGoombaSprite();
            this.Position = initalPosition;
            //magic numbers to offset the weird texture atlas resoultion and upscaling
            this.CollisionBox = new Rectangle((int)(Position.X + Sprite.Texture.Width / 32), (int)Position.Y, Sprite.Texture.Width, Sprite.Texture.Height * 2);
            drawBox = false;
            CurrentState = new GoombaAliveState(this);

        }

        public void Draw(SpriteBatch spritebatch)
        {
            Sprite.Draw(spritebatch, Position); //this shouldnt be hardcoded anymore 
            if (drawBox && !hasCollidedOnTop) Sprite.DrawBoundary(spritebatch, CollisionBox);
            if (hasCollidedOnTop)countDown++;
            
        }

        public void Update(GameTime gametime)
        {
            Sprite.Update(gametime);
            if (countDown == 90) this.Sprite.SetVisible();
        }

        public void Trigger()
        {
            CurrentState = new GoombaDeadState(this);
            countDown = 0;
        }

        public void SetPosition(Vector2 location)
        {
            Velocity = (float)6 * location;
            Position += Velocity;
            CollisionBox = new Rectangle((int)(Position.X + Sprite.Texture.Width / 32), (int)Position.Y, Sprite.Texture.Width, Sprite.Texture.Height * 2);
        }

        public void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    CollisionWithMario(entity);
                    break;
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
    }
}

