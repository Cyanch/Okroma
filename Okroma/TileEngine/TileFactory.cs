using Okroma.Physics;
using Okroma.Sprites;

namespace Okroma.TileEngine
{
    class TileFactory
    {
        public static Tile Create(int id, Sprite sprite)
        {
            return Create(id, sprite, Collider.None);
        }

        public static Tile Create(int id, Sprite sprite, Collider collider)
        {
            return new Tile(id, sprite, collider);
        }
    }
}
