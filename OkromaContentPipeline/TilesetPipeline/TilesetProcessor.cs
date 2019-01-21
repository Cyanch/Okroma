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
                
                var packedColorValue = RGBAHexadecimalToARGBPackedValue(hex);

                data.AddColorTilePair(packedColorValue, tilePath);
            }
            return data;
        }

        public uint RGBAHexadecimalToARGBPackedValue(string hexadecimal)
        {
            var r = uint.Parse(hexadecimal.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            var g = uint.Parse(hexadecimal.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            var b = uint.Parse(hexadecimal.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            var alpha = uint.Parse(hexadecimal.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);

            return (alpha << 24) | (b << 16) | (g << 8) | r;
        }
    }
}
