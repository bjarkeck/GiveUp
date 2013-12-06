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
    public class Testicle : GameObject, IGameObject
    {
        ParticleEmitter e1;
        public Vector2 Position { get; set; }

        public const char TileChar = 't';

        public override void Initialize(ContentManager content, Vector2 position)
        {
            this.Position = position;
            List<ParticleTexture> textures1 = new List<ParticleTexture>();

            //Her tilføjer hvilke billder vi vil bruge som particle texture.. og definere start farve og slut farve, samt start scale, og slut scale
            textures1.Add(new ParticleTexture(content.Load<Texture2D>("Images/Obstacles/sawblade"), new Color(Color.Red, (byte)70), Color.Transparent, 0.1f, 0.4f));
            textures1.Add(new ParticleTexture(content.Load<Texture2D>("Images/Obstacles/sawblade"), new Color(Color.Orange, (byte)10), Color.Transparent, 0.1f, 0.2f));
            textures1.Add(new ParticleTexture(content.Load<Texture2D>("Images/Obstacles/sawblade"), new Color(Color.Yellow, (byte)10), Color.Transparent, 0.1f, 0.2f));
            textures1.Add(new ParticleTexture(content.Load<Texture2D>("Images/Obstacles/sawblade"), new Color(Color.Black, (byte)0), new Color(Color.Black, (byte)10), 0.3f, 1f));

            e1 = new ParticleEmitter(
                particleTextures: textures1,
                particleSpeed: new Range<float>(500, 2000),
                rotationSpeed: new Range<float>(-0.4f, 0.4f),
                particleLife: new Range<int>(200, 500),
                angleDirection: 0,
                angleSpread: 2,
                maxNumberOfParitcles: 10000,
                particlesPerSeccond: 15000,
                addedVelocity: new Vector2(0, 0),
                gravity: Vector2.Zero
                );

        }

        public override void Update(GameTime gameTime)
        {
            //her sætter jeg direction til at pege på musen
            e1.AngleDirection = (int)(Player.Rectangle.Origin().AngleDegree(Player.InputHelper.MousePosition)) + 90;
            float dir = (int)(Player.Rectangle.Origin().AngleRadian(Player.InputHelper.MousePosition));

            if (Player.InputHelper.IsCurPress(MouseButtons.RightButton))
            {
                Player.Velocity += new Vector2((float)Math.Cos(dir) , (float)Math.Sin(dir));
            }


            //og lægger playerens velocirty til dne.
            e1.AddedVelocity = Player.Velocity;

            e1.Update(gameTime, Player.Rectangle.Origin());
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            e1.Draw(spriteBatch);
        }

    }
}
