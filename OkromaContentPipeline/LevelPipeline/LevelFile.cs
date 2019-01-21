using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using OkromaContentPipeline.JsonConverters;

namespace OkromaContentPipeline.LevelPipeline
{
    public class LevelFile
    {
        [JsonProperty("tilesets", Required = Required.Always)]
        public string[] TilesetPaths { get; set; }

        [JsonProperty("tilemaps", Required = Required.Always)]
        public string[] TileMapPaths { get; set; }

        [JsonProperty("playerSpawnPosition", Required = Required.Always)]
        [JsonConverter(typeof(XnaVector2JsonConverter))]
        public Vector2 PlayerSpawnPosition { get; set; }

        [JsonProperty("playerLayer", Required = Required.Always)]
        public byte PlayerLayer { get; set; }
    }
}
