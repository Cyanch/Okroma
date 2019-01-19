using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Okroma.Physics
{
    public interface ICollidableSource
    {
        IEnumerable<ICollidableGameObject2D> GetCollidables(Rectangle rectangle);
    }
}
