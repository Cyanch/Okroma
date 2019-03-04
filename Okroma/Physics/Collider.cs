using System;

namespace Okroma.Physics
{
    abstract class Collider
    {
        public static readonly Collider None = new NoCollider();

        readonly Func<Collider, CollisionAction> _onCollision;
        public Collider(Func<Collider, CollisionAction> onCollision)
        {
            this._onCollision = onCollision;
        }

        public CollisionAction Collide(Collider collider)
        {
            return _onCollision(collider);
        }

        private sealed class NoCollider : Collider
        {
            public NoCollider() : base((_) => { return CollisionAction.Ignore; })
            {
            }
        }
    }
}
