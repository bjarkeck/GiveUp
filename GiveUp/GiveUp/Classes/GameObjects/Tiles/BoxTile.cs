using GiveUp.Classes.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.GameObjects.Tiles
{
    class BoxTile : GameObject, IGameObject
    {
        public const char TileChar = 'G';
        public const byte LoadOrder = 1;

        private Texture2D texture;
        public bool Hide = false;
        public Rectangle Rectangle;

        public override void Initialize(ContentManager content, Vector2 position)
        {
            this.Position = position;
            this.texture = content.Load<Texture2D>("Images/Tiles/ground");
            this.Rectangle = new Rectangle((int)position.X,(int)position.Y,texture.Width,texture.Height);
        }

        public override void CollisionLogic()
        {
            if (HandleCollision.IsOnTopOf(ref Player.Rectangle, Rectangle, ref Player.Velocity, ref Player.Position))
                Player.CanJump = true;
            if (HandleCollision.IsRightOf(ref Player.Rectangle, Rectangle, ref Player.Velocity, ref Player.Position)) {
                Player.Animation.PlayAnimation("slide");
            }
            if (HandleCollision.IsLeftOf(ref Player.Rectangle, Rectangle, ref Player.Velocity, ref Player.Position))
            {
                Player.Animation.PlayAnimation("slide");
            }
            HandleCollision.IsBelowOf(ref Player.Rectangle, Rectangle, ref Player.Velocity, ref Player.Position);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.Cornsilk);
        }


    }
}
