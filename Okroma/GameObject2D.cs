using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Okroma
{
    public interface IDrawableGameObject2D : IGameObject2D, IHasTransform2D
    {
        float RenderDepth { get; }
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }

    public interface IUpdateableGameObject2D : IGameObject2D
    {
        void Update(GameTime gameTime);
    }

    public interface IGameObject2D
    {
        string Name { get; }
        string Id { get; }
    }

    public class GameObject2D : IGameObject2D
    {
        public string Name => GetName();
        public string Id { get; }

        public GameObject2D(string id)
        {
            this.Id = id;
        }

        protected virtual string GetName()
        {
            return Id;
        }
    }
}
