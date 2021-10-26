using GameSpace.Animations;
using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States;
using GameSpace.States.BlockStates;
using GameSpace.States.StateMachines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Objects.BlockObjects
{
    public class BrickBlockOneShroom : IGameObjects
    {
        public ISprite Sprite { get; set; }
        public Vector2 Position { get ; set ; }
        public Vector2 Velocity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector2 Acceleration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Rectangle CollisionBox { get; set; }

        public int ObjectID { get; set; }

        private bool drawBox;
        private bool revealedItem;
        private IBlockStates state;
        private IGameObjects shroom;
        private GameTime internalGameTime;
        private SpriteBatch internalSpritebatch;

        public BrickBlockOneShroom(Vector2 initialPosition)
        {
            state = new StateBrickBlockIdle();
            Position = initialPosition;
            Sprite = SpriteBlockFactory.GetInstance().ReturnBrickBlock(); // we could delete this line to save memory but i believe the garbage collector will get it 
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            drawBox = false;
            revealedItem = false;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            state.Draw(spritebatch, Position); 
            if (drawBox) state.DrawBounds(spritebatch, CollisionBox);
            if (internalSpritebatch == null) internalSpritebatch = spritebatch;
        }
        public void Update(GameTime gametime)
        {
            state.Update(gametime);
            if (internalGameTime == null) internalGameTime = gametime;
        }

        public void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    CollisionWithMario(entity);
                    break;
            }
        }

        private void CollisionWithMario(IGameObjects entity)
        {
            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.DOWN)
            {
                if (!revealedItem) this.Trigger();
            }
        }

        public void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

        public void Trigger()
        {
            state = new StateBrickBlockBumped(this);
            shroom = ObjectFactory.GetInstance().CreateOneUpShroomObject(new Vector2(Position.X - 4, Position.Y - Sprite.Texture.Height * 2 - 4));
            EntityManager.AddEntity(shroom);
            revealedItem = true;
        }


        public bool IsCurrentlyColliding()
        {
            throw new NotImplementedException();
        }

        public void UpdatePosition(Vector2 location, GameTime gameTime)
        {
            //block doesnt move
        }

    }
}
