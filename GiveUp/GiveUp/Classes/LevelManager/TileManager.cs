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
    public class TileManager
    {
        #region Properties and fields
        private ContentManager content;
        private Dictionary<char, Tuple<string, CollisionType>> tileTypes = new Dictionary<char, Tuple<string, CollisionType>>();
        private Dictionary<char, Texture2D> tileTextures = new Dictionary<char, Texture2D>();
        public Texture2D backgroundTexture;

        public string CurrentLevel = "";
        public int GridWidth { get; private set; }
        public int GridHeight { get; private set; }
        private readonly int TileWidth;
        private readonly int TileHeight;
        public int LevelHeight { get { return GridHeight * TileHeight; } }
        public int LevelWidth { get { return GridWidth * TileWidth; } }
        public List<Tile> Tiles = new List<Tile>();
        /// <summary>
        /// Start position, slut position, Obsticles osv...
        /// </summary>
        public UnAssignedCharecters<char, Vector2> UnassignedTiles = new UnAssignedCharecters<char, Vector2>();
        #endregion

        public TileManager(ContentManager Content, int tileWidth, int tileHeight)
        {
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            content = Content;
        }

        public void AddTileType(char charecter, string texture, CollisionType collisionType)
        {
            tileTypes.Add(charecter, new Tuple<string, CollisionType>(texture, collisionType));
        }

        public void LoadLevel(string txtPath)
        {
            //Clear old map:
            Tiles.Clear();
            CurrentLevel = txtPath;
            StreamReader file = new StreamReader("../../../" + txtPath);
            string[] levelLines = file.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            GridWidth = levelLines.Max(line => line.Length);
            GridHeight = levelLines.Count();

            for (int y = 0; y < levelLines.Length; y++)
            {
                for (int x = 0; x < levelLines[y].Length; x++)
                {
                    char key = levelLines[y][x];

                    //Load Texture if needed
                    if (tileTextures.ContainsKey(key) == false && tileTypes.ContainsKey(key))
                        tileTextures.Add(key, content.Load<Texture2D>(tileTypes[key].Item1));

                    //Hvis typen findes, add tile!
                    if (tileTypes.ContainsKey(key))
                        Tiles.Add(new Tile(tileTextures[key], new Rectangle(x * TileWidth, y * TileHeight, TileWidth, TileHeight), tileTypes[key].Item2));
                    else
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
            foreach (Tile tile in Tiles)
            {
                tile.Draw(spriteBatch);
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
