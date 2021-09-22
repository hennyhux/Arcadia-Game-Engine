using GameSpace.Factories;
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
        private BlockFactory blockFactory;

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

            blockFactory = new BlockFactory();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            Texture2D staticTexture = Content.Load<Texture2D>("mariosheet");
            blockFactory.LoadContent(Content);

            spriteList = new List<ISprite>()
            {
                blockFactory.ReturnUsedBlock(), blockFactory.ReturnStairBlock(),
                blockFactory.ReturnBrickBlock(), blockFactory.ReturnFloorBlock(),
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

            foreach (ISprite sprite in spriteList)
            {
                sprite.Draw(spriteBatch, new Vector2(0, 0));
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
