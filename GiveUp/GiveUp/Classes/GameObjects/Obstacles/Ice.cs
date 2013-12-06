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
    class Ice : GameObject, IGameObject
    {
        public const char TileChar = 'I';
        public const byte LoadOrder = 1;

        private Texture2D texture;
        public bool Hide = false;
        public Rectangle Rectangle;
        public float Friction;
        private Vector2 Position;

        public override void Initialize(ContentManager content, Vector2 position)
        {
            Position = position;
            Friction = Player.Friction;
            this.texture = content.Load<Texture2D>("Images/Tiles/IceGround");
            this.Rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public override void CollisionLogic()
        {
            if (HandleCollision.IsOnTopOf(ref Player.Rectangle, Rectangle, ref Player.Velocity, ref Player.Position))
            {
                Player.Animation.PlayAnimation("stand");
                Player.Friction = 0;
                Player.CanJump = true;
                
            }
            else
            {
                Player.Friction = Friction;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, new Color(68, 68, 68));
        }


    }
}