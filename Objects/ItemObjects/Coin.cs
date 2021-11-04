using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.Machines;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.ItemObjects
{
    public class Coin : AbstractItem
    {
        private readonly bool hasCollided;
        public Coin(Vector2 initalPosition)
        {
            ObjectID = (int)ItemID.COIN;
            Sprite = SpriteItemFactory.GetInstance().CreateCoin();
            Position = initalPosition;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2 / 4, Sprite.Texture.Height * 2);
            hasCollided = false;
            drawBox = false;
        }

        public override void Trigger()
        {
            Sprite.SetVisible();
            DeleteCollisionBox();
            //play sound effect for coinCollect
            MusicHandler.GetInstance().PlaySoundEffect(7);
        }

        public override void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    Trigger();
                    break;
            }
        }
    }
}
