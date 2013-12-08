using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Core
{
    public static class MouseHelper
    {
        public static Vector2 Position
        {
            get
            {
                return new Vector2((Mouse.GetState().X * (1600f / GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)),(Mouse.GetState().Y * (900f / GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)));
            }
        }

        public static Point ToPoint(this Vector2 v)
        {
            return new Point((int)v.X, (int)v.Y);
        }
    }
}
