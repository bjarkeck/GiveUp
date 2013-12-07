﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiveUp.Classes.Core;
using GiveUp.Classes.Screens;
using GiveUp.Classes.GameObjects.Obstacles;

namespace GiveUp.Classes.GameObjects.Obstacles
{
    class PushingWall : GameObject, IGameObject
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position;
        private float speed;

        public const byte LoadOrder = 51;
        public const char TileChar = 'W';

        // TODO En PushingWalls skal gælde for hele y aksen..
        public override void Initialize(ContentManager content, Vector2 position)
        {
            speed = 0.5f;
            Position = new Vector2(position.X, position.Y);
            Texture = content.Load<Texture2D>("Images/Tiles/ground");
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, 32, 32);
        }

        public void Movement()
        {
            // TODO Hvis der ikke findes en PushingWallActivationTile - Push med det samme.
            if (GetAllGameObjects<PushingWallActivationTile>().First().WallActivated == true)
            {
                Position.X += speed;
                Rectangle.X = (int)Position.X;
            }
        }

        public override void Update(GameTime gameTime)
        {
            Movement();
        }

        // TODO Fiks Pushing tile collision
        public override void CollisionLogic()
        {
            if (Player.Rectangle.Intersects(Rectangle) || Player.Rectangle.IsRightOf(Rectangle, Player.Velocity))
            {
                Player.Animation.PlayAnimation("stand");
                Player.Position.X = Rectangle.Right;
                Player.Velocity.X = 10;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color.White);
        }
    }
}
