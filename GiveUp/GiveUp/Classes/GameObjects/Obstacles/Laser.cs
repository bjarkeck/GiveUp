using GiveUp.Classes.GameObjects.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiveUp.Classes;
using GiveUp.Classes.Core;

namespace GiveUp.Classes.GameObjects.Obstacles
{
    public class Laser : GameObject, IGameObject
    {
        Texture2D texture;
        List<Rectangle> boxTiles;
        Random r = new Random();


        ParticleEmitter laserBeam;
        ParticleEmitter smoke;

        public const char TileChar = 'l';


        public override void Initialize(ContentManager content, Vector2 position)
        {

            List<ParticleTexture> list = new List<ParticleTexture>();
            List<ParticleTexture> list1 = new List<ParticleTexture>();
            list1.Add(new ParticleTexture(content.Load<Texture2D>("Images/Obstacles/laserParticle"), new Color(Color.White, 1f), new Color(Color.White, 0.00f), 1f, 0f));

            laserBeam = new ParticleEmitter(
                list1,
                new Range<float>(1, 2),
                new Range<float>(-1, 1),
                new Range<int>(300, 500),
                0,
                360,
                11000,
                10000,
                Vector2.Zero,
                Vector2.Zero);

            smoke = new ParticleEmitter(
                list1,
                new Range<float>(1, 2),
                new Range<float>(-1, 1),
                new Range<int>(300, 500),
                0,
                360,
                11000,
                10000,
                Vector2.Zero,
                Vector2.Zero);


            boxTiles = GetAllGameObjects<BoxTile>().Select(x => x.Rectangle).ToList();
            if (boxTiles.Any(x => x.X == position.X && x.Y == position.Y + 32))
            {
                texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonBodyT");
                position.Y += 32 - texture.Height;
            }
            else if (boxTiles.Any(x => x.X == position.X && x.Y == position.Y - 32))
            {
                texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonBodyB");
            }
            else if (boxTiles.Any(x => x.X == position.X - 32 && x.Y == position.Y))
            {
                texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonBodyR");

            }
            else if (boxTiles.Any(x => x.X == position.X + 32 && x.Y == position.Y))
            {
                texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonBodyL");
                position.X += 32 - texture.Width;
            }
            else
            {
                //Hvis den flyver i luften
                texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/AttatchedCannonBodyL");
            }
            this.Rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public override void Update(GameTime gameTime)
        {

            ;

            laserBeam.Update(gameTime, new Rectangle(Rectangle.X, Rectangle.Y, 400, 3));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            laserBeam.Draw(spriteBatch);
            spriteBatch.Draw(texture, Rectangle, new Color(255, 255, 255));
        }

    }
}
