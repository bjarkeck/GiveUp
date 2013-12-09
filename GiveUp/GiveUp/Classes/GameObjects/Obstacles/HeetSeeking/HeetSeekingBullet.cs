using GiveUp.Classes.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.GameObjects.Obstacles
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
                currenRotation = MathHelper.WrapAngle(CurveAngle(currenRotation, mewRotation, 0.03f));
            }

            Vector2 rotV = new Vector2((float)Math.Cos(currenRotation), (float)Math.Sin(currenRotation));

            if (speed <= maxSpeed)
            {
                speed += 0.05f;
            }

            position += rotV * speed;

            Rectangle.X = (int)position.X + (int)(rotV.X * 10);
            Rectangle.Y = (int)position.Y + (int)(rotV.Y * 10);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, currenRotation, texture.Origin(), 0.55f, SpriteEffects.None, 0);
        }



        private float CurveAngle(float from, float to, float step)
        {
            if (step == 0) return from;
            if (from == to || step == 1) return to;

            Vector2 fromVector = new Vector2((float)Math.Cos(from), (float)Math.Sin(from));
            Vector2 toVector = new Vector2((float)Math.Cos(to), (float)Math.Sin(to));

            Vector2 currentVector = Slerp(fromVector, toVector, step);

            return (float)Math.Atan2(currentVector.Y, currentVector.X);
        }
        private Vector2 Slerp(Vector2 from, Vector2 to, float step)
        {
            if (step == 0) return from;
            if (from == to || step == 1) return to;

            double theta = Math.Acos(Vector2.Dot(from, to));
            if (theta == 0) return to;

            double sinTheta = Math.Sin(theta);
            return (float)(Math.Sin((1 - step) * theta) / sinTheta) * from + (float)(Math.Sin(step * theta) / sinTheta) * to;
        }

    }
}
