using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace GameSpace.GameObjects.ExtraItemsObjects
{
    public class HiddenLevelVerticalPipe : Item
    {
        public HiddenLevelVerticalPipe(Vector2 initalPosition)
        {
            ObjectID = (int)ItemID.HIDDENLEVELVERTICALPIPE;
            Sprite = SpriteExtraItemsFactory.GetInstance().ReturnHiddenLevelVerticalPipe();
            Position = initalPosition;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            drawBox = false;
            Debug.WriteLine("EXTRA ITEM AT " + "(" + Position.X + ", " + Position.Y + ")");
        }

        public override void HandleCollision(IGameObjects entity)
        {
            hasCollided = true;
        }

    }
}
