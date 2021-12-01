using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Machines;
using Microsoft.Xna.Framework;
using GameSpace.Interfaces;
using System.Diagnostics;
using GameSpace.EntityManaging;
using GameSpace.GameObjects.BlockObjects;
namespace GameSpace.GameObjects.ItemObjects
{
    public class Vine : AbstractItem
    {
        private Vector2 teleportDestination;

        
        public Vine(Vector2 initialPosition)
        {
            ObjectID = (int)ItemID.VINE;
            Sprite = SpriteItemFactory.GetInstance().CreateVine();
            Position = initialPosition;
            drawBox = false;
            hasCollided = false;

        }

        public Vine(Vector2 initialPosition, int teleporter)
        {
            Debug.WriteLine("VINE TELEPORT CREATED");
            ObjectID = (int)ItemID.WARPVINEWITHBLOCK;
            Sprite = SpriteItemFactory.GetInstance().CreateVine();
            //CollisionBox = new Rectangle(0, 0, 0, 0);
            Position = initialPosition;
            drawBox = false;
            hasCollided = false;
            
        }

        public  void Trigger(IGameObjects mario)
        {
            if(ObjectID == (int)ItemID.WARPVINEWITHBLOCK && teleportDestination != Vector2.Zero)
            {
                mario.Position = teleportDestination;
            }
            else
            {
                //teleports mario 
                Debug.WriteLine("VINE TELEPORT TRIGGERED");
                Vine WarpVine = (Vine)FinderHandler.GetInstance().FindItem((int)ItemID.WARPVINEWITHBLOCK);
                Debug.WriteLine("Teleport Dest: {0}, {1}", WarpVine.Position.X + 50, WarpVine.Position.Y);
                teleportDestination = new Vector2(WarpVine.Position.X + 50, WarpVine.Position.Y);
                mario.Position = teleportDestination;
                WarpVine.SetTeleportDest(new Vector2(Position.X + 50, Position.Y));
                ((Mario)mario).EndClimbing();
            }
            
        }

        public void SetTeleportDest(Vector2 destination)
        {
            teleportDestination = destination;
        }

        public override void Update(GameTime gametime)
        {
            Sprite.Update(gametime);
            UpdateCollisionBox();
        }

        public override void UpdateCollisionBox()
        {
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y,
              Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);

            ExpandedCollisionBox = new Rectangle((int)Position.X, (int)Position.Y,
                Sprite.Texture.Width * 2, (Sprite.Texture.Height * 2) + 4);
        }

        public void CheckTeleport(IGameObjects mario)
        {
            if(mario.Position.Y <= 50 && ObjectID ==  (int)ItemID.VINE)
            {
                Trigger(mario);
            }
            if(ObjectID == (int)ItemID.WARPVINEWITHBLOCK)
            {
                Trigger(mario);
            }
        }

        public override void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    //CollisionHandler.GetInstance().EnemyToMarioCollision(this);
                    CheckTeleport(entity);
                    break;
            }
        }
    }
}
