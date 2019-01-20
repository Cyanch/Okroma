using Newtonsoft.Json;

namespace OkromaContentPipeline.LevelPipeline
{
    public class LevelFile
    {
        [JsonProperty("tilesets", Required = Required.Always)]
        public string[] TilesetPaths { get; set; }

        [JsonProperty("tilemaps", Required = Required.Always)]
        public string[] TileMapPaths { get; set; }
    }
}
