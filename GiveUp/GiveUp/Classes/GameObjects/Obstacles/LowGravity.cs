using Tempus.Classes.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tempus.Classes.GameObjects.Obstacles
{
    public class LowGravity : GameObject, IGameObject
    {
        public const char TileChar = 'o';


        public override void Initialize(Microsoft.Xna.Framework.Content.ContentManager content, Microsoft.Xna.Framework.Vector2 position)
        {
            Player.Gravity = new Microsoft.Xna.Framework.Vector2(0, 0.2f);
        }


        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if (Editor.IsEnable)
            {
                spriteBatch.Draw(GameLogic.ColorTexture(Color.Blue * 0.5f, spriteBatch), new Rectangle((int)InitialPosition.X, (int)InitialPosition.Y, 32, 32), Color.White);
            }
        }

    }
}
