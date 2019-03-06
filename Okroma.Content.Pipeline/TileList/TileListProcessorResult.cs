using System;
using System.Collections.Generic;

namespace Okroma.Content.Pipeline.TileList
{
    public class TileListProcessorResult
    {
        public IEnumerable<Tile> Tiles { get; }

        public TileListProcessorResult(IEnumerable<Tile> tiles)
        {
            Tiles = tiles ?? throw new ArgumentNullException(nameof(tiles));
        }
    }
}
