namespace Okroma.Physics
{
    interface ICanIntersect<TCollider> where TCollider : Collider
    {
        bool Intersects(TCollider other);
    }
}
