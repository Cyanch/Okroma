using System;

namespace Okroma.TileEngine.TileProperties
{
    [Flags]
    public enum TileWallJumpProperty : byte
    {
        None = 0,
        Left = 1 << 0,
        Right = 1 << 1,
        Both = Left | Right
    }
}
