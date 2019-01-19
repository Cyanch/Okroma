using Microsoft.Xna.Framework;
using System;

namespace Okroma.Cameras
{
    public interface ICamera
    {
        Matrix ViewMatrix { get; }
        Rectangle ViewRectangle { get; }
    }

    public class Camera : GameComponent, ICamera
    {
        public Matrix ViewMatrix { get; protected set; }
        public Rectangle ViewRectangle { get; protected set; }

        public float Zoom { get; set; } = 1f;

        public Camera(Game game) : base(game)
        {
        }
    }
}
