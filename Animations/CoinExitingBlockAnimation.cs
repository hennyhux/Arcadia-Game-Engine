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

        private GameTime gameTime;
        private int offset;
        private int celling;
        private IGameObjects coin;
        private bool revealed;

        public CoinExitingBlockAnimation(Vector2 location, GameTime gameTime)
        {

            this.coin = ObjectFactory.GetInstance().CreateCoinObject(location);
            this.initialLocation = location;
            offset = 3;
            celling = 90;
            this.gameTime = gameTime;
            revealed = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            coin.Draw(spriteBatch);
        }

        public void Update(GameTime gametime)
        {
            coin.UpdatePosition(new Vector2 (initialLocation.X + 6 , (initialLocation.Y - coin.Sprite.Texture.Height )- offset), gametime);
            if (offset < celling) offset += 4;
            if (!revealed && offset >= celling) 
            { 
                coin.Sprite.SetVisible();
                revealed = true;
            }
            coin.Update(gameTime);
        }
    }
}
