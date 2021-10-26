using System;
using System.Collections.Generic;
using System.Text;
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

namespace GameSpace.Objects.BackgroundObjects
{
    class BackgroundObjects : BackgroundObject
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
        public Rectangle ExpandedCollisionBox { get; set; }

        public Vector2 Parallax { get; set; }

        public BackgroundObjects(Vector2 position)
        {
            Sprite = BackgroundFactory.GetInstance().CreateRegularBackground();
            Position = position;
        }

        public override void Draw(SpriteBatch spritebatch, Vector2 position)
        {
            Sprite.Draw(spritebatch, position);
        }

        public override void Update(GameTime gametime)
        {
        }

        public override void Trigger()
        {

        }

        public override void UpdatePosition(Vector2 location, GameTime gameTime)
        {

        }

        public override void HandleCollision(IGameObjects entity)
        {

        }

        private void CollisionWithBlock(IGameObjects block)
        {

        }

        private void CollisionWithMario(IGameObjects mario)
        {

        }

        public override void ToggleCollisionBoxes()
        {

        }

        public override bool IsCurrentlyColliding()
        {
            throw new NotImplementedException();
        }

        private void UpdateCollisionBox(Vector2 location)
        {

        }
    }

}
