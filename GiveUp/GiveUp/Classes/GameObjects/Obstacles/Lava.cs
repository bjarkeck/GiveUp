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
    class Lava : GameObject, IGameObject
    {
        public const char TileChar = 'L';
        public const byte LoadOrder = 0;

        private Texture2D texture;
        public bool Hide = false;
        public Direction LastCollisionDirection = Direction.None;
        ParticleEmitter p;
        private Vector2 position;
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                Rectangle.X = (int)value.X;
                Rectangle.Y = (int)value.Y;
                position = value;
            }
        }
        public override void Initialize(ContentManager content, Vector2 position)
        {
            this.Position = position;
            this.texture = content.Load<Texture2D>("Images/Tiles/Lava");
            this.Rectangle = new Rectangle((int)position.X, (int)position.Y + 6, 32, 26);

            List<ParticleTexture> textures = new List<ParticleTexture>();
            textures.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/smoke_particle"), new Color(Color.White, 0.4f), new Color(Color.White, 0f), 0.1f, 0.15f));
            textures.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/smoke_particle"), new Color(Color.White, 0.4f), new Color(Color.White, 0), 0f, 0.2f));
            p = new ParticleEmitter(
                textures, 
                new Range<float>(0.1f, 0.2f),
                new Range<float>(-0.2f, 0.2f),
                new Range<int>(400, 1000), 
                0, 120, 200, 70, Vector2.Zero, Vector2.Zero);

        }
        bool plzDie = false;

        public override void Update(GameTime gameTime)
        {
            p.Update(gameTime, new Rectangle(Rectangle.X, Rectangle.Y + 4, Rectangle.Width, 3));
        }
        public override void CollisionLogic()
        {
          
            if (Player.Rectangle.PerPixesCollision(Rectangle, texture))
            {
                Player.ParticleManager.ParticleEmitters["BodyParts"].AddedVelocity = Player.Velocity;
                Player.ParticleManager.ParticleEmitters["Blood"].AddedVelocity = Player.Velocity;
                Player.ParticleManager.ParticleEmitters["BodyParts"].MaxNumberOfParitcles = 0;
                LevelManager.RestartLevel();
             
            }
        }
        float i = 0;
        public override void Draw(SpriteBatch spriteBatch)
        {
           
            spriteBatch.Draw(texture, Rectangle, new Rectangle(57-(int)i,0,57, 47), Color.White);
            i+=0.3333f;
        }
        public override void DrawAdditive(SpriteBatch spriteBatch)
        {
            p.Draw(spriteBatch);
        }

    }
}
