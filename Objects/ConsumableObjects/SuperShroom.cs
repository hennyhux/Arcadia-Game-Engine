using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.ItemObjects
{
    public class SuperShroom : AbstractItem
    {
        public SuperShroom(Vector2 initialPosition)
        {
            ObjectID = (int)ItemID.SUPERSHROOM;
            Sprite = SpriteItemFactory.GetInstance().CreateSuperShroom();
            Position = initialPosition;
            drawBox = false;
            hasCollided = false;
        }

        public override void UpdateCollisionBox()
        {
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y,
              Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);

            ExpandedCollisionBox = new Rectangle((int)Position.X, (int)Position.Y,
                Sprite.Texture.Width * 2, (Sprite.Texture.Height * 2) + 4);
        }
    }
}
