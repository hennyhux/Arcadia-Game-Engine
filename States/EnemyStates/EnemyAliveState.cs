using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States.EnemyStates
{
    public class EnemyAliveState : IEnemyStates
    {
        private IGameObjects enemy;

        public EnemyAliveState(IGameObjects enemy)
        {
            this.enemy = enemy;
        }
        public void Draw(SpriteBatch spritebatch, Texture2D texture)
        {
            enemy.Sprite.Texture = SpriteEnemyFactory.GetInstance().CreateGoombaSprite().Texture;
        }

        public void Update(GameTime gametime)
        {
            
        }
    }
}
