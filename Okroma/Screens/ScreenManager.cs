using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace Okroma.Screens
{
    public interface IScreenManagerService
    {
        SpriteBatch SpriteBatch { get; }
        SpriteFont Font { get; }
        Texture2D WhitePixel { get; }
        void AddScreen(GameScreen screen);
        void AddPreloadedScreen(GameScreen screen);
        void RemoveScreen(GameScreen screen);
        IReadOnlyList<GameScreen> GetScreens();
    }

    public class ScreenManager : DrawableGameComponent, IScreenManagerService
    {
        readonly List<GameScreen> screens = new List<GameScreen>();
        readonly List<GameScreen> screensToAdd = new List<GameScreen>();

        readonly string fontPath = Path.Combine("Fonts", "CyanchFont48");

        /// <summary>
        /// General-use SpriteBatch that all screens may use.
        /// </summary>
        public SpriteBatch SpriteBatch { get; private set; }
        /// <summary>
        /// Main font.
        /// </summary>
        public SpriteFont Font { get; private set; }
        /// <summary>
        /// A White Pixel Texture.
        /// </summary>
        public Texture2D WhitePixel { get; private set; }

        bool isInitialized;
        public ScreenManager(Game game) : base(game)
        {

        }

        public override void Initialize()
        {
            base.Initialize();

            isInitialized = true;
        }

        protected override void LoadContent()
        {
            //Set Content.
            var content = Game.Content;

            //Set SpriteBatch.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            //Set Font.
            Font = content.Load<SpriteFont>(fontPath);

            //Set White Pixel.
            WhitePixel = new Texture2D(GraphicsDevice, 1, 1);
            WhitePixel.SetData(new[] { Color.White });

            foreach (var screen in screens)
            {
                screen.LoadContent();
            }
        }

        protected override void UnloadContent()
        {
            foreach (var screen in screens)
            {
                screen.UnloadContent();
            }
        }

        public override void Update(GameTime gameTime)
        {
            screens.AddRange(screensToAdd);
            screensToAdd.Clear();

            var screenCount = screens.Count;
            bool isCovered = false;
            for (int i = screenCount - 1; i >= 0; i--)
            {
                if (screens.Count <= i)
                    continue;

                var screen = screens[i];
                bool isFocused = i == screenCount - 1;
                if (isFocused && Game.IsActive)
                    screen.HandleInput();
                screen.UpdateScreen(gameTime, new GameScreenInfo(isFocused, isCovered));
                isCovered |= !screen.IsPopup;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var screen in screens)
            {
                screen.DrawScreen(gameTime);
            }
        }

        public void AddScreen(GameScreen screen)
        {
            screen.Initialize(this);
            screensToAdd.Add(screen);

            if (isInitialized)
            {
                screen.LoadContent();
            }
        }

        public void AddPreloadedScreen(GameScreen screen)
        {
            screen.Initialize(this);
            screensToAdd.Add(screen);
        }

        public void RemoveScreen(GameScreen screen)
        {
            if (isInitialized)
            {
                screen.UnloadContent();
            }

            screens.Remove(screen);
            screensToAdd.Remove(screen);
        }

        public IReadOnlyList<GameScreen> GetScreens()
        {
            return new List<GameScreen>(screens);
        }

        /// <summary>
        /// Draws a black sprite across the entire screen.
        /// </summary>
        /// <param name="alpha">0-1 transparency</param>
        public void FadeBackBufferToBlack(float alpha)
        {
            var viewport = GraphicsDevice.Viewport;
            SpriteBatch.Begin();
            SpriteBatch.Draw(WhitePixel, GraphicsDevice.Viewport.Bounds, Color.Black * alpha);
            SpriteBatch.End();
        }
    }
}
