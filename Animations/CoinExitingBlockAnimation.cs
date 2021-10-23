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

        private float yVelocity;

        private IGameObjects coin;
        

        public CoinExitingBlockAnimation(Vector2 location)
        {

            this.coin = ObjectFactory.GetInstance().CreateCoinObject(location);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            coin.Draw(spriteBatch);
        }

        public void PlayAnimation()
        {
            
        }

        public void Update()
        {

        }
    }
}
