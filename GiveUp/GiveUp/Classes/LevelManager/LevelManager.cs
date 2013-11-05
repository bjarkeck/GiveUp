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
        public GridManager GridManager;
        List<IGameObject> GameObjects = new List<IGameObject>();
        public Player Player;
        public ContentManager Content
        {
            get
            {
                return ScreenManager.Current.Content;
            }
        }
        public LevelManagerr(Player player)
        {
            this.Player = player;
            GridManager = new GridManager(Content, 32, 32);
            GridManager.AddBackground("Images/Bgs/bg1");

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GridManager.Draw(spriteBatch);
            foreach (IGameObject item in GameObjects)
            {
                item.Draw(spriteBatch);
            }
        }

        public void LoadLevel(string p)
        {
            GameObjects.Clear();

            GridManager.LoadLevel(p);

            foreach (var unassigendTile in GridManager.UnassignedTiles)
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
                        obj.Player = Player;
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

            foreach (IGameObject obj in GameObjects)
            {
                obj.CollisionLogic();
            }
        }

    }
}
