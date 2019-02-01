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
            //Assumes Enum is of byte.
            byte propKey = (byte)property;
            byte propValue = (byte)Convert.ChangeType(value, Enum.GetUnderlyingType(value.GetType()));

            if (_properties.ContainsKey(propKey))
            {
                _properties[propKey] = propValue;
            }
            else
            {
                _properties.Add(propKey, propValue);
            }
        }

        /// <summary>
        /// Get's the properties value.
        /// </summary>
        /// <typeparam name="TProperty">The returning enum type.</typeparam>
        /// <param name="property">The property to get</param>
        /// <returns></returns>
        public TProperty GetProperty<TProperty>(TileProperty property)
        {
            return (TProperty)Enum.ToObject(typeof(TProperty), (byte)property);
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
    }
}
