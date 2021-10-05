using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.GameObjects.BlockObjects
{
    public class HiddenBlock : IGameObjects
    {

        private IBlockState state;
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }

        public Vector2 Location => throw new NotImplementedException();

        public HiddenBlock(Vector2 initalPosition)
        {
            this.state = new StateBlockIdle();
            this.Sprite = BlockSpriteFactory.GetInstance().ReturnHiddenBlock();
            this.Position = initalPosition; 
        }

        public void Draw(SpriteBatch spritebatch)
        {
            Sprite.Draw(spritebatch, Position); //this shouldnt be hardcoded anymore 
        }

        public void Update(GameTime gametime)
        {
            Sprite.Update(gametime);
        }

        public void Trigger()
        {
            state = new StateBlockBumped(this);
        }

        public void SetPosition(Vector2 location)
        {
            throw new NotImplementedException();
        }

        public void HandleCollsion()
        {
            throw new NotImplementedException();
        }

        public void HandleCollision(IGameObjects entity)
        {
            throw new NotImplementedException();
        }
    }
}
