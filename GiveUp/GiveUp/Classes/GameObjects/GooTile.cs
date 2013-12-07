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
        private Texture2D gooLeftSide;
        public Vector2 Position;
        public const char TileChar = 'M';
        List<Rectangle> boxTiles;

        public void Initialize(ContentManager content, Vector2 position)
        {
            Position = new Vector2(Position.X, Position.Y);
            gooLeftSide = content.Load<Texture2D>("Images/Tiles/gooGroundLeft.png");
            Rectangle = new Rectangle((int)position.X, (int)position.Y, 32, 32);

            boxTiles = GetAllGameObjects<BoxTile>().Select(x => x.Rectangle).ToList();

            if (boxTiles.Any(x => x.X == position.X && x.Y == position.Y + 32))
            {
                
            }
            else if (boxTiles.Any(x => x.X == position.X && x.Y == position.Y - 32))
            {
                
            }
            else if (boxTiles.Any(x => x.X == position.X - 32 && x.Y == position.Y))
            {
                

            }
            else if (boxTiles.Any(x => x.X == position.X + 32 && x.Y == position.Y))
            {
                
            }
            else
            {
                //Hvis den flyver i luften
                //texture = content.Load<Texture2D>("Images/Obstacles/Spikes/SpikeT");
            }
        }


        public void CollisionLogic()    
        {
            // TODO Tjek om den virker hver gang
            if (HandleCollision.IsOnTopOf(ref Player.Rectangle, Rectangle, ref Player.Velocity, ref Player.Position))
                Player.CanJump = true;

            if (HandleCollision.IsRightOf(ref Player.Rectangle, Rectangle, ref Player.Velocity, ref Player.Position))
            {
                if (Player.Velocity.Y > 0.1f)
                {
                    Player.Animation.PlayAnimation("slide");
                    Player.Velocity.Y = Player.Velocity.Y / 2;
                    Player.CanJump = true;
                    Player.CanDoubleJump = false;
                }
            }
            if (HandleCollision.IsLeftOf(ref Player.Rectangle, Rectangle, ref Player.Velocity, ref Player.Position))
            {
                if (Player.Velocity.Y > 0.1f)
                {
                    Player.Animation.PlayAnimation("slide");
                    Player.Velocity.Y = Player.Velocity.Y / 2;
                    Player.CanJump = true;
                    Player.CanDoubleJump = false;
                }
            }
            HandleCollision.IsBelowOf(ref Player.Rectangle, Rectangle, ref Player.Velocity, ref Player.Position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gooLeftSide, Rectangle, Color.White);
        }
    }
}

