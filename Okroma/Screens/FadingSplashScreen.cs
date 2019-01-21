using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Okroma.Common.MonoGame;
using System;

namespace Okroma.Screens
{
    public class FadingSplashScreen : SplashScreen
    {
        private TimeSpan fadeDuration;
        private TimeSpan normalDuration;

        private float alpha;
        private float elapsedTime;

        private readonly float a, b, c;

        public FadingSplashScreen(ContentReference<Texture2D> imageReference, TimeSpan normalDuration, TimeSpan fadeDuration) : base(imageReference)
        {
            this.normalDuration = normalDuration;
            this.fadeDuration = fadeDuration;

            a = (float)fadeDuration.TotalSeconds;
            b = a + (float)normalDuration.TotalSeconds;
            c = b + a;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            alpha = elapsedTime <= a ? elapsedTime / a :
                elapsedTime <= b ? 1 :
                1 - ((elapsedTime - b) / (c - b));

            if (elapsedTime >= c)
                Exit();

            base.Draw(gameTime, spriteBatch);
        }

        protected override void DrawImage(GameTime gameTime, SpriteBatch spriteBatch, Texture2D image, Rectangle destRect)
        {
            spriteBatch.Draw(image, destRect, Color.White * alpha);
        }
    }
}
