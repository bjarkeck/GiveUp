using GiveUp.Classes.Core;
using GiveUp.Classes.LevelManager;
using GiveUp.Classes.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Core
{
    public static class GameLogic
    {
        public static Vector2 Origin(this Texture2D texture)
        {
            return new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public static double AngleDegree(this Vector2 startPos, Vector2 target)
        {
            return Math.Atan2(target.Y - startPos.Y, target.X - startPos.X) * (180 / Math.PI);
        }

        public static double AngleRadian(this Vector2 startPos, Vector2 target)
        {
            return Math.Atan2(target.Y - startPos.Y, target.X - startPos.X);
        }
        
        public static bool IsLOS(float AngleDistance, float PositionDistance, Vector2 startPos, Vector2 target, float AngleB)
        {
            float AngleBetween = (float)AngleDegree(startPos, target);
            
            if ((AngleBetween <= (AngleB + (AngleDistance / 2f / 100f))) && (AngleB >= (AngleB - (AngleDistance / 2f / 100f))) && (Vector2.Distance(startPos, target) <= PositionDistance))
            {
                return true;
            }
            else return false;

        }
    }
}

