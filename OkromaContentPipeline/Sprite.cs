using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Newtonsoft.Json;
using OkromaContentPipeline.ContentWriters;
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

    public static class ContentWriterForSpriteExtension
    {
        public static void Write(this ContentWriter output, Sprite sprite)
        {
            output.Write(sprite.TexturePath);
            output.Write(sprite.SourceRectangle);
            output.Write(sprite.Origin);
        } 
    }
}
