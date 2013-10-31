using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Core
{

    //TODO Tilføj to lister en for "player" og en for "enemys"
    //Skal bruges for at tjekke collision imellem de to
    public class Actor : Sprite
    {
        public Actor(Texture2D texture, Vector2 position, CollisionType collisionType)
            :base(texture,position,collisionType)
        {

        }

        public Actor()
        {
            // TODO: Complete member initialization
        }

        public Vector2 Velocity;
        public float Acceleration;

        

        public virtual void Update(GameTime gameTime)
        {

            //TODO: Handle Collisiion
        }

    }

}
