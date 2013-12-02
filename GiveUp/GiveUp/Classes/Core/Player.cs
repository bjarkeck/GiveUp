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
    public class Player
    {

        public Vector2 Position = Vector2.Zero;
        public Vector2 Velocity;
        public float Acceleration;
        public Rectangle Rectangle;
        public SpriteAnimation Animation;

        public bool CanJump = true;
        public bool CanDoubleJump = false;
        public float StartJumpSpeed;

        public float Gravity;
        public float MaxSpeed;
        public float Friction;
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
            Animation = new SpriteAnimation(content.Load<Texture2D>("Images/Player/playerAnimation"), Position, 23, 30, 100);
            Animation.AddRow("stand", 0, 1);
            Animation.AddRow("run", 1, 15);
            Animation.AddRow("jump", 2, 1);
            Animation.AddRow("slide", 3, 1);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Animation.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            Rectangle = Animation.Rectangle;
            Animation.AnimationSpeed = 200 - Math.Abs(Velocity.X * 30);
            InputHelper.Update();
            Movement(gameTime);
            Animation.Update(gameTime, Position);
            Rectangle = Animation.Rectangle;
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

            //move player
            if (keyState.IsKeyDown(Keys.A))
                this.Velocity.X += this.Acceleration * -1 * gameTime.ElapsedGameTime.Milliseconds;
            if (keyState.IsKeyDown(Keys.D))
                this.Velocity.X += this.Acceleration * gameTime.ElapsedGameTime.Milliseconds;

            //Jump
            if (InputHelper.IsNewPress(Keys.Space))
                this.Jump();

            //Gravity
            Velocity.Y += Gravity;

            //Friction
            if (Math.Abs(Velocity.X) < Friction)
                Velocity.X = 0;
            else
                Velocity.X += Friction * (Velocity.X > 0 ? -1f : 1f);

            //Max Speed
            Velocity.X = MathHelper.Clamp(Velocity.X, MaxSpeed * -1, MaxSpeed);

            //Add To Position
            this.Position += this.Velocity;

            //Animation Direction
            if (Velocity.X > 0)
                Animation.FlipImage = false;
            else if (Velocity.X < 0)
                Animation.FlipImage = true;

            //Animation state
            if (Velocity.X == 0)
                Animation.PlayAnimation("stand");
            else
                Animation.PlayAnimation("run");
            if (CanJump == false || Math.Abs(Velocity.Y) > 1)
                Animation.PlayAnimation("jump");

        }
    }
}