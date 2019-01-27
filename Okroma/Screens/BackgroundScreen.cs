using Cyanch.Common.MonoGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Okroma.Screens
{
    public class BackgroundScreen : GameScreen
    {
        ContentReference<Texture2D> image;
        public Texture2D Image { get => image; }

        ContentManager content;
        public BackgroundScreen(ContentReference<Texture2D> imageReference)
        {
            this.image = imageReference;
        }

        public override void LoadContent()
        {
            content = CreateContentManager();
            image.Load(content);
        }

        public override void UnloadContent()
        {
            content.Unload();
        }

        protected override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();
            spriteBatch.Draw(image, GetDestinationRectangle(), Color.White);
            spriteBatch.End();
        }

        protected virtual Rectangle GetDestinationRectangle()
        {
            return ScreenManager.GraphicsDevice.Viewport.Bounds;
        }

        public void SetTransitionTime(TimeSpan timeIn, TimeSpan timeOut)
        {
            this.TransitionInTime = timeIn;
            this.TransitionOutTime = timeOut;
        }
    }
}
