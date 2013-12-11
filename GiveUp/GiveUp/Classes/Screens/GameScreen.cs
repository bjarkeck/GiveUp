using GiveUp.Classes.Core;
using GiveUp.Classes.LevelManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Screens
{
    public class GameScreen : BaseScreen
    {
        public LevelManagerr LevelManager;
        Player player;
        SpriteFont font;

        int levelId = 1;
        int subLevelId = 1;


        public GameScreen(bool pricticeRun = false, int levelId = 1, int subLevelId = 1)
        {
            this.practiceRun = pricticeRun;
            this.levelId = levelId;
            this.subLevelId = subLevelId;

            player = new Player();
            LevelManager = new LevelManagerr(player, pricticeRun);
        }

        public override void LoadContent()
        {
            //Det her er åbenbart nødvendigt??
            if (practiceRun)
            {
                LevelManager.StartLevel(levelId, subLevelId);
            }
            else
            {
                LevelManager.StartLevel(levelId, subLevelId);
            }
            player.LoadContent(Content);
            font = Content.Load<SpriteFont>("Fonts/font");
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            LevelManager.Update(gameTime);

            if (player.InputHelper.IsNewPress(Keys.Escape))
            {
                ScreenManager.Current.LoadScreen(new MenuSubLevelScreen(levelId));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            LevelManager.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.DrawString(font, "test", new Vector2(200, 200), Color.Black);
        }


        public override void DrawAdditive(SpriteBatch spriteBatch)
        {
            LevelManager.DrawAdditive(spriteBatch);
            player.DrawAdditive(spriteBatch);
        }


        public bool practiceRun { get; set; }
    }
}
