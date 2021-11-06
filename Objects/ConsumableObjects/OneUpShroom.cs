using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Machines;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.ItemObjects
{
    public class OneUpShroom : AbstractItem
    {
        public OneUpShroom(Vector2 initialPosition)
        {
            ObjectID = (int)ItemID.ONEUPSHROOM;
            Sprite = SpriteItemFactory.GetInstance().CreateOneUpShroom();
            Position = initialPosition;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            drawBox = false;
            hasCollided = false;
            ExpandedCollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 3);
            Velocity = new Vector2(100, 0);
        }

        public override void Trigger()
        {
            base.Trigger();
            MusicHandler.GetInstance().PlaySoundEffect(6);
            MarioHandler.GetInstance().IncrementMarioLives();
        }
    }
}
