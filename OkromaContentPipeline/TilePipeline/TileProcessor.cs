using Microsoft.Xna.Framework.Content.Pipeline;

namespace OkromaContentPipeline.TilePipeline
{
    [ContentProcessor(DisplayName = "Tile -- " + nameof(OkromaContentPipeline))]
    public class TileProcessor : ContentProcessor<TileFile, TileProcessorResult>
    {
        public override TileProcessorResult Process(TileFile input, ContentProcessorContext context)
        {
            return new TileProcessorResult(input);
        }
    }
}
