using Okroma.Physics;
using Okroma.World.Tiles.Objects;

namespace Okroma.World.Tiles
{
    public class CollidableTile : Tile, IHasCollisionMask
    {
        public int CollisionMask { get; }
        public bool IsWallJumpable { get; set; }

        public CollidableTile(string id, ISprite sprite, int collisionMask) : base(id, sprite)
        {
            this.CollisionMask = collisionMask;
        }

        public override ITileObject ToTileObject(TileLocation location)
        {
            var collidableTileObject = new CollidableTileObject(this, location, CollisionMask);
            collidableTileObject.CollisionProperties.IsWallJumpable = this.IsWallJumpable;
            return collidableTileObject;
        }
    }
}
