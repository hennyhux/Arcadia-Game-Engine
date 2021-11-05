using GameSpace.Abstracts;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Animations
{
    public class PlantComingOutOfPipe : IObjectAnimation
    {

        private AbstractEnemy item;
        private Vector2 initLocation;
        private Vector2 goalLocation;
        public PlantComingOutOfPipe(AbstractEnemy plant)
        {
            item = plant;
            initLocation = plant.Position;
            goalLocation = new Vector2(initLocation.X, initLocation.Y - 20);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (goalLocation.Y < item.Position.Y)
            {
                item.Velocity = new Vector2(0, -5);
            }

            else
            {
                item.Velocity = new Vector2(0, 0);
            }

            item.Draw(spriteBatch);
        }

        public void Update(GameTime gametime)
        {
            item.Update(gametime);
        }
    }
}
