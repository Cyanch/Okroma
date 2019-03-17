using Okroma.Sprites;
using Okroma.TileEngine.TileProperties;

namespace Okroma.TileEngine
{
    class TileFactory
    {
        public static Tile Create(int id, Sprite sprite)
        {
            return Create(id, sprite, TilePropertyCollection.Empty);
        }

        public static Tile Create(int id, Sprite sprite, IReadOnlyTilePropertyCollection property)
        {
            return new Tile(id, sprite, property);
        }

        /// <summary>
        /// Creates a <see cref="Tile"/> that does not do anything.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Tile CreateFake(int id)
        {
            return Create(id, Sprite.Null, TilePropertyCollection.Empty);
        }
    }
}
