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

        public CannonBullet(Texture2D texture, Vector2 startPosition, float shootAngle, float bulletSpeed)
        {
            this.texture = texture;
            this.Position = startPosition;
            this.velocity = new Vector2((float)Math.Cos(shootAngle) * bulletSpeed, (float)Math.Sin(shootAngle) * bulletSpeed);
        }

        public void Update(GameTime gameTime)
        {
            this.Position += velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}
