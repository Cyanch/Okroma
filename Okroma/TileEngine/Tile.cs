using Microsoft.Xna.Framework;
using Okroma.Sprites;
using System;

namespace Okroma.TileEngine
{
    struct Tile : IEquatable<Tile>
    {
        public int Id { get; }
        public Sprite Sprite { get; }
        public TileProperties Properties { get; }

        public const int Size = 32;
        public static readonly Vector2 Vector2Size = new Vector2(Size);

        public Tile(int id, Sprite sprite, TileProperties properties) : this()
        {
            this.Sprite = sprite ?? throw new ArgumentNullException(nameof(sprite));
            this.Properties = properties;
        }

        public override bool Equals(object obj)
        {
            return obj is Tile && Equals((Tile)obj);
        }

        public bool Equals(Tile other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
