using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiveUp.Classes.Screens;
using GiveUp.Classes.LevelManager;

namespace GiveUp.Classes.Core
{

    //TODO Tilføj to lister en for "player" og en for "enemys"
    //Skal bruges for at tjekke collision imellem de to
    public class Actor : Sprite
    {
        public Vector2 Velocity;
        public float Acceleration;
        

        public virtual void Update(GameTime gameTime)
        {
            Rectangle.X = (int)Position.X;
            Rectangle.Y = (int)Position.Y;

        }



    }

}
