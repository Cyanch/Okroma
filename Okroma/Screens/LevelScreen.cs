using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Okroma.Cameras;
using Okroma.Common;
using Okroma.Common.MonoGame;
using Okroma.GameControls;
using Okroma.Input;
using Okroma.Physics;
using Okroma.World;

namespace Okroma.Screens
{
    public class LevelScreen : GameScreen
    {
        Player player;
        Camera camera;
        const float initialCameraZoom = 0.5f;
        
        World2D world;
        ContentReference<Level> levelReference;

        CollidableSourceCollection collidables;
        public LevelScreen(ContentReference<Level> levelReference)
        {
            this.levelReference = levelReference;
        }

        protected override void Initialize()
        {
            camera = new PlayerCamera(Game, null)
            {
                Zoom = initialCameraZoom
            };

            collidables = new CollidableSourceCollection();
        }

        public override void LoadContent()
        {
            var content = CreateContentManager();
            //Level / World
            var level = content.Load(levelReference);
            world = level.Create(new Range<float>(0, 1));
            collidables.AddSource(world);
            (camera as PlayerCamera)?.SetBoundaries(new Rectangle(0, 0, GameScale.FromTile(level.LevelSize.X).Pixels, GameScale.FromTile(level.LevelSize.Y).Pixels));

            //Player
            //Temp Texture.
            Point playerSize = new Point(64, 64);
            var playerTexture = CreateSingleColorTexture(Color.BurlyWood, playerSize.X, playerSize.Y);
            player = new Player("Player", new Sprite(playerTexture, null, new Vector2(playerSize.X / 2, playerSize.Y)), new Transform2D(level.PlayerSpawnLocation.X, level.PlayerSpawnLocation.Y), collidables);
            (camera as PlayerCamera)?.SetTargetPlayer(player);
            collidables.AddSingle(player);
        }

        //temp.
        private Texture2D CreateSingleColorTexture(Color color, int width, int height)
        {
            Texture2D texture = new Texture2D(Game.GraphicsDevice, width, height);
            Color[] colorMap = new Color[width * height];
            for (int i = 0; i < width * height; i++)
            {
                colorMap[i] = color;
            }
            texture.SetData(colorMap);
            return texture;
        }

        public override void Update(GameTime gameTime, IGameScreenInfo info)
        {
            IGameControlsService controls = Game.Services.GetService<IGameControlsService>();
            player.HandleInput(controls);
            player.Update(gameTime);
            camera.Update(gameTime);
            world.Update(gameTime);

            if (Game.Services.GetService<IInputManagerService>().WasPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                ExitScreen();
                Game.Services.GetService<IScreenManagerService>().AddScreen(new MenuScreen());
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;
            var renderArea = camera.ViewRectangle;
            renderArea.Inflate(GameScale.TileSize, GameScale.TileSize);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, camera.ViewMatrix);
            {
                player.Draw(gameTime, spriteBatch, world.LayerToDepth(levelReference.Content.PlayerLayer));
                world.Draw(gameTime, spriteBatch, renderArea);
            }
            spriteBatch.End();
#if DEBUG
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.ViewMatrix);
            {
                if (DebugSetting.ShowCameraBounds)
                {
                    Utils.C3.Primitives2D.DrawRectangle(spriteBatch, camera.ViewRectangle, DebugSetting.ShowCameraBounds.GetArg<Color>(0), DebugSetting.ShowCameraBounds.GetArg<float>(1));
                }
                if (DebugSetting.ShowRenderBounds)
                {
                    Utils.C3.Primitives2D.DrawRectangle(spriteBatch, renderArea, DebugSetting.ShowRenderBounds.GetArg<Color>(0), DebugSetting.ShowRenderBounds.GetArg<float>(1));
                }
            }
            spriteBatch.End();
#endif
        }
    }
}
