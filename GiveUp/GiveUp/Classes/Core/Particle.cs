using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Core
{
    public class Particle
    {
        public float Scale { get; set; }
        public double Life { get; set; }
        public Vector2 Velocity;
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public ParticleColor Color { get; set; }
        public float Rotation { get; set; }
        public bool Collide = false;

        int currentLife;

        public Particle(Vector2 position, Vector2 Velocity, Texture2D texture, ParticleColor color, float rotation, int life, float scale)
        {

        }

        public void Update(GameTime gameTime, Vector2 Friction, Vector2 Gravity)
        {
            //Friction X
            if (Math.Abs(Velocity.X) < Friction.X)
                Velocity.X = 0;
            else
                Velocity.X += Friction.X * (Velocity.X > 0 ? -1f : 1f);

            //Friction Y
            if (Math.Abs(Velocity.Y) < Friction.Y)
                Velocity.Y = 0;
            else
                Velocity.Y += Friction.Y * (Velocity.Y > 0 ? -1f : 1f);

            //Velociy
            Position += Velocity;

            //Gravity
            Position += Gravity;

            //Drain Life
            Life -= gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture: Texture,
                position: Position,
                sourceRectangle: null,
                color: Color.Color((int)Life, currentLife),
                rotation: Rotation,
                origin: Texture.Origin(),
                scale: Scale,
                effect: SpriteEffects.None,
                depth: 1
            );
        }

    }
}
