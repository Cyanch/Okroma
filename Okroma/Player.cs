using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Okroma.Input;
using Okroma.Physics;
using System;

namespace Okroma
{
    public class Player : Controller2D, IUpdateableGameObject2D, IDrawableGameObject2D, IHandleInput
    {
        public ISprite Sprite { get; }
        Vector2 velocity;

        float jumpHeight = GameScale.FromTile(3.2f).Pixels;
        float timeToJumpApex = .3f;

        float gravity;
        float jumpVelocity;

        float speed = GameScale.FromTile(10).Pixels;

        const float accelerationInAir = 0.2f;
        const float accelerationOnGround = 0.2f;

        const float wallJumpTimeLimit = .1f;
        WallJumpController wallJumpController;
        public Player(string id, ISprite sprite, ITransform2D transform, ICollidableSource collidableSource, int width, int height) : base(id, transform, collidableSource, width, height)
        {
            this.Sprite = sprite;

            gravity = (2 * jumpHeight) / (float)Math.Pow(timeToJumpApex, 2);
            jumpVelocity = -Math.Abs(gravity) * timeToJumpApex;

            wallJumpController = new WallJumpController(this, TimeSpan.FromSeconds(wallJumpTimeLimit), 1.25f, 0.75f);
        }

        public float RenderDepth { get; set; }

        public override int CollisionMask => (int)Okroma.CollisionMask.Player;

        bool moveLeft, moveRight, jump, wallJump, shouldWallJumpUpward;
        public void HandleInput(IGameControlsService controls)
        {
            moveLeft = controls.Get(Control.MoveLeft);
            moveRight = controls.Get(Control.MoveRight);
            jump = controls.Get(Control.Jump);
            wallJump = controls.Get(Control.WallJump);
            shouldWallJumpUpward = controls.Get(Control.ShouldWallJumpUpward);
        }

        public void Update(GameTime gameTime)
        {
            bool isGrounded = Collision.Below;

            int inputX = 0;

            if (moveLeft)
                inputX -= 1;
            if (moveRight)
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

            if (jump)
            {
                if (isGrounded)
                {
                    velocity.Y = jumpVelocity;
                }
                else if (wallJump && wallJumpController.IsOnWall)
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
                velocity.Y = SmoothMovement(velocity.Y, 0, 0.25f);
            }
            else
            {
                velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            Move(velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            Bounds = new Rectangle((int)Transform.Position.X, (int)Transform.Position.Y, Sprite.TextureWidth, Sprite.TextureHeight);
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
                if (player.shouldWallJumpUpward)
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
