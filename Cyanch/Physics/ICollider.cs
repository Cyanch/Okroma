using C3;
using System;

namespace Cyanch.Physics
{
    public interface ICollider : IQuadStorable
    {
        event EventHandler Moved;

        bool CanCollide(ICollider collider);
    }
}
