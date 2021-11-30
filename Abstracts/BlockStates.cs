using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using GameSpace.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Abstracts
{
    public interface IBlockState
    {
        public void Draw(SpriteBatch spriteBatch, Vector2 location);
        public void DrawBounds(SpriteBatch spriteBatch, Rectangle CollisionBox);
        public void Update(GameTime gameTime);
        public void Trigger();
        public void RevealItem();
    }


    public abstract class BlockStates : IBlockState
    {
        public ISprite StateSprite { get; set; }
        internal protected HiddenBlock block;

        protected BlockStates(HiddenBlock block)
        {
            this.block = block;
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            StateSprite.Draw(spriteBatch, location);
        }

        public virtual void DrawBounds(SpriteBatch spriteBatch, Rectangle CollisionBox)
        {
            StateSprite.DrawBoundary(spriteBatch, CollisionBox);
        }

        public virtual void Update(GameTime gameTime)
        {
            StateSprite.Update(gameTime);
            UpdateCollisionBox();
        }

        public abstract void Trigger();
        public virtual void RevealItem()
        {

        }
        internal protected virtual void UpdateCollisionBox()
        {
            block.CollisionBox = new Rectangle((int)block.Position.X, (int)block.Position.Y,
                StateSprite.Texture.Width * 2, StateSprite.Texture.Height * 2);
        }
    }

    public abstract class Block : IGameObjects
    {
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }

        public int ObjectID { get; set; }
        internal IBlockState state;
        internal AbstractItem item;
        private bool drawBox;
        public virtual void Draw(SpriteBatch spritebatch)
        {
            state.Draw(spritebatch, Position);
            if (drawBox)
            {
                state.DrawBounds(spritebatch, CollisionBox);
            }
        }

        public virtual void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    CollisionHandler.GetInstance().BlockToMarioCollision(this);
                    break;
            }
        }

        public virtual bool RevealItem()
        {
            return false;
        }

        public virtual void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

        public virtual void Trigger()
        {
            state.Trigger();
        }

        public virtual void Update(GameTime gametime)
        {
            state.Update(gametime);
        }
    }

    public class StateHiddenBlockBumpedRedux : BlockStates
    {
        public StateHiddenBlockBumpedRedux(HiddenBlock block) : base(block)
        {
            StateSprite = new BumpAnimation(SpriteBlockFactory.GetInstance().ReturnUsedBlock().Texture, (int)block.Position.X, (int)block.Position.Y, 24);
        }

        public override void Trigger()
        {
            // does nothing
        }
    }

    public class StateHiddenBlockIdleRedux : BlockStates
    {
        public StateHiddenBlockIdleRedux(HiddenBlock block) : base(block)
        {
            StateSprite = SpriteBlockFactory.GetInstance().ReturnHiddenBlock();
        }

        public override void Trigger()
        {
            block.state = new StateHiddenBlockBumpedRedux(block);
        }
    }
}
