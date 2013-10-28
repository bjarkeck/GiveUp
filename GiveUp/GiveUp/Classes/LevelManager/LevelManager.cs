using GiveUp.Classes.Core;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.LevelManager
{
    public class LevelManagerr
    {
        public TileManager TileManager;
        public LevelManagerr(ContentManager content)
        {
            TileManager = new TileManager(content, 32, 32);
            TileManager.AddBackground("Images/Bgs/bg1");
            TileManager.AddTileType('G', "Images/Tiles/ground", CollisionType.Full);
            TileManager.AddTileType('^', "Images/Obstacles/thorns", CollisionType.PerPixelCollision);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            TileManager.Draw(spriteBatch);
        }

        public void LoadLevel(string p)
        {
            TileManager.LoadLevel(p);
        }
    }
}
