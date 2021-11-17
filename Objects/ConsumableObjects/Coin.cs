using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Machines;
using Microsoft.Xna.Framework;
using System.Diagnostics;

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
                UpdateCollisionBox(Position, gametime);
                UpdateCollisionBox();
            }
        }

        public override void Trigger()
        {
            base.Trigger();
            MusicHandler.GetInstance().PlaySoundEffect(7);
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

    public class HUDCoin : Coin
    {
        public HUDCoin(Vector2 pos) : base(Vector2.Zero)
        {
            Debug.Print("constructor");
            ObjectID = (int)ItemID.HUDCOIN;
            Sprite = SpriteItemFactory.GetInstance().CreateCoin();
            Position = new Vector2(0, 0);
            CollisionBox = new Rectangle(0, 0, 0, 0);
            hasCollided = false;
            drawBox = false;
        }

        /* public override void Update(GameTime gametime)
         {
             Sprite.Update(gametime);
         }*/

        public override void Trigger()
        {

        }
    }
}
