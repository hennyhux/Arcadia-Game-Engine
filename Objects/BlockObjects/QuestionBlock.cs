using GameSpace.Abstracts;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.BlockObjects
{
    public class QuestionBlock : AbstractBlock
    {
        public QuestionBlock(Vector2 initalPosition, AbstractItem item)
        {
            ObjectID = (int)BlockID.QUESTIONBLOCK;
            state = new StateQuestionBlockIdle();
            Sprite = SpriteBlockFactory.GetInstance().ReturnQuestionBlock();
            Position = initalPosition;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, (Sprite.Texture.Width * 2) / 3, Sprite.Texture.Height * 2);
            this.item = item;
        }

        public override bool RevealItem()
        {
            if (!revealedItem && item != null)
            {
                item.AdjustLocationComingOutOfBlock();
                TheaterHandler.GetInstance().AddItemToStage(item);
                revealedItem = true;
            }

            return revealedItem;
        }

        public override void Trigger()
        {
            state = new StateQuestionBlockBump(this);
            RevealItem();
        }
    }
}
