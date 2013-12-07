using GiveUp.Classes.Db;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Screens
{
    public class MenuPlayScreen : MenuScreen
    {
        SpriteFont font;
        string test = "hej";

        List<Texture2D> levelTextures = new List<Texture2D>();
        List<Rectangle> levelRectangles = new List<Rectangle>();

        public MenuPlayScreen()
        {
            this.ActiveScreenType = typeof(MenuPlayScreen);
        }

        public override void LoadContent()
        {
            font = Content.Load<SpriteFont>("Fonts/font");
            base.LoadContent();

            //box width = 250;
            //margin = 50;

            int startX = 0;
            int startY = 0;

            foreach (var subLevel in DataContext.Current.Levels.Where(x => x.SubLevelId == 1))
            {
                if (startX > MenuScreenBounderies.Width)
                {
                    startX = 0;
                    startY += 250;
                }

                int level = subLevel.LevelId;
                
                int numberOfSubLevels = DataContext.Current.Levels.Count(x => x.LevelId == level);
                int numberOfLevelsUnlockted = DataContext.Current.Levels.Count(x => x.LevelId == level && x.PreviousRunTime > 0);

                test = numberOfLevelsUnlockted + " / " + numberOfSubLevels;
                
                levelTextures.Add(Content.Load<Texture2D>("Levels/" + level + "/LevelBoxPassive"));
                levelRectangles.Add(new Rectangle(startX + MenuScreenBounderies.X, startY + MenuScreenBounderies.Y, 250, 200));
                startX += 50 + 250;
            }

        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);


            int i = 0;
            foreach (var t in levelTextures)
            {
                spriteBatch.Draw(t, levelRectangles[i], Color.White);
                i++;
            }



            spriteBatch.DrawString(font, test, new Vector2(MenuScreenBounderies.X, MenuScreenBounderies.Y - 30), Color.White);

        }

    }
}
