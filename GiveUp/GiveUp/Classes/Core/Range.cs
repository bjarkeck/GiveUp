using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Core
{
    public class Range<T> where T : IComparable<T>
    {
        public T Minimum { get; set; }
        public T Maximum { get; set; }

        public Range(T min, T max)
        {
            this.Minimum = min;
            this.Maximum = max;
        }
        public Range(T singleValue)
        {
            this.Minimum = singleValue;
            this.Maximum = singleValue;
        }

        public static Range<T> New(T min, T max)
        {
            return new Range<T>(min, max);
        }
        public static Range<T> New(T singleValue)
        {
            return new Range<T>(singleValue);
        }
    }
}
