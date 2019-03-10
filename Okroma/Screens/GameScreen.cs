using Microsoft.Xna.Framework;

namespace Okroma.Screens
{
    abstract class GameScreen
    {
        /// <summary>
        /// Determines if input will fallthrough to screen below.
        /// </summary>
        public bool AllowInputPassthrough { get; private set; }

        public void Initialize(ScreenManager screenManager) { }
        public virtual void LoadContent() { }
        public virtual void UnloadContent() { }

        public virtual void HandleInput()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime)
        {

        }
    }
}
