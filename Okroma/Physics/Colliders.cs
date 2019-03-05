using System.Collections;
using System.Collections.Generic;

namespace Okroma.Physics
{
    class Colliders : IEnumerable<Collider>
    {
        IEnumerable<Collider> _colliders;
        public Colliders(IEnumerable<Collider> colliders)
        {
            _colliders = colliders;
        }

        public IEnumerator<Collider> GetEnumerator()
        {
            return _colliders.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _colliders.GetEnumerator();
        }
    }
}
