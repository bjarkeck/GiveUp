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
    public class MenuSubLevelScreen : MenuScreen
    {
        private int level;
        public List<Level> lvlList = new List<Level>();
        int startX = 0;
        int startY = 0;

        public MenuSubLevelScreen(int level)
        {
            this.level = level;
        }

        public override void LoadContent()
        {
            base.LoadContent();

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
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (var item in lvlList)
            {
                spriteBatch.Draw(item.BoxTexture, item.Rectangle, Color.White);
            }
        }




    }
}
