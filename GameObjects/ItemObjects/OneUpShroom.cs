using System;
using System.Collections.Generic;
using System.Text;
using GameSpace.Interfaces;
using GameSpace.States.ItemStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.GameObjects.ItemObjects
{
    public class OneUpShroom : IItemObjects
    {
        private IItemStates state;

        public OneUpShroom(Game1 game)
        {
            state = new OneUpShroomState(game);
        }
        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            state.Draw(spritebatch, location);
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
