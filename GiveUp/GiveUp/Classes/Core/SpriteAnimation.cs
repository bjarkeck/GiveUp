using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Core
{
    public class SpriteAnimation
    {
        public Texture2D SpriteSheet { get; set; }
        public double AnimationSpeed { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, spriteWidth / 2, spriteHeight / 2);
            }
            set
            {
                this.position.X = value.X;
                this.position.Y = value.Y;
            }
        }

        Vector2 position;
        int spriteWidth;
        int spriteHeight;
        double timer;
        Dictionary<string, Tuple<int, int>> SpriteIndex = new Dictionary<string, Tuple<int, int>>();
        string currentAnimation;
        int currentRowIndex = 0;
        int currentFrameIndex = 0;
        Rectangle currentFrame;
        public bool FlipImage = false;

        public SpriteAnimation(Texture2D spriteSheet, Vector2 position, int spriteWidth, int spriteHeight, float animationSpeed)
        {
            this.SpriteSheet = spriteSheet;
            this.spriteWidth = spriteWidth;
            this.spriteHeight = spriteHeight;
            this.position = position;
            this.AnimationSpeed = animationSpeed;
            currentFrame = new Rectangle(0, 0, spriteWidth, spriteHeight);
        }

        public void AddRow(string animationName, int rowIndex, int frameCount)
        {
            SpriteIndex.Add(animationName, new Tuple<int, int>(rowIndex, frameCount - 1));
        }

        public void PlayAnimation(string name)
        {
            if (currentAnimation != name)
            {
                currentAnimation = name;
                currentRowIndex = SpriteIndex[name].Item1;
                currentFrameIndex = 0;
                currentFrame.X = 0;
                currentFrame.Y = SpriteIndex[name].Item1 * spriteHeight;
            }
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            this.position = position;

            timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (string.IsNullOrEmpty(currentAnimation) == false)
            {
                if (timer > AnimationSpeed)
                {
                    timer -= AnimationSpeed;
                    currentFrameIndex++;
                    if (currentFrameIndex > SpriteIndex[currentAnimation].Item2)
                        currentFrameIndex = 0;
                    currentFrame.X = currentFrameIndex * spriteWidth;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteSheet, position, currentFrame, Color.White, 0f, Vector2.Zero, 0.5f, (this.FlipImage ? SpriteEffects.FlipHorizontally : SpriteEffects.None), 0);

        }

    }
}
