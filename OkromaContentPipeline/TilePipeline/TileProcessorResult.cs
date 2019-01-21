namespace OkromaContentPipeline.TilePipeline
{
    public class TileProcessorResult
    {
        public string Type { get; }

        public Sprite Sprite { get; }

        public bool IsWallJumpable { get; }
        public TileProcessorResult(TileFile tileFile)
        {
            this.Type = tileFile.Type;
            this.Sprite = tileFile.Sprite;
            this.IsWallJumpable = tileFile.IsWallJumpable;
        }
    }
}
