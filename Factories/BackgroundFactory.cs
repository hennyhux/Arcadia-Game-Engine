using GameSpace.Sprites.Background;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Factories
{
    public class BackgroundFactory
    {
        private Texture2D RegularBackground;
        private Texture2D RegularBackground1;
        private Texture2D Mountain;
        private Texture2D Clouds;
        private Texture2D Clouds1;
        private Texture2D BlackWindow;
        private Texture2D GameOver;


        private static readonly BackgroundFactory instance = new BackgroundFactory();
        public static BackgroundFactory GetInstance()
        {
            return instance;
        }

        private BackgroundFactory()
        {

        }

        public void LoadContent(ContentManager content)
        {
            RegularBackground1 = content.Load<Texture2D>("Background/Backgrounds");
            RegularBackground = content.Load<Texture2D>("Background/small_BG");
            Mountain = content.Load<Texture2D>("Background/mountain");
            Clouds = content.Load<Texture2D>("Background/cloudsdual");
            Clouds1 = content.Load<Texture2D>("Background/clouds1");
            BlackWindow = content.Load<Texture2D>("BlackWindow");
            GameOver = content.Load<Texture2D>("Background/game-over-screen");
        }

        public BackgroundSprite CreateRegularBackground()//mario.SmallMarioJumpingSprite
        {
            return new BackgroundSprite(RegularBackground, new Vector2(0, 395));
        }

        public BackgroundSprite CreateGameOver()//mario.SmallMarioJumpingSprite
        {
            return new BackgroundSprite(GameOver, new Vector2(0, 395));
        }

        public BackgroundSprite CreateRegularBackground1()//mario.SmallMarioJumpingSprite
        {
            return new BackgroundSprite(RegularBackground1, new Vector2(0, 395));
        }

        public BackgroundSprite CreateBlackWindow()//mario.SmallMarioJumpingSprite
        {
            return new BackgroundSprite(BlackWindow, new Vector2(7008, 50));
        }

        public BackgroundSprite CreateBGMountainSprite()//mario.SmallMarioJumpingSprite
        {
            return new BackgroundSprite(Mountain, new Vector2(0, 100));
        }

        public BackgroundSprite CreateCloudsSprite()//mario.SmallMarioJumpingSprite
        {
            return new BackgroundSprite(Clouds, new Vector2(0, 0));
        }

        public BackgroundSprite CreateClouds1Sprite()//mario.SmallMarioJumpingSprite
        {
            return new BackgroundSprite(Clouds1, new Vector2(0, 0));
        }

        public BackgroundSprite CreateRegularBackground(Vector2 position)//mario.SmallMarioJumpingSprite
        {
            return new BackgroundSprite(RegularBackground, position);
        }

        public BackgroundSprite CreateBGMountainSprite(Vector2 position)//mario.SmallMarioJumpingSprite
        {
            return new BackgroundSprite(Mountain, position);
        }

        public BackgroundSprite CreateCloudsSprite(Vector2 position)//mario.SmallMarioJumpingSprite
        {
            return new BackgroundSprite(Clouds, position);
        }

    }
}
