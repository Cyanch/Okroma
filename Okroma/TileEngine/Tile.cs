using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Okroma.TileEngine
{
    struct Tile
    {
        public Texture2D Texture { get; }

        public const int Width = 32;
        public const int Height = 32;
        public static readonly Vector2 Size = new Vector2(Width, Height);

        public Tile(Texture2D texture) : this()
        {
            Texture = texture ?? throw new ArgumentNullException(nameof(texture));
        }
    }
}
