using GameSpace.Abstracts;
using GameSpace.Camera2D;
using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States.EnemyStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GameSpace.GameObjects.EnemyObjects
{
    public class Goomba : AbstractEnemy
    {
        public Goomba(Vector2 initalPosition)
        {
            ObjectID = (int)EnemyID.GOOMBA;
            direction = (int)eFacing.LEFT;
            Velocity = new Vector2(0, 0);
            Acceleration = new Vector2(0, 0);

            this.Sprite = SpriteEnemyFactory.GetInstance().CreateGoombaSprite();
            this.Position = initalPosition;
            this.CollisionBox = new Rectangle((int)(Position.X + Sprite.Texture.Width / 32), (int)Position.Y, Sprite.Texture.Width, Sprite.Texture.Height * 2);
            ExpandedCollisionBox = new Rectangle((int)(Position.X + Sprite.Texture.Width / 32), (int)Position.Y, Sprite.Texture.Width, Sprite.Texture.Height * 3);

            drawBox = false;
            state = new StateGoombaAlive();
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            state.Draw(spritebatch, Position);
            if (drawBox && !hasCollidedOnTop)
            {
                state.DrawBoundaries(spritebatch, CollisionBox);
                state.DrawBoundaries(spritebatch, ExpandedCollisionBox);
            }
            if (hasCollidedOnTop)countDown++;
        }

        public override void Update(GameTime gametime)
        {
            state.Update(gametime);
            if (!hasCollidedOnTop && IsInview()) UpdatePosition(Position, gametime);
            if (countDown == 90) state.StateSprite.SetVisible();
        }

        public override void Trigger()
        {
            state = new StateGoombaDead();
            countDown = 0;
        }

        public override void UpdatePosition(Vector2 location, GameTime gameTime)
        {

            if (EntityManager.IsGoingToFall((Goomba)this))
            {
                //Velocity = new Vector2(0, Velocity.Y);
                Acceleration = new Vector2(0, 400);
            }

            else if (!EntityManager.IsGoingToFall((Goomba)this))
            {
                Acceleration = new Vector2(0, 0);
                if (direction == (int)eFacing.RIGHT)Velocity = new Vector2(85, 0);
                if (direction == (int)eFacing.LEFT) Velocity = new Vector2(-85, 0);
            }

            Velocity += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            UpdateCollisionBox(Position);
        }


    }
}

