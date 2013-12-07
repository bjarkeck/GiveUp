using GiveUp.Classes.Core;
using GiveUp.Classes.GameObjects.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.GameObjects
{
    class GooTile : GameObject, IGameObject
    {
        private Texture2D texture;
        public Vector2 Position;
        public Vector2 BoxTilePosition;
        public const char TileChar = 'M';
        public Rectangle visualRectangle;
        public bool flipImage = false;
        public Direction GooDirection = Direction.None;

        public override void Initialize(ContentManager content, Vector2 position)
        {
            Position = new Vector2(position.X, position.Y);
            texture = content.Load<Texture2D>("Images/Tiles/Goo.png");
            Rectangle = new Rectangle(0, (int)position.Y, 7, 32);

            List<Rectangle> boxTiles = GetAllGameObjects<BoxTile>().Select(x => x.Rectangle).ToList();

            // Til højre for
            if (boxTiles.Any(x => x.X == position.X - 32 && x.Y == position.Y))
            {
                GooDirection = Direction.Right;
                Position = new Vector2(position.X - 7, position.Y);
                Rectangle.X = (int)Position.X;
                BoxTilePosition = new Vector2(Position.X - (32-7), Position.Y);
            }
            // Til venstre for
            if (boxTiles.Any(x => x.X == position.X + 32 && x.Y == position.Y))
            {
                GooDirection = Direction.Left;
                Position = new Vector2(position.X + 32, position.Y);
                BoxTilePosition = Position;
                Rectangle.X = (int)Position.X;
                flipImage = true;
            }
            visualRectangle = new Rectangle((int)Position.X, (int)Position.Y - 1, 7, 36);
        }

        public override void CollisionLogic()
        {
            var theTileBox = GetAllGameObjects<BoxTile>().FirstOrDefault(x => x.Position == BoxTilePosition);

            
            if (theTileBox != null && theTileBox.LastCollisionDirection == GooDirection)
            {
                if (Player.Velocity.Y > 0.1f)
                {
                    Player.Animation.PlayAnimation("slide");
                    Player.Velocity.Y = 0.8f;
                    Player.CanJump = true;
                    Player.CanDoubleJump = true;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, visualRectangle, null, Color.White, 0, Vector2.Zero, (flipImage ? SpriteEffects.FlipHorizontally : SpriteEffects.None), 0);
        }
    }
}

