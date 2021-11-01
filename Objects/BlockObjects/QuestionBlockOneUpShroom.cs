using GameSpace.Abstracts;
using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.BlockObjects
{
    public class QuestionBlockOneUpShroom : AbstractItemBlock
    {
        private IGameObjects shroom;
        public QuestionBlockOneUpShroom(Vector2 initalPosition)
        {
            ObjectID = (int)BlockID.QUESTIONBLOCK;
            state = new StateQuestionBlockIdle();
            Sprite = SpriteBlockFactory.GetInstance().ReturnQuestionBlock();
            Position = initalPosition;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, (Sprite.Texture.Width * 2) / 3, Sprite.Texture.Height * 2);
            drawBox = false;
        }

        public override void Trigger()
        {
            state = new StateQuestionBlockBump(this);
            shroom = ObjectFactory.GetInstance().CreateOneUpShroomObject(new Vector2(Position.X - 4, Position.Y - Sprite.Texture.Height * 2 - 8));
            EntityManager.AddEntity(shroom);
            revealedItem = true;
        }
    }
}
