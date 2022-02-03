using GameSpace.Abstracts;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using GameSpace.Machines;
using GameSpace.States;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;


namespace GameSpace.Objects.BlockObjects
{

    public class StateExplodingBrickBlock : BlockState
    {
        public StateExplodingBrickBlock()
        {
            StateSprite = SpriteBlockFactory.GetInstance().ReturnShatterBlock();
        }

    }

    public class StateGoneBrickBlock : BlockState
    {
        public StateGoneBrickBlock()
        {
            StateSprite = SpriteBlockFactory.GetInstance().ReturnShatterBlock();
            StateSprite.SetVisible();
        }
    }

    public class BrickBlock : Blocks
    {
        private int counter = 0;
        public BrickBlock(Vector2 initLocation)
        {
            ObjectID = (int)BlockID.BRICKBLOCK;
            Sprite = SpriteBlockFactory.GetInstance().ReturnBrickBlock();
            Position = initLocation;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            state = new StateBrickBlockIdle();
            hasCollided = false;
        }

        public override void Update(GameTime gametime)
        {
          
        }

        public override void Trigger()
        {

       

        }

        public override void HandleCollision(IGameObjects entity)
        {

            
        }

        public override bool RevealItem()
        {
            return false;
        }
    }

    public class BrickBlockWithItem : BrickBlock
    {
        private bool hasRevealedItem;
        public BrickBlockWithItem(Vector2 initLocation, Item item) : base(initLocation)
        {
            this.item = item;
            hasRevealedItem = false;
        }

     
    }
}
