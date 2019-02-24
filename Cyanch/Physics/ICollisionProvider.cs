using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Cyanch.Physics
{
    public interface ICollisionProvider
    {
        IReadOnlyList<ICollider> GetColliders(Rectangle rect);
        IReadOnlyList<ICollider> GetAllColliders();
    }

}
