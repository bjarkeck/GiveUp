using Tempus.Classes.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tempus.Classes.GameObjects.Obstacles
{
    public class StartPositionObj : GameObject, IGameObject
    {
        public const char TileChar = 'S';


        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if (Editor.IsEnable)
            {
                spriteBatch.Draw(GameLogic.ColorTexture(Color.Green * 0.5f, spriteBatch), new Rectangle((int)InitialPosition.X, (int)InitialPosition.Y, 32, 32), Color.White);
            }
        }
    }
}
