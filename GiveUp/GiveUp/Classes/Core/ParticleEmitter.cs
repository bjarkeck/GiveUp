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
        private int maxNumberOfParticles;
        public int MaxNumberOfParitcles {
            get
            {
                return maxNumberOfParticles;
            }
            set
            {
                if (StickyParticles && value > MaxNumberOfParitclesLimit)
                {
                    particlesToRemove = value - MaxNumberOfParitclesLimit;
                    if (particlesToRemove > Particles.Count())
                        particlesToRemove = Particles.Count();

                    maxNumberOfParticles = MaxNumberOfParitclesLimit;
                }
                else
                {
                    maxNumberOfParticles = value;
                }
            }
        }
        public int MaxNumberOfParitclesLimit { get; set; }
        public int ParticlesPerSeccond { get; set; }

        public Range<float> ParticleSpeed { get; set; }
        public Range<float> RotationSpeed { get; set; }
        public Range<int> ParticleLife { get; set; }

        public Vector2 AddedVelocity { get; set; }
        public Vector2 Gravity { get; set; }

        public List<ParticleTexture> ParticleTextures { get; set; }
        public List<Particle> Particles = new List<Particle>();

        public bool DrawAdditive = true;

        double timer;
        static Random r = new Random();
        public bool StickyParticles = false;
        private int particlesToRemove = 0;



        public ParticleEmitter(
            List<ParticleTexture> particleTextures,
            Range<float> particleSpeed,
            Range<float> rotationSpeed,
            Range<int> particleLife,
            int angleDirection,
            int angleSpread,
            int maxNumberOfParitcles,
            int particlesPerSeccond,
            Vector2 addedVelocity,
            Vector2 gravity,
            int maxNumberOfParitclesLimit = 4000,
            bool stickyParticles = false,
            bool drawAddtitive = true
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
            this.AddedVelocity = addedVelocity;
            this.Gravity = gravity;
            this.MaxNumberOfParitclesLimit = maxNumberOfParitclesLimit;
            this.StickyParticles = stickyParticles;
            this.DrawAdditive = drawAddtitive;

            timer = 0;
        }

        private void AddParticle(Vector2 position)
        {
            //Behøves ikke at udregnes hver gang... lav propertie fix.
            double angleDir = (AngleDirection - 90) * Math.PI / 180;
            double angleSpread = AngleSpread * Math.PI / 180 / 2;
            double minAngle = angleDir - angleSpread;
            double maxAngle = angleDir + angleSpread;

            double randomAngle = r.NextDouble() * (maxAngle - minAngle) + minAngle;
            float randomSpeed = (float)r.NextDouble(ParticleSpeed.Minimum, ParticleSpeed.Maximum) / 100f;

            Particle p = new Particle(
                position: new Vector2(position.X, position.Y),
                velocity: new Vector2((float)Math.Cos(randomAngle) * randomSpeed, (float)Math.Sin(randomAngle) * randomSpeed) + AddedVelocity * 0.2f,
                particleTexture: ParticleTextures[r.Next(0, ParticleTextures.Count())],
                rotation: (float)r.NextDouble(RotationSpeed.Minimum, RotationSpeed.Maximum),
                life: r.Next(ParticleLife.Minimum, ParticleLife.Maximum)
            );

            Particles.Add(p);
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            timer = gameTime.ElapsedGameTime.TotalMilliseconds;
            //AddParticles
            if (Particles.Count() < MaxNumberOfParitcles)
            {
                //Add this many:
                var ps = 1000f / ParticlesPerSeccond;
                while (timer > ps && Particles.Count() < MaxNumberOfParitcles)
                {
                    timer -= ps;
                    AddParticle(position);
                }
            }

            UpdateParticles(gameTime);
        }

        public void Update(GameTime gameTime, Rectangle position)
        {
            timer = gameTime.ElapsedGameTime.TotalMilliseconds;
            //AddParticles
            if (Particles.Count() < MaxNumberOfParitcles)
            {
                //Add this many:
                var ps = 1000f / ParticlesPerSeccond;
                while (timer > ps && Particles.Count() < MaxNumberOfParitcles)
                {
                    timer -= ps;
                    AddParticle(new Vector2(r.Next(position.X, position.X + position.Width), r.Next(position.Y, position.Y + position.Height)));
                }
            }
            UpdateParticles(gameTime);
        }
        public void Update(GameTime gameTime, Vector2 position, int distance, float rotation)
        {
            timer = gameTime.ElapsedGameTime.TotalMilliseconds;
            //AddParticles
            if (Particles.Count() < MaxNumberOfParitcles)
            {
                //Add this many:
                var ps = 1000f / ParticlesPerSeccond;
                while (timer > ps && Particles.Count() < MaxNumberOfParitcles)
                {
                    timer -= ps;
                    int randomDistance = r.Next(0,Math.Abs(distance));
                    AddParticle(new Vector2(position.X + (float)Math.Cos(rotation) * randomDistance, position.Y + (float)Math.Sin(rotation) * randomDistance));
                }
            }
            UpdateParticles(gameTime);
        }

        public void UpdateParticles(GameTime gameTime)
        {





            //UpdateParticles
            foreach (Particle particle in Particles.ToList())
            {
                particle.Update(gameTime, Gravity);

                //Remove Particles
                if (StickyParticles == false && particle.CurrentLife <= 0)
                {
                    Particles.Remove(particle);
                }
            }

            for (int i = 0; i < particlesToRemove - 1; i++)
            {
                if (Particles.Count() > i)
                {
                    Particles.Remove(Particles[i]);
                }
            }
            particlesToRemove = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle particle in Particles)
                particle.Draw(spriteBatch);
        }

    }
}
