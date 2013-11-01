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

        public int health;

        public Player()
        {
            this.Acceleration = 5.0f;
            this.Position = new Vector2(0, 500);
            this.StartJumpSpeed = -4f;
            this.Gravity = 0.05f;

            health = 1;

        }

        public void LoadContent(ContentManager content)
        {
            this.Texture = content.Load<Texture2D>("Images/Player/player.png");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Movement(gameTime);
        }

        public void Jump()
        {
            if (this.IsJumping == false)
            {
                this.Velocity.Y = this.StartJumpSpeed;
                this.IsJumping = true;
            }
        }

        public void DoubleJump()
        {
            if (this.IsJumping == true && this.isDoubleJump == false)
            {
                this.Velocity.Y = this.StartJumpSpeed;
                this.isDoubleJump = true;
            }
        }

        public void Movement(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.A))
                this.Velocity.X += this.Acceleration * -1;
            if (keyState.IsKeyDown(Keys.D))
                this.Velocity.X += this.Acceleration;
            if (keyState.IsKeyDown(Keys.Space))
                this.Jump();
            
            //Double Jump
            if (keyState.IsKeyDown(Keys.Space))
                this.DoubleJump();
            
            Position += Velocity;
        }

        

    }
}
