using GiveUp.Classes.Core;
using GiveUp.Classes.LevelManager;
using GiveUp.Classes.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public class GameObject
    {
        public Rectangle Rectangle;
        private Vector2 position;
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                Rectangle.X = (int)value.X;
                Rectangle.Y = (int)value.Y;
                position = value;
            }
        }

        public Player Player { get; set; }

        public LevelManagerr LevelManager
        {
            get
            {
                return ((GameScreen)ScreenManager.Current.CurrentScreen).LevelManager;
            }
        }

        public virtual void Initialize(ContentManager content, Vector2 position)
        {
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void CollisionLogic()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }