namespace Okroma.Physics
{
    static class CollisionHelper
    {
        public static bool Intersects<TCollider1, TCollider2>(TCollider1 collider1, TCollider2 collider2) where TCollider1 : Collider where TCollider2 : Collider
        {
            if (collider1 is IIntersect<TCollider2> collider1Intersector)
            {
                return collider1Intersector.Intersects(collider2);
            }
            else if (collider2 is IIntersect<TCollider1> collider2Intersector)
            {
                return collider2Intersector.Intersects(collider1);
            }

            throw new CouldNotFindValidIntersectsMethod(collider1.GetType().ToString(), collider2.GetType().ToString());
        }
    }
}
