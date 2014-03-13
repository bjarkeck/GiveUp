using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tempus.Classes.Core
{
    public class ParticleManager
    {
        public Dictionary<string, ParticleEmitter> ParticleEmitters = new Dictionary<string, ParticleEmitter>();


        public void Update(GameTime gameTime, Vector2 position)
        {
            foreach (var item in ParticleEmitters)
                item.Value.Update(gameTime, position);
        }
        public void Update(GameTime gameTime, Rectangle position)
        {
            foreach (var item in ParticleEmitters)
                item.Value.Update(gameTime, position);
        }

        public void AddEmitter(string p, ParticleEmitter particleEmitter)
        {
            ParticleEmitters.Add(p, particleEmitter);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in ParticleEmitters)
            {
                if (!item.Value.DrawAdditive)
                    item.Value.Draw(spriteBatch);
            }
        }
        public void DrawAdditive(SpriteBatch spriteBatch)
        {
            foreach (var item in ParticleEmitters)
                if (item.Value.DrawAdditive)
                    item.Value.Draw(spriteBatch);
        }

    }
}
