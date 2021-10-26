using System;
using System.Collections.Generic;
using System.Text;
using GameSpace.Objects.BackgroundObjects;
using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.Sprites;
using GameSpace.States;
using GameSpace.States.EnemyStates;
using GameSpace.States.StateMachines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Objects.BackgroundObjects
{
    public abstract class  BackgroundObject : IGameObjects
    {
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }

        public int ObjectID { get; set; }
        private Boolean hasCollidedOnTop;
        private Boolean drawBox;
        private int countDown;
        private int direction;

        public Vector2 Parallax { get; set; }

        protected BackgroundObject()
        {

        }

    public abstract void Draw(SpriteBatch spritebatch, Vector2 position);

    public  void Draw(SpriteBatch spritebatch) {  }
        public abstract void Update(GameTime gametime);
 

    public abstract void Trigger();


    public abstract void UpdatePosition(Vector2 location, GameTime gameTime);


    public abstract void HandleCollision(IGameObjects entity);

    public abstract void ToggleCollisionBoxes();

    public abstract bool IsCurrentlyColliding();

    }
}
