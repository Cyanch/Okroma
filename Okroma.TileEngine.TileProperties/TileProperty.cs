using System;

namespace Okroma.TileEngine.TileProperties
{
    public enum TileProperty
    {
        WallJump = 0
    }

    [Flags]
    public enum TileWallJumpProperty : byte
    {
        None = 0,
        Left = 1 << 0,
        Right = 1 << 1,
        Both = Left | Right
    }
}
