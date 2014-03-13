using Tempus.Classes.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Tempus.Classes.GameObjects.Obstacles
{
    public class HeatSeekingBullet
    {
        public Rectangle Rectangle;
        Vector2 position;
        float currenRotation;
        Texture2D texture;
        float speed = 0;
        float maxSpeed = 4;
        public HeatSeekingBullet(Texture2D t, double startRotation, Vector2 position)
        {
            this.texture = t;
            this.position = position;
            this.Rectangle = new Rectangle((int)position.X, (int)position.Y, 3, 3);
            this.currenRotation = (float)startRotation;

        }

        public void Update(GameTime gameTime, Player Player)
        {
            
            float mewRotation = (float)position.AngleRadian(new Vector2(Player.Rectangle.Origin().X, Player.Rectangle.Origin().Y - 10));

            if (speed > maxSpeed / 3)
            {
                currenRotation = MathHelper.WrapAngle(GameLogic.CurveAngle(currenRotation, mewRotation, 0.03f * Time.GameSpeed));
            }

            Vector2 rotV = new Vector2((float)Math.Cos(currenRotation), (float)Math.Sin(currenRotation));

            if (speed <= maxSpeed)
            {
                speed += 0.05f * Time.GameSpeed;
            }

            position += rotV * speed * gameTime.DeltaTime();

            Rectangle.X = (int)position.X + (int)(rotV.X * 10);
            Rectangle.Y = (int)position.Y + (int)(rotV.Y * 10);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, currenRotation, texture.Origin(), 0.55f, SpriteEffects.None, 0);
        }


    }
}
