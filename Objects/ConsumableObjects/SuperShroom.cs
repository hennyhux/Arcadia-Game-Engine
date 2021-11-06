using GameSpace.Abstracts;
using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.Machines;
using GameSpace.States.ItemStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameSpace.GameObjects.ItemObjects
{
    public class SuperShroom : AbstractItem
    {
        private IItemStates state;

        public SuperShroom(Vector2 initialPosition)
        {
            ObjectID = (int)ItemID.SUPERSHROOM;
            Sprite = SpriteItemFactory.GetInstance().CreateSuperShroom();
            Position = initialPosition;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            ExpandedCollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 3);

            drawBox = false;
            hasCollided = false;
            state = new StateSuperShroomHidden(this);
            //play sound effect for powerUpAppear
            //MusicHandler.GetInstance().PlaySoundEffect(4);
        }

    }
}
