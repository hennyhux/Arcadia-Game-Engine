using GameSpace.Animations;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States;
using GameSpace.States.StateMachines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameSpace.Objects.BlockObjects
{
    public class BrickBlockCoin : IGameObjects
    {
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector2 Acceleration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Rectangle CollisionBox { get; set; }

        public int ObjectID { get; set; }

        private bool drawBox;
        private readonly IBlockStateMachine state;
        private GameTime internalGameTime;

        public BrickBlockCoin(Vector2 initialPosition)
        {
            state = new SMCoinBrickBlock();
            ObjectID = (int)BlockID.COINBRICKBLOCK;
            Position = initialPosition;
            Sprite = SpriteBlockFactory.GetInstance().ReturnBrickBlock();
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            drawBox = false;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            state.Draw(spritebatch, Position);
            if (drawBox)
            {
                state.FindSprite().DrawBoundary(spritebatch, CollisionBox);
            }
        }
        public void Update(GameTime gametime)
        {
            state.Update(gametime);
        }

        public void HandleCollision(IGameObjects entity)
        {
            Trigger();
        }

        public void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

        public void Trigger()
        {
            state.SetSprite(new BumpAnimation(Sprite.Texture, (int)Position.X, (int)Position.Y, 24));
            AnimationHandler.GetInstance().AddAnimation(new CoinExitingBlockAnimation(Position, internalGameTime));
        }

        public bool IsCurrentlyColliding()
        {
            throw new NotImplementedException();
        }

        public void UpdatePosition(Vector2 location, GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public bool RevealItem()
        {
            throw new NotImplementedException();
        }
    }
}
