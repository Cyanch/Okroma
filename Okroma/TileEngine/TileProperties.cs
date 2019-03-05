using Microsoft.Xna.Framework;
using Okroma.Physics;
using CollisionFunction = Okroma.Physics.Collider.CollisionFunction;

namespace Okroma.TileEngine
{
    interface ITileProperties
    {
        CollisionFunction OnCollision { get; }
        Rectangle Bounds { get; }
    }
    
    struct TileProperties : ITileProperties
    {
        public static readonly TileProperties None = BeginConstruct().EndConstruct();
        
        public CollisionFunction OnCollision { get; }
        public Rectangle Bounds { get; }

        public TileProperties(CollisionFunction onCollision, Rectangle bounds)
        {
            this.OnCollision = onCollision;
            this.Bounds = bounds;
        }

        public Rectangle CreateBounds(int mapX, int mapY)
        {
            var bounds = Bounds;
            bounds.Offset(mapX * Tile.Size, mapY * Tile.Size);
            return bounds;
        }

        public static TilePropertiesConstruct BeginConstruct()
        {
            return new TilePropertiesConstruct();
        }

        internal class TilePropertiesConstruct : ITileProperties
        {
            public CollisionFunction OnCollision { get; private set; } = _ => CollisionAction.Ignore;
            public Rectangle Bounds { get; private set; } = new Rectangle(0, 0, Tile.Size, Tile.Size);

            public void SetCollisionFunction(CollisionFunction onCollision)
            {
                this.OnCollision = onCollision;
            }

            public void UseCustomBounds(Rectangle bounds)
            {
                this.Bounds = bounds;
            }

            public TileProperties EndConstruct()
            {
                return new TileProperties(OnCollision, Bounds);
            }
        }
    }
}
