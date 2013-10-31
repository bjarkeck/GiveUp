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

        public float startJumpSpeed;
        public int health;

        public Player()
        {
            this.Acceleration = 5.0f;
            this.Position = new Vector2(0, 82);
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

        public void Movement(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.A))
                Velocity.X += Acceleration * -1 * gameTime.ElapsedGameTime.Milliseconds;
            if (keyState.IsKeyDown(Keys.D))
                Velocity.X += Acceleration;
            if (keyState.IsKeyDown(Keys.Space))
                Jump();

            Position += Velocity;
        }

        public void Jump()
        {

        }

    }
}
