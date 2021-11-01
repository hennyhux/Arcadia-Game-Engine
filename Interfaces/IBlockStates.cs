using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Interfaces
{
    public interface IBlockStates
    {
        public void Draw(SpriteBatch spriteBatch, Vector2 location);
        public void Update(GameTime gameTime);
        public void DrawBounds(SpriteBatch spriteBatch, Rectangle CollisionBox);
    }
}
