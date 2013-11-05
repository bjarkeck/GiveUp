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
        List<IGameObject> GameObjects = new List<IGameObject>();
        public Actor Actor;
        public ContentManager Content { get; set; }
        public LevelManagerr(ContentManager content)
        {
            Content = content;
            TileManager = new TileManager(content, 32, 32);
            TileManager.AddBackground("Images/Bgs/bg1");
            TileManager.AddTileType('g', "Images/Tiles/ground", CollisionType.FullTop);
            TileManager.AddTileType('G', "Images/Tiles/ground", CollisionType.Full);
            TileManager.AddTileType('^', "Images/Obstacles/thorns", CollisionType.PerPixelCollision);
            TileManager.AddTileType('A', "Images/Tiles/activation", CollisionType.PerPixelCollision);

        }


        public void Draw(SpriteBatch spriteBatch)
        {
            TileManager.Draw(spriteBatch);
            foreach (IGameObject item in GameObjects)
            {
                item.Draw(spriteBatch);
            }
        }

        public void LoadLevel(string p)
        {
            TileManager.LoadLevel(p);

            foreach (var unassigendTile in TileManager.UnassignedTiles)
            {
                var gameObject = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(x => x.GetInterfaces()
                        .Contains(
                            typeof(IGameObject)) &&
                            x.GetConstructor(Type.EmptyTypes) != null &&
                            x.GetField("TileChar") != null &&
                            (char)x.GetField("TileChar").GetValue(null) == unassigendTile.Key);

                foreach (Vector2 position in unassigendTile.Value)
                {
                    if (gameObject != null && gameObject.Count() > 0)
                    {
                        IGameObject obj = (IGameObject)gameObject.Select(x => Activator.CreateInstance(x)).First();
                        GameObjects.Add(obj);
                        obj.Initialize(Content, position);
                        obj.Player = (Player)Actor;
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {

            foreach (IGameObject item in GameObjects)
            {
                item.Update(gameTime);
            }

            Actor.CollisionDirection = CollisionDirection.None;
            Actor.CurrentCollision = CollisionType.None;

            foreach (Tile tile in TileManager.Tiles)
            {
                if (tile.CollisionType == CollisionType.Full || tile.CollisionType == CollisionType.FullTop)
                {
                    if (HandleCollision.IsOnTopOf(ref Actor.Rectangle, tile.Rectangle, ref Actor.Velocity, ref Actor.Position))
                    {
                        Actor.CollisionDirection = CollisionDirection.Top;
                        Actor.CurrentCollision = tile.CollisionType;
                    }

                    if (HandleCollision.IsRightOf(ref Actor.Rectangle, tile.Rectangle, ref Actor.Velocity, ref Actor.Position))
                    {
                        Actor.CollisionDirection = CollisionDirection.Left;
                        Actor.CurrentCollision = tile.CollisionType;
                    }
                    if (HandleCollision.IsLeftOf(ref Actor.Rectangle, tile.Rectangle, ref Actor.Velocity, ref Actor.Position))
                    {
                        Actor.CollisionDirection = CollisionDirection.Right;
                        Actor.CurrentCollision = tile.CollisionType;

                    }
                    if (HandleCollision.IsBelowOf(ref Actor.Rectangle, tile.Rectangle, ref Actor.Velocity, ref Actor.Position))
                    {
                        Actor.CollisionDirection = CollisionDirection.Bottom;
                        Actor.CurrentCollision = tile.CollisionType;
                    }
                }
                foreach (IGameObject obj in GameObjects)
                {
                    obj.CollisionLogic();
                }

            }
        }

    }
}
