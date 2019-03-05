using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Okroma.Debug;
using Okroma.TileEngine;
using XnaGame = Microsoft.Xna.Framework.Game;

namespace Okroma
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game : XnaGame
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public const string Name = "Okroma";

        public const string TileListPath = "TileList";

        TileMap tMap;
        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            IsMouseVisible = true;
            Window.Title = Name;

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //TODO: use this.Content to load your game content here

            var tileList = Content.Load<TileList>(TileListPath);
            Services.AddService(tileList);

            int width = 1024;
            int height = 1024;
            tMap = new TileMap(width, height, 1);

            var task = System.Threading.Tasks.Task.Run(() =>
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        tMap[x, y, 0] = tileList[(x % 2) + 16 + (y % 2)];
                    }
                }
            });
        }

        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            // Exit() is obsolete on iOS
#if !__IOS__ && !__TVOS__
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
#endif
            // TODO: Add your update logic here			

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //TODO: Add your drawing code here

            spriteBatch.Begin();
            tMap.DrawTiles(gameTime, spriteBatch, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height));

            var colliders = tMap.GetColliders(new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height));
            foreach (var coll in colliders)
            {
                (coll as IDebuggable)?.DrawDebug(gameTime, spriteBatch, Color.White, DebugOption.DrawColliderBounds);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
