using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Okroma.Cameras;
using Okroma.Physics;
using Okroma.World;

namespace Okroma.Screens
{
    public class LevelScreen : GameScreen
    {
        Player player;
        Camera camera;
        const float initialCameraZoom = 0.5f;

        const int chunkSize = 8;
        World2D world;
        ICollidableSource collidables;

        string levelPath;
        public LevelScreen(string levelPath)
        {
            this.levelPath = levelPath;
        }

        protected override void Initialize()
        {
            camera = new PlayerCamera(Game, null);
            camera.Zoom = initialCameraZoom;
                
            var collidableSourceCollection = new CollidableSourceCollection();
            collidables = collidableSourceCollection;
            collidableSourceCollection.AddSingle(player);
            collidableSourceCollection.AddSource(world);
        }

        public override void LoadContent(ContentManager content)
        {
            player = new Player("Player", null, Transform2D.None, collidables);
            (camera as PlayerCamera)?.SetTargetPlayer(player);
            var level = content.Load<Level>(levelPath);
        }

        public override void Update(GameTime gameTime, IGameScreenInfo info)
        {
            player.HandleInput(Game.Services.GetService<Input.IGameControlsService>());
            player.Update(gameTime);
            camera.Update(gameTime);
            world.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            {
                player.Draw(gameTime, spriteBatch, world.LayerToDepth((int)Layers.Main));
                world.Draw(gameTime, spriteBatch, camera.ViewRectangle);
            }
        }

        public enum Layers
        {
            Main = 0
        }
    }
}
