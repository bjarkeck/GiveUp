using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.GameObjects.Obstacles.Cannon
{
    class CannonBullets : GameObject, IGameObject
    {
        Texture2D texture { get; set; }
        public Rectangle rectangle;
        
        List<CannonBullets> cannonBullets = new List<CannonBullets>();
        int speed = 10;
        public Vector2 velocity;

        public bool isVisible = false;

        public override void Initialize(ContentManager content, Vector2 position)
        {
            texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/bullet");
            rectangle = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            /*foreach (CannonBullets bullet in cannonBullets)
            {
                bullet.Position += bullet.velocity;
                if (Vector2.Distance(bullet.Position, Player.Position) > 100)
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
            }*/
        }

        public void Shoot()
        {
            speed = 10;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}
