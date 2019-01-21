using Microsoft.Xna.Framework.Content;
using Okroma.World.Tiles;

namespace Okroma.ContentReaders
{
    public class TileContentReader : ContentTypeReader<ITile>
    {
        protected override ITile Read(ContentReader input, ITile existingInstance)
        {
            string type = input.ReadString();
            var sprite = input.ReadSprite();

            bool isWallJumpable = input.ReadBoolean();

            if (type == "normal")
            {
                return new CollidableTile(input.AssetName, sprite, (int)CollisionMask.NormalTile) { IsWallJumpable = isWallJumpable };
            }
            else if (type == "background")
            {
                return new Tile(input.AssetName, sprite);
            }
            {
                throw new ContentLoadException(nameof(ITile) + " could not load, missing ITile type.");
            }
        }
    }
}
