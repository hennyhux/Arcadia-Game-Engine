using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.ItemObjects
{
    public class Coin : AbstractItem
    {
        public Coin(Vector2 initalPosition)
        {
            ObjectID = (int)ItemID.COIN;
            Sprite = SpriteItemFactory.GetInstance().CreateCoin();
            Position = initalPosition;
            hasCollided = false;
            drawBox = false;
        }

        public override void Update(GameTime gametime)
        {
            Sprite.Update(gametime);
            if (!hasCollided)
            {
                //UpdateSpeed();
                UpdatePosition(Position, gametime);
                UpdateCollisionBox();
            }
        }

        public override void Trigger()
        {
            base.Trigger();
            //play sound effect for coinCollect
            //MusicHandler.GetInstance().PlaySoundEffect(7);
        }

    }

    public class HiddenLevelCoin : Coin
    {
        public HiddenLevelCoin(Vector2 initalPosition) : base(initalPosition)
        {
            ObjectID = (int)ItemID.HIDDENLEVELCOIN;
            Sprite = SpriteItemFactory.GetInstance().CreateHiddenLevelCoin();
            Position = initalPosition;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2 / 4, Sprite.Texture.Height * 2);
            hasCollided = false;
            drawBox = false;
        }

        public override void Trigger()
        {
            base.Trigger();
        }
    }
}
