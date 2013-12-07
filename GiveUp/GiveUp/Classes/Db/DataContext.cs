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
            : base(@"Data Source=|DataDirectory|GiveUpDatabase.sdf")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Level> Levels { get; set; }

    }
}
