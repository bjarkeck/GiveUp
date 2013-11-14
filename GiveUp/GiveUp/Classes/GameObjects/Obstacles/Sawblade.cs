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
        public Texture2D texture { get; set; }
        public Rectangle rectangle;
        public float rotation = 0.5f;

        public const char TileChar = 'B';

        public override void Initialize(ContentManager content, Vector2 position)
        {
            Position = new Vector2(position.X, position.Y + 16);
            texture = content.Load<Texture2D>("Images/Obstacles/sawblade");
            rectangle = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
        }

        public override void CollisionLogic()
        {
            if (Player.Rectangle.PerPixesCollision(rectangle, texture))
            {
                this.LevelManager.RestartLevel();
            }
        }

        public override void Update(GameTime gameTime)
        {
            rotation += 0.2f;
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, Position, Color.White);
            spriteBatch.Draw(texture, Position, null, Color.White, rotation, Vector2.Zero, 1, SpriteEffects.None, 1);
        }
    }
}
