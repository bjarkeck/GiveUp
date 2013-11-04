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

        public Texture2D Texture
        {
            get { return texture; }
            set
            {
                this.Rectangle = new Rectangle(0, 0, value.Width, value.Height);
                texture = value;
            }
        }

        public Rectangle Rectangle;
        public Vector2 Position = Vector2.Zero;
        public CollisionType CollisionType;
        public bool Visible = true;

        public Sprite(Texture2D texture, CollisionType collisionType)
        {
            this.Texture = texture;
            this.CollisionType = collisionType;
        }

        public Sprite()
        {
            // TODO: Complete member initialization
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
                spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
