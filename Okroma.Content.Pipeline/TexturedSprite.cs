using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Newtonsoft.Json;
using Okroma.Content.Pipeline.JsonConverters;

namespace Okroma.Content.Pipeline
{
    public class TexturedSprite : IContentWriteable
    {
        [JsonProperty("texture", Required = Required.Always)]
        public string TexturePath { get; set; }

        [JsonProperty("sourceRectangle")]
        [JsonConverter(typeof(RectangleJsonConverter))]
        public Rectangle? Rectangle { get; set; }

        [JsonProperty("origin")]
        [JsonConverter(typeof(Vector2JsonConverter))]
        public Vector2 Origin { get; set; }

        public void Write(ContentWriter writer)
        {
            writer.Write(this.TexturePath);
            writer.Write(this.Rectangle.GetValueOrDefault());
            writer.Write(this.Origin);
        }
    }
}
