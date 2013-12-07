using GiveUp.Classes.Core;
using GiveUp.Classes.LevelManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.GameObjects.Obstacles.Cannon
{
    class CannonBullet
    {
        Texture2D texture;
        Vector2 velocity;
        Vector2 Position;
        public Rectangle CannonBulletRectangle;

        public CannonBullet(Texture2D texture, Vector2 startPosition, float shootAngle, float bulletSpeed)
        {
            this.texture = texture;
            this.Position = new Vector2(startPosition.X - 8, startPosition.Y - 8);
            this.velocity = new Vector2((float)Math.Cos(shootAngle) * bulletSpeed, (float)Math.Sin(shootAngle) * bulletSpeed);
        }

        public void Update(GameTime gameTime, Player player, LevelManagerr levelManager)
        {
            this.Position += velocity;

            CannonBulletRectangle = new Rectangle((int)Position.X, (int)Position.Y, 2, 2);

            if (CannonBulletRectangle.Intersects(player.Rectangle))
            {
                //levelManager.RestartLevel();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}
