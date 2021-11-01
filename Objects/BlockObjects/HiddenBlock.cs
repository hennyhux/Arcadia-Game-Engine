using GameSpace.Abstracts;
using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.BlockObjects
{
    public class HiddenBlock : AbstractBlock
    {
        public HiddenBlock(Vector2 initalPosition)
        {
            ObjectID = (int)BlockID.HIDDENBLOCK;
            state = new StateHiddenBlockIdle();
            Sprite = SpriteBlockFactory.GetInstance().ReturnHiddenBlock();
            Position = initalPosition;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
        }

        public override void Trigger()
        {
            state = new StateHiddenBlockBump(this);
        }

        public override void HandleCollision(IGameObjects entity)
        {
            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.DOWN && hasCollided == false && entity.Velocity.Y < 0)
            {
                Trigger();
                hasCollided = true;
            }
        }

        public override bool RevealItem()
        {
            throw new System.NotImplementedException();
        }
    }
}
