using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Machines;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.ItemObjects
{
    public class OneUpShroom : Item
    {
        public OneUpShroom(Vector2 initialPosition)
        {
            ObjectID = (int)ItemID.ONEUPSHROOM;
            Sprite = SpriteItemFactory.GetInstance().CreateOneUpShroom();
            Position = initialPosition;
            drawBox = false;
            hasCollided = false;
        }

        public override void Trigger()
        {
            base.Trigger();
            MusicHandler.GetInstance().PlaySoundEffect(6);
            //MarioHandler.GetInstance().IncrementMarioLives();//already increment lives in mario collision
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
