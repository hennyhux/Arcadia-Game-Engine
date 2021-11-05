using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.BlockObjects
{
    public class HiddenLevelFloorBlock : AbstractBlock
    {
        public HiddenLevelFloorBlock(Vector2 initalPosition)
        {
            ObjectID = (int)BlockID.HIDDENLEVELFLOORBLOCK;
            Position = initalPosition;
            Sprite = SpriteBlockFactory.GetInstance().ReturnHiddenLevelFloorBlock();
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            state = new StateHiddenLevelFloorBlock();
        }

        public override bool RevealItem()
        {
            throw new System.NotImplementedException();
        }
    }
}
