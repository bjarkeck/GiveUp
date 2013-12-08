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
        private List<BaseScreen> screens = new List<BaseScreen>();
        public BaseScreen CurrentScreen;
        public ContentManager Content;

        public ScreenManager(ContentManager content)
        {
            Content = content;
            LoadScreen(new MenuPlayScreen());
        }

        public void LoadScreen(BaseScreen screen, bool startNew = true)
        {
            BaseScreen screenFromList = screens.FirstOrDefault(x => x.GetType() == screen.GetType());
            if (startNew == true)
            {
                if (screenFromList != null)
                {
                    screens.Remove(screenFromList);
                    screens.Add(screen);
                }
                CurrentScreen = screen;
                CurrentScreen.LoadContent(Content);
                screens.Add(screen);
            }
            else
            {
                if (screenFromList != null)
                {
                    CurrentScreen = screenFromList;
                }
                else
                {
                    CurrentScreen = screen;
                    CurrentScreen.LoadContent(Content);
                    screens.Add(screen);
                }
            }
        }

        public void UnloadContent()
        {
            CurrentScreen.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            CurrentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScreen.Draw(spriteBatch);
        }
    }

}
