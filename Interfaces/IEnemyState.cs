using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Interfaces
{
    public interface IEnemyState
    {
        public ISprite StateSprite { get; set; }
        public bool CollidedWithMario { get; set; }
        public void Draw(SpriteBatch spritebatch, Vector2 location);
        public void Update(GameTime gametime);
        public void Trigger();
        public void DrawBoundaries(SpriteBatch spritebatch, Rectangle destination);

    }

}