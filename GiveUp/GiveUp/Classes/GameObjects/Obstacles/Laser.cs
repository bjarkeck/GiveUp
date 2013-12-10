using GiveUp.Classes.GameObjects.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiveUp.Classes;
using GiveUp.Classes.Core;

namespace GiveUp.Classes.GameObjects.Obstacles
{
    public class Laser : GameObject, IGameObject
    {
        Texture2D texture;
        Random r = GameLogic.r;
        ParticleEmitter laserBeam;
        ParticleEmitter warningBeam;
        double cannonDirection;
        Vector2 cannonPosition;
        private SpriteEffects spriteEffect;
        float rotation = 0;
        Direction dir = Direction.None;
        float range = 0;
        bool showBeam = false;
        bool showWarning = false;
        int timeBeforeBeem = 5000;
        int timeBeamDuration = 300;
        int timeBeforeWarning = 4000;
        int timer;

        public const char TileChar = 'l';

        public override void Initialize(ContentManager content, Vector2 position)
        {
            this.texture = content.Load<Texture2D>("Images/Obstacles/HeatSeeking/body");

            timer = r.Next(0, timeBeforeBeem + timeBeamDuration);

            if (GetAllGameObjects<BoxTile>().Any(x => x.Rectangle.X == position.X - 32 && x.Rectangle.Y == position.Y))
            {
                this.Rectangle = new Rectangle((int)position.X - 6, (int)position.Y, 19, 32);
                this.cannonPosition = new Vector2(Rectangle.X + 17 - 6 + 6, Rectangle.Y + 15);
                this.cannonDirection = Vector2.Zero.AngleRadian(new Vector2(1, 0));
                dir = Direction.Right;
                GameLogic.IsLineOfSight(1900, cannonPosition, new Rectangle(Rectangle.X + 1600, Rectangle.Y, 32, 32), out range, 5);
            }
            else if (GetAllGameObjects<BoxTile>().Any(x => x.Rectangle.X == position.X + 32 && x.Rectangle.Y == position.Y))
            {
                this.Rectangle = new Rectangle((int)position.X + 32 - 13 , (int)position.Y, 19, 32);
                spriteEffect = SpriteEffects.FlipHorizontally;
                this.cannonPosition = new Vector2(Rectangle.X - 4 + 6 - 6, Rectangle.Y + 15);
                this.cannonDirection = Vector2.Zero.AngleRadian(new Vector2(-1, 0));
                dir = Direction.Left;
                GameLogic.IsLineOfSight(1900, cannonPosition, new Rectangle(Rectangle.X - 1600, Rectangle.Y, 32, 32), out range, 5);
            }
            else if (GetAllGameObjects<BoxTile>().Any(x => x.Rectangle.X == position.X && x.Rectangle.Y == position.Y + 32))
            {
                this.Rectangle = new Rectangle((int)position.X, (int)position.Y + 6 + 32, 19, 32);
                this.cannonPosition = new Vector2(Rectangle.X + 15, Rectangle.Y - 11 - 6);
                this.cannonDirection = Vector2.Zero.AngleRadian(new Vector2(0, -1));
                rotation = (float)cannonDirection;
                dir = Direction.Top;
                GameLogic.IsLineOfSight(1900, cannonPosition, new Rectangle(Rectangle.X, Rectangle.Y - 900, 32, 32), out range, 5);
            }
            else if (GetAllGameObjects<BoxTile>().Any(x => x.Rectangle.X == position.X && x.Rectangle.Y == position.Y - 32))
            {
                this.Rectangle = new Rectangle((int)position.X + 32, (int)position.Y - 6, 19, 32);
                this.cannonPosition = new Vector2(Rectangle.X - 17, Rectangle.Y + 6 + 6);
                this.cannonDirection = Vector2.Zero.AngleRadian(new Vector2(0, 1));
                rotation = (float)cannonDirection;
                dir = Direction.Bottom;
                GameLogic.IsLineOfSight(1900, cannonPosition, new Rectangle(Rectangle.X, Rectangle.Y + 900, 32, 32), out range, 5);
            }
            range -= 15;
            addParticles(content);
        }

        private void addParticles(ContentManager content)
        {
            List<ParticleTexture> laserList = new List<ParticleTexture>();
            laserList.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/beamParticle" + ((dir == Direction.Top || dir == Direction.Bottom) ? "Vertical" : "Horisontal")), new Color(Color.Navy, 0.7f), new Color(Color.OldLace, 0f), 0.2f, 0.2f));
            laserList.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/beamParticle" + ((dir == Direction.Top || dir == Direction.Bottom) ? "Vertical" : "Horisontal")), new Color(Color.OldLace, 0.2f), new Color(Color.Navy, 0f), 0.2f, 0.2f));
            laserBeam = new ParticleEmitter(
                laserList,
                new Range<float>(4,20),
                new Range<float>(0, 0),
                new Range<int>(timeBeamDuration - (timeBeamDuration/20), timeBeamDuration),
                (dir == Direction.Top || dir == Direction.Bottom) ? 0 : 90,
                360,
                10 * (int)range,
                10 * (int)range,
                Vector2.Zero,
                Vector2.Zero);

            List<ParticleTexture> warningList = new List<ParticleTexture>();
            warningList.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/beamParticle" + ((dir == Direction.Top || dir == Direction.Bottom) ? "Vertical" : "Horisontal")), new Color(Color.White, 0.03f), new Color(Color.White, 0f), 0.3f, 0.1f));

            warningBeam = new ParticleEmitter(
                warningList,
                new Range<float>(4),
                new Range<float>(0, 0),
                new Range<int>((timeBeforeBeem - timeBeforeWarning) / 2, (int)((timeBeforeBeem - timeBeforeWarning)/1.3f)),
                (dir == Direction.Top || dir == Direction.Bottom) ? 0 : 90,
                3,
                10 * (int)range,
                (int)range,
                Vector2.Zero,
                Vector2.Zero);

        }

        public override void Update(GameTime gameTime)
        {
            switch (dir)
            {
                case Direction.Top:
                    laserBeam.Update(gameTime, new Rectangle((int)cannonPosition.X, (int)cannonPosition.Y - (int)range, 3, (int)range));
                    warningBeam.Update(gameTime, new Rectangle((int)cannonPosition.X, (int)cannonPosition.Y - (int)range, 3, (int)range));
                    break;
                case Direction.Bottom:
                    laserBeam.Update(gameTime, new Rectangle((int)cannonPosition.X, (int)cannonPosition.Y, 3, (int)range));
                    warningBeam.Update(gameTime, new Rectangle((int)cannonPosition.X, (int)cannonPosition.Y, 3, (int)range));
                    break;
                case Direction.Left:
                    laserBeam.Update(gameTime, new Rectangle((int)cannonPosition.X - (int)range, (int)cannonPosition.Y, (int)range, 3));
                    warningBeam.Update(gameTime, new Rectangle((int)cannonPosition.X - (int)range, (int)cannonPosition.Y, (int)range, 3));
                    break;
                case Direction.Right:
                    laserBeam.Update(gameTime, new Rectangle((int)cannonPosition.X, (int)cannonPosition.Y, (int)range, 3));
                    warningBeam.Update(gameTime, new Rectangle((int)cannonPosition.X, (int)cannonPosition.Y, (int)range, 3));
                    break;
            }


            showWarning = (timer > timeBeforeWarning && timer < timeBeforeBeem);
            showBeam = (timer > timeBeforeBeem);

            laserBeam.MaxNumberOfParitcles = showBeam ? 10 * (int)range : 0;
            warningBeam.MaxNumberOfParitcles = showWarning ? 20000 : 0;

            timer += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > timeBeforeBeem + timeBeamDuration)
                timer = 0;

            if (timer > timeBeforeBeem && timer < timeBeforeBeem + timeBeamDuration)
            {
                if (GameLogic.IsLineOfSight(range + 50, cannonPosition, Player.Rectangle, (float)cannonDirection))
                {
                    LevelManager.RestartLevel();
                }
            }
            else if (timer > timeBeforeWarning)
            {
                var test = "ASDA";
            }


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, null, Color.Orange, rotation, Vector2.Zero, spriteEffect, 0f);
        }

        public override void DrawAdditive(SpriteBatch spriteBatch)
        {
            laserBeam.Draw(spriteBatch);
            warningBeam.Draw(spriteBatch);
        }

    }
}
