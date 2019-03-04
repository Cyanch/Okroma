namespace Okroma.Physics
{
    interface IIntersect<TCollider> where TCollider : Collider
    {
        bool Intersects(TCollider other);
    }
}
