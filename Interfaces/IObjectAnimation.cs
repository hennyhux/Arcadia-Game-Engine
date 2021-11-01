using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Interfaces
{
    public interface IObjectAnimation
    {

        public void Update(GameTime gametime);
        public void Draw(SpriteBatch spriteBatch);
    }
}
