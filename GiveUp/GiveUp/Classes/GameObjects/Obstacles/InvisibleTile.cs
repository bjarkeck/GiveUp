using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.GameObjects.Obstacles
{
   public class InvisibleTile : GameObject, IGameObject
    {
       public const char TileChar = 'i';


       public override void Initialize(Microsoft.Xna.Framework.Content.ContentManager content, Microsoft.Xna.Framework.Vector2 position)
       {
           this.Rectangle = new Rectangle((int)position.X, (int)position.Y, 32, 32);

       }
    }
}
