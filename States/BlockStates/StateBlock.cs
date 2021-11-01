using GameSpace.Abstracts;
using GameSpace.Factories;
using GameSpace.Interfaces;

namespace GameSpace.States.BlockStates
{
    public class StateBrickBlockBump : AbstractBlockStates
    {
        public StateBrickBlockBump(IGameObjects block)
        {
            sprite = new BumpAnimation(SpriteBlockFactory.GetInstance().ReturnUsedBlock().Texture, (int)block.Position.X, (int)block.Position.Y, 24);
        }
    }

    public class StateBrickBlockIdle : AbstractBlockStates
    {
        public StateBrickBlockIdle()
        {
            sprite = SpriteBlockFactory.GetInstance().ReturnBrickBlock();
        }
    }

    public class StateHiddenBlockBump : AbstractBlockStates
    {
        public StateHiddenBlockBump(IGameObjects block)
        {
            sprite = sprite = new BumpAnimation(SpriteBlockFactory.GetInstance().ReturnUsedBlock().Texture, (int)block.Position.X, (int)block.Position.Y, 24);
        }
    }

    public class StateHiddenBlockIdle : AbstractBlockStates
    {
        public StateHiddenBlockIdle()
        {
            sprite = SpriteBlockFactory.GetInstance().ReturnHiddenBlock();
        }
    }

    public class StateFloorBlock : AbstractBlockStates
    {
        public StateFloorBlock()
        {
            sprite = SpriteBlockFactory.GetInstance().ReturnFloorBlock();
        }
    }

    public class StateStairBlock : AbstractBlockStates
    {
        public StateStairBlock()
        {
            sprite = SpriteBlockFactory.GetInstance().ReturnStairBlock();
        }
    }

}
