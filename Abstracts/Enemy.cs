using GameSpace.Camera2D;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Interfaces;
using GameSpace.Machines;
using GameSpace.Objects.EnemyObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Abstracts
{
    public abstract class Enemy : IGameObjects
    {
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }
        public Rectangle ExpandedCollisionBox { get; set; }
        public int ObjectID { get; set; }
        public int Direction { get; set; }

        internal IMobState state;
        internal bool drawBox;

        public virtual void Draw(SpriteBatch spritebatch)
        {
            state.Draw(spritebatch, Position);

            if (drawBox) state.DrawBoundingBox(spritebatch, CollisionBox);
        }

        public virtual void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    EnemyCollisionHandler.GetInstance().HandleMarioCollision(this);
                    break;

                case (int)BlockID.USEDBLOCK:
                case (int)BlockID.QUESTIONBLOCK:
                case (int)BlockID.FLOORBLOCK:
                case (int)BlockID.STAIRBLOCK:
                case (int)BlockID.COINBRICKBLOCK:
                case (int)BlockID.BRICKBLOCK:
                case (int)ItemID.BIGPIPE:
                case (int)ItemID.MEDIUMPIPE:
                case (int)ItemID.SMALLPIPE:
                case (int)ItemID.WARPPIPEBODY:
                case (int)ItemID.WARPPIPEHEAD:
                case (int)ItemID.WARPPIPEHEADWITHMOB:
                case (int)ItemID.WARPVINEWITHBLOCK:
                case (int)ItemID.WARPPIPEROOM:
                    EnemyCollisionHandler.GetInstance().HandleBlockCollision(this, entity);
                    break;

                case (int)ItemID.FIREBALL:
                    Trigger();
                    break;

                case (int)EnemyID.GOOMBA:
                    if (state is StateUberGoombaBersek)
                    {
                        entity.Trigger();
                        MarioHandler.GetInstance().IncrementMarioPoints(100);
                    }
                    break;
            }
        }

        public virtual bool RevealItem()
        {
            return false; 
        }

        public virtual void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

        public virtual void Trigger()
        {
            state.Trigger();
            MusicHandler.GetInstance().PlaySoundEffect(2);
        }

        public virtual void Update(GameTime gametime)
        {
            state.Update(gametime);
        }

        private protected bool IsInview()
        {
            Camera copyCam = FinderHandler.GetInstance().FindCameraCopy();
            return (Position.X > copyCam.Position.X && Position.X < copyCam.Position.X + 800);
        }
    }
}
