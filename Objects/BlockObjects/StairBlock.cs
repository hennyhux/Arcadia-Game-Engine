using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.BlockObjects
{
    public class StairBlock : AbstractBlock
    {
        public StairBlock(Vector2 initalPosition)
        {
            ObjectID = (int)BlockID.STAIRBLOCK;
            Position = initalPosition;
            Sprite = SpriteBlockFactory.GetInstance().ReturnStairBlock();
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            state = new StateStairBlock();
        }

        public override bool RevealItem()
        {
            throw new System.NotImplementedException();
        }
    }

}
