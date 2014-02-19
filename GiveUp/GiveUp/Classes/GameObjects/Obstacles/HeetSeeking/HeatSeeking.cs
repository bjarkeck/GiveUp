using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiveUp.Classes;
using GiveUp.Classes.Core;
using GiveUp.Classes.GameObjects.Tiles;

namespace GiveUp.Classes.GameObjects.Obstacles
{
    public class HeatSeeking : GameObject, IGameObject
    {
        public const char TileChar = 'e';

        List<HeatSeekingBullet> bullets = new List<HeatSeekingBullet>();
        Texture2D bodyTexture;
        Texture2D cannonTexture;
        Texture2D missileTexture;
        Vector2 cannonPosition;
        double cannonDirection;
        private SpriteEffects spriteEffect;
        float rotation = 0;

        public override void Initialize(ContentManager content, Vector2 position)
        {
            this.bodyTexture = content.Load<Texture2D>("Images/Obstacles/HeatSeeking/body");
            this.cannonTexture = content.Load<Texture2D>("Images/Obstacles/HeatSeeking/cannon");
            this.missileTexture = content.Load<Texture2D>("Images/Obstacles/HeatSeeking/missile");

            if (GetAllGameObjects<BoxTile>().Any(x => x.Rectangle.X == position.X - 32 && x.Rectangle.Y == position.Y))
            {
                this.Rectangle = new Rectangle((int)position.X - 6, (int)position.Y, 19, 32);
                this.cannonPosition = new Vector2(Rectangle.X + 17 - 6, Rectangle.Y + 15);
                this.cannonDirection = Vector2.Zero.AngleRadian(new Vector2(1, 0));
            }
            else if (GetAllGameObjects<BoxTile>().Any(x => x.Rectangle.X == position.X + 32 && x.Rectangle.Y == position.Y))
            {
                this.Rectangle = new Rectangle((int)position.X + 32 - 13, (int)position.Y, 19, 32);
                spriteEffect = SpriteEffects.FlipHorizontally;
                this.cannonPosition = new Vector2(Rectangle.X - 4 + 6, Rectangle.Y + 16);
                this.cannonDirection = Vector2.Zero.AngleRadian(new Vector2(-1, 0));
            }
            else if (GetAllGameObjects<BoxTile>().Any(x => x.Rectangle.X == position.X && x.Rectangle.Y == position.Y + 32))
            {
                this.Rectangle = new Rectangle((int)position.X, (int)position.Y + 6 + 32, 19, 32);
                this.cannonPosition = new Vector2(Rectangle.X + 15, Rectangle.Y - 11);
                this.cannonDirection = Vector2.Zero.AngleRadian(new Vector2(0, -1));
                rotation = (float)cannonDirection;
            }
            else if (GetAllGameObjects<BoxTile>().Any(x => x.Rectangle.X == position.X && x.Rectangle.Y == position.Y - 32))
            {
                this.Rectangle = new Rectangle((int)position.X + 32, (int)position.Y - 6, 19, 32);
                this.cannonPosition = new Vector2(Rectangle.X - 15, Rectangle.Y + 8);
                this.cannonDirection = Vector2.Zero.AngleRadian(new Vector2(0, 1));
                rotation = (float)cannonDirection;
            }

        }

        public override void Update(GameTime gameTime)
        {

            if (bullets.Count() == 0 && GameLogic.IsLineOfSight(cannonPosition, Player.Rectangle.Origin()))
            {
                bullets.Add(new HeatSeekingBullet(missileTexture, cannonDirection, new Vector2(cannonPosition.X, cannonPosition.Y)));
            }

            foreach (var x in bullets)
                x.Update(gameTime, Player);

            foreach (var item in bullets.ToList())
            {
                if (Player.Rectangle.Intersects(item.Rectangle))
                    LevelManager.RestartLevel();

                foreach (var b in GetAllGameObjects<BoxTile>())
                {
                    if (item.Rectangle.Intersects(b.Rectangle))
                        bullets.Remove(item);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var x in bullets)
                x.Draw(spriteBatch);
            spriteBatch.Draw(bodyTexture, Rectangle, null, Color.White, rotation, Vector2.Zero, spriteEffect, 0f);

            if (Editor.IsEnable)
            {
                spriteBatch.Draw(GameLogic.ColorTexture(Color.Yellow * 0.5f, spriteBatch), new Rectangle((int)InitialPosition.X, (int)InitialPosition.Y, 32, 32), Color.White);
            }
        }


    }


}
