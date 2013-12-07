using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Core
{
    class Button
    {
        public delegate void EventHandler();
        public EventHandler OnClick;
        private Texture2D texture;
        private Rectangle collisionBox;


        public Button(Texture2D bTexture, Vector2 position, EventHandler Click)
        {
            this.texture = bTexture;
            this.collisionBox = new Rectangle((int)position.X, (int)position.Y, bTexture.Width, bTexture.Height);
            OnClick += Click;
        }
        
        public void Update(GameTime gt)
        {
            if (collisionBox.Contains(Mouse.GetState().X, Mouse.GetState().Y) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                OnClick.Invoke();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, collisionBox, Color.White);
            Debug.Print(Mouse.GetState().X + " - " + collisionBox.X);
        }
    }
}
