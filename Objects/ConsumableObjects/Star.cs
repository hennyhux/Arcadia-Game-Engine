using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.ItemObjects
{
    public class Star : AbstractItem
    {
        public Star(Vector2 initialPosition)
        {
            ObjectID = (int)ItemID.STAR;
            Sprite = SpriteItemFactory.GetInstance().CreateStar();
            Position = initialPosition;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2 / 4, Sprite.Texture.Height * 2);
            drawBox = false;
            hasCollided = false;
        }

        public override void Trigger()
        {
            base.Trigger();
        }
    }

}
