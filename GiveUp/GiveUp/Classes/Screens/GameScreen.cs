using GiveUp.Classes.Core;
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
        public LevelManagerr LevelManager;
        Player player;

        public GameScreen()
        {
            player = new Player();
            LevelManager = new LevelManagerr(player);
        }

        public override void LoadContent()
        {

            LevelManager = new LevelManagerr(Content);
            LevelManager.LoadLevel(Path.Combine(Content.RootDirectory, "levels/Level 1 /Level 1.4.txt"));

            player = new Player();
            Vector2 startPosition = LevelManager.TileManager.UnassignedTiles['s'].First();
            player.Position = startPosition;

            player.LoadContent(Content);
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            LevelManager.Update(gameTime);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            LevelManager.Draw(spriteBatch);
            player.Draw(spriteBatch);
        }
    }
}
