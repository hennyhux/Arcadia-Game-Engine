using GameSpace.Abstracts;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using GameSpace.Objects.BlockObjects;

namespace GameSpace.States
{
    public class StateBlockBumped : AbstractBlockStates
    {
        public StateBlockBumped(IGameObjects block)
        {
            if (block is QuestionBlock || block is BrickBlockWithItem)
            {
                sprite = new BumpAnimation(SpriteBlockFactory.GetInstance().ReturnUsedBlock().Texture, (int)block.Position.X, (int)block.Position.Y, 24);
            }

            else
            {
                sprite = new BumpAnimation(block.Sprite.Texture, (int)block.Position.X, (int)block.Position.Y, 24);
            }
        }
    }
}
