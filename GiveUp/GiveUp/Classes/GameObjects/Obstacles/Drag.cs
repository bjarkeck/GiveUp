using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiveUp.Classes.Core;
using GiveUp.Classes.Screens;

namespace GiveUp.Classes.Core
{
    class Drag : GameObject, IGameObject
    {
        public Texture2D texture { get; set; }

        public const char TileChar = 'Q';
        public const byte LoadOrder = 3;

        public Vector2 Position { get; set; }

        public override void Initialize(ContentManager content, Vector2 position)
        {
            Position = new Vector2(position.X, position.Y);
            texture = content.Load<Texture2D>("Images/Obstacles/DragActivationTest");
            Player.Gravity.X += 0.4f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }

    }
}