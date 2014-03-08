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
    public abstract class DemoMenuScreen : BaseScreen
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
            bg = Content.Load<Texture2D>("Images/Menu/BackGround/menuBg");

            topBarTexture = Content.Load<Texture2D>("Images/Menu/BackGround/btnBarBg");


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
