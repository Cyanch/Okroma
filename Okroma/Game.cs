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

        const string tileListPath = "TileList";

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //TODO: use this.Content to load your game content here

            var tileList = Content.Load<TileList>(tileListPath);
            Services.AddService(tileList);
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

            base.Draw(gameTime);
        }
    }
}
