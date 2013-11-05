using GiveUp.Classes.Core;
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

        public const char TileChar = 'D';

        public void Initialize(ContentManager content, Vector2 position)
        {
            Position = position;
            texture = content.Load<Texture2D>("Images/Tiles/door.png");
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void CollisionLogic()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }

        public Texture2D texture { get; set; }

        public Vector2 Position { get; set; }
    }