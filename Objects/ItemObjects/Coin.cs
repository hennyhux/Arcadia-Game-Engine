using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.ItemObjects
{
    public class Coin : AbstractItem
    {
        private bool hasCollided;
        public Coin(Vector2 initalPosition)
        {
            ObjectID = (int)ItemID.COIN;
            Sprite = SpriteItemFactory.GetInstance().CreateCoin();
            Position = initalPosition;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2 / 4, Sprite.Texture.Height * 2);
            hasCollided = false;
            drawBox = false;
        }

        public override void HandleCollision(IGameObjects entity)
        {
            if (!hasCollided)
            {
                Trigger();
            }

            hasCollided = true;
        }

    }
}
