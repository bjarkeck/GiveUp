using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiveUp.Classes.Core;
using GiveUp.Classes.Screens;
using GiveUp.Classes.GameObjects.Obstacles;

namespace GiveUp.Classes.GameObjects.Obstacles
{
    class MovingWall : GameObject, IGameObject
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position;
        public Rectangle rectangle;
        private float speed;

        public const char TileChar = 'K';

        public override void Initialize(ContentManager content, Vector2 position)
        {
            speed = 0.5f;
            Position = new Vector2(position.X, position.Y);
            Texture = content.Load<Texture2D>("Images/Tiles/ground");
            rectangle = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        public void Movement()
        {
            if (GetAllGameObjects<MovingWallActivationTile>().First().WallActivated == true)
            {
                Position.X += speed;
                rectangle.X = (int)Position.X;
            }
        }

        public override void Update(GameTime gameTime)
        {
            Movement();
        }


        public override void CollisionLogic()
        {
            if (Player.Rectangle.Intersects(rectangle))
            {
                //LORT>
                Player.Animation.PlayAnimation("stand");
                if (Player.Position.X < this.Position.X + 28)
                {
                    Player.Position.X = this.Position.X + 28 ;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
