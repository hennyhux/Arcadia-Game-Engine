using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States.EnemyStates
{
    public class EnemyDeadState : IEnemyStates
    {

        public EnemyDeadState(IGameObjects enemy)
        {
            enemy.Sprite = SpriteEnemyFactory.GetInstance().CreateDeadGoombaSprite();
        }
        public void Draw(SpriteBatch spritebatch, Texture2D texture)
        {
            
        }

        public void Update(GameTime gametime)
        {
            
        }
    }
}
