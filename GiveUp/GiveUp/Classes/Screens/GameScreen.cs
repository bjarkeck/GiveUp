﻿using GiveUp.Classes.Core;
using GiveUp.Classes.LevelManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Screens
{
    public class GameScreen : BaseScreen
    {
        LevelManagerr levelManager;
        Player player;

        public GameScreen()
        {
            player = new Player();
        }

        public override void Update(GameTime gameTime)
        {
            //TODO actor / levels / collision / (update alt bevægelse)
            player.Update(gameTime);
        }

        public override void LoadContent()
        {
            levelManager = new LevelManagerr(Content);
            levelManager.LoadLevel(Path.Combine(Content.RootDirectory, "levels/Level 1 /Level 1.1.txt"));
            player.LoadContent(Content);

            base.LoadContent();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            levelManager.Draw(spriteBatch);
            player.Draw(spriteBatch);
        }
    }
}
