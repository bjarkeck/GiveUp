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
    class HorizontalMovingTiles : GameObject, IGameObject
    {
        public const char TileChar = 'N';
        public const byte LoadOrder = 0;
        public Vector2 Position;
        public Direction LastCollisionDirection = Direction.None;
        Texture2D texture { get; set; }

        float speed = 2.5f;
        int direction = 1;

        Rectangle movingTileRectangle;
        List<Rectangle> allGameObjects;

        public override void Initialize(ContentManager content, Vector2 position)
        {
            Position = new Vector2(position.X, position.Y);
            texture = content.Load<Texture2D>("Images/Tiles/ground");
            Rectangle = new Rectangle((int)position.X, (int)position.Y, 32, 32);
            //movingTileRectangle = new Rectangle((int)position.X, (int)position.Y, 32, 32);
        }


        public override void Update(GameTime gameTime)
        {
            allGameObjects = GetAllGameObjects<GameObject>().Where(x => x.Equals(this) == false).Select(x => x.Rectangle).ToList();
            Position.X += speed * direction;
            Rectangle.X = (int)Position.X;
            //movingTileRectangle.X = (int)Position.X;

        }

        public override void CollisionLogic()
        {
            LastCollisionDirection = Direction.None;

            if (HandleCollision.IsOnTopOf(ref Player.Rectangle, Rectangle, ref Player.Velocity, ref Player.Position))
            {
                LastCollisionDirection = Direction.Top;
                Player.Position.X += (int)(speed * direction);
                Player.CanJump = true;
                Console.WriteLine("ja");
            }
            else
            {
                Console.WriteLine("nej");
            }

            if (HandleCollision.IsRightOf(ref Player.Rectangle, Rectangle, ref Player.Velocity, ref Player.Position))
            {
                LastCollisionDirection = Direction.Right;
            }
            if (HandleCollision.IsLeftOf(ref Player.Rectangle, Rectangle, ref Player.Velocity, ref Player.Position))
            {
                LastCollisionDirection = Direction.Left;
            }
            if (HandleCollision.IsBelowOf(ref Player.Rectangle, Rectangle, ref Player.Velocity, ref Player.Position))
                LastCollisionDirection = Direction.Bottom;

            if (allGameObjects.Any(x => x.Intersects(Rectangle)))
            {
                direction *= -1;
            }


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.White);
        }
    }
}