using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Abstracts
{
    public abstract class AbstractBlock : IGameObjects
    {
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }

        public int ObjectID { get; set; }

        internal bool drawBox;
        internal bool hasCollided;
        internal IBlockStates state;


        public AbstractBlock()
        {
            drawBox = false;
        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            state.Draw(spritebatch, Position); //this shouldnt be hardcoded anymore 
            if (drawBox)
            {
                Sprite.DrawBoundary(spritebatch, CollisionBox);
            }
        }

        public virtual void Update(GameTime gametime)
        {
            state.Update(gametime);
        }

        public virtual void UpdatePosition(Vector2 location, GameTime gametime)
        {
            //Block does not move
        }
        public virtual bool IsCurrentlyColliding()
        {
            return false;
        }

        public virtual void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

        public virtual void Trigger()
        {

        }

        public virtual void HandleCollision(IGameObjects entity)
        {

        }
    }
}
