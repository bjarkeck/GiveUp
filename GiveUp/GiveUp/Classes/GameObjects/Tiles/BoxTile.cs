﻿using Tempus.Classes.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tempus.Classes.GameObjects.Tiles
{
    class BoxTile : GameObject, IGameObject
    {
        public const char TileChar = 'G';
        public const byte LoadOrder = 1;

        private Texture2D texture;
        public bool Hide = false;
        public Direction LastCollisionDirection = Direction.None;

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
        public override void Initialize(ContentManager content, Vector2 position)
        {
            this.Position = position;
            this.texture = content.Load<Texture2D>("Images/Tiles/ground");
            this.Rectangle = new Rectangle((int)position.X, (int)position.Y, 32, 32);
        }

        public override void CollisionLogic()
        {
            LastCollisionDirection = Direction.None;

            if (HandleCollision.IsOnTopOf(ref Player.Rectangle, Rectangle, ref Player.Velocity, ref Player.Position))
            {
                LastCollisionDirection = Direction.Top;
                Player.CanJump = true;
            }

            if (HandleCollision.IsRightOf(ref Player.Rectangle, Rectangle, ref Player.Velocity, ref Player.Position))
            {
                LastCollisionDirection = Direction.Right;
            }
            if (HandleCollision.IsLeftOf(ref Player.Rectangle, Rectangle, ref Player.Velocity, ref Player.Position))
            {
                LastCollisionDirection = Direction.Left;
            }
            if (HandleCollision.IsBelowOf(ref Player.Rectangle, Rectangle, ref Player.Velocity, ref Player.Position))
                LastCollisionDirection = Direction.Bottom;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.White);
        }

    }
}
