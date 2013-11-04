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
    class Player : Actor
    {

        private bool isGrounded
        {
            get
            {
                return CollisionDirection == CollisionDirection.Top;
            }
        }
        private bool canDoubleJump = false;
        private float startJumpSpeed;

        /// <summary>
        /// Must be called in up date allways!
        /// </summary>
        private float gravity;
        private int health;
        private float maxSpeed;
        private float friction;
        private InputHelper inputHelper = new InputHelper();

        KeyboardState oldState;

        public Player()
        {
            this.Acceleration = 0.1f;
            this.Position = new Vector2(200, 500);
            this.startJumpSpeed = -12f;
            this.gravity = 0.55f;
            this.maxSpeed = 5.2f;
            this.friction = 0.9f;

            health = 1;

        }

        public void LoadContent(ContentManager content)
        {
            this.Texture = content.Load<Texture2D>("Images/Player/player.png");
        }

        public override void Update(GameTime gameTime)
        {
            inputHelper.Update();

            Movement(gameTime);
            base.Update(gameTime);
        }

        public void Jump()
        {
            if (this.isGrounded || this.canDoubleJump)
            {
                canDoubleJump = isGrounded;
                this.Velocity.Y = this.startJumpSpeed;
            }
        }

        public void Movement(GameTime gameTime)
        {
            oldState = Keyboard.GetState();
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.A))
            {
                this.Velocity.X += this.Acceleration * -1 * gameTime.ElapsedGameTime.Milliseconds;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                this.Velocity.X += this.Acceleration * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (inputHelper.IsNewPress(Keys.Space))
                this.Jump();


                Velocity.Y += gravity;


            if (Math.Abs(Velocity.X) < friction)
                Velocity.X = 0;
            else
                Velocity.X += friction * (Velocity.X > 0 ? -1f : 1f);

            Velocity.X = MathHelper.Clamp(Velocity.X, maxSpeed * -1, maxSpeed);

            this.Position += this.Velocity;
        }
    }
}
