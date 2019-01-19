using Microsoft.Xna.Framework;

namespace Okroma.World
{
    public struct TileLocation
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public Point AsPixel { get; }
        public Point AsLocalized { get; }

        public Chunk2D Chunk { get; }
        public World2D World { get; }

        public TileLocation(int x, int y, int z, Chunk2D chunk, int chunkX, int chunkY)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.World = chunk.World;

            if (Chunk2D.IsValidChunkCoordinate(World, chunkX, chunkY))
            {
                this.AsLocalized = new Point(chunkX, chunkY);
            }
            else
            {
                var chunkPoint = World.ChunkPointFromTileLocation(x, y);
                chunk = World.GetChunk(chunkPoint.X, chunkPoint.Y);
                this.AsLocalized = new Point(X - chunk.Location.AsTile.X, Y - chunk.Location.AsTile.Y);
            }
            this.Chunk = chunk;

            this.AsPixel = new Point(X * World.Info.TileSize, Y * World.Info.TileSize);
        }

        public override string ToString()
        {
            return "X: " + X + ", Y: " + Y + ", Z: " + Z + ((Chunk != null) ? (" Chunk:" + Chunk.Location.ToString()) : string.Empty);
        }
    }
}
