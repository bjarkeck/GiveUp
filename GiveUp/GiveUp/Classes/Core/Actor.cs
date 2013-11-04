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
        public CollisionType CurrentCollision { get; set; }
        public CollisionDirection CollisionDirection { get; set; }

        
        public Actor(Texture2D texture, CollisionType collisionType)
            :base(texture,collisionType)
        {
            ((GameScreen)ScreenManager.Current.CurrentScreen).LevelManager.Actors.Add(this);
        }

        public Actor()
        {
             ((GameScreen)ScreenManager.Current.CurrentScreen).LevelManager.Actors.Add(this);
        }


        public virtual void Update(GameTime gameTime)
        {
            Rectangle.X = (int)Position.X;
            Rectangle.Y = (int)Position.Y;

        }



    }

}
