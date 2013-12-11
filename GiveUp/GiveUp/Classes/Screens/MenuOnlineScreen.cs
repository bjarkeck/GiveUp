using GiveUp.Classes.Core;
using GiveUp.Classes.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Screens
{
    class MenuOnlineScreen : MenuScreen
    {
        public MenuOnlineScreen()
        {
            this.ActiveScreenType = typeof(MenuOnlineScreen);

            foreach (var item in DataContext.Current.Levels)
            {
                item.BestPracticeTime = 50000;
                item.PreviousRunTime = 50000;
            }
            DataContext.Current.SaveChanges();

        }
    }
}
