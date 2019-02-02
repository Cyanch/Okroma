using C3;
using Cyanch;
using Cyanch.Common;
using Cyanch.Entities;
using Cyanch.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Okroma.Cameras;
using Okroma.GameControls;
using Okroma.Physics;
using Okroma.Screens.Menus;
using Okroma.World;
using System;

namespace Okroma.Screens
{
    public class GameplayScreen : GameScreen
    {
        Player player;
        Camera camera;
        const float initialCameraZoom = 1f;
        
        World2D world;
        ContentReference<Level> levelReference;

        CollidableSourceCollection collidables;
        public GameplayScreen(ContentReference<Level> levelReference)
        {
            this.levelReference = levelReference;
        }

        protected override void Initialize()
        {
            this.TransitionInTime = TimeSpan.FromSeconds(0.5f);

            camera = new PlayerCamera(Game, null)
            {
                Zoom = initialCameraZoom
            };
            DarkenEffectWhenCovered = 0.5f;
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
            Point playerSize = new Point(GameScale.TileSize, GameScale.TileSize);
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
        
        public override void HandleInput()
        {
            player.HandleInput(Game.Services.GetService<IGameControlsService>());

            if (Game.Services.GetService<IInputService>().IsPressed(Keys.Escape))
            {
                Game.Services.GetService<IScreenManagerService>().AddScreen(new OptionsMenuScreen(true));
            }
        }

        protected override void Update(GameTime gameTime, IGameScreenInfo info)
        {
            player.Update(gameTime);
            camera.Update(gameTime);
            world.Update(gameTime);
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
                    Primitives2D.DrawRectangle(spriteBatch, camera.ViewRectangle, DebugSetting.ShowCameraBounds.GetArg<Color>(0), DebugSetting.ShowCameraBounds.GetArg<float>(1));
                }
                if (DebugSetting.ShowRenderBounds)
                {
                    Primitives2D.DrawRectangle(spriteBatch, renderArea, DebugSetting.ShowRenderBounds.GetArg<Color>(0), DebugSetting.ShowRenderBounds.GetArg<float>(1));
                }
            }
            spriteBatch.End();
#endif
        }
    }
}
