using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Okroma.ContentExtensions;
using Okroma.Sprites;
using Okroma.TileEngine.TileProperties;

namespace Okroma.TileEngine
{
    class TileListReader : ContentTypeReader<TileList>
    {
        protected override TileList Read(ContentReader reader, TileList existingInstance)
        {
            var tileCount = reader.ReadInt32();

            Tile[] tiles = new Tile[tileCount];
            for (int i = 0; i < tileCount; i++)
            {
                var spriteCount = reader.ReadInt32();

                var sprites = new Sprite[spriteCount];
                for (int k = 0; k < spriteCount; k++)
                {
                    string texturePath = reader.ReadString();
                    Vector2 origin = reader.ReadVector2();
                    Rectangle? sourceRectangle = reader.ReadNullableRectangle();

                    var texture = reader.ContentManager.Load<Texture2D>(texturePath);
                    sprites[k] = SpriteFactory.Create(texture, sourceRectangle == Rectangle.Empty ? null : sourceRectangle, origin);
                }

                var tileProperties = new TilePropertyCollection();

                Rectangle? tileBounds = reader.ReadNullableRectangle();
                if (tileBounds.HasValue)
                {
                    tileProperties.AddProperty(new CustomCollisionBoundsTileProperty(tileBounds.Value));
                }

                tiles[i] = TileFactory.Create(i, sprites[0], tileProperties);
            }

            return new TileList(tiles);
        }
    }
}
