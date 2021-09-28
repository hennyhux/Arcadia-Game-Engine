using GameSpace.Factories;
using GameSpace.GameObjects;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.GameObjects.ItemObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameSpace
{
    public class Game1 : Game
    {
        private protected readonly GraphicsDeviceManager graphics;
        private protected SpriteBatch spriteBatch;

        #region Factories
        private BlockSpriteFactory blockSpriteFactory;
        private BlockObjectFactory blockObjectFactory;
        private MarioFactory marioFactory;
        private EnemySpriteFactory enemySpriteFactory;
        private ItemSpriteFactory itemSpriteFactory;
        private ItemObjectFactory itemObjectFactory;
        private BackgroundFactory backgroundFactory;
        private EnemyObjectFactory enemyObjectFactory;
        #endregion

        #region Lists
        private List<IBlockObjects> blocks;
        private List<IEnemyObjects> enemies;
        private List<IController> controllers;
        private List<IItemObjects> items;
        #endregion

        private Mario mario;
        public Mario GetMario { get => mario; }

        private ISprite MarioSprite;
        private ISprite Background;

        public GraphicsDeviceManager Graphics { get => graphics; }
        public BlockSpriteFactory BlockFactory { get => blockSpriteFactory; }
        public ItemSpriteFactory ItemSpriteFactory { get => itemSpriteFactory; }
        public MarioFactory GetMarioFactory { get => marioFactory; }
        public List<IBlockObjects> Blocks { get => blocks; }
        public List<IItemObjects> Items { get => items; }

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

            blockSpriteFactory = new BlockSpriteFactory();
            marioFactory = new MarioFactory(this);
            enemySpriteFactory = EnemySpriteFactory.GetInstance();
            enemyObjectFactory = EnemyObjectFactory.GetInstance();
            backgroundFactory = BackgroundFactory.GetInstance();
            blockObjectFactory = new BlockObjectFactory(this);
            itemSpriteFactory = ItemSpriteFactory.GetInstance();
            itemObjectFactory = new ItemObjectFactory(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            blockSpriteFactory.LoadContent(Content);
            marioFactory.LoadContent(Content);
            enemySpriteFactory.LoadContent(Content);
            backgroundFactory.LoadContent(Content);
            itemSpriteFactory.LoadContent(Content);
            backgroundFactory.LoadContent(Content);

            blocks = new List<IBlockObjects>()
            {
                blockObjectFactory.ReturnBrickBlockObject(), blockObjectFactory.ReturnStairBlockObject(),
                blockObjectFactory.ReturnFloorBlockObject(), blockObjectFactory.ReturnQuestionBlockObject(),
                blockObjectFactory.ReturnUsedBlockObject(), blockObjectFactory.ReturnHiddenBlockObject()
            };

            enemies = new List<IEnemyObjects>()
            {
                enemyObjectFactory.ReturnGoombaObject(), enemyObjectFactory.ReturnGreenKoopaObject(),
                enemyObjectFactory.ReturnRedKoopaObject()
            };

            items = new List<IItemObjects>()
            {
                itemObjectFactory.ReturnFireFlowerObject(), itemObjectFactory.ReturnOneUpShroomObject(), 
                itemObjectFactory.ReturnStarObject(), itemObjectFactory.ReturnSuperShroomObject(),
                itemObjectFactory.ReturnCoinObject()
            };

            mario = marioFactory.ReturnMario();
            MarioSprite = marioFactory.ReturnMarioStandingLeftSprite();
            Background = backgroundFactory.ReturnRegularBackground();

        }

        protected override void Update(GameTime gameTime)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }

            foreach (IBlockObjects block in blocks)
            {
                block.Update(gameTime);
            }

            foreach (IEnemyObjects enemy in enemies)
            {
                enemy.Update(gameTime);
            }
            foreach (IItemObjects item in items)
            {
                item.Update(gameTime);
            }

            mario.Update(gameTime);
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(blendState: BlendState.AlphaBlend);

            //Background.Draw(spriteBatch, new Vector2(450, 450));

            foreach (IBlockObjects block in blocks)
            {
                block.Draw(spriteBatch, new Vector2(0, 0));
            }

            foreach (IEnemyObjects enemy in enemies)
            {
                enemy.Draw(spriteBatch, new Vector2(0, 0));
            }

            foreach (IItemObjects item in items)
            {
                item.Draw(spriteBatch, new Vector2(0, 0));
            }

            mario.Draw(spriteBatch, new Vector2(500, 400));

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
