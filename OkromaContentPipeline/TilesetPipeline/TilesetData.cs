using System.Collections.Generic;

namespace OkromaContentPipeline.TilesetPipeline
{
    public class TilesetProcessorResult
    {
        public int ColorTilePairCount { get; }
        Dictionary<uint, string> packedColorTilePairs = new Dictionary<uint, string>();

        public TilesetProcessorResult(int colorTilePairCount)
        {
            this.ColorTilePairCount = colorTilePairCount;
        }

        public void AddColorTilePair(uint packedColorValue, string tilePath)
        {
            packedColorTilePairs.Add(packedColorValue, tilePath);
        }

        public IEnumerable<KeyValuePair<uint, string>> GetColorTilePairs()
        {
            return packedColorTilePairs;
        }
    }
}
