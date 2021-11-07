using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using Microsoft.Xna.Framework;
using GameSpace.Machines;

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
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2 / 4, Sprite.Texture.Height * 2);
            hasCollided = false;
        }

        public override void Trigger()
        {
            base.Trigger();
            MusicHandler.GetInstance().PlaySoundEffect(5);
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

            //play sound effect for powerUpAppear
            MusicHandler.GetInstance().PlaySoundEffect(4);

        }
        
    }
}
