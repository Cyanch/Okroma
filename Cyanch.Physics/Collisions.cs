using C3;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Cyanch.Physics
{
    public interface ICollisionService
    {
        void SetMapSize(Rectangle rectangle);

        IEnumerable<ICollider> GetAllColliders();
        IEnumerable<ICollider> GetColliders(Rectangle area);

        void RegisterCollider(ICollider collider);
        void UnregisterCollider(ICollider colider);
    }
    
    public class Collisions : ICollisionService
    {
        QuadTree<ICollider> colliders;

        public void SetMapSize(Rectangle mapSize)
        {
            colliders = new QuadTree<ICollider>(mapSize);
        }
        
        public IList<ICollider> GetColliders(Rectangle area)
        {
            return colliders.GetObjects(area);
        }

        public IList<ICollider> GetAllColliders()
        {
            return colliders.GetAllObjects();
        }

        IEnumerable<ICollider> ICollisionService.GetColliders(Rectangle area)
        {
            return GetColliders(area);
        }

        IEnumerable<ICollider> ICollisionService.GetAllColliders()
        {
            return GetAllColliders();
        }

        public void RegisterCollider(ICollider collider)
        {
            colliders.Add(collider);
            collider.Moved += Collider_Moved;
        }

        public void UnregisterCollider(ICollider collider)
        {
            colliders.Remove(collider);
            collider.Moved -= Collider_Moved;
        }

        private void Collider_Moved(object sender, System.EventArgs e)
        {
            if (sender is ICollider collider)
            {
                colliders.Move(collider);
            }
        }
    }
}
