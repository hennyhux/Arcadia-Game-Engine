using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Machines;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.ItemObjects
{
    public class Vine : AbstractItem
    {
        public Vine(Vector2 initialPosition)
        {
            ObjectID = (int)ItemID.VINE;
            Sprite = SpriteItemFactory.GetInstance().CreateVine();
            Position = initialPosition;
            drawBox = false;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2 / 4, Sprite.Texture.Height * 2);
            hasCollided = false;
        }

        public override void Trigger()
        {
            base.Trigger();
        }


        public override void UpdateCollisionBox()
        {
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y,
              Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);

            ExpandedCollisionBox = new Rectangle((int)Position.X, (int)Position.Y,
                Sprite.Texture.Width * 2, (Sprite.Texture.Height * 2) + 4);
        }
        public override void AdjustLocationComingOutOfBlock()
        {
            Position = new Vector2(Position.X - 4, Position.Y - Sprite.Texture.Height * 2);
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2 / 4, Sprite.Texture.Height * 2);

        }

    }
}
