using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace GiveUp.Classes.Db
{
    [Serializable()]
    public class Level
    {
        public int LevelId = 0;
        public int SubLevelId = 0;
        public int BestPracticeTime = 0;
        public int BestRunTime = 0;
        public int PreviousRunTime = 0;
        public int Deaths = 0;

        [NonSerialized]
        public Texture2D ImgTexture;
        [NonSerialized]
        public Texture2D BoxTexture;
        [NonSerialized]
        public Rectangle Rectangle;


        [NonSerialized]
        private bool? isUnlocked = null;
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

        [NonSerialized]
        private bool? isChallangeComplete;
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