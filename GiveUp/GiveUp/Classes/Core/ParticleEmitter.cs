using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Core
{
    public class ParticleEmitter
    {
        public int AngleDirection { get; set; }
        public int AngleSpread { get; set; }
        public int MaxNumberOfParitcles { get; set; }
        public int ParticlesPerSeccond { get; set; }

        public Range<float> ParticleSpeed { get; set; }
        public Range<float> RotationSpeed { get; set; }
        public Range<int> ParticleLife { get; set; }

        public Vector2 Friction { get; set; }
        public Vector2 AddedVelocity { get; set; }
        public Vector2 Gravity { get; set; }

        public List<ParticleTexture> ParticleTextures { get; set; }
        public List<Particle> Particles { get; set; }

        double timer;

        public ParticleEmitter(
            List<ParticleTexture> particleTextures,
            Range<float> particleSpeed,
            Range<float> rotationSpeed,
            Range<int> particleLife,
            int angleDirection,
            int angleSpread,
            int maxNumberOfParitcles,
            int particlesPerSeccond,
            Vector2 friction,
            Vector2 addedVelocity,
            Vector2 gravity
            )
        {
            this.ParticleTextures = particleTextures;
            this.ParticleSpeed = particleSpeed;
            this.RotationSpeed = rotationSpeed;
            this.ParticleLife = particleLife;
            this.AngleDirection = angleDirection;
            this.AngleSpread = angleSpread;
            this.MaxNumberOfParitcles = maxNumberOfParitcles;
            this.ParticlesPerSeccond = particlesPerSeccond;
            this.Friction = friction;
            this.AddedVelocity = addedVelocity;
            this.Gravity = gravity;

            timer = 0;
        }

        public void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalMilliseconds;
            //AddParticles
            if (Particles.Count() <  MaxNumberOfParitcles)
            {
                //Add this many:
                ParticlesPerSecond / timer;
            }

            //UpdateParticles
            foreach (Particle particle in Particles.ToList())
            {
                particle.Update(gameTime, Friction, Gravity);

                //Remove Particles
                if (particle.Life <= 0)
                {
                    Particles.Remove(particle);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
