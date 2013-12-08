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

                lvl.ImgTexture = Content.Load<Texture2D>("Levels/" + level + "/img");  // + lvl.SubLevelId
                lvl.Rectangle = new Rectangle(startX + MenuScreenBounderies.X + 2, startY + MenuScreenBounderies.Y + 2, 278, 159);

                lvl.BoxTexture = Content.Load<Texture2D>("Images/Menu/LevelBoxPassive");
                lvl.Rectangle = new Rectangle(startX + MenuScreenBounderies.X, startY + MenuScreenBounderies.Y, 282, 202);


                startX += 44 + 282;
            }



        }


        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (var item in lvlList)
            {
                spriteBatch.Draw(item.ImgTexture, new Rectangle(item.Rectangle.X + 2, item.Rectangle.Y + 2, 278, 159), Color.White);
                spriteBatch.Draw(item.BoxTexture, item.Rectangle, Color.White);



            }


        }




    }
}
