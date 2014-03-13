using Tempus.Classes.Core;
using Tempus.Classes.GameObjects.Tiles;
using Tempus.Classes.LevelManager;
using Tempus.Classes.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tempus.Classes.Core
{
    public static class GameLogic
    {
        private static Dictionary<Color, Texture2D> textures = new Dictionary<Color, Texture2D>();

        public static Texture2D ColorTexture(Color color, SpriteBatch spriteBatch)
        {
            if (!textures.ContainsKey(color))
            {
                Texture2D t = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                t.SetData(new Color[] { color });
                textures.Add(color, t);
            }
            return textures[color];
        }

        public static string ToTime(this int ms)
        {
            return TimeSpan.FromMilliseconds(ms).ToString(@"mm\:ss\:fff");
        }

        public static double NextDouble(this Random rnd, double min, double max)
        {
            return rnd.NextDouble() * (max - min) + min;
        }

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

        public static BoundingBox ToBoundingBox(this Rectangle item)
        {
            return new BoundingBox(new Vector3(item.X, item.Y, 0), new Vector3(item.X + item.Width, item.Y + item.Height, 0));
        }

        public static Random r = new Random();


        private static string parentLevel = "";


        public static List<Rectangle> tiles = new List<Rectangle>();
        public static void ResetLevelTiles()
        {
            var t = ((GameScreen)(ScreenManager.Current.CurrentScreen)).LevelManager;
            tiles.Clear();
            tiles = t.GameObjects.Where(x => x.GetType().Name == "BoxTile").Select(x => ((BoxTile)x).Rectangle).ToList();
        }

        public static bool IsLineOfSight(Vector2 startPos, Vector2 target, ref float distanceToHit)
        {
            
            if (ScreenManager.Current.CurrentScreen.GetType() == typeof(GameScreen))
            {
            //Distancen fra start til slut...
            distanceToHit = Math.Abs(startPos.Distance(target));


            var t = ((GameScreen)(ScreenManager.Current.CurrentScreen)).LevelManager;

            if (t.CurrentLevel + "." + t.CurrentSubLevel != parentLevel)
            {
                parentLevel = t.CurrentLevel + "." + t.CurrentSubLevel;
                //Get all se tiles
                tiles.Clear();
                tiles = t.GameObjects.Where(x => x.GetType().Name == "BoxTile").Select(x => ((BoxTile)x).Rectangle).ToList();
            }

            //Da Ray er en 3D ting, foregår det i Vector3, hvor vi bare sætter z axen til 0;
            //Tænk på en Ray, som en stråle den har en start position, og en retning...
            //Så for at lave en Ray, skal vi bruge en start position, og en retning.
            //Retningen laver vi ved at  lave en vector3, og trække startPos fra target. og normalize den.
            //Dont ask me why it is like dat.
            Vector3 v = new Vector3(target, 0) - new Vector3(startPos, 0);
            v.Normalize();
            Ray ray = new Ray(new Vector3(startPos.X, startPos.Y, 0), v);


            //Ray castíng bruger blandt andet bounding boxes... Så u stedet for rectangles, så bruger vi bounding boxes!
            List<BoundingBox> boundingBoxes = new List<BoundingBox>();

            //Her konvatere vi alle tiles fra rectangles til boundingboxes (3d, istedet for 2d)
            foreach (var item in tiles)
                //ToBoundingBox er en ExtentionMethod jeg har lavet... Se den hvis du vil se hvordan man konvatere 2dbox til en flad 3dbox.
                boundingBoxes.Add(item.ToBoundingBox());

            //Når vi bruger ray.Intersects metoden, retunere den distancen hen til den box man chekker, rammen den ikke boxen, returnere den null
            //Rammer den boxen, retunere den distancen hen til den

            bool hitFound = false;

            foreach (var item in boundingBoxes)
            {
                //Inde i den metoden foregår magien!
                //? tegnet, betyder bare at floaten godt må være null (? = nullable)
                float? distance = ray.Intersects(item);

                //Hvis den har ramt nået, har vi altså fået en distance. og der er nu en box i vejen, for at komme hen til target...
                if (distance != null)
                {
                    //Men for at tjekke at boksen ikke ligger på den anden sidde af target, tjekker vi lige at der er kortere afstand til boksen den har remt, end target vi prøver at komme hen til.
                    if (distance < distanceToHit)
                    {
                        //Siden distance er nullale er det lidt fucked, at få den ind i en normal float. da en normal float ikke må være null...
                        //jeg gjorde såen der: der er sikkert en smartere måde!
                        distanceToHit = (float)Convert.ToDouble(distance.ToString());
                        //Og så sørger vi også lige for at distancen altid er positiv.. (Hvis den rammer noget til venstre, er dne minus, til højre plus)
                        distanceToHit = Math.Abs(distanceToHit);
                        //Vi har altså ramt nået.
                        hitFound = true;
                    }
                }
            }

            if (hitFound)
                return false;

            //Vi har ikke ramt noget, target er i LineOfSight
            return true;
            }
            return false;
        }

        public static bool IsLineOfSight(Vector2 startPos, Vector2 target)
        {
            float nothing = 0;
            return IsLineOfSight(startPos, target, ref nothing);
        }

        public static float CurveAngle(float from, float to, float step)
        {
            if (step == 0) return from;
            if (from == to || step == 1) return to;

            Vector2 fromVector = new Vector2((float)Math.Cos(from), (float)Math.Sin(from));
            Vector2 toVector = new Vector2((float)Math.Cos(to), (float)Math.Sin(to));

            Vector2 currentVector = Slerp(fromVector, toVector, step);

            return (float)Math.Atan2(currentVector.Y, currentVector.X);
        }
        public static Vector2 Slerp(Vector2 from, Vector2 to, float step)
        {
            if (step == 0) return from;
            if (from == to || step == 1) return to;

            double theta = Math.Acos(Vector2.Dot(from, to));
            if (theta == 0) return to;

            double sinTheta = Math.Sin(theta);
            return (float)(Math.Sin((1 - step) * theta) / sinTheta) * from + (float)(Math.Sin(step * theta) / sinTheta) * to;
        }
    }
}


