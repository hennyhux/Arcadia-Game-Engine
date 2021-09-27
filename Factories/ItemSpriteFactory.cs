using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;
using GameSpace.Sprites;
using Microsoft.Xna.Framework;

namespace GameSpace.Factories
{
    public class ItemSpriteFactory
    {

        private static ItemSpriteFactory instance = new ItemSpriteFactory();
        public static ItemSpriteFactory Instance => instance;
        public ItemSpriteFactory()
        {
        }

        public void LoadContent(ContentManager content)
        {
            Star = content.Load<Texture2D>("Items/star");
            Coin = content.Load<Texture2D>("Items/coin");
            OneUpShroom = content.Load<Texture2D>("Items/1upshroom");
            SuperShroom = content.Load<Texture2D>("Items/supershroom");
            FireFlower = content.Load<Texture2D>("Items/flower");
        }
        public ISprite ReturnStar()
        {
            return new StarSprite(Star, 1, 4, 4, 0, 0);
        }

        public ISprite ReturnSuperShrooom()
        {
            return new SuperShroomSprite(SuperShrooom, 1, 1, 1, 0, 0);
        }

        public ISprite ReturnOneUpShroom()
        {
            return new OneUpShrromSprite(OneUpShroom, 1, 1, 1, 0, 0);
        }

        public ISprite ReturnFireFlower()
        {
            return new FireFLowerSprite(FireFlower, 1, 1, 1, 0, 0);
        }

        public ISprite ReturnCoin()
        {
            return new CoinSprite(HiddenBlock, 1, 4, 4, 0, 0);
        }

    }
}
