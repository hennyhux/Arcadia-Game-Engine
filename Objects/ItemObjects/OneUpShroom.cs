using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.Machines;
using GameSpace.States.ItemStates;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.ItemObjects
{
    public class OneUpShroom : AbstractItem
    {
        private readonly IItemStates state;

        private bool hasCollided;
        public Rectangle ExpandedCollisionBox { get; set; }
        public OneUpShroom(Vector2 initialPosition)
        {
            ObjectID = (int)ItemID.ONEUPSHROOM;
            Sprite = SpriteItemFactory.GetInstance().CreateOneUpShroom();
            Position = initialPosition;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            drawBox = false;
            hasCollided = false;
            ExpandedCollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 3);
            state = new StateOneUpShroomHidden(this);
        }

        public override void Trigger()
        {
            if (!hasCollided)
            {
                Sprite.SetVisible();
                CollisionBox = new Rectangle();
                //play sound effect for 1-Up being collected
                MusicHandler.GetInstance().PlaySoundEffect(6);
            }
            hasCollided = true;

            //Increase Mario's lives
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
