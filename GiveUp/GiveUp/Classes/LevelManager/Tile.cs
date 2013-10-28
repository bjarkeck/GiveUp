using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GiveUp.Classes.Core;

namespace GiveUp.Classes.LevelManager
{
    public class Tile
    {

        private Texture2D texture;
        public bool Hide = false;
        public Rectangle Rectangle;
        public CollisionType CollisionType;

        public Tile(Texture2D texture, Rectangle rectangle, CollisionType collisionType)
        {
            this.texture = texture;
            this.Rectangle = rectangle;
            this.CollisionType = collisionType;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Hide)
                spriteBatch.Draw(texture, Rectangle, Color.White);
        }

    }
}
