using Microsoft.Xna.Framework;
using Okroma.Sprites;
using System;

namespace Okroma.TileEngine
{
    struct Tile
    {
        public Sprite Sprite { get; }

        public const int Width = 32;
        public const int Height = 32;
        public static readonly Vector2 Size = new Vector2(Width, Height);

        public Tile(Sprite sprite) : this()
        {
            Sprite = sprite ?? throw new ArgumentNullException(nameof(sprite));
        }
    }
}
