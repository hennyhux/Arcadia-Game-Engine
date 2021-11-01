using GameSpace.Factories;
using GameSpace.GameObjects.ItemObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.States.ItemStates
{
    public class StateStarRight : IItemStates
    {
        public ISprite StateSprite { get; set; }
        public Star Star;
        private int countdown;
        private bool bounce;

        public StateStarRight(Star star)
        {
            StateSprite = SpriteItemFactory.GetInstance().CreateStar();
            Star = star;
            Star.Velocity = new Vector2(45, 0);
            Star.Acceleration = new Vector2(0, 400);
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            //StateSprite.Draw(spritebatch, location);
            if (bounce)
            {
                ++countdown;
            }
        }

        public void Trigger()
        {
            bounce = true;
        }

        public void Update(GameTime gametime)
        {
            if (countdown == 15)
            {
                Star.Acceleration = new Vector2(0, 400);
                bounce = false;
                countdown = 0;
            }

        }
    }
}
