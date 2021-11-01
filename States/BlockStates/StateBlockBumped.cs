using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;

namespace GameSpace.States
{
    public class StateBlockBumped : IObjectState
    {
        private readonly IGameObjects block;

        public StateBlockBumped(IGameObjects block)
        {
            this.block = block;
            if (block.Sprite.Texture.Name.Equals("Blocks/QuestionBlock"))
            {
                block.Sprite = new BumpAnimation(SpriteBlockFactory.GetInstance().ReturnUsedBlock().Texture, (int)block.Position.X, (int)block.Position.Y, 24);
            }

            else
            {
                block.Sprite = new BumpAnimation(block.Sprite.Texture, (int)block.Position.X, (int)block.Position.Y, 24);
            }
        }
        public void Update(GameTime gametime)
        {

        }
    }
}
