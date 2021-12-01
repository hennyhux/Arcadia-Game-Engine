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
            CollisionBox = new Rectangle(0, 0, 0, 0);
            Position = initialPosition;
            drawBox = false;
            hasCollided = true;
            
        }

        public  void Trigger(IGameObjects mario)
        {
            //teleports mario 
            Debug.WriteLine("VINE TELEPORT TRIGGERED");
            Vector2 teleportDest = FinderHandler.GetInstance().FindItem((int)ItemID.WARPVINEWITHBLOCK).Position;
            Debug.WriteLine("Teleport Dest: {0}, {1}", teleportDest.X + 50, teleportDest.Y);
            mario.Position = new Vector2(teleportDest.X + 50, teleportDest.Y);
            ((Mario)mario).EndClimbing();
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
