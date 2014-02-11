using GiveUp.Classes.Core;
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

        public override void LoadContent()
        {
            base.LoadContent();

            btnExit = new Button(Content, "Images/Menu/Buttons/btnExit", () => { Game1.ExitGame = true; }, ButtonSizeScaleFactor);
            btnExit.ButtonRectangle.X = (1600 / 2) - btnExit.ButtonRectangle.Width;
            btnExit.ButtonRectangle.Y = 500;

            btnDontExit = new Button(Content, "Images/Menu/Buttons/btnExit", () => {ScreenManager.Current.LoadScreen(new MenuPlayScreen(), false); }, ButtonSizeScaleFactor);
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
            
            btnExit.Draw(spriteBatch);

            btnDontExit.Draw(spriteBatch);
            

            
        }
    }
}
