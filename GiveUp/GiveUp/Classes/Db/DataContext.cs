using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace GiveUp.Classes.Db
{
    public class DataContext
    {
        public void ReCreateLeveldataForEachUser()
        {
            var levelDir = new DirectoryInfo("./Content/Levels/");

            foreach (DirectoryInfo item in levelDir.GetDirectories())
            {
                int levelName = int.Parse(item.Name);

                foreach (FileInfo f in item.GetFiles())
                {
                    if (f.Extension.ToLower().Contains("txt"))
                    {
                        int subLevel = int.Parse(f.Name.ToLower().Replace(".txt", ""));
                        if (Levels.Any(x => x.LevelId == levelName && x.SubLevelId == subLevel) == false)
                        {
                            Level level = new Level()
                            {
                                LevelId = levelName,
                                SubLevelId = subLevel
                            };
                            Levels.Add(level);
                        }
                    }
                }
            }
            SaveChanges();
        }

        public List<Level> Levels = new List<Level>();
        private const string fileName = "TempusData.bin";
        IsolatedStorageFile appStorage = IsolatedStorageFile.GetUserStoreForAssembly();

        public void SaveChanges()
        {
            using (var r = appStorage.OpenFile(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                var bformatter = new BinaryFormatter();
                bformatter.Serialize(r, Levels);
            }
        }

        public DataContext()
        {
            using (var r = appStorage.OpenFile(fileName, FileMode.OpenOrCreate, FileAccess.Read))
            {
                var bformatter = new BinaryFormatter();
                if (r.Length > 0)
                    Levels = (List<Level>)bformatter.Deserialize(r);

            }
        }

        private static DataContext current;
        public static DataContext Current
        {
            get
            {
                if (current == null)
                {
                    current = new DataContext();

                    if (current.Levels == null || current.Levels.Count() == 0)
                    {
                        current.ReCreateLeveldataForEachUser();
                    }
                }
                return current;
            }
            set
            {
                current = value;
            }

        }

    }
}
