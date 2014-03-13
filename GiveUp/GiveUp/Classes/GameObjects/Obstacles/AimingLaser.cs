using Tempus.Classes.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tempus.Classes.GameObjects.Obstacles
{
    public class AimingLaser : GameObject, IGameObject
    {
        public const char TileChar = 'p';
        ParticleManager particleManager;
        Vector2 position;
        float rotation = 0;

        public override void Initialize(ContentManager content, Vector2 position)
        {
            particleManager = new ParticleManager();
            particleManager = new ParticleManager();
            this.position = new Vector2(position.X + 16, position.Y + 16);

            List<ParticleTexture> texturess = new List<ParticleTexture>();
            texturess.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/beamParticleHorisontal"), new Color(Color.Red, 0.4f), new Color(Color.Yellow, 0f), 0.4f, 0.1f, true));
            texturess.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/beamParticleHorisontal"), new Color(Color.Yellow, 0.4f), new Color(Color.Orange, 0f), 0.2f, 0.1f, true));
            texturess.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/beamParticleHorisontal"), new Color(Color.Red, 0.4f), new Color(Color.Yellow, 0f), 0.2f, 0.1f, true));
            texturess.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/beamParticleHorisontal"), new Color(Color.Yellow, 0.4f), new Color(Color.Orange, 0f), 0.2f, 0.1f, true));
            texturess.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/beamParticleHorisontal"), new Color(Color.Yellow, 0.4f), new Color(Color.Orange, 0f), 0.02f, 0.02f, true));

            List<ParticleTexture> textures = new List<ParticleTexture>();
            textures.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/smoke_particle"), new Color(Color.Red, 0.4f), new Color(Color.Yellow, 0f), 0.5f, 0.1f, true));
            textures.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/smoke_particle"), new Color(Color.Yellow, 0.4f), new Color(Color.Orange, 0f), 0.3f, 0.1f, true));

            particleManager.AddEmitter("laser", new ParticleEmitter(
                texturess,
                    new Range<float>(10, 50),
                    new Range<float>(rotation),
                    new Range<int>(100, 200),
                0,
                360,
                8600,
                1000,
                Vector2.Zero,
                Vector2.Zero));


            particleManager.AddEmitter("station", new ParticleEmitter(
                textures,
                    new Range<float>(-15, 15),
                    new Range<float>(0.1f, 0.4f),
                    new Range<int>(100, 200),
                0,
                360,
                1200,
                80,
                Vector2.Zero,
                Vector2.Zero));

        }

        float tempDistance = 0;
        public override void Update(GameTime gameTime)
        {

            rotation -= 2f * (float)gameTime.ElapsedGameTime.TotalSeconds * Time.GameSpeed;

            //rotation = GameLogic.CurveAngle(rotation, (float)position.AngleRadian(MouseHelper.Position), 0.1f);
            float distance = 0;
            if (GameLogic.IsLineOfSight(position, new Vector2(position.X + (float)Math.Cos(rotation) * 1600, position.Y + (float)Math.Sin(rotation) * 1600), ref distance))
            {
                LevelManager.RestartLevel();
            }

            tempDistance = distance;
            //if (tempDistance < distance)
            //    tempDistance += 40;
            //if (tempDistance > distance)
            //    tempDistance = distance;


            Ray ray = new Ray(new Vector3(position.X, position.Y, 0), new Vector3((float)Math.Cos(rotation) * distance, (float)Math.Sin(rotation) * distance, 0));

            float? dist = ray.Intersects(Player.Rectangle.ToBoundingBox());
            if (dist != null)
            {
                if (Vector2.Distance(Player.Position, position) < distance)
                {
                    LevelManager.RestartLevel();
                }
            }




            particleManager.ParticleEmitters["laser"].RotationSpeed = new Range<float>(rotation);
            particleManager.ParticleEmitters["laser"].MaxNumberOfParitcles = (int)tempDistance * 50;
            particleManager.ParticleEmitters["laser"].ParticlesPerSeccond = (int)tempDistance * 20;
            particleManager.ParticleEmitters["laser"].Update(gameTime, position, (int)tempDistance, rotation);
            particleManager.ParticleEmitters["station"].Update(gameTime, position);
        }
        public override void DrawAdditive(SpriteBatch spriteBatch)
        {
            particleManager.DrawAdditive(spriteBatch);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Editor.IsEnable)
            {
                spriteBatch.Draw(GameLogic.ColorTexture(Color.Orange * 0.5f, spriteBatch), new Rectangle((int)InitialPosition.X, (int)InitialPosition.Y, 32, 32), Color.White);
            }
        }
        public int MyProperty { get; set; }

    }
}
