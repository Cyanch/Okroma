using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Okroma.Sprites;
using System;
using XnaGame = Microsoft.Xna.Framework.Game;

namespace Okroma.TileEngine
{
    struct Tile : IEquatable<Tile>
    {
        public int Id { get; }
        public Sprite Sprite { get; }

        public const int Width = 32;
        public const int Height = 32;
        public static readonly Vector2 Size = new Vector2(Width, Height);

        public Tile(int id, Sprite sprite) : this()
        {
            Sprite = sprite ?? throw new ArgumentNullException(nameof(sprite));
        }

        public override bool Equals(object obj)
        {
            return obj is Tile && Equals((Tile)obj);
        }

        public bool Equals(Tile other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }

    interface ITileService
    {
        Tile GetTile(int tileId);
    }

    class TileService : GameComponent, ITileService
    {
        Tile[] tiles;

        ContentManager content;
        public TileService(XnaGame game) : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            
            content = new ContentManager(Game.Services, Game.Content.RootDirectory);
        }

        public Tile GetTile(int tileId)
        {
            return tiles[tileId];
        }
    }
}
