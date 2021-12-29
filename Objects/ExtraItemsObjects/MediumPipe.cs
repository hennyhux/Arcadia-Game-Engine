using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.ExtraItemsObjects
{
    public class MediumPipe : Blocks
    {

        public MediumPipe(Vector2 initalPosition)
        {
            ObjectID = (int)ItemID.MEDIUMPIPE;
            Sprite = SpriteExtraItemsFactory.GetInstance().ReturnMediumPipe();
            Position = initalPosition;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            drawBox = false;
            state = new StateMediumPipeIdle();
        }

        public override bool RevealItem()
        {
            throw new System.NotImplementedException();
        }
    }

    public class StateMediumPipeIdle : AbstractBlockStates
    {
        public StateMediumPipeIdle()
        {
            sprite = SpriteExtraItemsFactory.GetInstance().ReturnMediumPipe();
        }
    }
}
