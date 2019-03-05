namespace Okroma.Physics
{
    abstract class Collider
    {
        public static readonly Collider None = new NoCollider();

        readonly CollisionFunction _onCollision;
        public Collider(CollisionFunction onCollision)
        {
            this._onCollision = onCollision;
        }

        public CollisionAction Collide(Collider collider)
        {
            return _onCollision(collider);
        }

        private sealed class NoCollider : Collider
        {
            public NoCollider() : base(_ => CollisionAction.Ignore )
            {
            }
        }

        public delegate CollisionAction CollisionFunction(Collider other);
    }

}
