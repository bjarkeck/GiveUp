using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.GameObjects.Obstacles.Cannon
{
                         //Nope, den skal ikke nedarve fra noget, alt den har behov for den foræret af AttachedCannon.
    class CannonBullet : GameObject, IGameObject
    {
        //yeah
        Texture2D texture;
        //yeah
        Vector2 velocity;

        //yeah
        public CannonBullet(Texture2D texture, Vector2 startPosition, float shootAngle, float bulletSpeed)
        {
            this.texture = texture;
            this.Position = startPosition;
            this.velocity = new Vector2((float)Math.Cos(shootAngle) * bulletSpeed, (float)Math.Sin(shootAngle) * bulletSpeed);
        }

        //Så dvs. at Update ikke skal overides, men bare være en normal metode..
        public override void Update(GameTime gameTime)
        {
            //Og så er der ikke nogen base.Update at kalde, så den linje skal bare slettes.
            base.Update(gameTime);


            //Her tilføjer du så velocitien til position som fx:
            //this.position += velocity
        }


        //yeah
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}
