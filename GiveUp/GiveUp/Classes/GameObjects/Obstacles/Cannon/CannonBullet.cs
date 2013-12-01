using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.GameObjects.Obstacles.Cannon
{
    class CannonBullet : GameObject, IGameObject
    {
        Texture2D texture;
        Vector2 velocity;

        public CannonBullet(Texture2D texture, Vector2 startPosition, float shootAngle, float bulletSpeed)
        {
            this.texture = texture;
            this.Position = startPosition;
            this.velocity = new Vector2((float)Math.Cos(shootAngle) * bulletSpeed, (float)Math.Sin(shootAngle) * bulletSpeed);
        }

        //Sidden dette representere 1 bullet, er det enste der skal ske her, er at opdatere kuglens position.
        //Om den har ramt muren eller ej... vent med det. start med at få den til at skyde.
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}

//Tip:
//ha en liste af denne class på attachedCannon
//Og tilføj til listen når dne skal skyde. og husk at foreache igennem liste i både update og draw.