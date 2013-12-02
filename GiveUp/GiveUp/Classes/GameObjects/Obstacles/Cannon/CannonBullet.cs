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

            //Her skal den så tjekke om kuglen intergere med player...
            //dvs at denne class mangler 2 ting for at være perfekt.
            //En rectangle til at checke .Intersects.
            //Den rectangle skal bare have texturens height og width, samt positions x og y.

            //Og så skal den her update funktion også have playeren som en parameter
            //  public void Update(GameTime gameTime, Player player, LevelManager levelManager)
            //Såen så vi kan tjekke selve kugles rektangle op imod playeresn
            //og så skal den også have LevelManager så den kan ænd
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}
