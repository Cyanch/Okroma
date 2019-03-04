using Microsoft.Xna.Framework;
using Okroma.Physics;
using Okroma.Sprites;
using System;

namespace Okroma.TileEngine
{
    struct Tile : IEquatable<Tile>
    {
        public int Id { get; }
        public Sprite Sprite { get; }
        public Collider Collider { get; }

        public const int Width = 32;    
        public const int Height = 32;
        public static readonly Vector2 Size = new Vector2(Width, Height);

        public Tile(int id, Sprite sprite, Collider collider) : this()
        {
            this.Sprite = sprite ?? throw new ArgumentNullException(nameof(sprite));
            this.Collider = collider ?? throw new ArgumentNullException(nameof(collider));
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
