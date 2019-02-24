using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Cyanch.Physics
{
    public interface ICollisionService
    {
        IReadOnlyList<ICollider> GetAllColliders();
        IReadOnlyList<ICollider> GetColliders(Rectangle area);

        void RegisterCollisionProvider(ICollisionProvider collider);
        void UnregisterCollisionProvider(ICollisionProvider colider);
    }
    
    public class Collisions : ICollisionService
    {
        List<ICollisionProvider> _providers = new List<ICollisionProvider>();
        
        public IReadOnlyList<ICollider> GetColliders(Rectangle area)
        {
            var colliders = new List<ICollider>();
            foreach (var provider in _providers)
            {
                colliders.AddRange(provider.GetColliders(area));
            }
            return colliders;
        }

        public IReadOnlyList<ICollider> GetAllColliders()
        {
            var colliders = new List<ICollider>();
            foreach (var provider in _providers)
            {
                colliders.AddRange(provider.GetAllColliders());
            }
            return colliders;
        }

        public void RegisterCollisionProvider(ICollisionProvider provider)
        {
            _providers.Add(provider);
        }

        public void UnregisterCollisionProvider(ICollisionProvider provider)
        {
            _providers.Remove(provider);
        }
    }
}
