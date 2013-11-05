using GiveUp.Classes.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Core
{
    public class ScreenManager
    {
        public static ScreenManager Current
        {
            get
            {
                return Game1.ScreenManager;
            }
        }
        public BaseScreen CurrentScreen;
        public ContentManager Content;

        public ScreenManager(ContentManager content)
        {
            Content = content;
            LoadScreen(new MenuScreen());
        }

        public void LoadScreen(BaseScreen screen)
        {
            CurrentScreen = screen;
            CurrentScreen.LoadContent(Content);
        }

        public  void UnloadContent()
        {
            CurrentScreen.UnloadContent();
        }

        public  void Update(GameTime gameTime)
        {
            CurrentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScreen.Draw(spriteBatch);
        }
    }

}
