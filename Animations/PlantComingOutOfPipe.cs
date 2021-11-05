using GameSpace.Abstracts;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Animations
{
    public class PlantComingOutOfPipe : IObjectAnimation
    {

        private AbstractEnemy item;
        private Vector2 initLocation;
        public PlantComingOutOfPipe(AbstractEnemy plant)
        {
            this.item = plant;
            initLocation = plant.Position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            item.Velocity = new Vector2(0, -2);
            item.Draw(spriteBatch);
        }

        public void Update(GameTime gametime)
        {
            item.Update(gametime);
        }
    }
}
