using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Okroma.Config;
using Okroma.Input;
using System;
using System.Collections.Generic;
using XnaGame = Microsoft.Xna.Framework.Game;

namespace Okroma.Screens
{
    interface ISharedObjectsService
    {
        SpriteBatch SpriteBatch { get; }
        SpriteFont Font { get; }
    }

    interface IScreenManagerService
    {
        void AddScreen(GameScreen screen);
        void RemoveScreen(GameScreen screen);
        GameScreen[] GetScreens();
    }

    class ScreenManager : DrawableGameComponent, IScreenManagerService, ISharedObjectsService
    {
        private readonly List<GameScreen> _screens = new List<GameScreen>();
        private IScreenManagerState _state;
        private InputManager _input;

        public SpriteBatch SpriteBatch { get; private set; }
        public SpriteFont Font { get; private set; }

        public event EventHandler<GameScreen> ScreenChanged;

        public ScreenManager(XnaGame game) : base(game)
        {
            _state = new UninitializedScreenManagerState(_screens);
        }

        public override void Initialize()
        {
            base.Initialize();

            this._state = new InitializedScreenManagerState(_screens);
        }

        public override void Update(GameTime gameTime)
        {
            var screens = GetScreens();
            bool inputBlocked = false;
            _input.Update();
            for (int i = screens.Length; i > 0; i--)
            {
                var screen = screens[i - 1];

                if (Game.IsActive && !inputBlocked)
                {
                    screen.HandleInput(_input);
                    if (!screen.AllowInputPassthrough)
                        inputBlocked = true;

                    screen.Update(gameTime);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var screen in _screens)
            {
                screen.Draw(gameTime);
            }
        }

        protected override void LoadContent()
        {
            var content = Game.Content;

            // Set ISharedObjectsService Objects.
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Font = content.Load<SpriteFont>(Game.Services.GetService<IConfigurationService>().GetPath(ConfigurationPaths.DefaultFont.ToString()));
            
            foreach (var screen in _screens)
            {
                screen.LoadContent();
            }
        }

        protected override void UnloadContent()
        {
            foreach (var screen in _screens)
            {
                screen.UnloadContent();
            }
        }

        public void AddScreen(GameScreen screen)
        {
            screen.Initialize(this);
            _state.AddScreen(screen);
            OnScreenChanged(screen);
            _screens.Add(screen);
        }

        public void RemoveScreen(GameScreen screen)
        {
            _state.RemoveScreen(screen);
            OnScreenChanged(screen);
            _screens.Remove(screen);
        }
        
        /// <summary>
        /// Returns a copy of rge <see cref="GameScreen"/> collection as an array/>.
        /// </summary>
        /// <returns></returns>
        public GameScreen[] GetScreens()
        {
            return _screens.ToArray();
        }

        /// <summary>
        /// Draws a black sprite across the entire screen.
        /// </summary>
        /// <param name="alpha">0-1 transparency</param>
        public void FadeBackBufferToBlack(float alpha)
        {
            var viewport = GraphicsDevice.Viewport;
            SpriteBatch.Begin();
            SpriteBatch.Draw(WhitePixel.Get(GraphicsDevice), GraphicsDevice.Viewport.Bounds, Color.Black * alpha);
            SpriteBatch.End();
        }

        protected virtual void OnScreenChanged(GameScreen changedScreen)
        {
            ScreenChanged?.Invoke(this, changedScreen);
        }
        
        private interface IScreenManagerState
        {
            void AddScreen(GameScreen screen);
            void RemoveScreen(GameScreen screen);
        }

        private class InitializedScreenManagerState : IScreenManagerState
        {
            private List<GameScreen> _screens;

            public InitializedScreenManagerState(List<GameScreen> screens)
            {
                _screens = screens ?? throw new ArgumentNullException(nameof(screens));
            }

            public void AddScreen(GameScreen screen) => screen.LoadContent();

            public void RemoveScreen(GameScreen screen) => screen.UnloadContent();

            public void LoadContent(ref IScreenManagerState state) { }
        }

        private class UninitializedScreenManagerState : IScreenManagerState
        {
            private List<GameScreen> _screens;

            public UninitializedScreenManagerState(List<GameScreen> screens)
            {
                _screens = screens ?? throw new ArgumentNullException(nameof(screens));
            }

            public void AddScreen(GameScreen screen) { }

            public void RemoveScreen(GameScreen screen) { }
        }
    }
}
