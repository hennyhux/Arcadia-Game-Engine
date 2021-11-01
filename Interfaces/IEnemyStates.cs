using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Interfaces
{
    public interface IEnemyStates
    {

        public void Draw(SpriteBatch spritebatch, Vector2 location);
        public void Update(GameTime gametime);
        public void DrawBoundaries(SpriteBatch spritebatch, Rectangle destination);
    }
}