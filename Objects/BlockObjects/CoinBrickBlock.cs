using GameSpace.Animations;
using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States;
using GameSpace.States.StateMachines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Objects.BlockObjects
{
    public class CoinBrickBlock : IGameObjects
    {
        public ISprite Sprite { get; set; }
        public Vector2 Position { get ; set ; }
        public Vector2 Velocity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector2 Acceleration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Rectangle CollisionBox { get; set; }

        public int ObjectID { get; set; }

        private bool drawBox;
        private IBlockStateMachine state;
        private GameTime internalGameTime;
        private SpriteBatch internalSpritebatch;

        public CoinBrickBlock (Vector2 initialPosition)
        {
            state = new SMCoinBrickBlock();
            ObjectID = (int)BlockID.COINBRICKBLOCK;
            Position = initialPosition;
            Sprite = SpriteBlockFactory.GetInstance().ReturnBrickBlock(); // we could delete this line to save memory but i believe the garbage collector will get it 
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            drawBox = false;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            state.Draw(spritebatch, Position); 
            if (drawBox) state.FindSprite().DrawBoundary(spritebatch, CollisionBox);
            if (internalSpritebatch == null) internalSpritebatch = spritebatch;
        }
        public void Update(GameTime gametime)
        {
            state.Update(gametime);
            if (internalGameTime == null) internalGameTime = gametime;
        }

        public void HandleCollision(IGameObjects entity)
        {
            this.Trigger();
        }

        public void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

        public void Trigger()
        {
            state.SetSprite(new BumpAnimation(Sprite.Texture, (int)Position.X, (int)Position.Y, 24));
            EntityManager.AddAnimation(new CoinExitingBlockAnimation(Position, internalGameTime));
        }

        public bool IsCurrentlyColliding()
        {
            throw new NotImplementedException();
        }

        public void SetPosition(Vector2 location)
        {
            throw new NotImplementedException();
        }

    }
}
