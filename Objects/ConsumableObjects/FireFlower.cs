using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Machines;
using Microsoft.Xna.Framework;


namespace GameSpace.GameObjects.ItemObjects
{
    public class FireFlower : Item
    {
        public FireFlower(Vector2 initialPosition)
        {
            ObjectID = (int)ItemID.FIREFLOWER;
            Sprite = SpriteItemFactory.GetInstance().CreateFireFlower();
            Position = new Vector2(initialPosition.X - 10, initialPosition.Y);
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2 / 4, Sprite.Texture.Height * 2);
            drawBox = false;
        }

        public override void Trigger()
        {
            base.Trigger();
            MusicHandler.GetInstance().PlaySoundEffect(5);
        }

        public override void Update(GameTime gametime)
        {
            Sprite.Update(gametime);
            if (!hasCollided)
            {
                UpdatePosition(Position, gametime);
                UpdateCollisionBox();
            }
        }
    }
}
