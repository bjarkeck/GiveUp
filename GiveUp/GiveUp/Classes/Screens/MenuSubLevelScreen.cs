﻿using GiveUp.Classes.Core;
using GiveUp.Classes.Db;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Screens
{
    public class MenuSubLevelScreen : MenuScreen
    {
        private int level;
        public List<Level> lvlList = new List<Level>();
        int startX = 0;
        int startY = 0;

        public Texture2D startChallengeButton;
        public Texture2D challengeScoreboard;

        public Rectangle startChallengeRectangle;
        public Rectangle challengeScoreboardRectangle;

        SpriteFont font;



        public MenuSubLevelScreen(int level)
        {
            this.level = level;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            font = Content.Load<SpriteFont>("Fonts/font");

            lvlList = DataContext.Current.Levels.Where(x => x.LevelId == level).ToList();

            foreach (Level lvl in lvlList)
            {
                if (startX > MenuScreenBounderies.Width - 300)
                {
                    startX = 0;
                    startY += 246;
                }

                lvl.BoxTexture = Content.Load<Texture2D>("Images/Menu/PracticeBoxPassive");
                lvl.Rectangle = new Rectangle(startX + MenuScreenBounderies.X, startY + MenuScreenBounderies.Y, 282, 202);

                startX += 44 + 282;
            }

            startChallengeButton = Content.Load<Texture2D>("Images/Menu/PlayChallengePassive");
            challengeScoreboard = Content.Load<Texture2D>("Images/Menu/runTime");

            startChallengeRectangle = new Rectangle(MenuScreenBounderies.Width - 283 + MenuScreenBounderies.X, MenuScreenBounderies.Y, 283, 67);
            challengeScoreboardRectangle = new Rectangle(MenuScreenBounderies.Width - 283 + MenuScreenBounderies.X, MenuScreenBounderies.Y + 98, 283, 390);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (var item in lvlList)
            {
                if (item.Rectangle.Contains(MouseHelper.Position.ToPoint()))
                {
                    item.BoxTexture = Content.Load<Texture2D>("Images/Menu/PracticeBoxActive");
                }
                else
                {
                    item.BoxTexture = Content.Load<Texture2D>("Images/Menu/PracticeBoxPassive");
                }
            }

            if (startChallengeRectangle.Contains(MouseHelper.Position.ToPoint()))
            {
                startChallengeButton = Content.Load<Texture2D>("Images/Menu/PlayChallengeActive");
            }
            else
            {
                startChallengeButton = Content.Load<Texture2D>("Images/Menu/PlayChallengePassive");
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            var levelLength = font.MeasureString("LEVEL " + level) * 0.7f;

            int i = 0;
            foreach (var item in lvlList)
            {
                spriteBatch.Draw(item.BoxTexture, item.Rectangle, Color.White);
                spriteBatch.DrawString(font, "LEVEL " + item.SubLevelId, new Vector2((item.Rectangle.X + 142) - (levelLength.X / 2f), item.Rectangle.Y + 10), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 1);
                spriteBatch.DrawString(font, item.BestPracticeTime.ToString(), new Vector2((item.Rectangle.X + 212), item.Rectangle.Y + 30), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 1);
                spriteBatch.DrawString(font, item.BestRunTime.ToString(), new Vector2((item.Rectangle.X + 212), item.Rectangle.Y + 50), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 1);
                spriteBatch.DrawString(font, item.PreviousRunTime.ToString(), new Vector2((item.Rectangle.X + 212), item.Rectangle.Y + 70), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 1);
                spriteBatch.DrawString(font, item.Deaths.ToString(), new Vector2((item.Rectangle.X + 212), item.Rectangle.Y + 100), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 1);
                spriteBatch.DrawString(font, item.BestRunTime.ToString(), new Vector2((challengeScoreboardRectangle.Right - 30), challengeScoreboardRectangle.Y + 50 + (34 * i)), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 1);
                i++;
            }

            spriteBatch.DrawString(font, "CHALLENGE " + level, new Vector2(MenuScreenBounderies.X + 5, MenuScreenBounderies.Y - 40), Color.White);

            spriteBatch.Draw(startChallengeButton, startChallengeRectangle, Color.White);
            spriteBatch.Draw(challengeScoreboard, challengeScoreboardRectangle, Color.White);
            //47
        }




    }
}