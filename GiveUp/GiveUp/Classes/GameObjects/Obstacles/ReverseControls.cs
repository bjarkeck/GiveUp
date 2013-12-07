using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiveUp.Classes.Core;
using GiveUp.Classes.Screens;

namespace GiveUp.Classes.GameObjects.Obstacles
{
    class ReverseControls : GameObject, IGameObject
    {
        public const char TileChar = 'F';

        public override void Initialize(ContentManager content, Vector2 position)
        {
            Player.ReverseControls = true;
        }

    }
}