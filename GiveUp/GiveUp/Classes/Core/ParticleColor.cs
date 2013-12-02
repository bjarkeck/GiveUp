using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Core
{
    public class ParticleColor
    {
        public Color StartColor;
        public Color EndColor;

        bool singleColor;

        public ParticleColor(Color singleColor)
        {
            this.EndColor = singleColor;
            this.StartColor = singleColor;
            this.singleColor = true;
        }

        public ParticleColor(Color startColor, Color endColor)
        {
            this.StartColor = startColor;
            this.EndColor = endColor;
            this.singleColor = false;
        }

        public Color Color(int life, int currentLife)
        {
            //Calculate Color..


            return StartColor;
        }
    }
}
