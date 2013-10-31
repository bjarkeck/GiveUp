using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Core
{
    class Player : Actor
    {
        public Player(Texture2D texture, Vector2 position, CollisionType collisionType)
            : base(texture, position, collisionType)
        {
        }

        public float startJumpSpeed;
        public int health;
    }
}
