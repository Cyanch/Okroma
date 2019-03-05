using Okroma.Sprites;

namespace Okroma.TileEngine
{
    sealed class TileList
    {
        readonly Tile[] _tiles;
        const int Reserved = 16;

        public static readonly Tile NullTile = TileFactory.Create(0, Sprite.Null, TileProperties.None);

        public TileList(Tile[] tiles)
        {
            this._tiles = new Tile[tiles.Length + Reserved];
            Include(tiles);
        }

        private void Include(Tile[] tiles)
        {
            for (int i = Reserved; i < tiles.Length + Reserved; i++)
            {
                this._tiles[i] = ChangeId(tiles[i - Reserved], i);
            }
        }

        private Tile ChangeId(Tile tile, int newId)
        {
            return TileFactory.Create(newId, tile.Sprite, tile.Properties);
        }

        public Tile this[int tileId] => _tiles[tileId];
    }
}
