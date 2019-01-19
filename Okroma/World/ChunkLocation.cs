using Microsoft.Xna.Framework;

namespace Okroma.World
{
    public struct ChunkLocation
    {
        public World2D World { get; }

        public int X { get; }
        public int Y { get; }

        public Point AsPixel { get; }
        public Point AsTile { get; }

        public ChunkLocation(World2D world, int x, int y)
        {
            this.World = world;
            this.X = x;
            this.Y = y;

            this.AsPixel = new Point(x * World.Info.ChunkSize, y * World.Info.ChunkSize);
            this.AsTile = new Point(x * World.Info.ChunkTileSpan, y * World.Info.ChunkTileSpan);
        }

        public Point ToPoint()
        {
            return new Point(X, Y);
        }

        public override string ToString()
        {
            return "X: " + X + ", Y: " + Y;
        }
    }
}
