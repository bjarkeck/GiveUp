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
        public double Life { get; set; }
        public Vector2 Velocity;
        public Vector2 Position { get; set; }
        public ParticleTexture ParticleTexture { get; set; }
        public float Rotation { get; set; }
        public bool Collide = false;

        int currentLife;

        public Particle(Vector2 position, Vector2 Velocity, ParticleTexture particleTexture, float rotation, int life, float scale)
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
                texture: ParticleTexture.Texture,
                position: Position,
                sourceRectangle: null,
                color: ParticleTexture.Color((int)Life, currentLife),
                rotation: Rotation,
                origin: ParticleTexture.Texture.Origin(),
                scale: ParticleTexture.Scale((int)Life, currentLife),
                effect: SpriteEffects.None,
                depth: 1
            );
        }

    }
}
