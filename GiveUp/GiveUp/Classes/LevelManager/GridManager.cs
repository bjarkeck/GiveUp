using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
using GiveUp.Classes.Core;

namespace GiveUp.Classes.LevelManager
{
    public class GridManager
    {
        #region Properties and fields
        private ContentManager content;
        public Texture2D backgroundTexture;

        public int GridWidth { get; private set; }
        public int GridHeight { get; private set; }
        private readonly int TileWidth;
        private readonly int TileHeight;
        public int LevelHeight { get { return GridHeight * TileHeight; } }
        public int LevelWidth { get { return GridWidth * TileWidth; } }
        /// <summary>
        /// Start position, slut position, Obsticles osv...
        /// </summary>
        public UnAssignedCharecters<char, Vector2> UnassignedTiles = new UnAssignedCharecters<char, Vector2>();
        #endregion

        public GridManager(ContentManager Content, int tileWidth, int tileHeight)
        {
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            content = Content;
        }
        public void LoadLevel(string levelData)
        {
            UnassignedTiles.Clear();
            string[] levelLines = levelData.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            GridWidth = levelLines.Max(line => line.Length);
            GridHeight = levelLines.Count();

            for (int y = 0; y < levelLines.Length; y++)
            {
                for (int x = 0; x < levelLines[y].Length; x++)
                {
                    char key = levelLines[y][x];
                    UnassignedTiles.Add(key, new Vector2(x * TileWidth, y * TileHeight));
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (backgroundTexture != null)
            {
                spriteBatch.Draw(backgroundTexture,Vector2.Zero,new Rectangle(0,0,1280,960),Color.White);
            }
        }

        public void AddBackground(string texture)
        {
            backgroundTexture = content.Load<Texture2D>(texture);
        }
    }

    public class UnAssignedCharecters<TKey, TValue> : Dictionary<TKey, List<TValue>>
    {
        public void Add(TKey key, TValue value)
        {
            if (!ContainsKey(key))
                Add(key, new List<TValue>());
            this[key].Add(value);
        }


        public List<TValue> this[TKey t]
        {
            get
            {
                return (base.ContainsKey(t)) ? base[t] : new List<TValue>();
            }
        }

    }

}
