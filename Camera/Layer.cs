using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameSpace.Camera2D;
using GameSpace.Sprites.Background;

namespace GameSpace.Camera2D
{


    public class Layer
    {
        public Layer(Camera camera)
        {
            _camera = camera;
            Parallax = Vector2.One;
            //Sprites = new List<ISprite>();
            Sprites = new List<BackgroundSprite>();
        }

        public Layer(Camera camera, BackgroundSprite Sprite, Vector2 parallax)
        {
            _camera = camera;
            Parallax = parallax;
            //Sprites = new List<ISprite>();
            Sprites = new List<BackgroundSprite>();
            //Sprites.Add((BackgroundSprite)Sprite);
            Sprites.Add(Sprite);
        }

        public Vector2 Parallax { get; set; }

        //public List<ISprite> Sprites { get; private set; }
        public List<BackgroundSprite> Sprites { get; private set; }

        public void Draw(SpriteBatch spriteBatch, Vector2 cameraLocation)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null, null, _camera.GetViewMatrix(Parallax));

            foreach (BackgroundSprite sprite in Sprites)
                sprite.Draw(spriteBatch, cameraLocation, Parallax);
            //sprite.Draw(spriteBatch, location);

            spriteBatch.End();
        }

        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return Vector2.Transform(worldPosition, _camera.GetViewMatrix(Parallax));
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, Matrix.Invert(_camera.GetViewMatrix(Parallax)));
        }

        private readonly Camera _camera;
    }
}
