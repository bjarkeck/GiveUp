using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Core
{
    public abstract class BaseScreen
    {
        public ContentManager Content; 

        public BaseScreen()
        {
        }

        internal void LoadContent(ContentManager content)
        {
            Content = content;
            LoadContent();

        }
        public virtual void LoadContent()
        {

        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual void DrawAdditive(SpriteBatch spriteBatch)
        {
        }
    }
}
