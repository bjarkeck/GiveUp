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
        public Player(Texture2D texture, Vector2 position, CollisionType collisionType)
            : base(texture, position, collisionType)
        {
            this.Acceleration = 5.0f;
            this.Position = new Vector2(200, 200);
        }

        public float startJumpSpeed;
        public int health;


        public void LoadContent(ContentManager content)
        {
            this.texture = content.Load<Texture2D>("Images/Player/player");
        }

        public void Movement()
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.A))
                Velocity.X += Acceleration * -1;
            if (keyState.IsKeyDown(Keys.D))
                Velocity.X += Acceleration;
            if (keyState.IsKeyDown(Keys.Space))
                Jump();
        }

        public void Jump()
        {

        }

    }
}
