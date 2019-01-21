using Microsoft.Xna.Framework.Content.Pipeline;

namespace OkromaContentPipeline.LevelPipeline
{
    [ContentProcessor(DisplayName = "Level -- " + nameof(OkromaContentPipeline))]
    public class LevelProcessor : ContentProcessor<LevelFile, ProcessorResult<LevelFile>>
    {
        public override ProcessorResult<LevelFile> Process(LevelFile input, ContentProcessorContext context)
        {
            return new ProcessorResult<LevelFile>(input);
        }
    }
}
