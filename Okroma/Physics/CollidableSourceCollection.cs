using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Okroma.Physics
{
    public class CollidableSourceCollection : ICollidableSource
    {
        List<ICollidableSource> sources = new List<ICollidableSource>();
        List<ICollidableGameObject2D> collidables = new List<ICollidableGameObject2D>();

        public void AddSource(ICollidableSource source)
        {
            sources.Add(source);
        }

        public void RemoveSource(ICollidableSource source)
        {
            sources.Remove(source);
        }

        public void AddSingle(ICollidableGameObject2D collidable)
        {
            collidables.Add(collidable);
        }

        public void RemoveSingle(ICollidableGameObject2D collidable)
        {
            collidables.Remove(collidable);
        }

        public IEnumerable<ICollidableGameObject2D> GetCollidables(Rectangle rectangle)
        {
            foreach (var source in sources)
            {
                foreach (var collidableGameObject in source.GetCollidables(rectangle))
                {
                    yield return collidableGameObject;
                }
            }
            foreach (var collidableGameObject in collidables)
            {
                yield return collidableGameObject;
            }
        }
    }
}
