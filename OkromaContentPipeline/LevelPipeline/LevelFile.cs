using Newtonsoft.Json;

namespace OkromaContentPipeline.LevelPipeline
{
    public class LevelFile
    {
        [JsonProperty("tilesets")]
        public string[] TilesetPaths { get; set; }

        [JsonProperty("tilemap")]
        public string TileMapPath { get; set; }
    }
}
