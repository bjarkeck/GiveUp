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



    }
}