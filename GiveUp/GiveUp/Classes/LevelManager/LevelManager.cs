using GiveUp.Classes.Core;
using Microsoft.Xna.Framework;
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
        public List<Actor> Actors { get; set; }
        public LevelManagerr(ContentManager content)
        {
            TileManager = new TileManager(content, 32, 32);
            TileManager.AddBackground("Images/Bgs/bg1");
            TileManager.AddTileType('G', "Images/Tiles/ground", CollisionType.Full);
            TileManager.AddTileType('^', "Images/Obstacles/thorns", CollisionType.PerPixelCollision);
            TileManager.AddTileType('D', "Images/Tiles/door", CollisionType.None);
            TileManager.AddTileType('A', "Images/Tiles/activation", CollisionType.PerPixelCollision);
            Actors = new List<Actor>();

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            TileManager.Draw(spriteBatch);
        }

        public void LoadLevel(string p)
        {
            TileManager.LoadLevel(p);
        }

        public void Update(GameTime gameTime)
        {

            foreach (Actor actor in Actors)
            {
                actor.CollisionDirection = CollisionDirection.None;
                actor.CurrentCollision = CollisionType.None;
            }
            foreach (Tile tile in TileManager.Tiles)
            {
                foreach (Actor actor in Actors)
                {

                    if (HandleCollision.IsOnTopOf(ref actor.Rectangle, tile.Rectangle, ref actor.Velocity, ref actor.Position))
                    {
                        actor.CollisionDirection = CollisionDirection.Top;
                        actor.CollisionType = CollisionType.Full;
                    }

                    if (HandleCollision.IsRightOf(ref actor.Rectangle, tile.Rectangle, ref actor.Velocity, ref actor.Position))
                        actor.CollisionDirection = CollisionDirection.Left;

                    if (HandleCollision.IsLeftOf(ref actor.Rectangle, tile.Rectangle, ref actor.Velocity, ref actor.Position))
                        actor.CollisionDirection = CollisionDirection.Right;

                    if (HandleCollision.IsBelowOf(ref actor.Rectangle, tile.Rectangle, ref actor.Velocity, ref actor.Position))
                        actor.CollisionDirection = CollisionDirection.Bottom;

                }
            }
        }
    }
}
