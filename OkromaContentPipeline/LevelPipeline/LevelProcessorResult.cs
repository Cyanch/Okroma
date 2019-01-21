using Microsoft.Xna.Framework;

namespace OkromaContentPipeline.LevelPipeline
{
    public class LevelProcessorResult
    {
        public string[] TilesetPaths { get; }
        public string[] TileMapPaths { get; }
        public Vector2 PlayerSpawnPosition { get; }
        public int PlayerLayer { get; }

        public LevelProcessorResult(LevelFile levelFile)
        {
            TilesetPaths = levelFile.TilesetPaths;
            TileMapPaths = levelFile.TileMapPaths;
            PlayerSpawnPosition = levelFile.PlayerSpawnPosition;
            PlayerLayer = levelFile.PlayerLayer;
        }
    }
}
