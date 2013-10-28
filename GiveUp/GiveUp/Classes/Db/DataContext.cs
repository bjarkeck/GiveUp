using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Db
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base(@"Data Source=C:\Users\b\Desktop\Git\GiveUp\GiveUp\GiveUp\System\GiveUpDatabase.sdf")
        {
        }

        public DbSet<TileData> TileData { get; set; }
        public DbSet<LevelData> LevelData { get; set; }
    }
}
