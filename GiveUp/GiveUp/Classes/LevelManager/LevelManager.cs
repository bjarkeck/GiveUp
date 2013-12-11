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
            spriteBatch.DrawString(font, str, new Vector2(50, 57), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);

            str = "Level " + CurrentSubLevel;
            spriteBatch.DrawString(font, str, new Vector2(50, 73), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);

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
            DirectoryInfo dir = new DirectoryInfo("../../../Content/Levels/" + level);
            foreach (FileInfo file in dir.GetFiles().Where(x => x.Extension.ToLower().Contains("txt")).OrderBy(x => x.Name))
                Levels.Add(file.OpenText().ReadToEnd());

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
                        DataContext.Current.SaveChanges();
                    }
                }
                else
                {
                    if (LevelTimer > 0)
                    {
                        lvl.PreviousRunTime = LevelTimer;
                        if (lvl.PreviousRunTime < lvl.BestRunTime || lvl.BestRunTime == 0)
                        {
                            lvl.BestRunTime = lvl.PreviousRunTime;
                        }
                    }
                }
            }
            else
            {
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
                        DataContext.Current.SaveChanges();
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

            LevelTimer += gameTime.ElapsedGameTime.Milliseconds;
            ChallengeTimer += gameTime.ElapsedGameTime.Milliseconds;

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
