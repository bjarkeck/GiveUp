using GiveUp.Classes.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GiveUp.Classes.LevelManager
{
    public class LevelManagerr
    {
        public TileManager TileManager;
        public List<IGameObject> GameObjects = new List<IGameObject>();
        public List<Actor> Actors = new List<Actor>();

        public LevelManagerr(ContentManager content)
        {
            TileManager = new TileManager(content, 32, 32);
            TileManager.AddBackground("Images/Bgs/bg1");
            TileManager.AddTileType('g', "Images/Tiles/ground", CollisionType.FullTop);
            TileManager.AddTileType('G', "Images/Tiles/ground", CollisionType.Full);
            TileManager.AddTileType('^', "Images/Obstacles/thorns", CollisionType.PerPixelCollision);
            TileManager.AddTileType('D', "Images/Tiles/door", CollisionType.Passable);
            TileManager.AddTileType('A', "Images/Tiles/activation", CollisionType.PerPixelCollision);

            foreach (var unassigendTile in TileManager.UnassignedTiles)
            {
                var gameObject = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(x => x.GetInterfaces()
                        .Contains(
                            typeof(IGameObject)) &&
                            x.GetConstructor(Type.EmptyTypes) != null &&
                            x.GetField("tileChar") != null &&
                            (char)x.GetField("tileChar").GetValue(null) == unassigendTile.Key);

                foreach (Vector2 position in unassigendTile.Value)
                {
                    if (gameObject != null && gameObject.Count() > 0)
                    {
                        IGameObject obj = (IGameObject)gameObject.Select(x => Activator.CreateInstance(x, position)).First();
                        GameObjects.Add(obj);
                        obj.Initialize(content, position);
                    }
                }
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IGameObject item in GameObjects)
            {
                item.Draw(spriteBatch);
            }
            TileManager.Draw(spriteBatch);
        }

        public void LoadLevel(string p)
        {
            TileManager.LoadLevel(p);
        }

        public void Update(GameTime gameTime)
        {

            foreach (IGameObject item in GameObjects)
            {
                item.Update(gameTime);
            }


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
                        actor.CurrentCollision = tile.CollisionType;
                    }

                    if (HandleCollision.IsRightOf(ref actor.Rectangle, tile.Rectangle, ref actor.Velocity, ref actor.Position))
                    {
                        actor.CollisionDirection = CollisionDirection.Left;
                        actor.CurrentCollision = tile.CollisionType;
                    }
                    if (HandleCollision.IsLeftOf(ref actor.Rectangle, tile.Rectangle, ref actor.Velocity, ref actor.Position))
                    {
                        actor.CollisionDirection = CollisionDirection.Right;
                        actor.CurrentCollision = tile.CollisionType;

                    }
                    if (HandleCollision.IsBelowOf(ref actor.Rectangle, tile.Rectangle, ref actor.Velocity, ref actor.Position))
                    {
                        actor.CollisionDirection = CollisionDirection.Bottom;
                        actor.CurrentCollision = tile.CollisionType;
                    }

                }
            }
        }

    }
}
