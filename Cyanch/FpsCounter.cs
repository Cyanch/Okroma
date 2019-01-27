using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Cyanch
{
    public interface IFpsCounterService
    {
        float Fps { get; }
    }

    public class FpsCounter : DrawableGameComponent, IFpsCounterService
    {
        public float Fps { get; private set; }
        public int DecimalPrecision { get; set; }

        public SpriteFont Font { get; set; }
        public Vector2 Position { get; set; }
        SpriteBatch spriteBatch;
        public FpsCounter(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {
            Fps = 1f / (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void Draw(GameTime gameTime)
        {
            if (Enabled)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(Font, string.Format("FPS: {0}", Math.Round(Fps, DecimalPrecision)), Position, Color.White);
                spriteBatch.End();
            }
        }
    }
}
