using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.GameObjects.Obstacles.Cannon
{
    class CannonBullets : AttachedCannon
    {
        Texture2D texture { get; set; }
        Rectangle rectangle;
        
        List<CannonBullets> cannonBullets = new List<CannonBullets>();
        float velocity = 10;
        bool isVisible = false;

        public override void Initialize(ContentManager content, Vector2 position)
        {
            texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/bullet");
            rectangle = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (CannonBullets bullet in cannonBullets)
            {
                //bullet.position += bullet.velocity;
                if (Vector2.Distance(bullet.Position, Player.Position) > 1000)
                {
                    bullet.isVisible = false;
                }
            }
            for (int i = 0; i < cannonBullets.Count; i++)
            {
                if (!cannonBullets[i].isVisible)
                {
                    cannonBullets.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Shoot()
        {
            CannonBullets newBullet = new CannonBullets();
            newBullet.Position = cannonPosition;
            newBullet.isVisible = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (CannonBullets bullet in cannonBullets)
                bullet.Draw(spriteBatch);
        }
    }
}
