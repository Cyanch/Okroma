using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Okroma.Screens
{
    public abstract class GameScreen
    {
        public bool Exited { get; private set; }
        protected Game Game { get; private set; }

        public void Initialize(Game game)
        {
            this.Game = game;
            Initialize();
        }

        protected virtual void Initialize()
        {

        }

        public virtual void LoadContent(ContentManager content)
        {

        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime gameTime, IGameScreenInfo info)
        {
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        public void Exit()
        {
            if (Exited)
                throw new InvalidOperationException(nameof(GameScreen) + " has already exited.");
            UnloadContent();
            Exited = true;
        }

        public event EventHandler OnRemovedFromScreenManager;
        public void NotifyRemovedFromScreenManager()
        {
            OnRemovedFromScreenManager?.Invoke(this, EventArgs.Empty);
        }
    }
}
