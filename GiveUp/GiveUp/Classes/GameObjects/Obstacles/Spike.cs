using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.GameObjects.Obstacles
{
    class Spike : GameObject, IGameObject
    {
        public Texture2D texture { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle rectangle;

        public const char TileChar = '^';

        public void Initialize(ContentManager content, Vector2 position)
        {
            Position = position;
            texture = content.Load<Texture2D>("Images/Obstacles/Spike");
            rectangle = new Rectangle((int)position.X, (int)position.Y + 28, texture.Width, texture.Height);
        }

        public void CollisionLogic()
        {
            if (rectangle.Intersects(Player.Rectangle))
            {
                Player.Die();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(Position.X, Position.Y + 23), Color.White);
        }
    }
}