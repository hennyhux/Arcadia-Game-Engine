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

        public GreenKoopa(Vector2 initalPosition)
        {
            ObjectID = (int)EnemyID.GREENKOOPA;
            this.Position = initalPosition;
            this.state = new KoopaAliveState();
            this.CollisionBox = new Rectangle((int)Position.X + state.StateSprite.Texture.Width / 4 + 2, (int)Position.Y,
                state.StateSprite.Texture.Width / 2, state.StateSprite.Texture.Height * 2);
            drawBox = false;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            state.Draw(spritebatch, Position);
            if (drawBox) state.StateSprite.DrawBoundary(spritebatch, CollisionBox);;
        }

        public void Update(GameTime gametime)
        {
            state.Update(gametime);
        }

        public void Trigger()
        {
            state = new KoopaShelledState();
        }

        public void SetPosition(Vector2 location)
        {
            throw new NotImplementedException();
        }

        public void HandleCollision(IGameObjects entity)
        {
            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.UP) this.Trigger();
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

