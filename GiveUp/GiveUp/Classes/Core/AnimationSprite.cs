using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Core
{
    public class AnimationSprite
    {
        public Texture2D Texture { get; set; }
        public int AnimationSpeed { get; set; }
         
        private Dictionary<string, int> animations = new Dictionary<string, int>();

        public AnimationSprite(ContentManager content, Point widthAndHeight)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }

        public void Update(GameTime gameTime)
        {
        }

    }
}
