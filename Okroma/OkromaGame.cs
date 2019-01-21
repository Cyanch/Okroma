using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Okroma.Input;
using Okroma.Screens;
using System.Collections.Generic;
using System.IO;

namespace Okroma
{
    /// <summary>
    /// Settings that are applied when <see cref="DebugSetting.EnableDebugSettings"/> is true.
    /// </summary>
    public struct DebugSetting
    {
        public const bool EnableDebugSettings = true;

        static DebugSetting() 
        {
            SkipSplash = true;
            ShowCameraBounds = new DebugSetting(false, Color.Orange, 8f);
            ShowRenderBounds = new DebugSetting(false, Color.Yellow, 4f);
        }

        /// <summary>
        /// Skip Splash Screen at start of application.
        /// </summary>
        public static DebugSetting SkipSplash { get; private set; }
        /// <summary>
        /// Shows the camera boundaries.
        /// </summary>
        public static DebugSetting ShowCameraBounds { get; private set; }
        /// <summary>
        /// Shows the boundaries of the designated render area.
        /// </summary>
        public static DebugSetting ShowRenderBounds { get; private set; }

        public bool Enabled { get; private set; }
        public IReadOnlyList<object> Arguments { get; private set; }

        public DebugSetting(bool enabled) : this(enabled, null)
        {
        }

        public DebugSetting(bool enabled, params object[] args) : this()
        {
            this.Enabled = EnableDebugSettings && enabled;
            this.Arguments = args;
        }

        public T GetArg<T>(int index)
        {
            return (T)Arguments[index];
        }

        public static implicit operator bool(DebugSetting setting)
        {
            return setting.Enabled;
        }

        public static implicit operator DebugSetting(bool enabled)
        {
            return new DebugSetting(enabled);
        }
    }

    public class OkromaGame : Game
    {
        GraphicsDeviceManager graphics;

        public static readonly string Name = "Okroma";

        public OkromaGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Window.Title = "Okroma";
#if DEBUG
            Window.Title += " (Developmental build. Do not redistribute)";
#endif

            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();

            IsMouseVisible = true;
            Window.AllowUserResizing = true;

            var screenManager = new ScreenManager(this);
            Components.Add(screenManager);
            Services.AddService<IScreenManagerService>(screenManager);

            var mouseClickManager = new MouseManager(this);
            Components.Add(mouseClickManager);
            Services.AddService<IScreenInteractService>(mouseClickManager);

            var controlsManager = new GameControlsManager(this);
            Components.Add(controlsManager);
            Services.AddService<IGameControlsService>(controlsManager);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //TODO: use this.Content to load your game content here 
            var screenManager = Services.GetService<IScreenManagerService>();

#if DEBUG
            if (DebugSetting.SkipSplash)
            {
                LoadTestLevel();
            }
            else
            {
#endif
                //var splashScreen = new FadingSplashScreen("", TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
                //screenManager.AddScreen(splashScreen, Content);
                //splashScreen.OnRemovedFromScreenManager += (sender, args) => LoadTestScene();

                //there is no splashscreen yet.
                LoadTestLevel();
#if DEBUG
            }
#endif

            void LoadTestLevel()
            {
                screenManager.AddScreen(new LevelScreen(Path.Combine("Levels", "Test")), Content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.TransparentBlack);
            base.Draw(gameTime);
        }
    }
}
