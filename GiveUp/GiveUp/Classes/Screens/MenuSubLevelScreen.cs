using Tempus.Classes.Core;
using Tempus.Classes.Db;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tempus.Classes.Screens
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
                if (item.Rectangle.Contains(MouseHelper.Position.ToPoint()) && item.IsUnlocked)
                {
                    item.BoxTexture = Content.Load<Texture2D>("Images/Menu/PracticeBoxActive");

                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        ScreenManager.Current.LoadScreen(new GameScreen(true, item.LevelId, item.SubLevelId));
                    }
                }
                else
                {
                    if (item.IsUnlocked)
                    {
                        item.BoxTexture = Content.Load<Texture2D>("Images/Menu/PracticeBoxPassive");
                    }
                    else
                    {
                        item.BoxTexture = Content.Load<Texture2D>("Images/Menu/PracticeBoxLocked");
                    }
                }
            }

            if (startChallengeRectangle.Contains(MouseHelper.Position.ToPoint()))
            {
                startChallengeButton = Content.Load<Texture2D>("Images/Menu/PlayChallengeActive");

                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    ScreenManager.Current.LoadScreen(new GameScreen(false, level, 1));
                }
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

            bool IsChallangeComplete = lvlList.FirstOrDefault().IsChallangeComplete;

            if (IsChallangeComplete)
            {
                spriteBatch.Draw(challengeScoreboard, challengeScoreboardRectangle, Color.White);
            }


            int i = 0;
            foreach (var item in lvlList)
            {
                spriteBatch.Draw(item.BoxTexture, item.Rectangle, Color.White);
                spriteBatch.DrawString(font, "LEVEL " + item.SubLevelId, new Vector2((item.Rectangle.X + 142) - (levelLength.X / 2f), item.Rectangle.Y + 10), (item.IsUnlocked ? Color.White : new Color(Color.White, 40)), 0, Vector2.Zero, 0.7f, SpriteEffects.None, 1);

                if (item.IsUnlocked)
                {
                    spriteBatch.DrawString(font, item.BestPracticeTime.ToTime(), new Vector2((item.Rectangle.X + 240 - 55), item.Rectangle.Y + 43), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 1);
                    spriteBatch.DrawString(font, item.BestRunTime.ToTime(), new Vector2((item.Rectangle.X + 240 - 55), item.Rectangle.Y + 73), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 1);
                    spriteBatch.DrawString(font, item.PreviousRunTime.ToTime(), new Vector2((item.Rectangle.X + 240 - 55), item.Rectangle.Y + 106), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 1);
                    spriteBatch.DrawString(font, item.Deaths.ToString(), new Vector2((item.Rectangle.X + 250), item.Rectangle.Y + 141), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 1);
                }
                if (IsChallangeComplete)
                {
                    spriteBatch.DrawString(font, "LEVEL " + item.SubLevelId, new Vector2(challengeScoreboardRectangle.Left + 16, challengeScoreboardRectangle.Y + 51 + ((i > 6 ? 35 : 34) * i)), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 1);
                    spriteBatch.DrawString(font, item.BestRunTime.ToTime(), new Vector2((challengeScoreboardRectangle.Right - 30 - 55), challengeScoreboardRectangle.Y + 51 + ((i > 6 ? 35 : 34) * i)), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 1);
                }
                i++;
            }

            if (IsChallangeComplete)
            {
                spriteBatch.DrawString(font, "Total ", new Vector2(challengeScoreboardRectangle.Left + 16, challengeScoreboardRectangle.Bottom - 28), new Color(255, 204, 0), 0, Vector2.Zero, 0.7f, SpriteEffects.None, 1);
                spriteBatch.DrawString(font, lvlList.Sum(x => x.BestRunTime).ToTime(), new Vector2(challengeScoreboardRectangle.Right - 30 - 55, challengeScoreboardRectangle.Bottom - 28), new Color(255, 204, 0), 0, Vector2.Zero, 0.7f, SpriteEffects.None, 1);
            }


            spriteBatch.DrawString(font, "CHALLENGE " + level, new Vector2(MenuScreenBounderies.X + 5, MenuScreenBounderies.Y - 40), Color.White);

            spriteBatch.Draw(startChallengeButton, startChallengeRectangle, Color.White);
            //47
        }




    }
}
