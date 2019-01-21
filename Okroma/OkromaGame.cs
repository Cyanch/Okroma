using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Okroma.Input;
using Okroma.Screens;
using System;

namespace Okroma
{
    /// <summary>
    /// Settings that are applied when OkromaGame.IsDebugBuild is true.
    /// </summary>
    public static class DebugSettings
    {
        public static readonly bool DisableDebugOptions = false;

        // Group - Bypasses.
       /// <summary>
       /// Skips Splash Screen.
       /// </summary>
        public static readonly bool SkipSplash = !DisableDebugOptions && true;
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
            if (DebugSettings.SkipSplash)
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
                screenManager.AddScreen(new LevelScreen("Levels\\Test"), Content);
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
