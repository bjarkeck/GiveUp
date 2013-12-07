using GiveUp.Classes.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GiveUp.Classes.LevelManager
{
    public class LevelManagerr
    {
        public int CurrentLevel = 1;
        public int CurrentSubLevel = 1;
        List<string> Levels = new List<string>();
        public GridManager GridManager;
        public List<IGameObject> GameObjects = new List<IGameObject>();
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

        public void StartLevel(int level, int subLevel = 1)
        {
            CurrentSubLevel = subLevel;
            CurrentLevel = level;
            DirectoryInfo dir = new DirectoryInfo("../../../Content/Levels/" + level);
            foreach (FileInfo file in dir.GetFiles().OrderBy(x => x.Name))
                Levels.Add(file.OpenText().ReadToEnd());

            startSubLevel(subLevel);
        }

        private void startSubLevel(int subLevel)
        {
            loadLevel(Levels[subLevel - 1]);
            Player.Position = GridManager.UnassignedTiles['S'].First();
        }
        public void RestartLevel()
        {
            changeLevel = true;
        }

        // TODO Fix player reset.
        private void loadLevel(string p)
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
                        obj.Player = Player;
                        obj.Initialize(Content, position);
                    }
                }
            }

            GameObjects = GameObjects
                            .OrderBy(x =>
                                x.GetType().GetField("LoadOrder") == null ?
                                    100 :
                                (byte)x.GetType().GetField("LoadOrder").GetValue(null)
                                ).ToList();
        }

        public void StartNextLevel()
        {
            if (Levels.Count() > CurrentSubLevel )
            {
                CurrentSubLevel = CurrentSubLevel + 1;
            }
            changeLevel = true;
        }

        public void Update(GameTime gameTime)
        {
            foreach (IGameObject item in GameObjects)
            {
                item.Update(gameTime);
                item.CollisionLogic();
            }

            if (changeLevel)
            {
                startSubLevel(CurrentSubLevel);
                changeLevel = false;
                Player.Position = GridManager.UnassignedTiles['S'].First();
            }
        }

        public bool changeLevel { get; set; }
    }
}
