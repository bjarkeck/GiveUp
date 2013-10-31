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
        public Texture2D Texture;
        public Rectangle Rectangle;
        public Vector2 Position = Vector2.Zero;
        public CollisionType CollisionType;
        public bool Visible = true;

        public Sprite(Texture2D texture, Vector2 position, CollisionType collisionType)
        {
            this.Texture = texture;
            this.Rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
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
