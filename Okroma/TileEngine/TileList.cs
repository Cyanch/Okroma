namespace Okroma.TileEngine
{
    sealed class TileList
    {
        readonly Tile[] _tiles;

        public TileList(Tile[] tiles)
        {
            this._tiles = tiles;
        }

        public Tile GetTile(int tileId)
        {
            return _tiles[tileId];
        }
    }
}
