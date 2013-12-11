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
        public int Life { get; set; }
        public Vector2 Velocity;
        public Vector2 Position { get; set; }
        public ParticleTexture ParticleTexture { get; set; }
        public float Rotation { get; set; }
        public bool Collide = false;

        public int CurrentLife;
        public float CurrentRotation;

        public Particle(Vector2 position, Vector2 velocity, ParticleTexture particleTexture, float rotation, int life)
        {
            this.Position = position;
            this.Velocity = velocity;
            this.ParticleTexture = particleTexture;
            this.Rotation = rotation;
            this.Life = life;
            CurrentLife = Life;
        }

        public void Update(GameTime gameTime, Vector2 Gravity)
        {
            if (CurrentLife > 0)
            {
                CurrentRotation += Rotation;

                //Gravity
                Velocity += Gravity;

                //Velociy
                Position += Velocity;


                //Drain Life
                CurrentLife -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture: ParticleTexture.Texture,
                position: Position,
                sourceRectangle: null,
                color: ParticleTexture.Color((int)Life, CurrentLife),
                rotation: CurrentRotation,
                origin: ParticleTexture.Texture.Origin(),
                scale: ParticleTexture.Scale((int)Life, CurrentLife),
                effect: SpriteEffects.None,
                depth: 1
            );
        }

    }
}
