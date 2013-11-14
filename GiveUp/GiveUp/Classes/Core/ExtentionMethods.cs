using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Core
{
    public static class ExtentionMethods
    {
        public static Vector2 Origin(this Texture2D texture)
        {
            return new Vector2(texture.Width / 2, texture.Height / 2);
        }
    }
}
