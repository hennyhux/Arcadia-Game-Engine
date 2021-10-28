using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using GameSpace.Enums;
using GameSpace.EntitiesManager;
using GameSpace.Abstracts;

namespace GameSpace.GameObjects.BlockObjects
{
    public class BrickBlock : AbstractBlock
    {
        public BrickBlock(Vector2 initalPosition)
        {
            this.ObjectID = (int)BlockID.BRICKBLOCK;
            this.Sprite = SpriteBlockFactory.GetInstance().ReturnBrickBlock();
            this.Position = initalPosition;
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            state = new StateBrickBlockIdle();
        }

        public override void Update(GameTime gametime)
        {

            state.Update(gametime);
            if((state is StateExplodingBlock))
            {
                //this.Position = new Vector2((float)-50, (float)50);
                BumpAnimation sprite = (BumpAnimation)Sprite;

                if (sprite.animationFinished)
                {
                    this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 0, 0);
                    this.Position = new Vector2((float)0, (float)0);
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
                        //Debug.WriteLine("BUMP BLOCK, mario PowerUp {0}", mario.marioPowerUpState);
                        this.Trigger();
                    }
                }
                
            } 
        }
    }
}
