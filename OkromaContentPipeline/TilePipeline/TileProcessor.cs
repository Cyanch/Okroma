using Microsoft.Xna.Framework.Content.Pipeline;

namespace OkromaContentPipeline.TilePipeline
{
    [ContentProcessor(DisplayName = "Tile -- " + nameof(OkromaContentPipeline))]
    public class TileProcessor : ContentProcessor<TileFile, >
    {
    }
}
