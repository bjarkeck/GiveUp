using GiveUp.Classes.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.GameObjects
{
    public class Finish : IGameObject
    {
        public const char TileChar = 'D';

        public void Initialize(ContentManager content, Vector2 position)
        {
            texture = content.Load<Texture2D>("Images/Player/door.png");
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(20,20), Color.White);
        }

        public Texture2D texture { get; set; }
    }
}
