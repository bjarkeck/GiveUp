using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Db
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Level> Levels { get; set; }

        public static User CurrentUser
        {
            get
            {
                // TODO Set current ved login.
                return DataContext.Current.Users.First();
            }
        }
    
    }
}
