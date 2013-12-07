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
            : base(@"Data Source=ns1.lilac.arvixe.com;Initial Catalog=minji;Persist Security Info=True;User ID=minji;Password=minjiminji")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Level> Levels { get; set; }



        private static DataContext current;
        public static DataContext Current
        {
            get
            {
                if (current == null)
                {
                    current = new DataContext();
                }
                return current;

            }

        }

    }
}
