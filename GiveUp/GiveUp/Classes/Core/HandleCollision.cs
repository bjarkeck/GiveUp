using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GiveUp.Classes.Core
{
    public static class HandleCollision
    {

        private static bool isBelowOf(this Rectangle player, Rectangle box, Vector2 velocity)
        {
            return velocity.Y < 0 &&
                    player.Top - velocity.Y + 1.1f > box.Bottom &&
                    player.Intersects(
                    new Rectangle(
                        box.X + (int)Math.Ceiling(velocity.X < 0 ? velocity.X : 0),
                        box.Y + (int)Math.Ceiling(velocity.Y < 0 ? velocity.Y : 0),
                        box.Width + (int)Math.Abs(velocity.X),
                        box.Height + (int)Math.Abs(velocity.Y)
                    ));
        }
        private static bool isRightOf(this Rectangle player, Rectangle box, Vector2 velocity)
        {
            return velocity.X < 0 &&
                    player.Left - velocity.X + 1 >= box.Right &&
                    player.Intersects(
                        new Rectangle(
                            box.X + (int)Math.Ceiling(velocity.X < 0 ? velocity.X : 0),
                            box.Y + (int)Math.Ceiling(velocity.Y < 0 ? velocity.Y : 0),
                            box.Width + (int)Math.Abs(velocity.X),
                            box.Height + (int)Math.Abs(velocity.Y)
                        )
                    );
        }

        public static bool IsLeftOf(this Rectangle player, Rectangle box, Vector2 velocity)
        {
            return
                    velocity.X > 0 &&
                    player.Right - velocity.X - 1 <= box.Left &&
                    player.Intersects(
                        new Rectangle(
                            box.X + (int)Math.Ceiling(velocity.X < 0 ? velocity.X : 0),
                            box.Y + (int)Math.Ceiling(velocity.Y < 0 ? velocity.Y : 0),
                            box.Width + (int)Math.Abs(velocity.X),
                            box.Height + (int)Math.Abs(velocity.Y)
                        )
                    );
        }
        public static bool IsOnTopOf(this Rectangle player, Rectangle box, Vector2 velocity)
        {
            return velocity.Y > 0 &&
                    player.Bottom - velocity.Y - 1.1f < box.Top &&
                    player.Intersects(
                        new Rectangle(
                            box.X + (int)Math.Ceiling(velocity.X < 0 ? velocity.X : 0),
                            box.Y - 1 + (int)Math.Ceiling(velocity.Y < 0 ? velocity.Y : 0),
                            box.Width + (int)Math.Abs(velocity.X),
                            box.Height + (int)Math.Abs(velocity.Y)
                        ));
        }
        public static bool IsRightOf(this Rectangle player, Rectangle box, Vector2 velocity)
        {
            if (player.isBelowOf(box, velocity))
                return false;
            return isRightOf(player, box, velocity);
        }
        public static bool IsBelowOf(this Rectangle player, Rectangle box, Vector2 velocity)
        {
            if (player.isRightOf(box, velocity))
                return false;

            return isBelowOf(player, box, velocity);
        }

        public static bool IsOnTopOf(ref Rectangle player, Rectangle box, ref Vector2 velocity, ref  Vector2 playerPosition, bool positionBoxes = true)
        {
            bool rtn = player.IsOnTopOf(box, velocity);
            if (positionBoxes && rtn)
            {
                velocity.Y = 0;
                playerPosition.Y = box.Y - player.Height;
                player.Y = (int)playerPosition.Y;
            }
            return rtn;
        }
        public static bool IsRightOf(ref Rectangle player, Rectangle box, ref Vector2 velocity, ref  Vector2 playerPosition, bool positionBoxes = true)
        {
            bool rtn = player.IsRightOf(box, velocity);
            if (positionBoxes && rtn)
            {
                velocity.X = 0;
                playerPosition.X = box.Right;
                player.X = (int)playerPosition.X;
            }
            return rtn;
        }
        public static bool IsLeftOf(ref Rectangle player, Rectangle box, ref Vector2 velocity, ref  Vector2 playerPosition, bool positionBoxes = true)
        {
            bool rtn = player.IsLeftOf(box, velocity);
            if (positionBoxes && rtn)
            {
                velocity.X = 0;
                playerPosition.X = (int)Math.Ceiling((double)(box.Left - player.Width));
                player.X = (int)playerPosition.X;
            }
            return rtn;
        }
        public static bool IsBelowOf(ref Rectangle player, Rectangle box, ref Vector2 velocity, ref  Vector2 playerPosition, bool positionBoxes = true)
        {
            bool rtn = player.IsBelowOf(box, velocity);
            if (positionBoxes && rtn)
            {
                velocity.Y = 0;
                playerPosition.Y = box.Bottom;
                player.Y = (int)playerPosition.Y;
            }
            return rtn;
        }

        public static bool PerPixesCollision(this Rectangle player, Rectangle box, Texture2D boxTexture)
        {
            if (player.Intersects(box))
            {
                Color[] imageData = new Color[boxTexture.Width * boxTexture.Height];
                boxTexture.GetData<Color>(imageData);

                int xStart = player.X < box.X ? 0 : player.X - box.X;
                int xEnd = player.Right > box.Right ? box.Width : player.Right - box.X;
                int yStart = player.Y < box.Y ? 0 : player.Y - box.Y;
                int yEnd = player.Bottom > box.Bottom ? box.Height : player.Bottom - box.Y;

                for (int x = xStart; x < xEnd; x++)
                {
                    for (int y = yStart; y < yEnd; y++)
                    {
                        if (
                            imageData[x + y * boxTexture.Width].A > 0
                            && player.X <= x + box.X && player.Right >= x + box.X
                            && player.Y <= y + box.Y && player.Bottom >= y + box.Y
                        )
                        {
                            return true;
                        }
                    }
                }

            }
            return false;
        }

    }
}
