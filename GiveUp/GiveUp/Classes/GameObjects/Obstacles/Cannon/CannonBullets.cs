using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.GameObjects.Obstacles.Cannon
{
    //CannonBullets skal være i ental (CannonBullet) da en klasse representere 1 bullet
                          //wtf?^^ væk med den
    class CannonBullets : AttachedCannon
    {
        Texture2D texture { get; set; }
        Rectangle rectangle;
        
        List<CannonBullets> cannonBullets = new List<CannonBullets>();

        //en velocity har x og y - velocitty skal være vector2
        float velocity = 10;

        //is visivle? Enten findes de eller også så findes de ikke :p
        bool isVisible = false;

        //bullets bliver initaliseret fra AttachedCannon, og billedet til bullet ville jeg ligge i attachedCannon, så jeg ville lave en constructer til det. fx denne constructer:
        //
        //public CannonBullets(Texture texture, Vector2 startPosition, float shootAngle, float bulletSpeed)
        //{
        //    this.texture = texture;
        //    this.position = startPosition;
        //    this.velocity = new Vector2((float)Math.Cos(shootAngle) * bulletSpeed, (float)Math.Sin(shootAngle) * bulletSpeed);
        //}

        //væk med den.
        public override void Initialize(ContentManager content, Vector2 position)
        {
            texture = content.Load<Texture2D>("Images/Obstacles/AttachedCannon/bullet");
            rectangle = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
        }

        //Sidden dette representere 1 bullet, er det enste der skal ske her, er at opdatere kuglens position.
        //Om den har ramt muren eller ej... vent med det. start med at få den til at skyde.
        public override void Update(GameTime gameTime)
        {
            foreach (CannonBullets bullet in cannonBullets)
            {
                //bullet.position += bullet.velocity;
                if (Vector2.Distance(bullet.Position, Player.Position) > 1000)
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

        //Well som sagt, det er et skud.. så bare fjerne den her metode.
        public void Shoot()
        {
            CannonBullets newBullet = new CannonBullets();
            newBullet.Position = cannonPosition;
            newBullet.isVisible = true;
        }

        //Den skal bare tegne sig selv. så ikke noget foreach
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (CannonBullets bullet in cannonBullets)
                bullet.Draw(spriteBatch);
        }
    }
}

//Tip:
//ha en liste af denne class på attachedCannon
//Og tilføj til listen når dne skal skyde. og husk at foreache igennem liste i både update og draw.