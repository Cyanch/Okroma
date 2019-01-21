using Microsoft.Xna.Framework.Content.Pipeline;

namespace OkromaContentPipeline.LevelPipeline
{
    [ContentProcessor(DisplayName = "Level -- " + nameof(OkromaContentPipeline))]
    public class LevelProcessor : ContentProcessor<LevelFile, LevelProcessorResult>
    {
        public override LevelProcessorResult Process(LevelFile input, ContentProcessorContext context)
        {
            return new LevelProcessorResult(input);
        }
    }
}
