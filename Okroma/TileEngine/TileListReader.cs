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
                string texturePath = reader.ReadString();
                int sourceRectangleX = reader.ReadInt32();
                int sourceRectangleY = reader.ReadInt32();
                int sourceRectangleW = reader.ReadInt32();
                int sourceRectangleH = reader.ReadInt32();
                Vector2 origin = reader.ReadVector2();

                var texture = reader.ContentManager.Load<Texture2D>(texturePath);
                var sourceRectangle = new Rectangle(sourceRectangleX, sourceRectangleY, sourceRectangleW, sourceRectangleH);

                Tile tile = TileFactory.Create(i, SpriteFactory.Create(texture, sourceRectangle, origin));
            }

            return new TileList(tiles);
        }
    }
}
