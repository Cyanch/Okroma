using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace Okroma.Screens
{
    public interface IScreenManagerService
    {
        void AddScreen(GameScreen screen);
        void RemoveScreen(GameScreen screen);
    }

    public class ScreenManager : DrawableGameComponent, IScreenManagerService
    {
        readonly List<GameScreen> screens = new List<GameScreen>();
        readonly List<GameScreen> screensToAdd = new List<GameScreen>();

        readonly string fontPath = Path.Combine("Fonts", "Cyfont-I");

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
            Font = content.Load<SpriteFont>(Path.Combine("Fonts", "Cyfont-I"));

            //Set White Pixel.
            WhitePixel = new Texture2D(GraphicsDevice, 1, 1);
            WhitePixel.SetData(new []{ Color.White });

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
            for (int i = screenCount - 1; i >= 0; i--)
            {
                var screen = screens[i];

                screen.Update(gameTime, new GameScreenInfo(i == screenCount));
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

            if (isInitialized)
            {
                screen.LoadContent();
            }

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
    }
}
