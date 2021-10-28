using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GameSpace.GameObjects.BlockObjects
{
    public class FloorBlock : AbstractBlock
    {

        public FloorBlock(Vector2 initalPosition)
        {
            ObjectID = (int)BlockID.FLOORBLOCK;
            Position = initalPosition;
            Sprite = SpriteBlockFactory.GetInstance().ReturnFloorBlock();
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            state = new StateFloorBlock(); 
        }

    }
}
