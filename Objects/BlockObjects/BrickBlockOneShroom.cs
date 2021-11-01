using GameSpace.Abstracts;
using GameSpace.EntitiesManager;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;

namespace GameSpace.Objects.BlockObjects
{
    public class BrickBlockOneShroom : AbstractItemBlock
    {

        private IGameObjects shroom;
        public BrickBlockOneShroom(Vector2 initialPosition)
        {
            state = new StateBrickBlockIdle();
            Position = initialPosition;
            Sprite = SpriteBlockFactory.GetInstance().ReturnBrickBlock();
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            drawBox = false;
            revealedItem = false;
        }

        public override void Trigger()
        {
            state = new StateBrickBlockBump(this);
            shroom = ObjectFactory.GetInstance().CreateOneUpShroomObject(new Vector2(Position.X - 4, Position.Y - 5 - Sprite.Texture.Height * 2 - 4));
            EntityManager.AddEntity(shroom);
            revealedItem = true;
        }
    }
}
