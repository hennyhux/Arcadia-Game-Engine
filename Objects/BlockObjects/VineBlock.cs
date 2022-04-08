using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.BlockObjects
{
    public class VineBlock : Blocks
    {
        public VineBlock(Vector2 initalPosition)
        {
            ObjectID = (int)BlockID.VINEBLOCK;
            Position = initalPosition;
         
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
        }

        public override bool RevealItem()
        {
            throw new System.NotImplementedException();
        }
    }
}
