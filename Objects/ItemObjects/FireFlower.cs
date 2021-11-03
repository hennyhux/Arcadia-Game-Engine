using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using GameSpace.Machines;


namespace GameSpace.GameObjects.ItemObjects
{
    public class FireFlower : AbstractItem
    {
        public FireFlower(Vector2 initialPosition)
        {
            ObjectID = (int)ItemID.FIREFLOWER;
            Sprite = SpriteItemFactory.GetInstance().CreateFireFlower();
            Position = initialPosition;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2 / 4, Sprite.Texture.Height * 2);
            drawBox = false;
            //play sound effect for powerUpAppear
            MusicMachine.GetInstance().PlaySoundEffect(4);  

        }

        public override void Trigger()
        {
            Sprite.SetVisible();
            CollisionBox = new Rectangle(1, 1, 0, 0);
            //play sound effect for powerUpCollect
            MusicMachine.GetInstance().PlaySoundEffect(5);

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
