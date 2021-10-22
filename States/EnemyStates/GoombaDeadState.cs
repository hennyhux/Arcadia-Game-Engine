using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States.EnemyStates
{
    public class GoombaDeadState : IEnemyStates
    {

        public GoombaDeadState(IGameObjects enemy)
        {
            enemy.Sprite = SpriteEnemyFactory.GetInstance().CreateDeadGoombaSprite();
        }

        public ISprite StateSprite { get; set; }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            
        }

        public void Update(GameTime gametime)
        {
            
        }
    }
}
