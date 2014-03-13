using Tempus.Classes.Core;
using Tempus.Classes.LevelManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tempus.Classes.Screens
{
    public class GameScreen : BaseScreen
    {
        public LevelManagerr LevelManager;
        Player player;

        int levelId = 1;
        int subLevelId = 1;
        private static Editor editor;


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
                LevelManager.StartLevel(levelId, subLevelId);
            else
                LevelManager.StartLevel(levelId, subLevelId);

            player.LoadContent(Content);

            if (editor == null)
                editor = new Editor(Content);
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            LevelManager.Update(gameTime);

            if (player.InputHelper.IsNewPress(Keys.Escape))
            {
                ScreenManager.Current.LoadScreen(new DemoScreen());
            }
            editor.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            LevelManager.Draw(spriteBatch);
            player.Draw(spriteBatch);
            editor.Draw(spriteBatch);
           
        }

        public override void DrawAdditive(SpriteBatch spriteBatch)
        {
            LevelManager.DrawAdditive(spriteBatch);
            player.DrawAdditive(spriteBatch);
        }

        public bool practiceRun { get; set; }
    }
}
