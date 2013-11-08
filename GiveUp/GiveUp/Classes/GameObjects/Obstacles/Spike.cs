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
    class Spike : GameObject, IGameObject
    {
        public Texture2D texture { get; set; }
        public Rectangle rectangle;

        public const char TileChar = '^';

        public override void Initialize(ContentManager content, Vector2 position)
        {
            Position = new Vector2(position.X, position.Y + (32-9));
            texture = content.Load<Texture2D>("Images/Obstacles/Spike");
            rectangle = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
        }

        public override void CollisionLogic()
        {
            if (Player.Rectangle.PerPixesCollision(rectangle, texture))
            {
                this.LevelManager.RestartLevel();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}