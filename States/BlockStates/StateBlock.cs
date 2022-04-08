using GameSpace.Abstracts;
using GameSpace.Factories;
using GameSpace.Interfaces;

namespace GameSpace.States.BlockStates
{
    public class StateBrickBlockBump : BlockState
    {
        public StateBrickBlockBump(IGameObjects block)
        {
            StateSprite = new BumpAnimation(SpriteBlockFactory.GetInstance().ReturnUsedBlock().Texture, (int)block.Position.X, (int)block.Position.Y, 24);
        }
    }

    public class StateHiddenLevelBrickBlockBump : BlockState
    {
        public StateHiddenLevelBrickBlockBump(IGameObjects block)
        {
            StateSprite = new BumpAnimation(SpriteBlockFactory.GetInstance().ReturnUsedBlock().Texture, (int)block.Position.X, (int)block.Position.Y, 24);
        }
    }

    public class StateBrickBlockIdle : BlockState
    {
        public StateBrickBlockIdle()
        {
            StateSprite = SpriteBlockFactory.GetInstance().ReturnBrickBlock();
        }
    }

    public class StateHiddenLevelBrickBlockIdle : BlockState
    {
        public StateHiddenLevelBrickBlockIdle()
        {
            StateSprite = SpriteBlockFactory.GetInstance().ReturnHiddenLevelBrickBlock();
        }
    }

    public class StateHiddenBlockBump : BlockState
    {
        public StateHiddenBlockBump(IGameObjects block)
        {
            StateSprite = new BumpAnimation(SpriteBlockFactory.GetInstance().ReturnUsedBlock().Texture, (int)block.Position.X, (int)block.Position.Y, 24);
        }
    }

    public class StateHiddenBlockIdle : BlockState
    {
        public StateHiddenBlockIdle()
        {
            StateSprite = SpriteBlockFactory.GetInstance().ReturnHiddenBlock();
        }
    }

    public class StateFloorBlock : BlockState
    {
        public StateFloorBlock()
        {
            StateSprite = SpriteBlockFactory.GetInstance().ReturnFloorBlock();
        }
    }

    public class StateVineBlock : BlockState
    {
        public StateVineBlock()
        {
           // StateSprite = SpriteBlockFactory.GetInstance().ReturnVineBlock();
        }
    }

    public class StateHiddenLevelFloorBlock : BlockState
    {
        public StateHiddenLevelFloorBlock()
        {
            StateSprite = SpriteBlockFactory.GetInstance().ReturnHiddenLevelFloorBlock();
        }
    }

    public class StateStairBlock : BlockState
    {
        public StateStairBlock()
        {
            StateSprite = SpriteBlockFactory.GetInstance().ReturnStairBlock();
        }
    }

}
