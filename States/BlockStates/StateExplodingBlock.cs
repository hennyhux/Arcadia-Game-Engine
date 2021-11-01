using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;

namespace GameSpace.States
{
    public class StateExplodingBlock : IObjectState
    {
        private readonly IGameObjects block;

        public StateExplodingBlock(IGameObjects block)
        {
            this.block = block;
            if (block.Sprite.Texture.Name.Equals("Blocks/BrickBlock"))
            {
                block.Sprite = new BumpAnimation(SpriteBlockFactory.GetInstance().ReturnShatterBlock().Texture, (int)block.Position.X, (int)block.Position.Y, 48);
            }

            else
            {
                block.Sprite = new BumpAnimation(block.Sprite.Texture, (int)block.Position.X, (int)block.Position.Y, 48);
            }
        }
        public void Update(GameTime gametime)
        {

        }
    }
}
