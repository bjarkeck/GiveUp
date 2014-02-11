using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.GameObjects
{
    class ButtonActivator : GameObject, IGameObject
    {
        public Vector2 Position;
        public Rectangle rectangle;
        public Texture2D doorLocked;
        public Texture2D doorUnlocked;
        public const byte LoadOrder = 0;

        public const char TileChar = 'K';
        public bool isLocked = false;

        public void Initialize(ContentManager content, Vector2 position)
        {
            Position = position;
            doorLocked = content.Load<Texture2D>("Images/Tiles/buttonActivatorLocked");
            doorUnlocked = content.Load<Texture2D>("Images/Tiles/buttonActivatorUnlocked");
            rectangle = new Rectangle((int)position.X, (int)position.Y, doorLocked.Width, doorLocked.Height);
        }

        public void Update(GameTime gameTime)
        {
            if (isLocked == true)
            {
                doorLocked = doorUnlocked;
            }
        }

        public void CollisionLogic()    
        {
            if (Player.Rectangle.Intersects(rectangle))
            {
                isLocked = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(doorLocked, Position, Color.White);
        }

        public Rectangle Rectangle { get; set; }
    }
}
