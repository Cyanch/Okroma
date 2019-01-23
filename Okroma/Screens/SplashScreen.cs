using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Okroma.Common;
using Okroma.Common.MonoGame;

namespace Okroma.Screens
{
    public class SplashScreen : BackgroundScreen
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

        public SplashScreen(ContentReference<Texture2D> imageReference) : base(imageReference)
        {
        }

        protected override void Initialize()
        {
            Game.Window.ClientSizeChanged += Window_ClientSizeChanged;
        }

        public override void UnloadContent()
        {
            Game.Window.ClientSizeChanged -= Window_ClientSizeChanged;
        }

        protected override Rectangle GetDestinationRectangle()
        {
            return Bounds;
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
