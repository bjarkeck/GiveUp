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

        private bool isGrounded = true;
        private bool canDoubleJump = false;
        private float startJumpSpeed;
        private float gravity;
        private int health;
        private InputHelper inputHelper = new InputHelper();

        public Player()
        {
            this.Acceleration = 1.0f;
            this.Position = new Vector2(0, 500);
            this.startJumpSpeed = -4f;
            this.gravity = 0.05f;

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
                this.isGrounded = false;
                this.Velocity.Y = this.startJumpSpeed;
            }
        }

        public void Movement(GameTime gameTime)
        {

            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.A))
                this.Velocity.X += this.Acceleration * -1;
            if (keyState.IsKeyDown(Keys.D))
                this.Velocity.X += this.Acceleration;

            if (inputHelper.IsNewPress(Keys.Space))
                this.Jump();

            if (!isGrounded)
            {
                Velocity.Y += gravity;
            }

            Position += Velocity;
        }
    }
}
