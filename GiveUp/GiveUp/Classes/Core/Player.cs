using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Core
{
    public class Player : Actor
    {

        public bool CanJump = true;
        public bool CanDoubleJump = false;
        public float StartJumpSpeed;
        public Vector2 PreviousPosition = Vector2.Zero;

        /// <summary>
        /// Must be called in up date allways!
        /// </summary>
        public float Gravity;
        public float MaxSpeed;
        public float Friction;
        public float Angle = 10f;
        public InputHelper InputHelper = new InputHelper();

        public Player()
        {
            this.Acceleration = 0.2f;
            this.Position = Vector2.Zero;
            this.StartJumpSpeed = -12f;
            this.Gravity = 0.55f;
            this.MaxSpeed = 5.2f;
            this.Friction = 0.9f;
        }

        public void LoadContent(ContentManager content)
        {
            this.Texture = content.Load<Texture2D>("Images/Player/player");
        }

        public override void Update(GameTime gameTime)
        {
            InputHelper.Update();
            Movement(gameTime);
            
            base.Update(gameTime);
        }

        public void Die()
        {
            //Splat
        }

        public void Jump()
        {
            if (this.CanJump || this.CanDoubleJump)
            {
                CanDoubleJump = CanJump;
                CanJump = false;
                this.Velocity.Y = this.StartJumpSpeed;
            }
        }

        public void Movement(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.A))
            {
                this.Velocity.X += this.Acceleration * -1 * gameTime.ElapsedGameTime.Milliseconds;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                this.Velocity.X += this.Acceleration * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (InputHelper.IsNewPress(Keys.Space))
                this.Jump();
            Velocity.Y += Gravity;

            if (Math.Abs(Velocity.X) < Friction)
                Velocity.X = 0;
            else
                Velocity.X += Friction * (Velocity.X > 0 ? -1f : 1f);

            Velocity.X = MathHelper.Clamp(Velocity.X, MaxSpeed * -1, MaxSpeed);

            PreviousPosition = new Vector2(Position.X, Position.Y);

            this.Position += this.Velocity;
        }
    }
}
