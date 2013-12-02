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
        //Yeah
        Texture2D bulletTexture;
        Texture2D cannonTexture;

        //Umidlbart kan jeg ikke se nogle grund til denne. læser det fra toppen så kan være jeg får en aaah oplevelse senere^^
        Rectangle cannonBulletRectangle;
        Rectangle rectangle { get; set; }

        //En bullet position? Det er måske der den bliver fyrret afsted fra?
        public Vector2 bulletPosition;
        public Vector2 cannonPosition;
        float minRotation = 1.3033f;
        float maxRotation = 4.96f;
        float cannonRotation = 10;
        //Ikke nødvendigt dat det er den sammme rotation som cannonRotation.
        float bulletRotation = 0;

        List<CannonBullet> cannonBullets = new List<CannonBullet>();

        public override void Initialize(ContentManager content, Vector2 position)
        {
            bulletTexture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/bullet");

            List<Rectangle> boxTilesboxTiles = LevelManager.GameObjects.Where(x => x.GetType().Name == "BoxTile").Select(x => ((BoxTile)x).Rectangle).ToList();
            var boxTile = LevelManager.GameObjects.Where(x => x.GetType().Name == "BoxTile").Select(x => ((BoxTile)x).Position);
            cannonTexture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonCannon");

            if (boxTile.Any(x => x.X == position.X && x.Y == position.Y + 32))
            {
                texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonBodyT");
                position.Y += 32 - texture.Height;

                cannonPosition = new Vector2(position.X + 16, position.Y + 7);
                maxRotation -= 1.5707f;
                minRotation -= 1.5707f;
            }
            else if (boxTile.Any(x => x.X == position.X && x.Y == position.Y - 32))
            {
                texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonBodyB");
                cannonPosition = new Vector2(position.X + 16, position.Y);
                maxRotation += 1.5707f;
                minRotation += 1.5707f;
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
            if (GameLogic.IsLineOfSight(10200, cannonPosition, Player.Rectangle))
            {
                float rotation = (float)Math.Atan2(
                    Convert.ToDouble(cannonPosition.Y - (Player.Rectangle.Origin().Y))
                    ,
                    Convert.ToDouble(cannonPosition.X - (Player.Rectangle.Origin().X))
                    ) + 3.1416f;
                if (rotation < minRotation || rotation > maxRotation)
                    cannonRotation = rotation;

                
                //Da bulletposition er det samme som cannon position, hvorfor så ikke bare bruger cannonPosition?
                //Fjern bulletposition.
                bulletPosition = new Vector2(cannonPosition.X, cannonPosition.Y);
                cannonBulletRectangle = new Rectangle((int)bulletPosition.X, (int)bulletPosition.Y, 2, 2);

                //Her bruger du også kanonens rotation, som jeg også havde gjordt, dvs der ikke er brug for "bulletRotation"
                //Ud over det, skydder den rigtig, rigtig hurtig nu, så der skal nok en fireRate på når du har fået det til at virke :p
                cannonBullets.Add(new CannonBullet(bulletTexture, bulletPosition, rotation, 10));
            }

            //Det er kuglens ansvar at tjekke om kuglen har ramt player, så bare kør et foreachloop hvor du updater alle bullets, mere skal der ikke ske her..
            //Tjek bullet class for kommentar der..
            //Så grunden til at kuglerne ikke flytter sig, er fordi at du ikke updater alle bullets..
            //For lige at gentage mig selv^^

            //foreach (var bullet in cannonBullets)
            //    bullet.Update(gameTime, Player, LevelManager);

            foreach (var bullet in cannonBullets)
            {
                if (cannonBulletRectangle.Intersects(Player.Rectangle))
                {
                    LevelManager.RestartLevel();
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
            spriteBatch.Draw(texture, Position, new Color(90, 150, 250));


            //Der behøves du ikke bulletRotation, hvis du gerne vil scale den til 0.5f så gør billedet mindre i stedet..
            //Men den her draw skal du slet ikke bruge, da det er alle bullets der skal tegnes, og de allerede har en draw metode.
            //foreach (CannonBullet bullet in cannonBullets)
            //    bullet.Draw(spriteBatch);
            spriteBatch.Draw(bulletTexture, bulletPosition, null, Color.White, bulletRotation, Vector2.Zero, 0.5f, SpriteEffects.None, 1);
        }
    }
}
