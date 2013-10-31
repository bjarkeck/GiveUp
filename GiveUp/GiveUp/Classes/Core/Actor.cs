using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Core
{
    public class Actor : Sprite
    {
        public Actor(Texture2D texture, Vector2 position, CollisionType collisionType)
            :base(texture,position,collisionType)
        {

        }

        public Vector2 Velocity { get; set; }

        public virtual void Update(GameTime gameTime)
        {

            //TODO: Handle Collisiion
        }

    }

}
