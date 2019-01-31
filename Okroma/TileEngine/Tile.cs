using Okroma.TileEngine.TileProperties;
using System;
using System.Collections.Generic;
using System.IO;

namespace Okroma.TileEngine
{
    public struct Tile
    {
        public ushort Id { get; } // [ Tileset ]
        public byte Meta { get; } // [ Subset ]

        private Dictionary<byte, byte> _properties;

        public Tile(ushort id, byte meta) : this()
        {
            Id = id;
            Meta = meta;

            _properties = new Dictionary<byte, byte>();
        }

        public void SetProperty<TProperty>(TileProperty property, TProperty value) where TProperty : Enum
        {
            byte propKey = (byte)property;
            //Enum.GetUnderlyingType(typeof(TProperty));
            if (_properties.ContainsKey(propKey))
            {
            }
        }


        public static void Write(BinaryWriter writer, Tile tile)
        {
            writer.Write(tile.Id);
            writer.Write(tile.Meta);
        }

        public static Tile Read(BinaryReader reader)
        {
            ushort id = reader.ReadUInt16();
            byte meta = reader.ReadByte();

            return new Tile(id, meta);
        }

        public TileWallJumpProperty WallJumpProperty { get; set; }
    }
}
