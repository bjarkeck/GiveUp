using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiveUp.Classes.Core;
using GiveUp.Classes.Screens;

namespace GiveUp.Classes.GameObjects.Obstacles
{
    class PushingWallActivationTile : GameObject, IGameObject
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle rectangle;
        public bool WallActivated = false;

        public const char TileChar = 'L';

        public override void Initialize(ContentManager content, Vector2 position)
        {
            Position = new Vector2(position.X, position.Y);
            Texture = content.Load<Texture2D>("Images/Obstacles/DragActivationTest");
            rectangle = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        public override void CollisionLogic()
        {
            if (Player.Rectangle.PerPixesCollision(rectangle, Texture))
            {
                WallActivated = true;
            }
            else
            {
                WallActivated = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

    }
}
