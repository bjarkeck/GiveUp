using GiveUp.Classes.Core;
using GiveUp.Classes.Db;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
            Content.Unload();
            font = Content.Load<SpriteFont>("Fonts/font");
            base.LoadContent();

            //box width = 250;
            //margin = 50;

            int startX = 0;
            int startY = 0;

            foreach (var subLevel in DataContext.Current.Levels.Where(x => x.SubLevelId == 1).OrderBy(x => x.LevelId))
            {
                if (startX > MenuScreenBounderies.Width)
                {
                    startX = 0;
                    startY += 246;
                }

                int level = subLevel.LevelId;

                int numberOfSubLevels = DataContext.Current.Levels.Count(x => x.LevelId == level);
                int numberOfLevelsUnlockted = DataContext.Current.Levels.Count(x => x.LevelId == level && x.PreviousRunTime > 0);

                test = numberOfLevelsUnlockted + " / " + numberOfSubLevels;

                levelTextures.Add(Content.Load<Texture2D>("Levels/" + level + "/img"));
                levelRectangles.Add(new Rectangle(startX + MenuScreenBounderies.X + 2, startY + MenuScreenBounderies.Y + 2, 278, 159));


                levelTextures.Add(Content.Load<Texture2D>("Images/Menu/LevelBoxPassive"));
                levelRectangles.Add(new Rectangle(startX + MenuScreenBounderies.X, startY + MenuScreenBounderies.Y, 282, 202));

                startX += 44 + 282;
            }

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


            int i = 0;
            foreach (var item in levelRectangles)
            {
                if (item.Width == 282)
                {
                    if (item.Contains(MouseHelper.Position.ToPoint()))
                    {
                        levelTextures[i] = Content.Load<Texture2D>("Images/Menu/LevelBoxActive");
                    }
                    else
                    {
                        levelTextures[i] = Content.Load<Texture2D>("Images/Menu/LevelBoxPassive");
                    }

                    if (item.Contains(MouseHelper.Position.ToPoint()) && Mouse.GetState().LeftButton == ButtonState.Pressed)                                                                                                                              
                    {
                        ScreenManager.Current.LoadScreen(new MenuSubLevelScreen(1));
                    }
                }
                i++;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            int i = 0;
            foreach (var t in levelRectangles)
            {
                if (t.Width != 282)
                    spriteBatch.Draw(levelTextures[i], t, Color.White);
                i++;
            }

            i = 0;
            foreach (var t in levelRectangles)
            {
                if (t.Width == 282)
                    spriteBatch.Draw(levelTextures[i], t, Color.White);
                i++;
            }


            spriteBatch.DrawString(font, test, new Vector2(MenuScreenBounderies.X, MenuScreenBounderies.Y - 30), Color.White);

        }

    }
}
