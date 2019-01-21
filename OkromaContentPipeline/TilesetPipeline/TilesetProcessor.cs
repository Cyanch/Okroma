using Microsoft.Xna.Framework.Content.Pipeline;
using System.Collections.Generic;

namespace OkromaContentPipeline.TilesetPipeline
{
    [ContentProcessor(DisplayName = "Tileset -- " + nameof(OkromaContentPipeline))]
    public class TilesetProcessor : ContentProcessor<Dictionary<string, string>, TilesetProcessorResult>
    {
        public override TilesetProcessorResult Process(Dictionary<string, string> input, ContentProcessorContext context)
        {
            var data = new TilesetProcessorResult(input.Count);
            foreach (var item in input)
            {
                string hex = item.Key;
                string tilePath = item.Value;

                //RGBA Hex to ARGB uint.
                var r = uint.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                var g = uint.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                var b = uint.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                var alpha = uint.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);

                uint packedColorValue = (alpha << 24) | (b << 16) | (g << 8) | r;
                data.AddColorTilePair(packedColorValue, tilePath);
            }
            return data;
        }
    }
}
