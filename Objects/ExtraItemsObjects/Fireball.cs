using GameSpace.Abstracts;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace GameSpace.GameObjects.ItemObjects
{
    public class StateFireBallFlying : AbstractBlockStates
    {
        public StateFireBallFlying()
        {
            sprite = SpriteItemFactory.GetInstance().CreateFireBall();
        }
    }

    public class StateFireBallExploded : AbstractBlockStates
    {
        public StateFireBallExploded()
        {
            sprite = SpriteItemFactory.GetInstance().CreateHitFireBall();
        }
    }


    public class Fireball : AbstractItem
    {

        private IBlockStates state;
        private bool hasCollided;
        public Mario mario;

        public Fireball()
        {
            ObjectID = (int)ItemID.FIREBALL;
            Sprite = SpriteItemFactory.GetInstance().CreateFireBall();
            mario = FinderHandler.GetInstance().FindMario();
            Position = mario.Position;
            CollisionBox = new Rectangle((int)Position.X + 5, (int)Position.Y, (Sprite.Texture.Width * 2 / 8) - 10, Sprite.Texture.Height * 2 + 5);
            Velocity = new Vector2(200, 0);
            hasCollided = false;
            drawBox = false;
            ++mario.numFireballs;
            state = new StateFireBallFlying();
        }

        public override void Trigger()
        {
            state = new StateFireBallExploded();
        }

        public override void Update(GameTime gametime)
        {
            if (!hasCollided)
            {
                state.Update(gametime);

                UpdatePosition(Position, gametime);
                UpdateCollisionBox();

            }

            if (state is StateFireBallExploded)
            {
                hasCollided = true;
                DeleteCollisionBox();
            }

        }

        public override void Draw(SpriteBatch spritebatch)
        {
            if (!hasCollided)
            {
                state.Draw(spritebatch, Position); //this shouldnt be hardcoded anymore 
                if (drawBox)
                {
                    Sprite.DrawBoundary(spritebatch, CollisionBox);
                }
            }
        }
        public override void HandleCollision(IGameObjects entity)
        {

            switch (entity.ObjectID)
            {
                case (int)EnemyID.GOOMBA:
                case (int)EnemyID.GREENKOOPA:
                case (int)EnemyID.REDKOOPA:
                    Trigger();
                    break;

                case (int)BlockID.USEDBLOCK:
                case (int)BlockID.QUESTIONBLOCK:
                case (int)BlockID.FLOORBLOCK:
                case (int)BlockID.STAIRBLOCK:
                case (int)BlockID.COINBRICKBLOCK:
                case (int)BlockID.BRICKBLOCK:
                case (int)EnemyID.PLANT:
                    Trigger();
                    break;
            }

        }
    }
}

