using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using OkromaContentPipeline.JsonConverters;

namespace OkromaContentPipeline
{
    public class Sprite
    {
        [JsonProperty("texture", Required = Required.Always)]
        public string TexturePath { get; set; }
        [JsonProperty("sourceRectangle", ItemConverterType = typeof(XnaRectangleJsonConverter))]
        public Rectangle SourceRectangle { get; set; } = Rectangle.Empty;
        [JsonProperty("origin")]
        public Vector2 Origin { get; set; }
    }
}
