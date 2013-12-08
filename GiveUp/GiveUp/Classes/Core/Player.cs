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
        public bool ReverseControls = false;
        public bool DragActivated = false;
        public float StartJumpSpeed;

        public Vector2 Gravity;
        public float MaxSpeed;
        public float MaxDrag;
        public float Friction;
        public InputHelper InputHelper = new InputHelper();


        public Player()
        {
            this.Acceleration = 0.2f;
            this.Position = Vector2.Zero;
            this.StartJumpSpeed = -8f;
            this.Gravity = new Vector2(0, 0.44f);
            this.MaxSpeed = 3.2f;
            this.MaxDrag = 3.1f;
            this.Friction = 0.9f;
        }

        public void LoadContent(ContentManager content)
        {
            Animation = new SpriteAnimation(content.Load<Texture2D>("Images/Player/playerAnimation"), Position, 46, 60, 5);
            Animation.AddRow("stand", 0, 1);
            Animation.AddRow("run", 1, 15);
            Animation.AddRow("jump", 2, 1);
            Animation.AddRow("slide", 3, 1);
            Animation.AddRow("push", 4, 1);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Animation.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            Rectangle = Animation.Rectangle;
            Animation.AnimationSpeed = 150 - Math.Abs(Velocity.X * 30);
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

            if (keyState.IsKeyDown(ReverseControls ? Keys.D : Keys.A)) {
                Animation.PlayAnimation("run");
                this.Velocity.X += this.Acceleration * -1 * gameTime.ElapsedGameTime.Milliseconds;
            }
            else if (keyState.IsKeyDown(ReverseControls ? Keys.A : Keys.D))
            {
                Animation.PlayAnimation("run");
                this.Velocity.X += this.Acceleration * gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                Animation.PlayAnimation("stand");
            }
            //Jump
            if (InputHelper.IsNewPress(Keys.Space))
                this.Jump();


            //Friction
            if (Math.Abs(Velocity.X) < Friction)
                Velocity.X = 0;
            else
                Velocity.X += Friction * (Velocity.X > 0 ? -1f : 1f);


            //Gravity
            Velocity += Gravity;

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
            if (CanJump == false || Math.Abs(Velocity.Y) > 1)
                Animation.PlayAnimation("jump");
        }
    }
}