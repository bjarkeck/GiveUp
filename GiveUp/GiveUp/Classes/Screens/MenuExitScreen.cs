using GiveUp.Classes.Core;
using GiveUp.Classes.Db;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Screens
{
    public class MenuExitScreen : MenuScreen
    {

        public MenuExitScreen()
        {
            this.ActiveScreenType = typeof(MenuExitScreen);
        }
        Button btnExit;
        Button btnDontExit;

        SpriteFont font;
        public override void LoadContent()
        {
            base.LoadContent();

            btnExit = new Button(Content, "Images/Menu/Buttons/btnExit", () => { Game1.ExitGame = true; }, ButtonSizeScaleFactor);
            btnExit.ButtonRectangle.X = (1600 / 2) - btnExit.ButtonRectangle.Width;
            btnExit.ButtonRectangle.Y = 500;
            font = Content.Load<SpriteFont>("Fonts/fontBold");

            btnDontExit = new Button(Content, "Images/Menu/Buttons/btnKeepPlaying", () => { ScreenManager.Current.LoadScreen(new MenuPlayScreen(), false); }, ButtonSizeScaleFactor);
            btnDontExit.ButtonRectangle.X = (1600 / 2);
            btnDontExit.ButtonRectangle.Y = 500;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            btnDontExit.Update(gameTime);
            btnExit.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            string str = "REALLY!?";
            spriteBatch.DrawString(font, str, new Vector2(1600 / 2 - font.MeasureString(str).X / 2, 400), Color.White);
            btnExit.Draw(spriteBatch);

            btnDontExit.Draw(spriteBatch);
            

            
        }
    }
}
