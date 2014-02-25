using GiveUp.Classes.Core;
using GiveUp.Classes.Db;
using GiveUp.Classes.Screens;
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
        public int LevelTimer = 0;
        public int ChallengeTimer = 0;
        public bool PracticeRun = false;
        private List<Level> dbLevels;
        private SpriteFont font;
        private bool firstRun = true;


        public ContentManager Content
        {
            get
            {
                return ScreenManager.Current.Content;
            }
        }

        public LevelManagerr(Player player, bool practiceRun = false)
        {
            PracticeRun = practiceRun;
            this.Player = player;
            GridManager = new GridManager(Content, 32, 32);
            GridManager.AddBackground("Images/Bgs/bg1");

            font = Content.Load<SpriteFont>("Fonts/font");

        }


        public void DrawAdditive(SpriteBatch spriteBatch)
        {

            foreach (IGameObject item in GameObjects)
                item.DrawAdditive(spriteBatch);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GridManager.Draw(spriteBatch);
            foreach (IGameObject item in GameObjects)
            {
                item.Draw(spriteBatch);
            }

            string str = "Challenge " + CurrentLevel;
            spriteBatch.DrawString(font, str, new Vector2(50, 847), Color.Black, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);

            str = "Level " + CurrentSubLevel;
            spriteBatch.DrawString(font, str, new Vector2(50, 863), Color.Black, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);

            if (PracticeRun == false)
            {
                str = "Challenge time: " + (ChallengeTimer.ToTime());
                spriteBatch.DrawString(font, str, new Vector2(1600 - 50 - font.MeasureString(str).X * 0.7f, 57), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);
            }
            else
            {
                str = "Previous time: " + dbLevels.First(x => x.SubLevelId == CurrentSubLevel).PreviousRunTime.ToTime();
                spriteBatch.DrawString(font, str, new Vector2(1600 - 50 - font.MeasureString(str).X * 0.7f, 73), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);

                str = "Best time: " + dbLevels.First(x => x.SubLevelId == CurrentSubLevel).BestPracticeTime.ToTime();
                spriteBatch.DrawString(font, str, new Vector2(1600 - 50 - font.MeasureString(str).X * 0.7f, 89), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);
            }

            str = "Time: " + LevelTimer.ToTime();
            spriteBatch.DrawString(font, str, new Vector2(1600 - 50 - font.MeasureString(str).X * 0.7f, (PracticeRun ? 57 : 73)), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);
        }

        public void StartLevel(int level, int subLevel = 1)
        {
            dbLevels = DataContext.Current.Levels.Where(x => x.LevelId == level).ToList();
            CurrentSubLevel = subLevel;
            CurrentLevel = level;
            DirectoryInfo dir = new DirectoryInfo("./Content/Levels/" + level);
            foreach (FileInfo file in dir.GetFiles().Where(x => x.Extension.ToLower().Contains("txt")).OrderBy(x => x.Name))
            {
                var asdf = file.OpenText();
                Levels.Add(asdf.ReadToEnd());
                asdf.Close();
            }

            StartNextLevel();
        }

        private void startSubLevel(int subLevel)
        {
            LevelTimer = 0;
            if (Levels.Count() < CurrentSubLevel)
            {
                ScreenManager.Current.LoadScreen(new MenuSubLevelScreen(CurrentLevel), true);
            }
            else
            {
                loadLevel(Levels[subLevel - 1]);
                Player.Position = GridManager.UnassignedTiles['S'].First();
            }
        }
        public void RestartLevel()
        {
            StartNextLevel(false);
        }
        Dictionary<IGameObject, Vector2> go = new Dictionary<IGameObject, Vector2>();


        private void loadLevel(string p)
        {
            GameObjects.Clear();

            if (firstRun)
            {
                Player.Velocity = Vector2.Zero;
                firstRun = false;
            }
            Player.CanDoubleJump = true;
            Player.CanJump = true;
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
                        obj.InitialPosition = position;
                        go.Add(obj, position);
                    }
                }
            }

            GameObjects = GameObjects
                            .OrderBy(x =>
                                x.GetType().GetField("LoadOrder") == null ?
                                    100 :
                                (byte)x.GetType().GetField("LoadOrder").GetValue(null)
                                ).ToList();

            foreach (IGameObject g in GameObjects)
            {
                g.Initialize(Content, go[g]);
            }
        }

        public void StartNextLevel(bool runCompleted = false)
        {
            if (runCompleted)
            {
                Level lvl = dbLevels.First(x => x.SubLevelId == CurrentSubLevel);
                if (PracticeRun)
                {
                    if (LevelTimer > 0)
                    {
                        lvl.PreviousRunTime = LevelTimer;
                        if (lvl.PreviousRunTime < lvl.BestPracticeTime || lvl.BestPracticeTime == 0)
                        {
                            lvl.BestPracticeTime = lvl.PreviousRunTime;
                        }
                    }
                }
                else
                {
                    if (LevelTimer > 0)
                    {
                        lvl.PreviousRunTime = LevelTimer;
                    }
                }
            }
            else
            {
                Level lvl = dbLevels.First(x => x.SubLevelId == CurrentSubLevel);
                lvl.Deaths += 1;
                Player.Die(Player.Position);
            }

            LevelTimer = 0;

            if (PracticeRun == false && runCompleted)
            {
                CurrentSubLevel += 1;
                if (Levels.Count() < CurrentSubLevel)
                {
                    int bestRunTime = DataContext.Current.Levels.Where(x => x.LevelId == CurrentLevel).Sum(c => c.BestRunTime);
                    if (bestRunTime == 0 || ChallengeTimer < bestRunTime)
                    {
                        foreach (var item in dbLevels)
                        {
                            item.BestRunTime = item.PreviousRunTime;
                        }
                    }
                    ScreenManager.Current.LoadScreen(new MenuSubLevelScreen(CurrentLevel), true);
                }
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

            LevelTimer += (int)((float)gameTime.ElapsedGameTime.Milliseconds * Time.GameSpeed);
            ChallengeTimer += (int)((float)gameTime.ElapsedGameTime.Milliseconds * Time.GameSpeed);

            if (changeLevel)
            {
                startSubLevel(CurrentSubLevel);
                changeLevel = false;
                Player.Position = GridManager.UnassignedTiles['S'].First();
            }
        }

        public bool changeLevel { get; set; }

        public bool IsOccupied(Vector2 position)
        {
            return GameObjects.Any(x => x.InitialPosition.X == position.X && x.InitialPosition.Y == position.Y);
        }

        internal void AddObsticle(IGameObject gameObject, Vector2 position, bool add)
        {
            if (add)
            {
                if (!IsOccupied(position))
                {
                    IGameObject obj = (IGameObject)Activator.CreateInstance(gameObject.GetType());
                    GameObjects.Add(obj);
                    obj.Player = Player;
                    obj.InitialPosition = position;
                    obj.Initialize(Content, position);
                    go.Add(obj, position);
                }
            }
            else
            {
                var goo = GameObjects.FirstOrDefault(x => x.InitialPosition.X == position.X && x.InitialPosition.Y == position.Y);
                if (goo != null)
                {
                    GameObjects.Remove(goo);
                    var goar = go.First(x => x.Value.X == position.X && x.Value.Y == position.Y);
                    go.Remove(goar.Key);
                }
            }

        }

        public void SaveChanges()
        {

            string lvl = "";

            char[,] lvlData = new char[70, 70];


            foreach (var item in GameObjects.OrderBy(x => x.InitialPosition.Y))
            {
                lvlData[(int)(item.InitialPosition.X / 32), (int)(item.InitialPosition.Y / 32)] = (char)(item.GetType().GetField("TileChar").GetValue(item));
            }

            for (int y = 0; y < lvlData.GetLength(1); y++)
            {
                for (int x = 0; x < lvlData.GetLength(0); x++)
                {
                    lvl += (lvlData[x, y] == default(char) ? '-' : lvlData[x, y]);
                }
                lvl += Environment.NewLine;
            }

            Levels[CurrentSubLevel - 1] = lvl;

            try
            {
                File.WriteAllText("./Content/Levels/" + CurrentLevel + "/" + CurrentSubLevel + ".txt", lvl, Encoding.UTF8);
                File.WriteAllText("../../../Content/Levels/" + CurrentLevel + "/" + CurrentSubLevel + ".txt", lvl, Encoding.UTF8);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
