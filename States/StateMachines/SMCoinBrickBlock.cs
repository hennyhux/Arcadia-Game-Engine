using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameSpace.States.StateMachines
{
    public class SMCoinBrickBlock : IBlockStateMachine
    {
        private ISprite sprite;
        private bool hasBeenTriggered;

        public SMCoinBrickBlock()
        {
            sprite = SpriteBlockFactory.GetInstance().ReturnBrickBlock();
            hasBeenTriggered = false;
        }

        public ISprite FindSprite()
        {
            return sprite;
        }

        public void SetSprite(ISprite sprite)
        {
            this.sprite = sprite;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void Trigger()
        {
            if (!hasBeenTriggered)
            {
                sprite = SpriteBlockFactory.GetInstance().ReturnUsedBlock();
                hasBeenTriggered = true;
            }
        }

        public void Update(GameTime gametime)
        {
            sprite.Update(gametime);
        }

        public void SetLocationRectangle()
        {
            throw new NotImplementedException();
        }

        public void SetLocationVector()
        {
            throw new NotImplementedException();
        }

    }
}
