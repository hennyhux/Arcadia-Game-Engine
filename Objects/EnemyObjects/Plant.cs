using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Machines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Objects.EnemyObjects
{
    public class Plant : AbstractEnemy
    {
        public Plant(Vector2 initalPosition)
        {
            ObjectID = (int)EnemyID.GREENKOOPA;
            drawBox = false;
            Position = initalPosition;
            state = new StatePlantIdle();
            UpdateCollisionBox(Position);
        }

        public override void Update(GameTime gametime)
        {
            if (!(state is StatePlantDead))
            {
                state.Update(gametime);
                UpdatePosition(Position, gametime);
            }

        }

        public override void Draw(SpriteBatch spritebatch)
        {
             if (!(state is StatePlantDead))base.Draw(spritebatch);
        }

        public override void Trigger()
        {
            base.Trigger();
            MarioHandler.GetInstance().IncrementMarioPoints(200);
            state = new StatePlantDead();
            this.DeleteCollisionBox();
        }

    }

    public class StatePlantIdle : AbstractEnemyState
    {
        public StatePlantIdle()
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreatePlantSprite();
        }
    }

    public class StatePlantDead : AbstractEnemyState
    {
        public StatePlantDead()
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreatePlantSprite();
        }
    }

}
