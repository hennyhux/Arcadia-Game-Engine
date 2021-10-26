using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using GameSpace.EntitiesManager;
using System.Diagnostics;
using GameSpace.Abstracts;

namespace GameSpace.GameObjects.BlockObjects
{
    public class QuestionBlockOneUpShroom : AbstractItemBlock
    {
        private IGameObjects shroom;
        public QuestionBlockOneUpShroom(Vector2 initalPosition)
        {
            this.ObjectID = (int)BlockID.QUESTIONBLOCK;
            this.state = new StateQuestionBlockIdle();
            this.Sprite = SpriteBlockFactory.GetInstance().ReturnQuestionBlock();
            this.Position = initalPosition;
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, (Sprite.Texture.Width * 2) / 3, Sprite.Texture.Height * 2);
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
