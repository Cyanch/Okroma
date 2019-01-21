using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Okroma.ContentReaders
{
    public class LevelContentReader : ContentTypeReader<Level>
    {
        protected override Level Read(ContentReader input, Level existingInstance)
        {
            int tilesetCount = input.ReadInt32();
            var tilesets = new List<Tileset>();
            for (int i = 0; i < tilesetCount; i++)
            {
                tilesets.Add(input.ContentManager.Load<Tileset>(input.ReadString()));
            }
            var colorMaps = new List<Color[]>();
            int mapCount = input.ReadInt32();
            Point mapSize = Point.Zero;
            for (int i = 0; i < mapCount; i++)
            {
                var mapImage = input.ContentManager.Load<Texture2D>(input.ReadString());
                var colorMap = new Color[mapImage.Width * mapImage.Height];
                mapImage.GetData(colorMap);
                colorMaps.Add(colorMap);
                var imageSize = new Point(mapImage.Width, mapImage.Height);
                if (mapSize == Point.Zero)
                {
                    mapSize = imageSize;
                }
                else if (mapSize != imageSize)
                {
                    throw new ContentLoadException("Maps with varying sizes from eachother cannot be loaded together!");
                }
            }
            Vector2 playerSpawnLocation = input.ReadVector2();
            byte playerLayer = input.ReadByte();

            return new Level(tilesets, mapSize, colorMaps, playerSpawnLocation, playerLayer);
        }
    }
}
