using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.ExtraItemsObjects
{
    public class SmallPipe : AbstractBlock
    {

        public SmallPipe(Vector2 initalPosition)
        {
            ObjectID = (int)ItemID.SMALLPIPE;
            Sprite = SpriteExtraItemsFactory.GetInstance().ReturnSmallPipe();
            Position = initalPosition;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            drawBox = false;
            state = new StateSmallPipeIdle();
        }

        public override bool RevealItem()
        {
            return false;
        }
    }

    public class StateSmallPipeIdle : AbstractBlockStates
    {
        public StateSmallPipeIdle()
        {
            sprite = SpriteExtraItemsFactory.GetInstance().ReturnSmallPipe();
        }
    }
}
