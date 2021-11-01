using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameSpace.GameObjects.BlockObjects
{
    public class UsedBlock : IGameObjects
    {

        private readonly IObjectState state;
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }

        public Vector2 Location => throw new NotImplementedException();

        public Rectangle CollisionBox { get; set; }

        public int ObjectID { get; set; }
        private readonly Boolean hasCollided;
        private Boolean drawBox;

        public UsedBlock(Vector2 initalPosition)
        {
            ObjectID = (int)BlockID.USEDBLOCK;
            state = new StateBlockIdle();
            Sprite = SpriteBlockFactory.GetInstance().ReturnUsedBlock();
            Position = initalPosition;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
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

        public void UpdatePosition(Vector2 location, GameTime gameTime)
        {
            throw new NotImplementedException();
        }
        public void HandleCollision(IGameObjects entity)
        {

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
