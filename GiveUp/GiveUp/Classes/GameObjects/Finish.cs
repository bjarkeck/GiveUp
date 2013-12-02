using GiveUp.Classes.Core;
using GiveUp.Classes.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    class Finish : GameObject, IGameObject
    {
        public Player Player { get; set; }
        public Texture2D texture { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle rectangle;
        public const byte LoadOrder = 0;

        public const char TileChar = 'D';

        public void Initialize(ContentManager content, Vector2 position)
        {
            Position = position;
            texture = content.Load<Texture2D>("Images/Tiles/ClosedDoor");
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            if (Player.InputHelper.IsNewPress(Keys.Enter))
            {
                this.LevelManager.StartNextLevel();
            }
        }

        public void CollisionLogic()
        {
            if (Player.Rectangle.IsOnTopOf(rectangle, Player.Velocity))
            {
                this.LevelManager.StartNextLevel();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }