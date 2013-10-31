using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Core
{
    public class Sprite
    {
        private Texture2D texture;
        public Rectangle Rectangle;
        public Vector2 Position;
        public CollisionType CollisionType;
        public bool Visible = true;

        public Sprite(Texture2D texture, Vector2 position, CollisionType collisionType)
        {
            this.texture = texture;
            this.Rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.CollisionType = collisionType;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
                spriteBatch.Draw(texture, Rectangle, Color.White);
        }
    }
}
