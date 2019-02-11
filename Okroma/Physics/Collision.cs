using Cyanch.Physics;
using Okroma.TileEngine;

namespace Okroma.Physics
{
    public static class Collision
    {
        public static bool CanCollide(ICollider collider1, ICollider collider2)
        {
            if (collider1 is Tile && collider2 is Tile)
            {
                return true;
            }

            return false;
        }
    }
}
