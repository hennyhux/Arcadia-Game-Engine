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
using System.Diagnostics;


namespace GameSpace.Objects.BlockObjects
{

    public class StateExplodingBrickBlock : AbstractBlockStates
    {
        public StateExplodingBrickBlock() 
        {
            sprite = SpriteBlockFactory.GetInstance().ReturnShatterBlock();

        }
    }

    public class StateGoneBrickBlock : AbstractBlockStates
    {
        public StateGoneBrickBlock()
        {
            sprite = SpriteBlockFactory.GetInstance().ReturnShatterBlock();
            sprite.SetVisible();
        }
    }

    public class BrickBlock : AbstractBlock
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
            state.Update(gametime);
            if (state is StateExplodingBrickBlock) 
            { 
                ++this.counter;
                if (this.counter >= 10)
                {
                    state = new StateGoneBrickBlock();
                    CollisionBox = new Rectangle();
                }
            }
        }

        public override void Trigger()
        {

            if (!hasCollided && MarioHandler.GetInstance().IsCurrentlyBigMario())
            {
                state = new StateExplodingBrickBlock();
                MusicHandler.GetInstance().PlaySoundEffect(9);
            }

            else if (!hasCollided)
            {
                state = new StateBlockBumped(this);
                MusicHandler.GetInstance().PlaySoundEffect(8);
            }

        }

        public override void HandleCollision(IGameObjects entity)
        {

            if (CollisionHandler.GetInstance().DetectCollisionDirection(this, entity) == (int)CollisionDirection.DOWN)
            {
                if (entity is Mario)
                {
                    RevealItem();
                    Trigger();
                }
            }
        }

        public override bool RevealItem()
        {
            return false;
        }
    }

    public class BrickBlockWithItem : BrickBlock
    {
        private bool hasRevealedItem;
        public BrickBlockWithItem(Vector2 initLocation, AbstractItem item) : base(initLocation)
        {
            this.item = item;
            hasRevealedItem = false;
        }

        public override bool RevealItem()
        {
            if (!hasRevealedItem)
            {
                item.AdjustLocationComingOutOfBlock();
                TheaterHandler.GetInstance().AddItemToStage(item);
                hasRevealedItem = true;
            }
            return hasRevealedItem;
        }
    }
}
