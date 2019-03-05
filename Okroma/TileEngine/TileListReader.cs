using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Okroma.Sprites;

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
                    int sourceRectangleX = reader.ReadInt32();
                    int sourceRectangleY = reader.ReadInt32();
                    int sourceRectangleW = reader.ReadInt32();
                    int sourceRectangleH = reader.ReadInt32();
                    Vector2 origin = reader.ReadVector2();

                    var texture = reader.ContentManager.Load<Texture2D>(texturePath);
                    Rectangle? sourceRectangle = new Rectangle(sourceRectangleX, sourceRectangleY, sourceRectangleW, sourceRectangleH);
                    sprites[k] = SpriteFactory.Create(texture, sourceRectangle == Rectangle.Empty ? null : sourceRectangle, origin);
                }
                
                tiles[i] = TileFactory.Create(i, sprites[0]);
            }

            return new TileList(tiles);
        }
    }
}
