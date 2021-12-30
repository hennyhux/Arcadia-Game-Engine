using GameSpace.Handlers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Abstracts
{
    public abstract class GameState
    {
        #region Fields

        protected ContentManager content;

        protected GraphicsDevice graphicsDevice;

        protected GameRoot game;

        #endregion
        public GameState(GameRoot game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            this.game = game;

            this.graphicsDevice = graphicsDevice;

            this.content = content;
        }

        public virtual void Initialize()
        {

        }
        public abstract void Reset();
        public abstract void Restart();
        public abstract void LoadContent();

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public virtual InputHandler Input { get; set; }
    }
}
