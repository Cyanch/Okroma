using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Okroma.Content.Pipeline.JsonConverters;

namespace Okroma.Content.Pipeline
{
    public class TexturedSprite
    {
        [JsonProperty("texture", Required = Required.Always)]
        public string TexturePath { get; set; }

        [JsonProperty("sourceRectangle")]
        [JsonConverter(typeof(RectangleJsonConverter))]
        public Rectangle? Rectangle { get; set; }

        [JsonProperty("origin")]
        [JsonConverter(typeof(Vector2JsonConverter))]
        public Vector2 Origin { get; set; }
    }
}
