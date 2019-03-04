namespace Okroma.TileEngine
{
    class TileMap
    {
        class TileMapLayer
        {
            Tile[,] tiles;

            /// <summary>
            /// <see cref="TileMapLayer"/> constructor.
            /// </summary>
            /// <param name="mapWidth">Width of map</param>
            /// <param name="mapHeight">Height of map</param>
            /// <param name="initializingTile">Initial tile</param>
            public TileMapLayer(int mapWidth, int mapHeight, Tile initializingTile)
            {
                tiles = new Tile[mapWidth, mapHeight];
            }

            public void SetTile(int x, int y, Tile tile)
            {
                tiles[x, y] = tile;
            }

            public Tile GetTile(int x, int y)
            {
                return tiles[x, y];
            }
        }
    }
}
