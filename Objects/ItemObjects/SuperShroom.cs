using System;
using System.Collections.Generic;
using System.Text;
using GameSpace.Interfaces;
using GameSpace.States.ItemStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.GameObjects.ItemObjects
{
    public class SuperShroom : IGameObjects
    {
        private IItemStates state;

        public SuperShroom(GameRoot game)
        {
            state = new SuperShroomState(game);
        }

        public Vector2 Location => throw new NotImplementedException();

        public ISprite Sprite { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector2 Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector2 Velocity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector2 Acceleration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Rectangle CollisionBox { get; set; }

        public int ObjectID => throw new NotImplementedException();

        public void Draw(SpriteBatch spritebatch)
        {
            state.Draw(spritebatch, new Vector2(50, 250));
        }

        public void HandleCollision(IGameObjects entity)
        {
            throw new NotImplementedException();
        }

        public void HandleCollsion()
        {
            throw new NotImplementedException();
        }

        public void SetPosition(Rectangle destination)
        {
            throw new NotImplementedException();
        }

        public void SetPosition(Vector2 location)
        {
            throw new NotImplementedException();
        }

        public void ToggleCollisionBoxes()
        {
            throw new NotImplementedException();
        }

        public void Trigger()
        {
            state.Trigger();
        }

        public void Update(GameTime gametime)
        {
            state.Update(gametime);
        }
    }
}
