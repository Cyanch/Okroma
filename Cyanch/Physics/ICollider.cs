using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Cyanch.Physics
{
    public interface ICollider
    {
        /// <summary>
        /// Collision Bounding Box
        /// </summary>
        Rectangle Bounds { get; }

        /// <summary>
        /// Do the colliders pass through eachother.
        /// </summary>
        /// <param name="collider"></param>
        void IsPassable(ICollider collider);

        /// <summary>
        /// Object has collided!
        /// </summary>
        /// <param name="collider"></param>
        void Collide(ICollider collider);
    }
    
    /// <summary>
    /// Wraps an <see cref="ICollisionProvider"/> around a <see cref="ICollider"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Collider<T> : ICollisionProvider where T : ICollider
    {
        private readonly IReadOnlyList<ICollider> _collider;

        public Collider(ICollider collider)
        {
            _collider = new List<ICollider>
            {
                collider
            };
        }

        IReadOnlyList<ICollider> ICollisionProvider.GetAllColliders()
        {
            return _collider;
        }

        IReadOnlyList<ICollider> ICollisionProvider.GetColliders(Rectangle rect)
        {
            return _collider;
        }
    }
}
