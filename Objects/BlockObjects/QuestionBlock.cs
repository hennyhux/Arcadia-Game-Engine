using GameSpace.Abstracts;
using GameSpace.Animations;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.BlockObjects
{
    public class QuestionBlock : AbstractItemBlock
    {
        public QuestionBlock(Vector2 initalPosition)
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
            AnimationMachine.GetInstance().AddAnimation(new CoinExitingBlockAnimation(Position, internalGametime));
            revealedItem = true;
        }
    }
}
