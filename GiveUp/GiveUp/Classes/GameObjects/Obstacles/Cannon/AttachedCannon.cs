using GiveUp.Classes.GameObjects.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using GiveUp.Classes.Core;
using System.Text;
using GiveUp.Classes.GameObjects.Obstacles.Cannon;

namespace GiveUp.Classes.GameObjects.Obstacles
{
    class AttachedCannon : GameObject, IGameObject
    {
        public const char TileChar = 'c';
        Texture2D texture;
        Texture2D bulletTexture;
        Texture2D cannonTexture;

        Rectangle rectangle { get; set; }

        public Vector2 cannonPosition;
        float cannonRotation = 10;
        TimeSpan fireRate = TimeSpan.FromMilliseconds(1000);
        TimeSpan timeElapsed;
        List<Rectangle> boxTiles;
        List<CannonBullet> cannonBullets = new List<CannonBullet>();
        
        public override void Initialize(ContentManager content, Vector2 position)
        {
            bulletTexture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/bullet1");

            boxTiles = LevelManager.GameObjects.Where(x => x.GetType().Name == "BoxTile").Select(x => ((BoxTile)x).Rectangle).ToList();
            var boxTile = LevelManager.GameObjects.Where(x => x.GetType().Name == "BoxTile").Select(x => ((BoxTile)x).Position);
            cannonTexture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonCannon");

            if (boxTile.Any(x => x.X == position.X && x.Y == position.Y + 32))
            {
                texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonBodyT");
                position.Y += 32 - texture.Height;

                cannonPosition = new Vector2(position.X + 16, position.Y + 7);
            }
            else if (boxTile.Any(x => x.X == position.X && x.Y == position.Y - 32))
            {
                texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonBodyB");
                cannonPosition = new Vector2(position.X + 16, position.Y);
            }
            else if (boxTile.Any(x => x.X == position.X - 32 && x.Y == position.Y))
            {
                texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonBodyR");
                cannonPosition = new Vector2(position.X + 4, position.Y + 15);

            }
            else if (boxTile.Any(x => x.X == position.X + 32 && x.Y == position.Y))
            {
                texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonBodyL");
                position.X += 32 - texture.Width;

                cannonPosition = new Vector2(position.X + 10, position.Y + 16);
            }
            else
            {
                //Hvis den flyver i luften
                texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonBodyL");
            }

            this.Position = position;
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            float rotation = (float)Math.Atan2(
                Convert.ToDouble(cannonPosition.Y - (Player.Rectangle.Origin().Y))
                ,
                Convert.ToDouble(cannonPosition.X - (Player.Rectangle.Origin().X))
                ) + 3.1416f;
            
            if (GameLogic.IsLineOfSight(10200, cannonPosition, Player.Rectangle))
            {
                    cannonRotation = rotation;

                if ((timeElapsed += gameTime.ElapsedGameTime) > fireRate)
                {
                    timeElapsed = TimeSpan.Zero;
                    cannonBullets.Add(new CannonBullet(bulletTexture, cannonPosition, rotation, 10));
                }
            }
                                                //.ToList() så vi kan fjerne dem mens vi går igennemListen
            foreach (CannonBullet bullet in cannonBullets)
            {
                bullet.Update(gameTime, Player, LevelManager);
                
                //Lav collision her...foreach boxTile... dvs at bullets skal have en public Rectangle.

            }

            if (HandleCollision.PerPixesCollision(ref Player.Rectangle, rectangle, texture, ref Player.Velocity, ref Player.Position))
            {
                Player.CanJump = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(cannonTexture, cannonPosition, null, Color.White, cannonRotation, Vector2.Zero, 1, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, Position, new Color(90, 150, 250));

            foreach (CannonBullet bullet in cannonBullets)
                bullet.Draw(spriteBatch);
        }
    }
}

