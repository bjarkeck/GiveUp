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
    public class Wooo<T1, T2, T3, T4>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }

        public Wooo(T1 t1, T2 t2, T3 t3, T4 t4)
        {
            this.Item1 = t1;
            this.Item2 = t2;
            this.Item3 = t3;
            this.Item4 = t4;
        }
    }

    public class Testicle : GameObject, IGameObject
    {
        public const char TileChar = 't';
        public List<Wooo<ParticleEmitter, Vector2, float, int>> list = new List<Wooo<ParticleEmitter, Vector2, float, int>>();
        Random r = new Random();

        public override void Initialize(ContentManager content, Vector2 position)
        {
            for (int i = 0; i < 22; i++)
            {
                List<ParticleTexture> l = new List<ParticleTexture>();

                for (int y = 0; y < 1; y++)
                {
                    l.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/" + (r.Next(0, 2) == 0 ? "Moon" : "Moon")),
                    new Color((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(100, 255), (byte)r.Next(0, 20)),
                    new Color((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(100, 255), (byte)r.Next(0, 0)),
                    r.Next(0, 100) / 100f,
                    r.Next(0, 100) / 100f
                    ));
                    l.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/" + (r.Next(0, 2) == 0 ? "smoke_particle" : "smoke_particle")),
                    new Color((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(100, 200), (byte)r.Next(0, 80)),
                    new Color((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(100, 200), (byte)r.Next(0, 0)),
                    r.Next(0, 15) / 100f,
                    r.Next(0, 15) / 100f
                    ));
                }
                ParticleEmitter e = new ParticleEmitter(
                    l,
                    new Range<float>(0, 40),
                    new Range<float>(-0.02f, 0.02f),
                    new Range<int>(100, r.Next(200, 900)),
                    r.Next(0, 360),
                    r.Next(0, 360),
                    r.Next(0, 10000),
                    r.Next(1000, 2000),
                    new Vector2(r.Next(-2, 2) / 100f, r.Next(-2, 2) / 100f),
                    new Vector2(r.Next(-2, 2) / 100f, r.Next(-2, 2) / 100f)
                );

                Vector2 v = new Vector2(r.Next(50, 1500), r.Next(50, 850));

                var s = new Wooo<ParticleEmitter, Vector2, float, int>(e, v, 1f, 10);

                list.Add(s);
            }
        }

        public override void Update(GameTime gameTime)
        {
            int i = 0;

            foreach (var item in list)
            {
                i++;

                float mewRotation = (float)item.Item2.AngleRadian(MouseHelper.Position);
                item.Item3 = MathHelper.WrapAngle(GameLogic.CurveAngle(item.Item3, mewRotation, 0.06f));
                Vector2 rotV = new Vector2((float)Math.Cos(item.Item3), (float)Math.Sin(item.Item3));
                item.Item2 += rotV * item.Item4;


                item.Item1.Update(gameTime, item.Item2);
            }

        }

        public override void DrawAdditive(SpriteBatch spriteBatch)
        {
            foreach (var item in list)
            {
                item.Item1.Draw(spriteBatch);
            }
        }

    }
}
