using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.GameObjects.Obstacles
{
    public class LowGravity : GameObject, IGameObject
    {
        public const char TileChar = 'o';


        public override void Initialize(Microsoft.Xna.Framework.Content.ContentManager content, Microsoft.Xna.Framework.Vector2 position)
        {
            Player.Gravity = new Microsoft.Xna.Framework.Vector2(0, 0.2f);
        }

    }
}
