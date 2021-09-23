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
        private List<ISprite> enemySpriteList;
        private List<IController> controllers;
        private BlockFactory blockFactory;
        private MarioFactory marioFactory;
        private EnemyFactory enemyFactory;

        private ISprite MarioSprite;

        public List<ISprite> SpriteList { get => spriteList; }
        public ISprite GetMarioSprite { get => MarioSprite; }
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
            marioFactory = new MarioFactory();
            enemyFactory = new EnemyFactory();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            blockFactory.LoadContent(Content);
            marioFactory.LoadContent(Content);
            enemyFactory.LoadContent(Content);

            spriteList = new List<ISprite>()
            {
                blockFactory.ReturnUsedBlock(), blockFactory.ReturnStairBlock(),
                blockFactory.ReturnBrickBlock(), blockFactory.ReturnFloorBlock(),
                blockFactory.ReturnHiddenBlock(), blockFactory.ReturnQuestionBlock(),
            };

            enemySpriteList = new List<ISprite>()
            {
                enemyFactory.ReturnGoomba()
            };

            MarioSprite = marioFactory.ReturnMarioStandingLeftSprite();
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

            foreach (ISprite sprite in enemySpriteList)
            {
                sprite.Update(gameTime);
            }

            MarioSprite.Update(gameTime);

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

            foreach (ISprite sprite in enemySpriteList)
            {
                sprite.Draw(spriteBatch, new Vector2(0, 0));
            }

            MarioSprite.Draw(spriteBatch, new Vector2(500, 200));
            

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
