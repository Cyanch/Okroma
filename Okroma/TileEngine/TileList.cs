namespace Okroma.TileEngine
{
    sealed class TileList
    {
        readonly Tile[] _tiles;
        
        public TileList(Tile[] tiles)
        {
            this._tiles = tiles;
        }

        public Tile this[int tileId] => _tiles[tileId];
    }
}
