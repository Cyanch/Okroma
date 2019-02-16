using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Okroma.TileEngine.TileProperties;
using System.Collections.Generic;

namespace OkromaContentPipeline.TilePipeline
{
    public class TileFile
    {
        [JsonProperty("type", Required = Required.Always)]
        public string Type { get; set; }

        [JsonProperty("texture", Required = Required.Always)]
        public string TexturePath { get; set; }

        [JsonProperty("properties")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Dictionary<TileProperty, string> Properties { get; set; }
    }
}
