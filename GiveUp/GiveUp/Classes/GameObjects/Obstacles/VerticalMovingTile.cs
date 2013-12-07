using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiveUp.Classes.Core;
using GiveUp.Classes.Screens;

namespace GiveUp.Classes.GameObjects.Obstacles
{
    class VerticalMovingTile : GameObject, IGameObject
    {
        public const char TileChar = 'V';
        public const byte LoadOrder = 0;

        public Rectangle Rectangle;
        private Vector2 position;
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                Rectangle.X = (int)value.X;
                Rectangle.Y = (int)value.Y;
                position = value;
            }
        }

        Texture2D texture { get; set; }
        float speed = 2.5f;
        int topBounderie;
        int direction = 1;
        int range = 64;
        int bottomBounderie;

        public override void Initialize(ContentManager content, Vector2 position)
        {
            topBounderie = (int)position.Y - range;
            bottomBounderie = (int)position.Y + range;
            Position = new Vector2(position.X, position.Y);
            texture = content.Load<Texture2D>("Images/Tiles/ground");
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
        }

        public void Movement()
        {
            if (Position.Y > bottomBounderie)
                direction *= -1;
            if (Position.Y < topBounderie)
                direction += 1;

            Position = new Vector2(Position.X, Position.Y + speed * direction);
        }

        public override void CollisionLogic()
        {
            // TODO Fiks Moveing tile Collision
            if (HandleCollision.IsOnTopOf(ref Player.Rectangle, Rectangle, ref Player.Velocity, ref Player.Position))
            {
                Player.Position.Y = Position.Y - Player.Rectangle.Height + (speed*direction);

                Player.CanJump = true;
            }
            else if (HandleCollision.IsRightOf(ref Player.Rectangle, Rectangle, ref Player.Velocity, ref Player.Position))
            {
                Player.Animation.PlayAnimation("slide");
                if (Player.Velocity.Y > 0.1f)
                {
                    Player.Velocity.Y = Player.Velocity.Y / 2;
                    Player.CanJump = true;
                    Player.CanDoubleJump = false;
                }
            }
            else if (HandleCollision.IsLeftOf(ref Player.Rectangle, Rectangle, ref Player.Velocity, ref Player.Position))
            {
                Player.Animation.PlayAnimation("slide");
                if (Player.Velocity.Y > 0.1f)
                {
                    Player.Velocity.Y = Player.Velocity.Y / 2;
                    Player.CanJump = true;
                    Player.CanDoubleJump = false;
                }
            }
            else if (HandleCollision.IsBelowOf(ref Player.Rectangle, Rectangle, ref Player.Velocity, ref Player.Position))
            {
                Player.Position.Y += speed;
            }
            else if (Player.Rectangle.Intersects(Rectangle))
            {
                if (Rectangle.Bottom - Player.Rectangle.Top > Player.Rectangle.Bottom - Rectangle.Top)
                {
                    Player.Position.Y = Rectangle.Top -1;
                }
                else
                {
                    Player.Position.Y = Rectangle.Bottom + Player.Rectangle.Height +1;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            Movement();

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
