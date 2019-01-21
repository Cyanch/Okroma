using Microsoft.Xna.Framework;
using System;

namespace Okroma.Cameras
{
    public class PlayerCamera : Camera
    {
        public Player TargetPlayer { get; protected set; }
        public Rectangle Boundaries { get; protected set; }
        public PlayerCamera(Game game, Player player) : base(game)
        {
            this.TargetPlayer = player;
        }

        Vector2 currentCameraPos;
        public override void Update(GameTime gameTime)
        {
            Vector2 focus = GetPlayerPosition();
            //// TODO: make this less hardcoded, more dynamic, and more flexible.
            Vector2 target = new Vector2(
                MathHelper.Clamp(
                    focus.X - Game.GraphicsDevice.Viewport.TitleSafeArea.Center.X / Zoom,
                    Boundaries.Left,
                    Boundaries.Right - Game.GraphicsDevice.Viewport.TitleSafeArea.Width / Zoom),
                MathHelper.Clamp(
                    focus.Y - Game.GraphicsDevice.Viewport.TitleSafeArea.Center.Y / Zoom,
                    Boundaries.Top,
                    Boundaries.Bottom - Game.GraphicsDevice.Viewport.TitleSafeArea.Height / Zoom));

            currentCameraPos.X = SmoothTransition(currentCameraPos.X, target.X, 0.2f);
            currentCameraPos.Y = target.Y;

            ViewMatrix = Matrix.CreateTranslation(-new Vector3(currentCameraPos, 0)) * Matrix.CreateScale(Zoom);
            ViewRectangle = new Rectangle(target.ToPoint(), new Vector2(Game.GraphicsDevice.Viewport.TitleSafeArea.Width / Zoom, Game.GraphicsDevice.Viewport.TitleSafeArea.Height / Zoom).ToPoint());
        }

        float SmoothTransition(float current, float target, float smoothing)
        {
            var rem = target - current;
            if (target == 0 && Math.Abs(current) < 0.05f)
            {
                return 0;
            }
            return current + (rem * smoothing);
        }

        float Ease(float x, float a)
        {
            return (float)Math.Pow(x, a) / ((float)Math.Pow(x, a) + (float)Math.Pow(1 - x, a));
        }

        public void SetTargetPlayer(Player player)
        {
            this.TargetPlayer = player;
        }

        public void SetBoundaries(Rectangle boundaries)
        {
            this.Boundaries = boundaries;
        }

        Vector2 GetPlayerPosition()
        {
            return new Vector2(TargetPlayer.Bounds.Center.X, TargetPlayer.Bounds.Bottom);  //Bottom Center of player.
        }
    }
}
