using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States.EnemyStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.GameObjects.EnemyObjects
{
    public class GreenKoopa : IGameObjects
    {
        private IEnemyStates state;
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }
        public int ObjectID { get; set; }

        private Boolean hasCollided;
        private Boolean drawBox;

        private int countDown;
        private Boolean inFrame; //is the current enemy inside of the viewport? 


        public GreenKoopa(Vector2 initalPosition)
        {
            ObjectID = (int)EnemyID.GREENKOOPA;
            this.Position = initalPosition;
            this.state = new KoopaAliveState();
            this.CollisionBox = new Rectangle((int)Position.X + state.StateSprite.Texture.Width / 4 + 2, (int)Position.Y,
                state.StateSprite.Texture.Width / 2, state.StateSprite.Texture.Height * 2);
            drawBox = false;
            hasCollided = false;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            Sprite.Draw(spritebatch, Position);
            if (drawBox) Sprite.DrawBoundary(spritebatch, CollisionBox);
            if (hasCollided) countDown++;
        }

        public void Update(GameTime gametime)
        {
            Sprite.Update(gametime);
            if (countDown == 225) this.Sprite = SpriteEnemyFactory.GetInstance().CreateShelledWithLegsGreenKoopaSprite();

            if (countDown == 550) 
            {
                this.Sprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaSprite();
                countDown = 0;
                hasCollided = false; 
            }
            
        }

        public void Trigger()
        {
            this.Sprite = SpriteEnemyFactory.GetInstance().CreateShelledGreenKoopaSprite();
            countDown = 0;
        }

        public void SetPosition(Vector2 location)
        {
            throw new NotImplementedException();
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

        private void CollisionWithMario(IGameObjects mario)
        {
            if (EntityManager.DetectCollisionDirection(this, mario) == (int)CollisionDirection.UP)
            {
                this.Trigger();
                this.CollisionBox = new Rectangle(1, 1, 0, 0);
                if (!hasCollided) hasCollided = true;
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

