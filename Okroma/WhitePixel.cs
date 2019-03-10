using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Okroma
{
    public static class WhitePixel
    {
        private static IWhitePixelLoadState _state = new NotLoadedWhitePixel();
        public static Texture2D Get(GraphicsDevice graphicsDevice)
        {
            return _state.Get(ref _state, graphicsDevice);
        }
        
        private interface IWhitePixelLoadState
        {
            Texture2D Get(ref IWhitePixelLoadState state, GraphicsDevice graphicsDevice);
        }

        private class NotLoadedWhitePixel : IWhitePixelLoadState
        {
            public Texture2D Get(ref IWhitePixelLoadState state, GraphicsDevice graphicsDevice)
            {
                var whitePixel = new Texture2D(graphicsDevice, 1, 1);
                whitePixel.SetData(new Color[] { Color.White });
                state = new LoadedWhitePixelState(whitePixel);
                return whitePixel;
            }
        }

        private class LoadedWhitePixelState : IWhitePixelLoadState
        {
            private readonly Texture2D _whitePixel;
            public LoadedWhitePixelState(Texture2D whitePixel)
            {
                this._whitePixel = whitePixel;
            }

            public Texture2D Get(ref IWhitePixelLoadState state, GraphicsDevice graphicsDevice)
            {
                return _whitePixel;
            }
        }
    }
}
