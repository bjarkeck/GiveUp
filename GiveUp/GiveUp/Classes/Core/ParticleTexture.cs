using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Core
{
    public class ParticleTexture
    {
        public Texture2D Texture;
        private Color startColor;
        private Color endColor;

        public float startScale;
        public float endScale;


        bool singleColor;

        public ParticleTexture(Texture2D texture, Color singleColor, float startScale = 1, float endScale = 1)
        {
            this.Texture = texture;
            this.endColor = singleColor;
            this.startColor = singleColor;
            this.singleColor = true;
            this.startScale = startScale;
            this.endScale = endScale;
        }

        public ParticleTexture(Texture2D texture, Color startColor, Color endColor, float startScale = 1, float endScale = 1)
        {
            this.Texture = texture;
            this.startColor = startColor;
            this.endColor = endColor;
            this.singleColor = false;
            this.startScale = startScale;
            this.endScale = endScale;
        }

        public Color Color(int life, int currentLife)
        {
            if (currentLife < 0)
                return endColor;

            float scaleFactor = 1 - (float)currentLife / (float)life;
            return new Color(
                (byte)(startColor.R + (endColor.R - startColor.R) * scaleFactor),
                (byte)(startColor.G + (endColor.G - startColor.G) * scaleFactor),
                (byte)(startColor.B + (endColor.B - startColor.B) * scaleFactor),
                (byte)(startColor.A + (endColor.A - startColor.A) * scaleFactor)
            );

        }
        public float Scale(int life, int currentLife)
        {
            if (currentLife < 0)
                return endScale;

            float scaleFactor = 1 - (float)currentLife / (float)life;

            return startScale + (endScale - startScale) * scaleFactor;
        }

        public static ParticleTexture New(Texture2D texture, Color startColor, Color endColor, float startScale = 1, float endScale = 1)
        {
            return new ParticleTexture(texture, startColor, endColor, startScale, endScale);
        }
        public static ParticleTexture New(Texture2D texture, Color singleColor, float startScale = 1, float endScale = 1)
        {
            return new ParticleTexture(texture, singleColor, startScale, endScale);
        }
    }
}
