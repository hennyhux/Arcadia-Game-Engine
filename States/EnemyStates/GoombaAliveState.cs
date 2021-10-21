using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States.EnemyStates
{
    public class GoombaAliveState : IEnemyStates
    {
        private IGameObjects enemy;
        

        public GoombaAliveState(IGameObjects enemy)
        {
            this.enemy = enemy;
        }

        public ISprite StateSprite { get; set; }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            enemy.Sprite.Texture = SpriteEnemyFactory.GetInstance().CreateGoombaSprite().Texture;
        }

        public void Update(GameTime gametime)
        {
            
        }
    }
}
