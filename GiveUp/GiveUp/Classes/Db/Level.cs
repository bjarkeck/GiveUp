using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Db
{
    public class Level
    {
        public int Id { get; set; }
        public int LevelId { get; set; }
        public int BestPracticeTime { get; set; }
        public int BestRunTime { get; set; }
        public int PreviousRunTime { get; set; }
        public virtual User User { get; set; }
        public int Deaths { get; set; }


    }
}