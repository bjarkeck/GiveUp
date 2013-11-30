﻿using GiveUp.Classes.Core;
using GiveUp.Classes.GameObjects.Tiles;
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
        public static Vector2 Origin(this Rectangle rect)
        {
            return new Vector2(rect.Width / 2 + rect.X, rect.Height / 2 + rect.Y);
        }

        public static double AngleDegree(this Vector2 startPos, Vector2 target)
        {
            return Math.Atan2(target.Y - startPos.Y, target.X - startPos.X) * (180 / Math.PI);
        }

        public static double AngleRadian(this Vector2 startPos, Vector2 target)
        {
            return Math.Atan2(target.Y - startPos.Y, target.X - startPos.X);
        }

        public static float Distance(this Vector2 startPos, Vector2 target)
        {
            return (float)Math.Sqrt(Math.Pow(Math.Abs(startPos.Y - target.Y), 2) + Math.Pow(Math.Abs(startPos.X - target.X), 2));
        }

        /// <summary>
        /// Check line of sight.
        /// </summary>
        /// <param name="distance">Sight range</param>
        /// <param name="startPos"></param>
        /// <param name="target">Fx Player.Rectangle</param>
        public static bool IsLineOfSight(float distance, Vector2 startPos, Rectangle target)
        {
            //Check hvis afstandne mellem startPos og target er længere end distance. og retuner false hvis den er...
            //Ligesom AngleRadian og AngleDegree, må du gerne lave en Distance Extention method ogs også...
            //Hvis du ikke kender til extention methods så google det lige, de er guld vær :)
            if (startPos.Distance(target.Origin()) > distance)
                return false;
           
            List<Rectangle> tiles = ((GameScreen)(ScreenManager.Current.CurrentScreen))
                                        .LevelManager
                                        .GameObjects
                                        .Where(x => x.GetType().Name == "BoxTile")
                                        .Select(x => ((BoxTile)x).Rectangle).ToList();

            //Vores fake bullet vi skydder afsted.
            Vector2 bulletPosition = new Vector2(startPos.X, startPos.Y);
            Rectangle bulletRectangle = new Rectangle((int)startPos.X, (int)startPos.Y, 2, 2);


            //Få rotationen mellem startPos og target
            double rotationToTarget = startPos.AngleRadian(target.Origin());

            //Ud fra rotationen laver vi en velecity som vores check bullet skal flyve med.
            Vector2 bulletVelocity = new Vector2((float)Math.Cos(rotationToTarget * 10), (float)Math.Sin(rotationToTarget * 10));

            //Mens at vores checkbullet ikke kollidere med target, får vi kuglen til at flyve
            while (bulletRectangle.Intersects(target) == false)
            {
                foreach (var item in tiles)
            {
                    if (bulletRectangle.Intersects(item))
                        return false;
                }
                //hvis kuglen intersekter med nogle tiles, så retuner false.

                bulletPosition += bulletVelocity;
                bulletRectangle.X = (int)bulletPosition.X;
                bulletRectangle.Y = (int)bulletPosition.Y;
                //Tilføje kuglens velocity til kuglens position;
            }

            //Hvis den kom igennem loopet betyder det at kuglen har ramt spilleren, og så skal der retuneres true.
            return true;
        }
    }
}

