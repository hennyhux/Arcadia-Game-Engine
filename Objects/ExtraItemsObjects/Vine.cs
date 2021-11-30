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
            hasCollided = false;
        }

        public override void Trigger()
        {
            //teleports mario 
        }

        public override void Update(GameTime gametime)
        {
            Sprite.Update(gametime);
            UpdateCollisionBox();
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
