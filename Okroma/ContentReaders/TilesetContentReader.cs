using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Okroma.World.Tiles;

namespace Okroma.ContentReaders
{
    public class TilesetContentReader : ContentTypeReader<Tileset>
    {
        protected override Tileset Read(ContentReader input, Tileset existingInstance)
        {
            var count = input.ReadInt32();
            var tileset = new Tileset();
            for (int i = 0; i < count; i++)
            {
                var color = new Color(input.ReadUInt32());
                var tile = input.ContentManager.Load<ITile>(input.ReadString());
                tileset.Add(color, tile);
            }
            return tileset;
        }
    }
}
