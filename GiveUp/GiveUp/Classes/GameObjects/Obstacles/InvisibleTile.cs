using Tempus.Classes.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tempus.Classes.GameObjects.Obstacles
{
   public class InvisibleTile : GameObject, IGameObject
    {
       public const char TileChar = 'i';


       public override void Initialize(Microsoft.Xna.Framework.Content.ContentManager content, Microsoft.Xna.Framework.Vector2 position)
       {
           this.Rectangle = new Rectangle((int)position.X, (int)position.Y, 32, 32);

       }

       public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
       {
           if (Editor.IsEnable)
           {
               spriteBatch.Draw(GameLogic.ColorTexture(Color.Red * 0.5f, spriteBatch), new Rectangle((int)InitialPosition.X, (int)InitialPosition.Y, 32, 32), Color.White);
           }
       }
    }
}
