using Microsoft.Xna.Framework.Content.Pipeline;
using System.Collections.Generic;

namespace Okroma.Content.Pipeline.TileList
{
    [ContentProcessor(DisplayName = "TileList - Okroma.Content.Pipeline")]
    public class TileListProcessor : ContentProcessor<TileListFile, TileListProcessorResult>
    {
        public override TileListProcessorResult Process(TileListFile file, ContentProcessorContext context)
        {
            var presets = file.Presets;

            var tiles = new List<Tile>();

            foreach (var tile in file.Tiles)
            {
                var properties = new TileProperties();
                foreach (var variant in tile.Variants)
                {
                    properties = properties.Override(presets[variant]);
                }
                properties = properties.Override(tile.Properties);

                tiles.Add(new Tile() { Sprites = tile.Sprites, Properties = properties });
            }

            return new TileListProcessorResult(tiles);
        }
    }
}
