﻿using GameSpace.Abstracts;
using GameSpace.EntitiesManager;
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
using GameSpace.Machines;


namespace GameSpace.Objects.BlockObjects
{

    public class StateExplodingBrickBlock : AbstractBlockStates
    {
        public StateExplodingBrickBlock()
        {
            sprite = SpriteBlockFactory.GetInstance().ReturnShatterBlock();
        }
    }

    public class BrickBlock : AbstractBlock
    {
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
        }

        public override void Trigger()
        {

            if (state is StateBlockIdle && !hasCollided && MarioHandler.GetInstance().IsCurrentlyBigMario())
            {
                state = new StateExplodingBrickBlock();
            }

            else if (state is StateBrickBlockIdle && !hasCollided)
            {
                state = new StateBlockBumped(this);
                MusicHandler.GetInstance().PlaySoundEffect(8);
            }

        }

        public override void HandleCollision(IGameObjects entity)
        {

            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.DOWN)
            {
                if (entity is Mario)
                {
                    Mario mario = (Mario)entity;
                    if (mario.marioPowerUpState is BigMarioState || mario.marioPowerUpState is FireMarioState)
                    {
                        Debug.WriteLine("SHATTER BLOCK, mario PowerUp {0}", mario.marioPowerUpState);
                        //state = new StateExplodingBlock(this);
                        MusicHandler.GetInstance().PlaySoundEffect(9);
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
