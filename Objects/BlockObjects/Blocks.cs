using GameSpace.Abstracts;
using GameSpace.EntitiesManager;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using GameSpace.States;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace GameSpace.Objects.BlockObjects
{
    public class BrickBlocks : AbstractBlock
    {
        public BrickBlocks(Vector2 initLocation)
        {
            ObjectID = (int)BlockID.BRICKBLOCK;
            Sprite = SpriteBlockFactory.GetInstance().ReturnBrickBlock();
            Position = initLocation;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            state = new StateBrickBlockIdle();
        }

        public override void Update(GameTime gametime)
        {

            state.Update(gametime);
            if (state is StateExplodingBlock)
            {
                //this.Position = new Vector2((float)-50, (float)50);
                BumpAnimation sprite = (BumpAnimation)Sprite;

                if (sprite.animationFinished)
                {
                    CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 0, 0);
                    Position = new Vector2(0, 0);
                    if (Sprite.GetVisibleStatus() == true)
                    {
                        Sprite.SetVisible();
                    }

                    //this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 0, 0);
                }

            }
        }

        public override void Trigger()
        {
            state = new StateBrickBlockBump(this);
        }


        public override void HandleCollision(IGameObjects entity)
        {
            hasCollided = true;

            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.DOWN)
            {
                if (entity is Mario)
                {
                    Mario mario = (Mario)entity;
                    if (mario.marioPowerUpState is BigMarioState || mario.marioPowerUpState is FireMarioState)
                    {
                        Debug.WriteLine("SHATTER BLOCK, mario PowerUp {0}", mario.marioPowerUpState);
                        //state = new StateExplodingBlock(this);
                    }
                    else
                    {
                        RevealItem();
                        Trigger();
                    }
                }

            }
        }

        public override bool RevealItem()
        {
            return false;
        }
    }

    public class BrickBlockWithItem : BrickBlocks
    {
        private bool hasRevealedItem;
        public BrickBlockWithItem(Vector2 initLocation, IGameObjects item) : base(initLocation)
        {
            this.item = item;
            hasRevealedItem = false;
        }

        public override bool RevealItem()
        {
            if (!hasRevealedItem)
            {
                item.Position = new Vector2(item.Position.X, item.Position.Y - 32);
                TheaterMachine.GetInstance().AddItemToStage(item);
                hasRevealedItem = true;
            }
            return hasRevealedItem;
        }
    }
}
