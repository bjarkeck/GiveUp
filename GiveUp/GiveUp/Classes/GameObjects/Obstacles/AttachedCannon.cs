using GiveUp.Classes.GameObjects.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using GiveUp.Classes.Core;
using System.Text;

namespace GiveUp.Classes.GameObjects.Obstacles
{
    class AttachedCannon : GameObject, IGameObject
    {
        public const char TileChar = 'c';
        private Texture2D texture;
        private Rectangle rectangle { get; set; }
        private Direction direction = Direction.Down;

        public override void Initialize(ContentManager content, Vector2 position)
        {

            var boxTile = LevelManager.GameObjects.Where(x => x.GetType().Name == "BoxTile").Select(x => ((BoxTile)x).Position);

            if (boxTile.Any(x => x.X == position.X - 32 && x.Y == position.Y))
            {
                texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonBodyR");
            }
            else if (boxTile.Any(x => x.X == position.X && x.Y == position.Y - 32))
            {
                texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonBodyB");
            }
            else if (boxTile.Any(x => x.X == position.X && x.Y == position.Y + 32))
            {
                texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonBodyT");
                position.Y += 32 - texture.Height;
            }
            else if (boxTile.Any(x => x.X == position.X + 32 && x.Y == position.Y))
            {
                texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonBodyL");
                position.X += 32 - texture.Width;
            }
            else
            {
                //Hvis den flyveri luften
                texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonBodyL");
            }

            this.Position = position;
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        private Vector2 intersectPosition = new Vector2();

        public override void Update(GameTime gameTime)
        {
            if (HandleCollision.PerPixesCollision(ref Player.Rectangle, rectangle, texture, ref Player.Velocity, ref Player.Position))
            {
                Player.CanJump = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }

    }
}
