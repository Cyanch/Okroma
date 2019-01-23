using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Okroma.Common;
using Okroma.Common.MonoGame;

namespace Okroma.Screens
{
    public class SplashScreen : GameScreen
    {
        private Invalidatable<Rectangle> bounds;
        private Invalidatable<Rectangle> Bounds
        {
            get
            {
                if (bounds.Value == null || !bounds.IsValid)
                    bounds = CalculateBounds();
                return bounds;
            }
        }

        private readonly ContentReference<Texture2D> imageReference;
        protected Texture2D Image => imageReference;

        ContentManager content;

        public SplashScreen(ContentReference<Texture2D> imageReference)
        {
            this.imageReference = imageReference;
        }

        protected override void Initialize()
        {
            Game.Window.ClientSizeChanged += Window_ClientSizeChanged;
        }

        public override void LoadContent()
        {
            this.content = CreateContentManager();

            content.Load(imageReference);
        }

        public override void UnloadContent()
        {
            Game.Window.ClientSizeChanged -= Window_ClientSizeChanged;
            content.Unload();
        }

        protected override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            DrawImage(gameTime, spriteBatch, Image, Bounds);
            spriteBatch.End();
        }

        protected virtual void DrawImage(GameTime gameTime, SpriteBatch spriteBatch, Texture2D image, Rectangle destRect)
        {
            spriteBatch.Draw(image, destRect, Color.White);
        }

        private void Window_ClientSizeChanged(object sender, System.EventArgs e)
        {
            bounds.Invalidate();
        }

        private Rectangle CalculateBounds()
        {
            var view = Game.GraphicsDevice.Viewport.TitleSafeArea;
            float scale = CalculateAspectScale();
            var size = new Point((int)(Image.Width * scale), (int)(Image.Height * scale));

            return new Rectangle(
                view.Center.X - size.X / 2,
                view.Center.Y - size.Y / 2,
                (int)(Image.Width * scale),
                (int)(Image.Height * scale)
                );
        }

        private float CalculateAspectScale()
        {
            var view = Game.GraphicsDevice.Viewport.TitleSafeArea;

            float scale = MathHelper.Min(
                   (float)view.Width / Image.Width,
                   (float)view.Height / Image.Height);

            return scale;
        }
    }
}
