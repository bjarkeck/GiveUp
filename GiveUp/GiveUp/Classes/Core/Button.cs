using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        public Rectangle ButtonRectangle = new Rectangle();

        private Texture2D textureStatic;
        private Texture2D textureActive;
        private Texture2D textureHover;

        public Button(ContentManager content, string imagePath, EventHandler Click, float buttonSizeScaleFactor, bool active = false)
        {
            textureStatic = content.Load<Texture2D>(imagePath + "Static");
            textureActive = content.Load<Texture2D>(imagePath + "Active");
            textureHover = content.Load<Texture2D>(imagePath + "Hover");

            ButtonRectangle.Y = 0;
            ButtonRectangle.Height = (int)(textureStatic.Height * buttonSizeScaleFactor);
            ButtonRectangle.Width = (int)(textureStatic.Width * buttonSizeScaleFactor);

            this.texture = active ? textureActive : textureStatic;
            // this.collisionBox = new Rectangle((int)position.X, (int)position.Y, bTexture.Width, bTexture.Height);
            OnClick += Click;
        }

        public void Update(GameTime gt)
        {
            if (this.texture != this.textureActive)
            {
                if (ButtonRectangle.Contains(MouseHelper.Position.ToPoint()))
                {
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        OnClick.Invoke();
                    }
                    this.texture = textureHover;
                }
                else
                {
                    this.texture = textureStatic;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, ButtonRectangle, Color.White);
            Debug.Print(Mouse.GetState().X + " - " + ButtonRectangle.X);
        }
    }
}
