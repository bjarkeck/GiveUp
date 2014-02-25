using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public static class Time
    {
        public static float GameSpeed = 1f;

        public static float DeltaTime(this GameTime gameTime)
        {
            return GameSpeed * (float)gameTime.ElapsedGameTime.Milliseconds / 16f;
        }
    }
