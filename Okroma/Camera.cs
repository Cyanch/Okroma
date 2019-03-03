using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Okroma
{
    //All untested!
    // todo: test.

    class Camera
    {
        public float Zoom { get; set; }
        public Vector2 Position { get; set; }

        public Rectangle VisibleArea { get; protected set; }
        public Matrix Transform { get; protected set; }

        public Rectangle Bounds { get; protected set; }

        public Camera(Viewport viewport)
        {
            this.Zoom = 1f;
            this.Bounds = viewport.TitleSafeArea;
        }
        
        public void Update(Viewport viewport)
        {
            this.Bounds = viewport.TitleSafeArea;
        }
    }

    /// <summary>
    /// A <see cref="Camera"/> that follows an object.
    /// </summary>
    class FollowCamera : Camera
    {
        private float _smoothing;
        public float Smoothing
        {
            get { return _smoothing; }
            set
            {
                _smoothing = MathHelper.Clamp(value, 0, 1);
            }
        }
        
        public FollowCamera(Viewport viewport) : base(viewport)
        {
        }

        private Vector2 SmoothMovement(Vector2 targetPosition)
        {
            Vector2 distance = targetPosition - this.Position;
            return this.Position + (distance * (1 - Smoothing));
        }
    }
}
