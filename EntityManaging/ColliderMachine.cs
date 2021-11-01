using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.GameObjects.ExtraItemsObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;

namespace GameSpace.EntityManaging
{
    public class ColliderMachine
    {
        private static readonly ColliderMachine instance = new ColliderMachine();
        private static readonly IGameObjects marioInstance = EntityManager.FindItem((int)AvatarID.MARIO);
        public static ColliderMachine GetInstance()
        {
            return instance;
        }

        private ColliderMachine()
        {

        }

        public void HandleMarioCollision(BigPipe pipe)
        {
            switch (DetectCollisionDirection(marioInstance, pipe))
            {
                case (int)CollisionDirection.UP:

                    break;
            }
        }

        private int DetectCollisionDirection(IGameObjects a, IGameObjects b)
        {
            Rectangle overLappedRectangle = Rectangle.Intersect(a.CollisionBox, b.CollisionBox);
            int direction = 0;

            if (!overLappedRectangle.IsEmpty)
            {
                if (overLappedRectangle.Width > overLappedRectangle.Height && a.Position.Y < b.Position.Y)
                {
                    direction = (int)CollisionDirection.DOWN;
                }

                if (overLappedRectangle.Width > overLappedRectangle.Height && a.Position.Y > b.Position.Y)
                {
                    direction = (int)CollisionDirection.UP;
                }

                if (overLappedRectangle.Height > overLappedRectangle.Width && a.Position.X > b.Position.X)
                {
                    direction = (int)CollisionDirection.RIGHT;
                }

                if (overLappedRectangle.Height > overLappedRectangle.Width && a.Position.X < b.Position.X)
                {
                    direction = (int)CollisionDirection.LEFT;
                }
            }

            return direction;
        }
        public bool IntersectAABB(IGameObjects a, IGameObjects b)
        {

            if (a.CollisionBox.X + a.CollisionBox.Width < b.CollisionBox.X || a.CollisionBox.X > b.CollisionBox.X + b.CollisionBox.Width)
            {
                return false;
            }

            if (a.CollisionBox.Y + a.CollisionBox.Height < b.CollisionBox.Y || a.CollisionBox.Y > b.CollisionBox.Y + b.CollisionBox.Height)
            {
                return false;
            }

            else { return a.CollisionBox.Intersects(b.CollisionBox); }

        }
    }


}
