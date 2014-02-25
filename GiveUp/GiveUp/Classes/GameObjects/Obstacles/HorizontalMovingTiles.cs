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
    class HorizontalMovingTiles : GameObject, IGameObject
    {
        public const char TileChar = 'N';
        public const byte LoadOrder = 0;
        public Vector2 Position;
        public Direction LastCollisionDirection = Direction.None;
        Texture2D texture { get; set; }

        int speed = 2;
        public int direction = 1;

        Rectangle movingTileRectangle;
        List<Rectangle> allGameObjects;
        List<HorizontalMovingTiles> movingTileObjects;

        public override void Initialize(ContentManager content, Vector2 position)
        {
            Position = new Vector2(position.X, position.Y);
            texture = content.Load<Texture2D>("Images/Obstacles/movingTile");
            Rectangle = new Rectangle((int)position.X, (int)position.Y + 32, 32, 6);

            allGameObjects = GetAllGameObjects<GameObject>().Where(x => x.GetType() != typeof(HorizontalMovingTiles)).Select(x => x.Rectangle).ToList();
            movingTileObjects = GetAllGameObjects<GameObject>().Where(x => x.GetType() == typeof(HorizontalMovingTiles)).Select(x => (HorizontalMovingTiles)x).ToList();

        }


        public override void Update(GameTime gameTime)
        {

            CollisionLogicc(gameTime);

            Position.X += speed * direction * gameTime.DeltaTime();
            Rectangle.X = (int)Position.X;
            //movingTileRectangle.X = (int)Position.X;

        }

        private int prevDirection = 1;

        public void CollisionLogicc(GameTime gameTime)
        {
            LastCollisionDirection = Direction.None;
            bool isOnTopOf = HandleCollision.IsOnTopOf(ref Player.Rectangle, Rectangle, ref Player.Velocity, ref Player.Position);
            if (isOnTopOf)
            {
                LastCollisionDirection = Direction.Top;
                Player.Position.X += (int)(speed * direction) * gameTime.DeltaTime();
                Player.CanJump = true;
            }

            if (allGameObjects.Any(x => x.Intersects(new Rectangle(Rectangle.X, Rectangle.Y - 4, Rectangle.Width, Rectangle.Height))))
            {
                direction *= -1;

                //Find alle moving tiles til højre, og stop while loopet hvis der ikke findes nogen til højre.
                float currentX = this.InitialPosition.X;
                while (true)
                {
                    HorizontalMovingTiles rightTile = movingTileObjects.FirstOrDefault(x => x.InitialPosition.X == currentX + 32 && x.InitialPosition.Y == this.InitialPosition.Y);
                    if (rightTile == null)
                        break;
                    else
                    {
                        rightTile.direction *= -1;

                    }

                    currentX += 32;
                }

                //Find alle moving tiles til venstre, og stop while loopet hvis der ikke findes nogen til venstre.
                currentX = this.InitialPosition.X;
                while (true)
                {
                    HorizontalMovingTiles leftTile = movingTileObjects.FirstOrDefault(x => x.InitialPosition.X == currentX - 32 && x.InitialPosition.Y == this.InitialPosition.Y);
                    if (leftTile == null)
                        break;
                    else
                    {
                        leftTile.direction *= -1;

                        if (direction < 0)
                        {
                            leftTile.Update(gameTime);
                            leftTile.Update(gameTime);
                        }
                    }
                    currentX -= 32;
                }

                if (isOnTopOf)
                {
                    if (prevDirection != direction)
                    {
                        Player.Position.X += (int)((speed * direction) * gameTime.DeltaTime() * 2);

                    }
                    Player.Position.X += (int)((speed * direction) * gameTime.DeltaTime() * 2);
                }
            }

            prevDirection = direction;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            if (Editor.IsEnable)
            {
                spriteBatch.Draw(GameLogic.ColorTexture(Color.Aqua * 0.4f, spriteBatch), new Rectangle((int)InitialPosition.X, (int)InitialPosition.Y, 32, 32), Color.White);
            }
            spriteBatch.Draw(texture, new Rectangle(Rectangle.X, Rectangle.Y - 32 + 6, Rectangle.Width, 32), Color.White);
        }
    }
}