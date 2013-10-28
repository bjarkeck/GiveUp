using GiveUp.Classes.Core;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.LevelManager
{
    public class LevelManager
    {
        private TileManager tileManager;
        public LevelManager(ContentManager content)
        {
            tileManager = new TileManager(content, 32, 32);
            tileManager.AddBackground("Images/Bgs/bg1");
            tileManager.AddTileType('G', "Images/Tiles/ground", CollisionType.Full);
            tileManager.AddTileType('^', "Images/Obstacles/thorns", CollisionType.PerPixelCollision);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
