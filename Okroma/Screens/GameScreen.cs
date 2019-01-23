using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace Okroma.Screens
{
    public abstract class GameScreen
    {
        public bool IsExiting { get; private set; }

        protected Game Game { get; private set; }
        protected ScreenManager ScreenManager { get; private set; }

        public bool IsPopup { get; protected set; }

        public void Initialize(ScreenManager screenManager)
        {
            this.Game = screenManager.Game;
            this.ScreenManager = screenManager;

            IsExiting = false;

            Initialize();
        }

        protected virtual void Initialize()
        {
        }

        public virtual void LoadContent()
        {

        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime gameTime, IGameScreenInfo info)
        {
        }

        public void DrawScreen(GameTime gameTime)
        {
            Draw(gameTime);
            // [Transition]
        }

        protected virtual void Draw(GameTime gameTime)
        {
        }

        protected ContentManager CreateContentManager()
        {
            return new ContentManager(Game.Services, Game.Content.RootDirectory);
        }

        public void ExitScreen()
        {
            IsExiting = true;
            ScreenManager.RemoveScreen(this);
        }
    }
}
