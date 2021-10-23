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

namespace GameSpace.GameObjects.BlockObjects
{
    public class BrickBlock : IGameObjects
    {

        private IObjectState state;
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }

        public int ObjectID { get; set; }

        private Boolean hasCollided;
        private Boolean drawBox;

        public BrickBlock(Vector2 initalPosition)
        {
            this.ObjectID = (int)BlockID.BRICKBLOCK;
            this.state = new StateBlockIdle();
            this.Sprite = SpriteBlockFactory.GetInstance().ReturnBrickBlock();
            this.Position = initalPosition;
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            drawBox = false;
            //Debug.WriteLine("BRICK BLOCK AT " + "(" + this.Position.X + ", " + this.Position.Y + ")");
        }

        public void Draw(SpriteBatch spritebatch)
        {
            Sprite.Draw(spritebatch, Position); //this shouldnt be hardcoded anymore 
            if (drawBox) Sprite.DrawBoundary(spritebatch, CollisionBox);
        }

        public void Update(GameTime gametime)
        {

            Sprite.Update(gametime);
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

        public void Trigger()
        {
            state = new StateBlockBumped(this);
        }

        public void SetPosition(Vector2 newLocation)
        {

        }

        public void HandleCollision(IGameObjects entity)
        {
            //hasCollided = true;

            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.DOWN)
            {
                if (entity is Mario)
                {
                    Mario mario = (Mario)entity;
                    if (mario.marioPowerUpState is BigMarioState || mario.marioPowerUpState is FireMarioState)
                    {
                        Debug.WriteLine("SHATTER BLOCK, mario PowerUp {0}", mario.marioPowerUpState);
                        state = new StateExplodingBlock(this);
                    }
                    else
                    {
                        //Debug.WriteLine("BUMP BLOCK, mario PowerUp {0}", mario.marioPowerUpState);
                        this.Trigger();
                    }
                }
                
            } 
        }

        public void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

        public bool IsCurrentlyColliding()
        {
            throw new NotImplementedException();
        }
    }
}
