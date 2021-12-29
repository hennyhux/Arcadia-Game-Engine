using GameSpace.Abstracts;
using Microsoft.Xna.Framework;
using GameSpace.Enums;
using GameSpace.Objects.EnemyObjects;

namespace GameSpace.Objects.BlockObjects
{
    public class WarpPipeWithPlant : Blocks
    {
        private Plant plant;
        public WarpPipeWithPlant(Vector2 location)
        {
            ObjectID = (int)ItemID.WARPPIPEHEADWITHMOB;


        }

        public override bool RevealItem()
        {
            return true;
        }
    }


}
