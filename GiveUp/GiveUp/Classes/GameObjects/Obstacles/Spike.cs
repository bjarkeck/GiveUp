using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tempus.Classes.Core;
using Tempus.Classes.Screens;
using Tempus.Classes.GameObjects.Tiles;

namespace Tempus.Classes.GameObjects.Obstacles
{
    class Spike : GameObject, IGameObject
    {
        public Texture2D texture { get; set; }
        public Vector2 Position;

        List<Rectangle> boxTiles;

        public const char TileChar = '^';

        public override void Initialize(ContentManager content, Vector2 position)
        {
            Position = new Vector2(position.X, position.Y + (32-9));
            texture = content.Load<Texture2D>("Images/Obstacles/Spikes/SpikeB");

            boxTiles = GetAllGameObjects<BoxTile>().Select(x => x.Rectangle).ToList();

            if (boxTiles.Any(x => x.X == position.X && x.Y == position.Y + 32))
            {
                texture = content.Load<Texture2D>("Images/Obstacles/Spikes/SpikeB");
                position.Y += 32 - texture.Height;

                Position = new Vector2(position.X + 16, position.Y + 7);
            }
            else if (boxTiles.Any(x => x.X == position.X && x.Y == position.Y - 32))
            {
                texture = content.Load<Texture2D>("Images/Obstacles/Spikes/SpikeT");
                Position = new Vector2(position.X + 16, position.Y);
            }
            else if (boxTiles.Any(x => x.X == position.X - 32 && x.Y == position.Y))
            {
                texture = content.Load<Texture2D>("Images/Obstacles/Spikes/SpikeL");
                Position = new Vector2(position.X + 4, position.Y + 15);

            }
            else if (boxTiles.Any(x => x.X == position.X + 32 && x.Y == position.Y))
            {
                texture = content.Load<Texture2D>("Images/Obstacles/Spikes/SpikeR");
                position.X += 32 - texture.Width;

                Position = new Vector2(position.X + 10, position.Y + 16);
            }
            else
            {
                //Hvis den flyver i luften
                texture = content.Load<Texture2D>("Images/Obstacles/Spikes/SpikeT");
            }

            this.Position = position;
            this.Rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
        

        public override void CollisionLogic()
        {
            if (Player.Rectangle.PerPixesCollision(Rectangle, texture))
            {
                this.LevelManager.RestartLevel();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}