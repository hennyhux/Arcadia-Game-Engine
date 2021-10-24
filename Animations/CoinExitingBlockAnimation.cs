using GameSpace.EntitiesManager;
using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Animations
{
    public class CoinExitingBlockAnimation : IObjectAnimation
    {
        private Vector2 initialLocation;
        private Vector2 endLocation;
        private ISprite sprite;
        private GameTime gameTime;
        private float yVelocity;
        private int offset;
        private int celling;
        private IGameObjects coin;

        public CoinExitingBlockAnimation(Vector2 location, GameTime gameTime)
        {

            this.coin = ObjectFactory.GetInstance().CreateCoinObject(location);
            this.initialLocation = location;
            offset = 3;
            celling = 90;
            this.gameTime = gameTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            coin.Draw(spriteBatch);
        }

        public void Update(GameTime gametime)
        {
            coin.SetPosition(new Vector2 (initialLocation.X + 6 , (initialLocation.Y - coin.Sprite.Texture.Height )- offset));
            if (offset <= celling) offset += 3;
            else coin.Sprite.SetVisible();
            coin.Update(gameTime);
        }
    }
}
