using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Okroma.Cameras;
using Okroma.Common;
using Okroma.Input;
using Okroma.Physics;
using Okroma.World;
using Okroma.World.Tiles;
using System;
using System.Collections.Generic;

namespace Okroma.Screens
{
    public class TestScreen : GameScreen
    {
        World2D world;
        Player player;
        PlayerCamera playerCamera;
        CollidableSourceCollection collidableSources;
        public TestScreen()
        {
        }
        
        /// <summary>
        /// <see cref="Chunk2D"/> size in tiles.
        /// </summary>
        const int chunkSize = 8;
        
        protected override void Initialize()
        {
            playerCamera = new PlayerCamera(Game, player);
            playerCamera.Zoom = 0.5f;

            collidableSources = new CollidableSourceCollection();
        }

        public override void LoadContent(ContentManager content)
        {
            Texture2D mapTexture = content.Load<Texture2D>("map");
            Color[] colorArray = new Color[mapTexture.Width * mapTexture.Height];
            mapTexture.GetData(colorArray);

            world = new World2D(GameScale.TileSize, (byte)Enum.GetValues(typeof(Layer)).Length, new Range<float>(0, 1), chunkSize, mapTexture.Width / chunkSize, mapTexture.Height / chunkSize);
            collidableSources.AddSource(world);
            playerCamera.SetBoundaries(new Rectangle(0, 0, GameScale.FromTile(mapTexture.Width).Pixels, GameScale.FromTile(mapTexture.Height).Pixels));

            int playerWidth = 64;
            int playerHeight = 64;
            player = new Player("Player", new Sprite(CreateSingleColorTexture(Color.BurlyWood, playerWidth, playerHeight)), new Transform2D(64, 64), collidableSources, playerWidth, playerHeight);
            collidableSources.AddSingle(player);
            playerCamera.SetTargetPlayer(player);

            var grayTile = content.Load<ITile>("GrayTile");
            var coatedGrayTile = content.Load<ITile>("CoatedGrayTile");

            for (int mY = 0; mY < mapTexture.Height; mY++)
            {
                for (int mX = 0; mX < mapTexture.Width; mX++)
                {
                    Color color = colorArray[mX + mY * mapTexture.Width];
                    if (color == Color.Gray)
                    {
                        world.PlaceTile(mX, mY, (int)Layer.Forward, grayTile);
                    }
                    else if (color == new Color(64, 64, 64))
                    {
                        world.PlaceTile(mX, mY, (int)Layer.Forward, coatedGrayTile);
                    }
                }
            }
        }

        /// <summary>
        /// Layers, Lower# = Above.
        /// </summary>
        public enum Layer : byte
        {
            Forward = 0,
            Back = 1,
        }

        public Texture2D CreateSingleColorTexture(Color color, int width, int height)
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
            player.HandleInput(Game.Services.GetService<IGameControlsService>());
            player.Update(gameTime);
            playerCamera.Update(gameTime);
            world.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Level
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, playerCamera.ViewMatrix);
            {
                world.Draw(gameTime, spriteBatch, playerCamera.ViewRectangle);
                player.Draw(gameTime, spriteBatch, world.LayerToDepth((int)Layer.Back));
            }
            spriteBatch.End();
        }

        public class CollidableSourceCollection : ICollidableSource
        {
            List<ICollidableSource> sources = new List<ICollidableSource>();
            List<ICollidableGameObject2D> collidables = new List<ICollidableGameObject2D>();

            public void AddSource(ICollidableSource source)
            {
                sources.Add(source);
            }

            public void RemoveSource(ICollidableSource source)
            {
                sources.Remove(source);
            }

            public void AddSingle(ICollidableGameObject2D collidable)
            {
                collidables.Add(collidable);
            }

            public void RemoveSingle(ICollidableGameObject2D collidable)
            {
                collidables.Remove(collidable);
            }

            public IEnumerable<ICollidableGameObject2D> GetCollidables(Rectangle rectangle)
            {
                foreach (var source in sources)
                {
                    foreach (var collidableGameObject in source.GetCollidables(rectangle))
                    {
                        yield return collidableGameObject;
                    }
                }
                foreach (var collidableGameObject in collidables)
                {
                    yield return collidableGameObject;
                }
            }
        }
    }
}
