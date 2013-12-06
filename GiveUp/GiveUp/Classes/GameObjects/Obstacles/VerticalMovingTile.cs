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
                Rectangle.Y = (int)value.Y;
                Rectangle.X = (int)value.X;
                position = value;
            }
        }

        Texture2D texture { get; set; }
        float rotation = 0.5f;
        float speed = 2.5f;
        int leftBounderie;
        int direction = 1;
        int range = 64;
        int rightBounderie;

        public override void Initialize(ContentManager content, Vector2 position)
        {
            leftBounderie = (int)position.Y - range;
            rightBounderie = (int)position.Y + range;
            Position = new Vector2(position.X, position.Y);
            texture = content.Load<Texture2D>("Images/Tiles/ground");
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
        }

        public void Movement()
        {
            if (Position.Y < rightBounderie)
                direction += 1;
            if (Position.Y > leftBounderie)
                direction *= -1;

            Position = new Vector2(Position.Y + speed * direction, Position.X);
        }

        public override void Update(GameTime gameTime)
        {
            Movement();

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
            
        }
    }
}
