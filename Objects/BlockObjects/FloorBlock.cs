using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.BlockObjects
{
    public class FloorBlock : Blocks
    {
        public FloorBlock(Vector2 initalPosition)
        {
            ObjectID = (int)BlockID.FLOORBLOCK;
            Position = initalPosition;
            Sprite = SpriteBlockFactory.GetInstance().ReturnFloorBlock();
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            state = new StateFloorBlock();
        }

        public override bool RevealItem()
        {
            throw new System.NotImplementedException();
        }
    }
}
