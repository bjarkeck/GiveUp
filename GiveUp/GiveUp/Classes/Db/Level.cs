using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Db
{
    public class Level
    {


        public Level()
        {

        }


        public int Id { get; set; }
        public int LevelId { get; set; }
        public int SubLevelId { get; set; }
        public int BestPracticeTime { get; set; }
        public int BestRunTime { get; set; }
        public int PreviousRunTime { get; set; }
        public virtual User User { get; set; }
        public int Deaths { get; set; }

        [NotMapped]
        public Texture2D ImgTexture { get; set; }
        [NotMapped]
        public Texture2D BoxTexture { get; set; }
        [NotMapped]
        public Rectangle Rectangle { get; set; }




        private bool? isUnlocked;
        [NotMapped]
        public bool IsUnlocked
        {
            get
            {
                if (SubLevelId == 1)
                {
                    return true;
                }
                if (isUnlocked == null)
                {
                    isUnlocked = DataContext.Current.Levels.FirstOrDefault(x => x.LevelId == LevelId && x.SubLevelId == SubLevelId - 1).PreviousRunTime > 0;
                }
                return isUnlocked == true;

            }
        }

        private bool? isChallangeComplete;
        [NotMapped]
        public bool IsChallangeComplete
        {
            get
            {
                if (isChallangeComplete == null)
                {
                    isChallangeComplete = DataContext.Current.Levels.Any(x => x.LevelId == LevelId && x.BestRunTime == 0) == false;
                }
                return isChallangeComplete == true;
            }
        }
    }
}