using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameSpace
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private List<ISprite> spriteList;
        private List<IController> controllers;

        public List<ISprite> SpriteList { get => spriteList; }
        public GraphicsDeviceManager Graphics { get => graphics; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            controllers = new List<IController>()
            {
                new KeyboardInput(this), new ControllerInput(this)
            };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            Texture2D staticTexture = Content.Load<Texture2D>("mariosheet");

            spriteList = new List<ISprite>()
            {
                new StaticSprite(staticTexture, 4, 14, 1),
                new AnimatedSprite(staticTexture, 4, 14, 4, 0, 0, GraphicsDevice),
                new MovingStaticSprite(staticTexture, 4, 14, 1),
                new MovingAnimatedSprite(staticTexture, 4, 14, 3, 0, 1, GraphicsDevice)

            };
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }

            foreach (ISprite sprite in spriteList)
            {
                sprite.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(blendState: BlendState.AlphaBlend);
            spriteBatch.DrawString(font,
                "Press Q/Esc (START) to quit\n" +
                "Press f (D-PAD DOWN) to toggle fullscreen mode\n" +
                "Press w (A) to display a static sprite\n" +
                "Press e (B) to display an animated sprite\n" +
                "Press r (X) to display a moving, animated sprite\n" +
                "Press t (Y) to display a moving, static sprite\n",
                new Vector2(50.0f, 0.00f), Color.Indigo);

            foreach (ISprite sprite in spriteList)
            {
                sprite.Draw(spriteBatch, new Vector2(300, 250));
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
