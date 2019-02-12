﻿using Cyanch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Okroma.TileEngine;
using Okroma.TileEngine.TileProperties;
using System;

namespace Okroma.ContentReaders
{
    public class TileContentReader : ContentTypeReader<TileData>
    {
        protected override TileData Read(ContentReader input, TileData existingInstance)
        {
            string type = input.ReadString();
            var texture = input.ContentManager.Load<Texture2D>(input.ReadString());

            var id = int.Parse(input.AssetName);
            
            GameScale textureWidth = GameScale.FromPixel(texture.Width);
            GameScale textureHeight = GameScale.FromPixel(texture.Height);

            int atlasId = id % (textureWidth.Tiles * textureHeight.Tiles);

            int tX = atlasId % textureWidth.Tiles;
            int tY = (int)Math.Floor(tX / (float)textureWidth.Tiles);

            Rectangle sourceRect = new Rectangle(GameScale.FromTile(tX).Pixels, GameScale.FromTile(tY).Pixels, GameScale.TileSize, GameScale.TileSize);

            var bounds = new Rectangle(0, 0, GameScale.TileSize, GameScale.TileSize);

            TileData data = NewDataFromType(type, texture, sourceRect, bounds);
            ApplyProperties(input, data);

            return data;
        }

        private TileData NewDataFromType(string type, Texture2D texture, Rectangle sourceRect, Rectangle bounds)
        {
            switch (type)
            {
                case "normal":
                    return new TileData(new Sprite(texture, sourceRect, GameScale.TileSizePoint.ToVector2() / 2), bounds);

                case "background":
                    return new TileData(new Sprite(texture, sourceRect, GameScale.TileSizePoint.ToVector2() / 2), Rectangle.Empty);



                default:
                    throw new ContentLoadException(string.Format(nameof(Tile) + " could not load. What is '{0}'?", type));
            }
        }

        private void ApplyProperties(ContentReader reader, TileData data)
        {
            int propertyCount = reader.ReadInt32();
            for (int i = 0; i < propertyCount; i++)
            {
                TileProperty property = (TileProperty)Enum.ToObject((typeof(TileProperty)), reader.ReadInt32());

                switch (property)
                {
                    case TileProperty.WallJump:
                        data.Properties.WallJump = (TileWallJumpProperty)Enum.ToObject(typeof(TileWallJumpProperty), reader.ReadByte());
                        break;
                }
            }
        }
    }
}
