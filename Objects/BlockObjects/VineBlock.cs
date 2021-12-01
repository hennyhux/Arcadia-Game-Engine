using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.BlockObjects
{
    public class VineBlock : AbstractBlock
    {
        public VineBlock(Vector2 initalPosition)
        {
            ObjectID = (int)BlockID.VINEBLOCK;
            Position = initalPosition;
            Sprite = SpriteBlockFactory.GetInstance().ReturnVineBlock();
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            state = new StateVineBlock();
        }

        public override bool RevealItem()
        {
            throw new System.NotImplementedException();
        }
    }
}
