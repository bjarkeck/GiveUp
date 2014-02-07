using GiveUp.Classes.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.GameObjects.Obstacles
{
    public class AimingLaser : GameObject, IGameObject
    {
        public const char TileChar = 'p';
        ParticleManager particleManager;
        Vector2 position;
        float rotation;

        public override void Initialize(ContentManager content, Vector2 position)
        {
            particleManager = new ParticleManager();
            this.position = new Vector2(position.X + 16, position.Y + 16);

            List<ParticleTexture> texturess = new List<ParticleTexture>();
            texturess.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/beamParticleHorisontal"), new Color(Color.Red, 0.4f), new Color(Color.Yellow, 0f), 0.4f, 0.1f, true));
            texturess.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/beamParticleHorisontal"), new Color(Color.Yellow, 0.4f), new Color(Color.Orange, 0f), 0.2f, 0.1f, true));
            texturess.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/beamParticleHorisontal"), new Color(Color.Red, 0.4f), new Color(Color.Yellow, 0f), 0.2f, 0.1f, true));
            texturess.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/beamParticleHorisontal"), new Color(Color.Yellow, 0.4f), new Color(Color.Orange, 0f), 0.2f, 0.1f, true));
            texturess.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/beamParticleHorisontal"), new Color(Color.Yellow, 0.4f), new Color(Color.Orange, 0f), 0.02f, 0.02f, true));
            texturess.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/smoke_particle"), new Color(Color.Blue, 0.9f), new Color(Color.Orange, 0f), 0.2f, 0.02f, true));
            texturess.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/smoke_particle"), new Color(Color.Blue, 0.9f), new Color(Color.Orange, 0f), 0.2f, 0.02f, true));

            particleManager.AddEmitter("laser", new ParticleEmitter(
                texturess,
                    new Range<float>(10,50),
                    new Range<float>(rotation),
                    new Range<int>(100, 600),
                0,
                360,
                8600,
                1000,
                Vector2.Zero,
                Vector2.Zero));

            rotation = (float)position.AngleRadian(Player.Rectangle.Origin());
        }

        float tempDistance = 0;
        public override void Update(GameTime gameTime)
        {
            this.position = Player.Rectangle.Origin();
            rotation = GameLogic.CurveAngle(rotation, (float)position.AngleRadian(MouseHelper.Position), 0.1f);
            float distance = 0;
            GameLogic.IsLineOfSight(position, new Vector2(position.X + (float)Math.Cos(rotation) * 1600, position.Y + (float)Math.Sin(rotation) * 1600), ref distance);

            if (tempDistance < distance)
                tempDistance += 40;
            if (tempDistance > distance)
                tempDistance = distance;

            particleManager.ParticleEmitters["laser"].RotationSpeed = new Range<float>(rotation);
            particleManager.ParticleEmitters["laser"].MaxNumberOfParitcles = (int)tempDistance * 50;
            particleManager.ParticleEmitters["laser"].ParticlesPerSeccond = (int)tempDistance * 20;
            particleManager.ParticleEmitters["laser"].Update(gameTime, position, (int)tempDistance, rotation);
        }
        public override void DrawAdditive(SpriteBatch spriteBatch)
        {
            particleManager.DrawAdditive(spriteBatch);
        }

        public int MyProperty { get; set; }

    }
}
