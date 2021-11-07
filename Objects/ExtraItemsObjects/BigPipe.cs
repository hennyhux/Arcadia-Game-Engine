using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.ExtraItemsObjects
{
    public class BigPipe : AbstractBlock
    {
        public BigPipe(Vector2 initalPosition)
        {
            ObjectID = (int)ItemID.BIGPIPE;
            Sprite = SpriteExtraItemsFactory.GetInstance().ReturnBigPipe();
            Position = initalPosition;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            drawBox = false;
            state = new StateBigPipeIdle();
        }

        public override bool RevealItem()
        {
            throw new System.NotImplementedException();
        }
    }

    public class StateBigPipeIdle : AbstractBlockStates
    {
        public StateBigPipeIdle()
        {
            sprite = SpriteExtraItemsFactory.GetInstance().ReturnBigPipe();
        }
    }
}
