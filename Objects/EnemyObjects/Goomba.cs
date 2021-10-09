using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
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
        private IObjectState state;
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }

        public Rectangle CollisionBox { get; set; }

        public int ObjectID { get; set; }
        private Boolean hasCollided;
        private Boolean drawBox;

        public Goomba(Vector2 initalPosition)
        {
            //some initial state 
            ObjectID = (int)EnemyID.GOOMBA;
            this.Sprite = SpriteEnemyFactory.GetInstance().CreateGoombaSprite();
            this.Position = initalPosition;
            //magic numbers to offset the weird texture atlas resoultion 
            this.CollisionBox = new Rectangle((int)Position.X + Sprite.Texture.Width / 4, (int)Position.Y, Sprite.Texture.Width / 2, Sprite.Texture.Height * 2);
            drawBox = false;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            Sprite.Draw(spritebatch, Position); //this shouldnt be hardcoded anymore 
            if (drawBox) Sprite.DrawBoundary(spritebatch, CollisionBox);
        }

        public void Update(GameTime gametime)
        {
            Sprite.Update(gametime);
        }

        public void Trigger()
        {
            
        }

        public void SetPosition(Vector2 location)
        {
            Velocity = (float)5 * location;
            Position += Velocity;
            CollisionBox = new Rectangle((int)Position.X + Sprite.Texture.Width / 4, (int)Position.Y, Sprite.Texture.Width / 2, Sprite.Texture.Height * 2);
        }

        public void HandleCollision(IGameObjects entity)
        {
            if (!hasCollided) hasCollided = true;

            switch (entity.ObjectID)
            {
                case (int)BlockID.BRICKBLOCK:
                    CollisionWithBumpBlock(entity);
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
        private void MoveObject(int offsetX, int offsetY)
        {
            this.Position = new Vector2((int)(Position.X - offsetX), (int)(Position.Y - offsetY ));
            this.CollisionBox = new Rectangle((int)(Position.X - offsetX) + Sprite.Texture.Width / 4,
                (int)(Position.Y - offsetY), Sprite.Texture.Width / 2, Sprite.Texture.Height * 2);
        }

        private void CollisionWithBumpBlock(IGameObjects entity)
        {
            //the offsetY SHOULD be 0, but I believe due to some issues with the sprite res, a fudge factor of 6
            //is required to ensure proper allignment after collision
            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.LEFT) { MoveObject(1, 0); }

            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.RIGHT) { MoveObject(-1, 0); }

            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.UP) { MoveObject(0, -1); }

            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.DOWN){ MoveObject(0, 1); }
        }

        #endregion
        public void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }
    }
}

