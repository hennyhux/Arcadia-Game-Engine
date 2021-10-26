using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Abstracts
{
    public abstract class AbstractItemBlock : IGameObjects
    {
        public ISprite Sprite { get ; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }
        public int ObjectID { get; set; }

        public IBlockStates state;
        public bool drawBox;
        public GameTime internalGametime;
        public bool revealedItem;


        public virtual void Draw(SpriteBatch spritebatch)
        {
            state.Draw(spritebatch, Position);
            if (drawBox) state.DrawBounds(spritebatch, CollisionBox);
        }

        public virtual void Update(GameTime gametime)
        {
            state.Update(gametime);
            internalGametime = gametime;
        }

        public virtual void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

        public virtual void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    CollisionWithMario(entity);
                    break;
            }
        }

        public virtual void CollisionWithMario(IGameObjects entity)
        {
            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.DOWN)
            {
                if (!revealedItem) this.Trigger();
            }
        }

        public virtual bool IsCurrentlyColliding()
        {
            return false; //future use 
        }

        public virtual void Trigger()
        {
            //override... 
        }

        public virtual void UpdatePosition(Vector2 location, GameTime gametime)
        {
            //block doesnt move; future use? 
        }
    }
}
