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
    class Sawblade : GameObject, IGameObject
    {
        public const char TileChar = 'B';
        public const byte LoadOrder = 0;
        public Vector2 Position;
        Texture2D texture { get; set; }

        //Hejsa 3

        float rotation = 0.5f;
        float speed = 2.5f;
        int direction = 1;

        Rectangle sawBladeRectangle;
        List<Rectangle> allGameObjects;

        public override void Initialize(ContentManager content, Vector2 position)
        {
            Position = new Vector2(position.X, position.Y);
            texture = content.Load<Texture2D>("Images/Obstacles/sawblade");
            Rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            sawBladeRectangle = new Rectangle((int)position.X, (int)position.Y + 16, texture.Width, texture.Height);
        }


        public override void Update(GameTime gameTime)
        {
            allGameObjects = GetAllGameObjects<GameObject>().Where(x => x.Equals(this) == false).Select(x => x.Rectangle).ToList();
            rotation += 0.2f;
            Position.X += speed * direction;
            Rectangle.X = (int)Position.X;
            sawBladeRectangle.X = (int)Position.X;

        }

        public override void CollisionLogic()
        {
            if (allGameObjects.Any(x => x.Intersects(Rectangle)))
            {
                direction *= -1;
            }


            if (sawBladeRectangle.Intersects(Player.Rectangle))
            {
                LevelManager.RestartLevel();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(Position.X+16 , Position.Y + 32), null, Color.White, rotation, texture.Origin(), 1, SpriteEffects.None, 1);
        }
    }
}