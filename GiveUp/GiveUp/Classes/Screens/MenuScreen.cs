using GiveUp.Classes.Core;
using GiveUp.Classes.Db;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Screens
{


    public abstract class MenuScreen : BaseScreen
    {
        private List<Button> buttons = new List<Button>();
        private Texture2D bg;
        private Texture2D topBarTexture;
        private int buttonsWidth;
        private int startPosition;
        public float ButtonSizeScaleFactor;
        public Type ActiveScreenType;
        public Rectangle MenuScreenBounderies = new Rectangle(0, 120, 1260, 0);


        public override void LoadContent()
        {
            //DataContext.ReCreateLeveldataForEachUser();

            bg = Content.Load<Texture2D>("Images/Menu/BackGround/menuBg");

            topBarTexture = Content.Load<Texture2D>("Images/Menu/BackGround/btnBarBg");

            //Udregn scaleringsfakotr
            ButtonSizeScaleFactor = 74f / topBarTexture.Height;

            //Tilføj knapperC:\Users\Purup\Documents\GitHub\GiveUp\GiveUp\GiveUp\Content\Images\Menu\Buttons\btnExitStatic.png
            buttons.Add(new Button(Content, "Images/Menu/Buttons/btnPlay", () => { ScreenManager.Current.LoadScreen(new GameScreen(), false); }, ButtonSizeScaleFactor, ActiveScreenType == typeof(MenuPlayScreen)));
            buttons.Add(new Button(Content, "Images/Menu/Buttons/btnOnline", () => { ScreenManager.Current.LoadScreen(new MenuOnlineScreen(), false); }, ButtonSizeScaleFactor, ActiveScreenType == typeof(MenuOnlineScreen)));
            buttons.Add(new Button(Content, "Images/Menu/Buttons/btnSettings", () => { ScreenManager.Current.LoadScreen(new MenuSettingsScreen(), false); }, ButtonSizeScaleFactor, ActiveScreenType == typeof(MenuSettingsScreen)));
            buttons.Add(new Button(Content, "Images/Menu/Buttons/btnExit", () => { ScreenManager.Current.LoadScreen(new MenuExitScreen(), false); }, ButtonSizeScaleFactor, ActiveScreenType == typeof(MenuExitScreen)));

            //Udregn alle knappers bredde:
            buttonsWidth = buttons.Sum(x => x.ButtonRectangle.Width);

            //Udregn start position:
            startPosition = (int)((1600 - buttonsWidth) / 2f);

            MenuScreenBounderies.X = (int)((1600 - MenuScreenBounderies.Width) / 2f);

            int currentXOffset = 0;
            foreach (var item in buttons)
            {
                item.ButtonRectangle.X = startPosition + currentXOffset;
                currentXOffset += item.ButtonRectangle.Width;
            }

            base.LoadContent();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            foreach (Button b in buttons)
            {
                b.Update(gameTime);
            }

            //Loads gameScreen
            //ScreenManager.Current.LoadScreen(new GameScreen());
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bg, new Rectangle(0, 0, 1600, 900), Color.White);
            spriteBatch.Draw(topBarTexture, new Rectangle(0, 0, startPosition, (int)(topBarTexture.Height * ButtonSizeScaleFactor)), Color.White);
            spriteBatch.Draw(topBarTexture, new Rectangle(startPosition + buttonsWidth, 0, startPosition, (int)(topBarTexture.Height * ButtonSizeScaleFactor)), Color.White);

            foreach (Button b in buttons)
            {
                b.Draw(spriteBatch);
            }

            base.Draw(spriteBatch);
        }
    }
}
