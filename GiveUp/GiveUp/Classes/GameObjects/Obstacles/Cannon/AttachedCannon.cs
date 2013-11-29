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
        Texture2D cannonTexture;
        Rectangle rectangle { get; set; }
        Vector2 cannonPosition;
        float minRotation = 1.3033f;
        float maxRotation = 4.96f;
        float cannonRotation = 10;

        List<CannonBullets> cannonBullets = new List<CannonBullets>();

        public override void Initialize(ContentManager content, Vector2 position)
        {

            var boxTile = LevelManager.GameObjects.Where(x => x.GetType().Name == "BoxTile").Select(x => ((BoxTile)x).Position);
            cannonTexture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonCannon");

            if (boxTile.Any(x => x.X == position.X - 32 && x.Y == position.Y))
            {
                texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonBodyR");
                cannonPosition = new Vector2(position.X + 4, position.Y + 15);

            }
            else if (boxTile.Any(x => x.X == position.X && x.Y == position.Y - 32))
            {
                texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonBodyB");
                cannonPosition = new Vector2(position.X + 16, position.Y);
                maxRotation += 1.5707f;
                minRotation += 1.5707f;
            }
            else if (boxTile.Any(x => x.X == position.X && x.Y == position.Y + 32))
            {
                texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonBodyT");
                position.Y += 32 - texture.Height;

                cannonPosition = new Vector2(position.X + 16, position.Y + 7);
                maxRotation -= 1.5707f;
                minRotation -= 1.5707f;
            }
            else if (boxTile.Any(x => x.X == position.X + 32 && x.Y == position.Y))
            {
                texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonBodyL");
                position.X += 32 - texture.Width;

                cannonPosition = new Vector2(position.X + 10, position.Y + 16);
                maxRotation += 1.5707f * 2;
                minRotation += 1.5707f * 2;
            }
            else
            {
                //Hvis den flyver i luften
                texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonBodyL");
            }

            this.cannonRotation = minRotation;

            this.Position = position;
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }


        public override void Update(GameTime gameTime)
        {
            if (GameLogic.IsLOS(90, 200, cannonPosition, Player.Position, Player.Angle))
            {
                float rotation = (float)Math.Atan2(
                    Convert.ToDouble(cannonPosition.Y - (Player.Position.Y + Player.Texture.Origin().Y))
                    ,
                    Convert.ToDouble(cannonPosition.X - (Player.Position.X + Player.Texture.Origin().X))
                    ) + 3.1416f;


                if (rotation < minRotation || rotation > maxRotation)
                    cannonRotation = rotation;

                foreach (CannonBullets bullet in cannonBullets)
                {
                    bullet.Position += bullet.velocity;
                    if (Vector2.Distance(bullet.Position, Player.Position) > 100)
                    {
                        bullet.isVisible = false;
                    }
                }
                for (int i = 0; i < cannonBullets.Count; i++)
                {
                    if (!cannonBullets[i].isVisible)
                    {
                        cannonBullets.RemoveAt(i);
                        i--;
                    }
                }
            }
            
            if (HandleCollision.PerPixesCollision(ref Player.Rectangle, rectangle, texture, ref Player.Velocity, ref Player.Position))
            {
                Player.CanJump = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(cannonTexture, cannonPosition, null, Color.White, cannonRotation, Vector2.Zero, 1, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, Position, new Color(90,150,250));
        }

        public void Shoot()
        {
            
        }

    }
}
