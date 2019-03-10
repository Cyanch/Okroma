using Microsoft.Xna.Framework;
using Okroma.Input;

namespace Okroma.Screens
{
    abstract class GameScreen
    {
        public enum ScreenState
        {
            TransitionIn,
            Active,
            TransitionOff,
            Inactive
        }
        
        /// <summary>
        /// Determines if input will fallthrough to screen below.
        /// </summary>
        public bool AllowInputPassthrough { get; private set; }
        
        public ScreenState State { get; private set; }

        public void Initialize(ScreenManager screenManager) { }
        public virtual void LoadContent() { }
        public virtual void UnloadContent() { }

        public virtual void HandleInput(InputManager input)
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
