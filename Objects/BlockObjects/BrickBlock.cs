using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using GameSpace.Enums;

namespace GameSpace.GameObjects.BlockObjects
{
    public class BrickBlock : IGameObjects
    {

        private IBlockState state;
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }

        public int ObjectID { get; set; }

        private Boolean hasCollided;
        private Boolean drawBox;

        public BrickBlock(Vector2 initalPosition)
        {
            this.ObjectID = (int)BlockID.BRICKBLOCK;
            this.state = new StateBlockIdle();
            this.Sprite = SpriteBlockFactory.GetInstance().ReturnBrickBlock();
            this.Position = initalPosition;
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            drawBox = false;
            //Debug.WriteLine("BRICK BLOCK AT " + "(" + this.Position.X + ", " + this.Position.Y + ")");
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
            state = new StateBlockBumped(this);
        }

        public void SetPosition(Vector2 newLocation)
        {
           Velocity = (float)4 * newLocation;
           Position += Velocity;
           CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);

        }

        //SWEPT DETECTION
        public void HandleCollision(IGameObjects entity)
        {
            if (!hasCollided)hasCollided = true;
            //find the direction of collision...
            
            //after finding the direction of collision, play animation 
            if (FindCollisionDetection(entity) == (int)CollisionDirection.DOWN)
            {
                this.Trigger();
            }
        }

        private int FindCollisionDetection(IGameObjects entity)
        {
            Rectangle overlap = Rectangle.Intersect(this.CollisionBox, entity.CollisionBox);
            int direction = 0;
            if (overlap.Width > overlap.Height)
            {
                direction = (int)CollisionDirection.DOWN; // or up! need to find a way to determine it 
            }

            return direction;
        }

        public void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }
    }
}
