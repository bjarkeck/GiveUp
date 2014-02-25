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
        public const byte LoadOrder = 10;

        private Texture2D texture;
        public bool Hide = false;
        public Direction LastCollisionDirection = Direction.None;
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
            spriteBatch.Draw(texture, new Rectangle(Rectangle.X, Rectangle.Y, (int)i, Rectangle.Height), new Rectangle(57 - RealSize(i), 0, 57, 47), Color.White);
            spriteBatch.Draw(texture, new Rectangle(Rectangle.X + (int)i, Rectangle.Y, Rectangle.Width - (int)i, Rectangle.Height), new Rectangle(0, 0, 57 - RealSize(i), 47), Color.White);
            i += 0.3333f ;

            if (i > Rectangle.Width)
                i = 0;
        }

        private int RealSize(float v)
        {
            if (v == 0)
                return 0;
            return (int)(v / Rectangle.Width * 57f);
        }

    }
}
