using Okroma.Sprites;

namespace Okroma.TileEngine
{
    class TileFactory
    {
        public static Tile Create(int id, Sprite sprite)
        {
            return Create(id, sprite, TileProperties.None);
        }

        public static Tile Create(int id, Sprite sprite, TileProperties property)
        {
            return new Tile(id, sprite, property);
        }

        /// <summary>
        /// Creates a <see cref="Tile"/> that does not do anything.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Tile CreateBlankTile(int id)
        {
            return Create(0, Sprite.Null, TileProperties.None);
        }
    }
}
