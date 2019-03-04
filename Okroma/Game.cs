using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        const string tileListPath = "TileList";

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

            var tileList = Content.Load<TileList>(tileListPath);
            Services.AddService(tileList);

            tMap = new TileMap(4, 4, tileList[0]);
            tMap[0, 0, 0] = tileList[1];
            tMap[1, 1, 0] = tileList[1];
            tMap[2, 2, 0] = tileList[1];
            tMap[3, 3, 0] = tileList[1];


            tMap[3, 0, 0] = tileList[2];
            tMap[2, 1, 0] = tileList[2];
            tMap[1, 2, 0] = tileList[2];
            tMap[0, 3, 0] = tileList[2];
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
            tMap.DrawLayers(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
