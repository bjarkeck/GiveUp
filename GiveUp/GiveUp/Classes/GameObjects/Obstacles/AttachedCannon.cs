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
        Texture2D texture;
        Texture2D cannonTexture;
        Rectangle rectangle { get; set; }
        Vector2 cannonPosition;
        float minRotation = 20;
        float maxRotation = 80;
        float cannonRotation = 10;

        public override void Initialize(ContentManager content, Vector2 position)
        {

            var boxTile = LevelManager.GameObjects.Where(x => x.GetType().Name == "BoxTile").Select(x => ((BoxTile)x).Position);
            cannonTexture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonCannon");
            cannonPosition = new Vector2(position.X + 4, position.Y + 15);
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


        public override void Update(GameTime gameTime)
        {
            cannonRotation = (float)Math.Atan2(
                Convert.ToDouble(Player.Position.X - cannonPosition.X),
                Convert.ToDouble(Player.Position.Y - cannonPosition.Y)
            );
            if (HandleCollision.PerPixesCollision(ref Player.Rectangle, rectangle, texture, ref Player.Velocity, ref Player.Position))
            {
                Player.CanJump = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(cannonTexture, cannonPosition, null, Color.White, cannonRotation, Vector2.Zero, 1, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, Position, Color.White);
        }

    }
}
