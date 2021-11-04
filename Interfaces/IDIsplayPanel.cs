using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Interfaces
{
    public interface IDIsplayPanel
    {
        bool IsEnabled { get; set; }

        void Update();

        void Draw(SpriteBatch spritebatch);
    }
}
