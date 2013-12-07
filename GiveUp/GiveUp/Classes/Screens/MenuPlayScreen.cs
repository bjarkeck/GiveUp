using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Screens
{
    public class MenuPlayScreen : MenuScreen
    {
        SpriteFont font;
        string test = "hej";

        public MenuPlayScreen()
        {
            this.ActiveScreenType = typeof(MenuPlayScreen);
        }

        public override void LoadContent()
        {
            font = Content.Load<SpriteFont>("Fonts/font");
            base.LoadContent();
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(font, test, new Vector2(500, 500), Color.White);
        }

    }
}
