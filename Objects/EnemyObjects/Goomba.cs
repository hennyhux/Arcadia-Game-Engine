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
        private Boolean hasCollided;
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
            CurrentState = new EnemyAliveState(this);

        }

        public void Draw(SpriteBatch spritebatch)
        {
            Sprite.Draw(spritebatch, Position); //this shouldnt be hardcoded anymore 
            if (drawBox && !hasCollided) Sprite.DrawBoundary(spritebatch, CollisionBox);
            if (hasCollided)countDown++;
            
        }

        public void Update(GameTime gametime)
        {
            Sprite.Update(gametime);
            if (countDown == 90) this.Sprite.SetVisible();
        }

        public void Trigger()
        {
            CurrentState = new EnemyDeadState(this);
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
            if (!hasCollided) hasCollided = true;

            switch (entity.ObjectID)
            {
                case (int)BlockID.BRICKBLOCK:
                    CollisionWithBumpBlock(entity);
                    Trigger();
                    break;

                case (int)BlockID.QUESTIONBLOCK:
                    CollisionWithBumpBlock(entity);
                    break;

                case (int)ItemID.COIN:
                    //change internal state to include one coin...
                    break;

                case (int)EnemyID.GOOMBA:
                    //goomba dead state...
                    break;
            }
        }

        #region Testing Methods
        //GameSpace.States.EnemyStates.EnemyDeadState
        private void MoveObjectOffset(int offsetX, int offsetY)
        {

            this.CollisionBox = new Rectangle((int)(Position.X - offsetX) + Sprite.Texture.Width / 4,
            (int)(Position.Y - offsetY), Sprite.Texture.Width / 2, Sprite.Texture.Height * 2);
            this.Position = new Vector2((int)(Position.X - offsetX), (int)(Position.Y - offsetY));
            
        }

        private void CollisionWithBumpBlock(IGameObjects entity)
        {
            //the offsetY SHOULD be 0, but I believe due to some issues with the sprite res, a fudge factor of 6
            //is required to ensure proper allignment after collision
            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.LEFT) { MoveObjectOffset(1, 0); }

            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.RIGHT) { MoveObjectOffset(-1, 0); }

            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.UP) { MoveObjectOffset(0, -1); }

            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.DOWN){ MoveObjectOffset(0, 1); }
        }

        #endregion
        public void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }
    }
}

