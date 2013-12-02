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
        public int EmisionSpeed { get; set; }

        public float MinParticleSpeed { get; set; }
        public float MaxParticleSpeed { get; set; }
        public float MinRotationSpeed { get; set; }
        public float MaxRotationSpeed { get; set; }
        public int MaxParticleLife { get; set; }
        public int MinParticleLife { get; set; }
        
        public Vector2 Friction { get; set; }
        public Vector2 AddedVelocity { get; set; }
        public Vector2 Gravity { get; set; }

        public bool RunOnce { get; set; }
        public List<ParticleColor> ParticleColors { get; set; }
        public List<Particle> Particles { get; set; }

        DateTime timer;

        public ParticleEmitter()
        {

        }

        public void Update(GameTime gameTime)
        {
            //AddParticles
            if (true)
            {
                
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
