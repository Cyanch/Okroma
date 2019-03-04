using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace Okroma.Content.Pipeline.TileList
{
    [ContentTypeWriter]
    public class TileListWriter : ContentTypeWriter<TileListProcessorResult>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "Okroma.TileEngine.TileListReader, Okroma";
        }

        protected override void Write(ContentWriter writer, TileListProcessorResult value)
        {
            writer.Write(value.Data.Tiles.Length);

            foreach (var tile in value.Data.Tiles)
            {
                writer.Write(tile.Sprites.Length);
                foreach (var sprite in tile.Sprites)
                {
                    writer.Write(sprite.TexturePath);

                    var sourceRectangle = sprite.Rectangle.GetValueOrDefault();
                    writer.Write(sourceRectangle.X);
                    writer.Write(sourceRectangle.Y);
                    writer.Write(sourceRectangle.Width);
                    writer.Write(sourceRectangle.Height);

                    writer.Write(sprite.Origin);
                }
            }
        }
    }
}
