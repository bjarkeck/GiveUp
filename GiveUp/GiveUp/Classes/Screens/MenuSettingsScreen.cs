﻿using Tempus.Classes.Core;
using Tempus.Classes.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tempus.Classes.Screens
{
    class MenuSettingsScreen : MenuScreen
    {
        public MenuSettingsScreen()
        {
            this.ActiveScreenType = typeof(MenuSettingsScreen);

            foreach (var item in DataContext.Current.Levels)
            {
                item.BestPracticeTime = 0;
                item.PreviousRunTime = 0;
                item.BestRunTime = 0;
            }
            DataContext.Current.SaveChanges();
        }
    }
}
