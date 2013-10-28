using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Db
{
    public class LevelData
    {
        public int Id { get; set; }
        public ICollection<TileData> TileData { get; set; }
        public string LevelName { get; set; }
        public float Gravity { get; set; }

        /// <summary>
        /// Så ved alle top levels er parentLevel = null;
        /// </summary>
        public virtual LevelData parentLevel { get; set; }
    }
}