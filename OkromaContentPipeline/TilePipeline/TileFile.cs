using Newtonsoft.Json;

namespace OkromaContentPipeline.TilePipeline
{
    public class TileFile
    {
        [JsonProperty("type", Required = Required.Always)]
        public string Type { get; set; }

        [JsonProperty("sprite", Required = Required.Always)]
        public Sprite Sprite { get; set; }

        [JsonProperty("canWallJumpOn")]
        public bool IsWallJumpable { get; set; }
    }
}
