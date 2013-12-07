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
    class Sawblade : GameObject, IGameObject
    {
        public const char TileChar = 'B';
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
        float rotation = 0.5f;
        float speed = 2.5f;
        int leftBounderie;
        int direction = 1;
        int range = 64;
        int rightBounderie;

        public override void Initialize(ContentManager content, Vector2 position)
        {
            leftBounderie = (int)position.X - range;
            rightBounderie = (int)position.X + range;
            Position = new Vector2(position.X, position.Y + 16);
            texture = content.Load<Texture2D>("Images/Obstacles/sawblade");
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
        }

        public void Movement()
        {
            // TODO Fiks Sawblade collision bounderies
            if (Position.X < leftBounderie)
                direction += 1;
            if (Position.X > rightBounderie)
                direction *= -1;

            Position = new Vector2(Position.X + speed * direction, Position.Y);
        }

        public override void Update(GameTime gameTime)
        {
            rotation += 0.2f;
            Movement();

            if (Player.Rectangle.PerPixesCollision(Rectangle,texture))
            {
                LevelManager.RestartLevel();
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
           //spriteBatch.Draw(texture, Position, Color.White);
            spriteBatch.Draw(texture, new Vector2(Position.X + 16,Position.Y + 16), null, Color.White, rotation, texture.Origin(), 1, SpriteEffects.None, 0);
        }
    }
}
