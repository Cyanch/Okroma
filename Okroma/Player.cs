using Cyanch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Okroma.GameControls;
using Okroma.Physics;
using System;

namespace Okroma
{
    public class Player : Controller2D, IUpdateableGameObject2D, IDrawableGameObject2D, IHandleGameControlInput
    {
        //Physics
        public override int CollisionMask => (int)Okroma.CollisionMask.Player;
        //Movement
        Vector2 velocity;
        float speed = GameScale.FromTile(10).Pixels;
        //Jumping/Gravity
        float jumpHeight = GameScale.FromTile(3.2f).Pixels;
        float timeToJumpApex = .3f;
        float gravity, jumpVelocity;

        //Rendering
        public float RenderDepth { get; set; }
        public ISprite Sprite { get; }

        //Width & Height
        protected override int Width => Sprite.TextureWidth;
        protected override int Height => Sprite.TextureHeight;

        //Accelerations
        const float accelerationInAir = 0.2f;
        const float accelerationOnGround = 0.2f;

        //Wall Jumping
        const float wallJumpTimeLimit = .1f;
        WallJumpController wallJumpController;
        const float accelerationOnWall = 0.2f; // Acceleration toward 0. So deacceleration really.

        //Input
        PlayerControls control;
        public Player(string id, ISprite sprite, ITransform2D transform, ICollidableSource collidableSource) : base(id, transform, collidableSource)
        {
            this.Sprite = sprite;

            gravity = (2 * jumpHeight) / (float)Math.Pow(timeToJumpApex, 2);
            jumpVelocity = -Math.Abs(gravity) * timeToJumpApex;

            wallJumpController = new WallJumpController(this, TimeSpan.FromSeconds(wallJumpTimeLimit), 1.25f, 0.75f);
        }

        public void HandleInput(IGameControlsService controls)
        {
            control.Reset();
            control.MoveLeft = controls.Get(GameControl.MoveLeft).HasFlag(ControlProperty.Held);
            control.MoveRight = controls.Get(GameControl.MoveRight).HasFlag(ControlProperty.Held);
            control.MoveUp = controls.Get(GameControl.MoveUp).HasFlag(ControlProperty.Held);
            ControlProperty jumpControl = controls.Get(GameControl.Jump);
            control.Jump = jumpControl.HasFlag(ControlProperty.Held);
            control.WallJump = jumpControl.HasFlag(ControlProperty.JustPressed);
        }

        public void Update(GameTime gameTime)
        {
            bool isGrounded = Collision.Below;

            int inputX = 0;

            if (control.MoveLeft)
                inputX -= 1;
            if (control.MoveRight)
                inputX += 1;

            if (Collision.Below || Collision.Above)
            {
                velocity.Y = 0;
            }
            if (Collision.Left || Collision.Right)
            {
                velocity.X = 0;
            }

            wallJumpController.Update(gameTime);

            if (control.Jump)
            {
                if (isGrounded)
                {
                    velocity.Y = jumpVelocity;
                }
                else if (control.WallJump && wallJumpController.IsOnWall)
                {
                    wallJumpController.PerformWallJump(ref velocity);
                }
            }

            float targetVelocityX = inputX * speed;
            velocity.X = SmoothMovement(
                velocity.X,
                targetVelocityX,
                isGrounded ? accelerationOnGround : accelerationInAir);

            if (wallJumpController.IsOnWall)
            {
                velocity.Y = SmoothMovement(velocity.Y, 0, accelerationOnWall);
            }
            else
            {
                velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            Move(velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            Console.WriteLine(Transform.Position);
            Bounds = new Rectangle((int)(Transform.Position.X - Width / 2), (int)(Transform.Position.Y - Height), Sprite.TextureWidth, Sprite.TextureHeight);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, float depth)
        {
            this.RenderDepth = depth;
            Sprite.DrawAt(gameTime, spriteBatch, Transform, Color.White, SpriteEffects.None, depth);
        }

        void IDrawableGameObject2D.Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Draw(gameTime, spriteBatch, RenderDepth);
        }

        private struct PlayerControls
        {
            public bool MoveLeft { get; set; }
            public bool MoveRight { get; set; }
            public bool MoveUp { get; set; }
            public bool Jump { get; set; }
            public bool WallJump { get; set; }

            public void Reset()
            {
                MoveLeft = MoveRight = MoveUp = Jump = WallJump = false;
            }
        }

        private class WallJumpController
        {
            public bool IsOnWall { get; private set; }
            private TimeSpan wallJumpRemainingTime;
            public TimeSpan WallJumpMaxTimeLimit { get; }
            public WallDirection Direction { get; private set; }

            public float VelocityXModifier { get; }
            public float VelocityYModifier { get; }

            Player player;
            public WallJumpController(Player player, TimeSpan wallJumpMaxTimeLimit, float velocityXModifier, float velocityYModifier)
            {
                this.player = player;
                this.WallJumpMaxTimeLimit = wallJumpMaxTimeLimit;

                this.VelocityXModifier = velocityXModifier;
                this.VelocityYModifier = velocityYModifier;
            }

            public void Update(GameTime gameTime)
            {
                if (player.Collision.Below || player.Collision.Above)
                {
                    IsOnWall = false;
                }

                if (player.Collision.Left && player.Collision.IsWallJumpable)
                {
                    Direction = WallDirection.Left;
                    EnableWallJump();
                }
                else if (player.Collision.Right && player.Collision.IsWallJumpable)
                {
                    Direction = WallDirection.Right;
                    EnableWallJump();
                }
                else if (wallJumpRemainingTime.Milliseconds > 0)
                {
                    wallJumpRemainingTime -= gameTime.ElapsedGameTime;
                }
                else
                {
                    Reset();
                }
            }

            public void PerformWallJump(ref Vector2 velocity)
            {
                velocity.X = player.jumpVelocity * VelocityXModifier * ((int)Direction - 1);
                if (player.control.MoveUp)
                {
                    velocity.Y = player.jumpVelocity * VelocityYModifier;
                }
                Reset();
            }

            private void EnableWallJump()
            {
                wallJumpRemainingTime = WallJumpMaxTimeLimit;
                IsOnWall = true;
            }

            private void Reset()
            {
                Direction = WallDirection.None;
                wallJumpRemainingTime = TimeSpan.Zero;
                IsOnWall = false;
            }

            public enum WallDirection
            {
                Left = 0,
                None = 1,
                Right = 2
            }
        }
    }
}
