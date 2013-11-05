﻿using GiveUp.Classes.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    class Finish : IGameObject
    {
        public Player Player { get; set; }
        public Texture2D texture { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle rectangle;

        public const char TileChar = 'D';

        public void Initialize(ContentManager content, Vector2 position)
        {
            Position = position;
            texture = content.Load<Texture2D>("Images/Tiles/door.png");
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void CollisionLogic()
        {
            if (rectangle.Intersects(Player.Rectangle))
            {
                
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }