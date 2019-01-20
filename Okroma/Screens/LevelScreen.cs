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

        const int chunkSize = 8;
        World2D world;
        ICollidableSource collidables;

        protected override void Initialize()
        {
            var collidableSourceCollection = new CollidableSourceCollection();
            collidables = collidableSourceCollection;
            collidableSourceCollection.AddSingle(player);
            collidableSourceCollection.AddSource(world);
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            player = new Player("Player", null, Transform2D.None, collidables);
        }

        public override void Update(GameTime gameTime, IGameScreenInfo info)
        {
            base.Update(gameTime, info);
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
