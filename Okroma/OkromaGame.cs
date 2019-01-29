using Cyanch;
using Cyanch.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Okroma.GameControls;
using Okroma.Screens;
using System;
using System.Collections.Generic;

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
            Cyanch_UI_ShowGrid = new DebugSetting(true, Color.Red, Color.Orange);
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

        public static DebugSetting Cyanch_UI_ShowGrid { get; private set; }

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

        public FpsCounter FpsCounter { get; private set; }

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
            Window.ClientSizeChanged += Window_ClientSizeChanged;

            var screenManager = new ScreenManager(this);
            Components.Add(screenManager);
            Services.AddService<IScreenManagerService>(screenManager);

            var inputState = new InputState(this);
            Components.Add(inputState);
            Services.AddService<IInputService>(inputState);

            var controlsManager = new GameControlsManager(this);
            Components.Add(controlsManager);
            Services.AddService<IGameControlsService>(controlsManager);

            FpsCounter = new FpsCounter(this);
            Components.Add(FpsCounter);
            FpsCounter.Enabled = false;

            base.Initialize();
        }

        private void Window_ClientSizeChanged(object sender, System.EventArgs e)
        {
            System.Console.WriteLine(Window.ClientBounds);
        }

        protected override void LoadContent()
        {
            //TODO: use this.Content to load your game content here 
            var screenManager = Services.GetService<IScreenManagerService>();
            FpsCounter.Font = screenManager.Font;
#if DEBUG
            if (DebugSetting.SkipSplash)
            {
                LoadMenuScreen(screenManager);
            }
            else
            {
#endif
                //there is no splashscreen yet.
                LoadMenuScreen(screenManager);
#if DEBUG
            }
#endif
        }

        public void LoadMenuScreen(IScreenManagerService screenManager)
        {
            var screen = new BackgroundScreen("Background");
            screen.SetTransitionTime(TimeSpan.FromSeconds(0.5f), TimeSpan.FromSeconds(0.5f));
            screenManager.AddScreen(screen);
            screenManager.AddScreen(new Screens.Menus.MainMenuScreen());
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
